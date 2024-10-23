using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.RevisionOCDL.Commands
{
    public class ActualizarResultadoOCDL : IRequest<Response<bool>>
    {
        public IEnumerable<long> Resultados { get; set; } = new List<long>();
    }
    public class ActualizarResultadoOSCDLHandler : IRequestHandler<ActualizarResultadoOCDL, Response<bool>>
    {
        private readonly IResultado _repository;
        public ActualizarResultadoOSCDLHandler(IResultado repository)
        {
            _repository = repository;
        }
        public async Task<Response<bool>> Handle(ActualizarResultadoOCDL request, CancellationToken cancellationToken)
        {
            if (request.Resultados.Any())
            {
                var resultadosEnviados = await _repository.EnviarResultados(request.Resultados);

                if (resultadosEnviados == 0)
                {
                    throw new ArgumentException("Los resultados no cumplen con el valor \"OK\", en el campo validación final.");
                }

                return new Response<bool>(true);
            }
            else
            {
                throw new ArgumentException("No se encontraron resultados para liberar.");
            }
        }
    }
}
