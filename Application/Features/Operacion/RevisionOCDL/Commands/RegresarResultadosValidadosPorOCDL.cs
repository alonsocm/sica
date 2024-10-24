using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.RevisionOCDL.Commands
{
    public class RegresarResultadosValidadosPorOCDL : IRequest<Response<bool>>
    {
        public IEnumerable<long> Resultados { get; set; } = new List<long>();
    }
    public class RegresarResultadosValidadosPorOCDLHandler : IRequestHandler<RegresarResultadosValidadosPorOCDL, Response<bool>>
    {
        private readonly IResultado _repository;
        public RegresarResultadosValidadosPorOCDLHandler(IResultado repository)
        {
            _repository = repository;
        }
        public async Task<Response<bool>> Handle(RegresarResultadosValidadosPorOCDL request, CancellationToken cancellationToken)
        {
            if (request.Resultados.Any())
            {
                var resultadosEnviados = await _repository.RegresarResultadosValidadosPorOCDL(request.Resultados);
                return new Response<bool>(true);
            }
            else
            {
                throw new ArgumentException("No se encontraron resultados a regresar.");
            }
        }
    }
}
