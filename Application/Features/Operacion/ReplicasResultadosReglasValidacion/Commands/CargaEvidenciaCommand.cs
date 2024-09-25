using Application.DTOs;
using Application.Features.Operacion.SupervisionMuestreo.Commands;
using Application.Interfaces.IRepositories;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Application.DTOs.EvidenciasMuestreo;
using Application.Enums;
using Application.Exceptions;
using Application.Features.CargaMasivaEvidencias.Commands;
using Application.Models;

namespace Application.Features.Operacion.ReplicasResultadosReglasValidacion.Commands
{
    public class CargaEvidenciaCommand: IRequest<Response<bool>>
    {
        public List<IFormFile> Archivos { get; set; } = new List<IFormFile>();

    }

    public class CargaEvidenciaHandler: IRequestHandler<CargaEvidenciaCommand, Response<bool>>
    {

        private readonly IReplicasResultadosReglasValidacionRepository _replicasRepository;      
        private readonly IArchivoService _archivos;
        private readonly IParametroRepository _parametroRepository;
        private readonly IResultado _resultado;
        private readonly IVwClaveMonitoreo _vwClaveMonitoreoRepository;
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly IEvidenciasReplicasResultadoReglasValidacionRepository _repositoryEvidencias;

        public CargaEvidenciaHandler(
            IArchivoService archivos, IReplicasResultadosReglasValidacionRepository replicasRepository,
            IParametroRepository parametroRepository, IResultado resultado, IVwClaveMonitoreo vwClaveMonitoreoRepository, IMuestreoRepository muestreoRepository,
           IEvidenciasReplicasResultadoReglasValidacionRepository repositoryEvidencias)
        {
          
            _archivos = archivos;
            _replicasRepository = replicasRepository;
            _parametroRepository = parametroRepository;
            _resultado = resultado;
            _vwClaveMonitoreoRepository = vwClaveMonitoreoRepository;
            _muestreoRepository = muestreoRepository;
            _repositoryEvidencias = repositoryEvidencias;
        }

        public async Task<Response<bool>> Handle(CargaEvidenciaCommand request, CancellationToken cancellationToken)
        {
            List<Models.EvidenciasReplicasResultado> evidenciasPorClaveUnica = _archivos.OrdenarEvidenciasClaveUnica(request.Archivos);
            foreach (var evidenciasReplica in evidenciasPorClaveUnica)
            {
                
                var muestreo = evidenciasReplica.ClaveUnica.Substring(0, evidenciasReplica.ClaveUnica.IndexOf('-') + 7);
                var clavesMonitoreos = await _vwClaveMonitoreoRepository.ObtenerElementosPorCriterioAsync(e => e.ClaveMuestreo == muestreo) ?? throw new ApiException("No se encontraron claves de muestreo correspondientes a las evidencias procesadas");
                
                var datParametro = evidenciasReplica.ClaveUnica.Replace(muestreo, "");
                if (datParametro.Contains("mgL")) datParametro = datParametro.Replace("mgL", "mg/L");


                var muestreoBasse = _muestreoRepository.ObtenerElementoConInclusiones(e => e.ProgramaMuestreoId == clavesMonitoreos.FirstOrDefault().ProgramaMuestreoId, x => x.EvidenciaMuestreo).FirstOrDefault() ?? throw new ApiException($"No se encontró en la base de datos, el registro del muestreo: {evidenciasReplica.ClaveUnica}");


                var parametro = await _parametroRepository.ObtenerElementosPorCriterioAsync(e => e.ClaveParametro == datParametro) ?? throw new ApiException("No se encontraron claves únicas correspondientes a las evidencias procesadas");
                var resultadoMuestreo = await _resultado.ObtenerElementosPorCriterioAsync(e => e.Muestreo.Id == muestreoBasse.Id && e.ParametroId.Equals(parametro.FirstOrDefault().Id)) ?? throw new ApiException("No se encontraron claves únicas correspondientes a las evidencias procesadas");
                var replicaResultado = await _replicasRepository.ObtenerElementosPorCriterioAsync(e => e.ResultadoMuestreoId == resultadoMuestreo.FirstOrDefault().Id);

                
                List<string> muestreosProcesados = new();

                try
                {
                    _archivos.GuardarEvidencias(evidenciasReplica);
                    muestreosProcesados.Add(evidenciasReplica.ClaveUnica);

                    foreach (var archivo in evidenciasReplica.Archivos)
                    {
                        var nuevoRegistro = new EvidenciasReplicasResultadoReglasValidacion()
                        {
                            ReplicasResultadoReglasValidacionId = replicaResultado.FirstOrDefault().Id,
                            NombreArchivo = archivo.FileName
                    
                        };

                        _repositoryEvidencias.Insertar(nuevoRegistro);
                    }
                }
                catch (Exception ex)
                {
                    muestreosProcesados.ForEach(claveUnica => _archivos.EliminarEvidencias(claveUnica));
                    throw new ApiException($"Ocurrió un error al guardar las evidencias del muestreo: {evidenciasReplica.ClaveUnica}. Error: {ex.Message}");
                }
            }

            return new Response<bool>(true);
        }
    }
}
