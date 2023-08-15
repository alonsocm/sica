using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.Emergencias.Commands
{
    public class ExisteEmergenciaQuery : IRequest<Response<bool>>
    {
        public string NombreEmergencia { get; set; }
    }

    public class ExisteEmergenciaQueryHandler : IRequestHandler<ExisteEmergenciaQuery, Response<bool>>
    {
        private readonly IEmergenciaRepository _emergenciaRepository;
        public ExisteEmergenciaQueryHandler(IEmergenciaRepository emergenciaRepository)
        {
            _emergenciaRepository = emergenciaRepository;
        }

        public async Task<Response<bool>> Handle(ExisteEmergenciaQuery request, CancellationToken cancellationToken)
        {
            var existeEmergencia = await _emergenciaRepository.ExisteElementoAsync(x => x.NombreEmergencia == request.NombreEmergencia);

            return new Response<bool>(existeEmergencia);
        }
    }
}
