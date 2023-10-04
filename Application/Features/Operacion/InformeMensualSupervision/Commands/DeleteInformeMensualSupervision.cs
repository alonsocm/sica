using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.InformeMensualSupervision.Commands
{
    public class DeleteInformeMensualSupervision : IRequest<Response<bool>>
    {
        public long InformeId { get; set; }
    }

    public class DeleteInformeMensualSupervisionHandler : IRequestHandler<DeleteInformeMensualSupervision, Response<bool>>
    {
        private readonly IInformeMensualSupervisionRepository _informeMensualSupervisionRepository;
        public DeleteInformeMensualSupervisionHandler(IInformeMensualSupervisionRepository informeMensualSupervisionRepository)
        {
            _informeMensualSupervisionRepository = informeMensualSupervisionRepository;
        }

        public async Task<Response<bool>> Handle(DeleteInformeMensualSupervision request, CancellationToken cancellationToken)
        {
            bool eliminado = await _informeMensualSupervisionRepository.DeleteInformeMensualAsync(request.InformeId);

            return new Response<bool>(eliminado);
        }
    }
}
