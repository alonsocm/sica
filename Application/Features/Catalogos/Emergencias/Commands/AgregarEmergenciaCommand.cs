using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Catalogos.Emergencias.Commands
{
    public class AgregarEmergenciaCommand : IRequest<Response<bool>>
    {
        public Emergencia Emergencia { get; set; }
    }

    public class AgregarEmergenciaCommandHandler : IRequestHandler<AgregarEmergenciaCommand, Response<bool>>
    {
        private readonly IEmergenciaRepository _emergenciaRepository;
        public AgregarEmergenciaCommandHandler(IEmergenciaRepository emergenciaRepository)
        {
            _emergenciaRepository = emergenciaRepository;
        }

        public async Task<Response<bool>> Handle(AgregarEmergenciaCommand request, CancellationToken cancellationToken)
        {
            _emergenciaRepository.Insertar(request.Emergencia);

            return new Response<bool>(true);
        }
    }
}
