using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Liberacion
{
    public class LiberarCommand : IRequest<Response<bool>>
    {
        public IEnumerable<long> Resultados { get; set; } = new List<long>();
    }

    public class LiberarCommandHandler : IRequestHandler<LiberarCommand, Response<bool>>
    {
        private readonly IResultado _resultadosRepository;

        public LiberarCommandHandler(IResultado resultadoRepository)
        {
            _resultadosRepository = resultadoRepository;
        }

        public async Task<Response<bool>> Handle(LiberarCommand request, CancellationToken cancellationToken)
        {
            if (request.Resultados.Any())
            {
                var resultadosLiberados = await _resultadosRepository.LiberarResultados(request.Resultados);

                if (resultadosLiberados == 0)
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
