using Application.DTOs.EvidenciasMuestreo;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Evidencias.Queries
{
    public class GetInformacionEvidencias : IRequest<Response<IEnumerable<InformacionEvidenciaDto>>>
    {
    }

    public class GetInformacionEvidenciasHandler : IRequestHandler<GetInformacionEvidencias, Response<IEnumerable<InformacionEvidenciaDto>>>
    {
        private readonly IEvidenciaMuestreoRepository _evidenciaMuestreoRepository;

        public GetInformacionEvidenciasHandler(IEvidenciaMuestreoRepository evidenciaMuestreoRepository)
        {
            _evidenciaMuestreoRepository=evidenciaMuestreoRepository;
        }

        public async Task<Response<IEnumerable<InformacionEvidenciaDto>>> Handle(GetInformacionEvidencias request, CancellationToken cancellationToken)
        {
            var evidencias = await _evidenciaMuestreoRepository.GetInformacionEvidenciasAsync();

            return new Response<IEnumerable<InformacionEvidenciaDto>>(evidencias);
        }
    }
}
