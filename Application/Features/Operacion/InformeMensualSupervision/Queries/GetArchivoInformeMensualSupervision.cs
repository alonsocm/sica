using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Operacion.InformeMensualSupervision.Queries
{
    public class GetArchivoInformeMensualSupervision : IRequest<Response<ArchivoInformeMensualSupervision>>
    {
        public long InformeId { get; set; }
        public int Tipo { get; set; }
    }

    public class GetArchivoInformeMensualSupervisionHandler : IRequestHandler<GetArchivoInformeMensualSupervision, Response<ArchivoInformeMensualSupervision>>
    {
        private readonly IInformeMensualSupervisionRepository _repository;
        public GetArchivoInformeMensualSupervisionHandler(IInformeMensualSupervisionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<ArchivoInformeMensualSupervision>> Handle(GetArchivoInformeMensualSupervision request, CancellationToken cancellationToken)
        {
            return new Response<ArchivoInformeMensualSupervision>(await _repository.GetArchivoInformeMensual(request.InformeId, request.Tipo));
        }
    }
}