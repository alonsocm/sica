using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.SupervisionMuestreo.Queries
{
    public class DeleteArchivoSupervisionMuestreo : IRequest<Response<bool>>
    {
        public string NombreArchivo { get; set; }
        public long SupervisionId { get; set; }
    }

    public class DeleteArchivoSupervisionMuestreoHandler : IRequestHandler<DeleteArchivoSupervisionMuestreo, Response<bool>>
    {
        private readonly IEvidenciaSupervisionMuestreoRepository _evidenciaSupervisionMuestreoRepository;
        private readonly IArchivoService _archivoService;

        public DeleteArchivoSupervisionMuestreoHandler(IEvidenciaSupervisionMuestreoRepository evidenciaMuestreoRepository, IArchivoService archivoService)
        {
            _evidenciaSupervisionMuestreoRepository=evidenciaMuestreoRepository;
            _archivoService=archivoService;
        }

        public async Task<Response<bool>> Handle(DeleteArchivoSupervisionMuestreo request, CancellationToken cancellationToken)
        {
            var evidenciaDb = _evidenciaSupervisionMuestreoRepository.ObtenerElementosPorCriterioAsync(x => x.NombreArchivo == request.NombreArchivo && x.SupervisionMuestreoId == request.SupervisionId).Result.ToList();

            if (!evidenciaDb.Any())
                throw new KeyNotFoundException($"No se encontró ningún archivo de evidencia con el nombre {request.NombreArchivo}");

            if (evidenciaDb.Count > 1)
                throw new KeyNotFoundException($"Se encontró más de un archivo el nombre {request.NombreArchivo}");

            _evidenciaSupervisionMuestreoRepository.Eliminar(evidenciaDb.FirstOrDefault());
            var eliminado = _archivoService.EliminarArchivoSupervisionMuestreo(evidenciaDb.FirstOrDefault()?.NombreArchivo, request.SupervisionId.ToString());

            return new Response<bool>(eliminado);
        }
    }
}
