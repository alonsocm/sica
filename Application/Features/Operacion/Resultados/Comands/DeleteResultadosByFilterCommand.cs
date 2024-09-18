using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Resultados.Comands
{
    public class DeleteResultadosByFilterCommand : IRequest<Response<bool>>
    {
        public IEnumerable<long> ResultadosIds { get; set; }
    }

    public class DeleteResultadosByFilterCommandHandler : IRequestHandler<DeleteResultadosByFilterCommand, Response<bool>>
    {
        private readonly IResultado _resultadoRepository;
        public DeleteResultadosByFilterCommandHandler(IResultado resultadoRepository)
        {
            _resultadoRepository = resultadoRepository;
        }
        public async Task<Response<bool>> Handle(DeleteResultadosByFilterCommand request, CancellationToken cancellationToken)
        {
            if (request.ResultadosIds.Any())
            {
                await _resultadoRepository.EliminarAsync(x => request.ResultadosIds.Contains(x.Id));
                return new Response<bool> { Succeded = true };
            }

            return new Response<bool> { Succeded = false, Message="No se proporcionaron resultados para eliminar" };
        }
    }
}
