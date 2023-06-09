﻿using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.CargaMasivaEvidencias.Commands
{
    public class CargaEvidenciasCommand : IRequest<Response<bool>>
    {
        public List<IFormFile> Archivos { get; set; } = new List<IFormFile>();
    }

    public class CargaEvidenciasCommandHandler : IRequestHandler<CargaEvidenciasCommand, Response<bool>>
    {
        private readonly IArchivoService _archivos;
        private readonly IEvidenciaMuestreoRepository _evidenciaMuestreoRepository;
        private readonly IVwClaveMonitoreo _vwClaveMonitoreoRepository;
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly ITipoEvidenciaMuestreoRepository _tipoEvidenciaMuestreoRepository;

        public CargaEvidenciasCommandHandler(IArchivoService archivos, IEvidenciaMuestreoRepository evidenciaMuestreoRepository, IVwClaveMonitoreo vwClaveMonitoreoRepository, IMuestreoRepository muestreoRepository, ITipoEvidenciaMuestreoRepository tipoEvidenciaMuestreoRepository)
        {
            _archivos=archivos;
            _evidenciaMuestreoRepository=evidenciaMuestreoRepository;
            _vwClaveMonitoreoRepository=vwClaveMonitoreoRepository;
            _muestreoRepository=muestreoRepository;
            _tipoEvidenciaMuestreoRepository=tipoEvidenciaMuestreoRepository;
        }

        public async Task<Response<bool>> Handle(CargaEvidenciasCommand request, CancellationToken cancellationToken)
        {
            List<Models.EvidenciasMuestreo> evidenciasPorMuestreo = _archivos.OrdenarEvidenciasPorMuestreo(request.Archivos);
            var tiposEvidencia = await _tipoEvidenciaMuestreoRepository.ObtenerTodosElementosAsync();

            foreach (var evidenciasMuestreo in evidenciasPorMuestreo)
            {
                var clavesMonitoreos = await _vwClaveMonitoreoRepository.ObtenerElementosPorCriterioAsync(e => e.ClaveMuestreo == evidenciasMuestreo.Muestreo)??throw new ApiException("No se encontraron claves de muestreo correspondientes a las evidencias procesadas");
                var programaMuestreoId = clavesMonitoreos.FirstOrDefault()?.ProgramaMuestreoId;
                var muestreo = _muestreoRepository.ObtenerElementosPorCriterioAsync(e => e.ProgramaMuestreoId == programaMuestreoId).Result.FirstOrDefault()??throw new ApiException($"No se encontró en la base de datos, el registro del muestreo: {evidenciasMuestreo.Muestreo}");
                List<EvidenciaMuestreo> evidencias = new();
                List<string> muestreosProcesados = new();

                try
                {
                    _archivos.GuardarEvidencias(evidenciasMuestreo);
                    muestreosProcesados.Add(evidenciasMuestreo.Muestreo);

                    foreach (var archivo in evidenciasMuestreo.Archivos)
                    {
                        var sufijoEvidencia = archivo.FileName.Substring(archivo.FileName.LastIndexOf('-') + 1, 1);
                        var tipoEvidenciaId = tiposEvidencia.First(f => f.Sufijo == sufijoEvidencia).Id;

                        evidencias.Add(new()
                        {
                            MuestreoId = muestreo.Id,
                            TipoEvidenciaMuestreoId = tipoEvidenciaId,
                            NombreArchivo = archivo.FileName.ToUpper(),
                        });
                    }

                    muestreo.EstatusId = (int)Enums.EstatusMuestreo.EvidenciasCargadas;
                    muestreo.FechaCargaEvidencias = DateTime.Now;
                }
                catch (Exception ex)
                {
                    muestreosProcesados.ForEach(muestreo => _archivos.EliminarEvidencias(muestreo));
                    throw new ApiException($"Ocurrió un error al guardar las evidencias del muestreo: {evidenciasMuestreo.Muestreo}. Error: {ex.Message}");
                }

                _evidenciaMuestreoRepository.InsertarRango(evidencias);
                _muestreoRepository.Actualizar(muestreo);
            }

            return new Response<bool>(true);
        }
    }
}
