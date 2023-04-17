using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Evidencias.Queries
{
    public class GetEvidenciaByNombre : IRequest<Response<ArchivoDto>>
    {
        public string NombreArchivo { get; set; }
    }

    public class GetEvidenciaByNombreHandler : IRequestHandler<GetEvidenciaByNombre, Response<ArchivoDto>>
    {
        private readonly IEvidenciaMuestreoRepository _evidenciaMuestreoRepository;
        private readonly IArchivoService _archivoService;

        public GetEvidenciaByNombreHandler(IEvidenciaMuestreoRepository evidenciaMuestreoRepository, IArchivoService archivoService)
        {
            _evidenciaMuestreoRepository=evidenciaMuestreoRepository;
            _archivoService=archivoService;
        }

        public async Task<Response<ArchivoDto>> Handle(GetEvidenciaByNombre request, CancellationToken cancellationToken)
        {
            var evidenciaDb = await _evidenciaMuestreoRepository.ObtenerElementosPorCriterioAsync(x => x.NombreArchivo == request.NombreArchivo);

            if (!evidenciaDb.Any())
            {
                throw new KeyNotFoundException($"No se encontró ningún archivo de evidencia con el nombre {request.NombreArchivo}");
            }
            
            var archivoEvidencia = _archivoService.ObtenerEvidencia(evidenciaDb.FirstOrDefault()?.NombreArchivo);
            return new Response<ArchivoDto>(archivoEvidencia);
        }
    }
}
