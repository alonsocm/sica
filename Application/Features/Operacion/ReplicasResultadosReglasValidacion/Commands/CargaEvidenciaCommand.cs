using Application.Interfaces.IRepositories;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Application.Exceptions;
using System.Data;

namespace Application.Features.Operacion.ReplicasResultadosReglasValidacion.Commands
{
    public class CargaEvidenciaCommand : IRequest<Response<bool>>
    {
        public List<IFormFile> Archivos { get; set; } = new List<IFormFile>();   

    }

    public class CargaEvidenciaHandler : IRequestHandler<CargaEvidenciaCommand, Response<bool>>
    {

        private readonly IArchivoService _archivos;
        private readonly IEvidenciasReplicasResultadoReglasValidacionRepository _repositoryEvidencias;

        private readonly IRepository<RelacionEvidenciasReplicaResultadosReglas> _relEvidenciasRepository;

        public CargaEvidenciaHandler(IArchivoService archivos, IRepository<RelacionEvidenciasReplicaResultadosReglas> relEvidenciasRepository,
            IEvidenciasReplicasResultadoReglasValidacionRepository repositoryEvidencias)
        {
            _archivos = archivos;
            _repositoryEvidencias = repositoryEvidencias;
            _relEvidenciasRepository = relEvidenciasRepository;
        }

        public async Task<Response<bool>> Handle(CargaEvidenciaCommand request, CancellationToken cancellationToken)
        {
            var evidencias = _repositoryEvidencias.ObtenerTodosElementosAsync().Result.ToList();
            string mensaje = string.Empty;
            List<string> archivoNoRegistrado = new List<string>();
            request.Archivos.ToList().ForEach(y =>
            {
                if (!evidencias.Select(x => x.NombreArchivo).ToList().Contains(y.FileName))
                {
                    mensaje += "Error:" + y.FileName + "no se encuentra cargado previamente" + "\n";
                }
            });
            if (mensaje != string.Empty) throw new ApiException(mensaje);

            List<string> muestreosProcesados = new();
            try
            {
                _archivos.GuardarEvidencias(request.Archivos);
                muestreosProcesados.Add(DateTime.Now.ToShortDateString());
                evidencias.ToList().ForEach(evidencia => { evidencia.Cargado = true; evidencia.FechaCarga = DateTime.Now; });
                await _repositoryEvidencias.ActualizarAsync(evidencias.ToList());
            }
            catch (Exception ex)
            {
                muestreosProcesados.ForEach(fechaCreacion => _archivos.EliminarEvidencias(fechaCreacion));
                throw new ApiException($"Ocurrió un error al guardar las evidencias con el nombre: . Error: {ex.Message}");
            }

            return new Response<bool>(true);
        }
    }
}