using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Resultados.Comands
{
    public class DeleteResultadosByMuestreoIdCommand : IRequest<Response<bool>>
    {
        public IEnumerable<int> Muestreos { get; set; }
    }

    public class DeleteResultadosByMuestreoIdCommandHandler : IRequestHandler<DeleteResultadosByMuestreoIdCommand, Response<bool>>
    {
        private readonly IResultado _resultadoRepository;
        public DeleteResultadosByMuestreoIdCommandHandler(IResultado resultadoRepository)
        {
            _resultadoRepository = resultadoRepository;
        }

        public async Task<Response<bool>> Handle(DeleteResultadosByMuestreoIdCommand request, CancellationToken cancellationToken)
        {
            if (request.Muestreos.Any())
            {
                await _resultadoRepository.EliminarAsync(x => request.Muestreos.ToList().Contains((int)x.MuestreoId));
                return new Response<bool> { Succeded = true };
            }

            throw new ArgumentException("No se han proporcionado muestreos para eliminar sus resultados");
        }
    }
}
