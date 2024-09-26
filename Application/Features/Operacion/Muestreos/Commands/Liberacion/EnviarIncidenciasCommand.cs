using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Liberacion
{
    public class EnviarIncidenciasCommand : IRequest<Response<bool>>
    {
        public IEnumerable<long> ResultadosId { get; set; } = new List<long>();
    }

    public class EnviarIncidenciasCommandHandler : IRequestHandler<EnviarIncidenciasCommand, Response<bool>>
    {
        private readonly IResultado _resultadosRepository;

        public EnviarIncidenciasCommandHandler(IResultado resultadosRepository)
        {
            _resultadosRepository = resultadosRepository;
        }

        public async Task<Response<bool>> Handle(EnviarIncidenciasCommand request, CancellationToken cancellationToken)
        {
            if (request.ResultadosId.Any())
            {
                var resultadosEnviados = await _resultadosRepository.EnviarResultadoAIncidencias(request.ResultadosId);

                if (resultadosEnviados == 0)
                {
                    throw new ArgumentException("No se han podido enviar los resultados a incidencias.");
                }

                return new Response<bool>(true);
            }
            else
            {
                throw new ArgumentException("No se han proporcionado resultados para enviar a incidencias.");
            }
        }
    }
}
