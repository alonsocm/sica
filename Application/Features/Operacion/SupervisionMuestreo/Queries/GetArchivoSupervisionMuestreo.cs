using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.SupervisionMuestreo.Queries
{
    public class GetArchivoSupervisionMuestreo : IRequest<Response<ArchivoDto>>
    {
        public string NombreArchivo { get; set; }
        public long SupervisionId { get; set; }
    }

    public class GetEvidenciaByNombreHandler : IRequestHandler<GetArchivoSupervisionMuestreo, Response<ArchivoDto>>
    {
        private readonly IEvidenciaSupervisionMuestreoRepository _evidenciaSupervisionMuestreoRepository;
        private readonly IArchivoService _archivoService;

        public GetEvidenciaByNombreHandler(IEvidenciaSupervisionMuestreoRepository evidenciaMuestreoRepository, IArchivoService archivoService)
        {
            _evidenciaSupervisionMuestreoRepository=evidenciaMuestreoRepository;
            _archivoService=archivoService;
        }

        public async Task<Response<ArchivoDto>> Handle(GetArchivoSupervisionMuestreo request, CancellationToken cancellationToken)
        {
            var evidenciaDb = await _evidenciaSupervisionMuestreoRepository.ObtenerElementosPorCriterioAsync(x => x.NombreArchivo == request.NombreArchivo && x.SupervisionMuestreoId == request.SupervisionId);

            if (!evidenciaDb.Any())
            {
                throw new KeyNotFoundException($"No se encontró ningún archivo de evidencia con el nombre {request.NombreArchivo}");
            }

            var archivoEvidencia = _archivoService.ObtenerArchivoSupervisionMuestreo(evidenciaDb.FirstOrDefault()?.NombreArchivo, request.SupervisionId.ToString());
            return new Response<ArchivoDto>(archivoEvidencia);
        }
    }
}
