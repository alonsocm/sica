using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Resultados.Comands
{
    public class DeleteResultadosByIdCommand : IRequest<Response<bool>>
    {
        public List<long> lstResultadosId { get; set; }
    }

    public class DeleteResultadosByIdCommandHandler : IRequestHandler<DeleteResultadosByIdCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _repositoryAsync;
        private readonly IResultado _resultadoRepository;
        public DeleteResultadosByIdCommandHandler(IMuestreoRepository repositoryAsync, IResultado resultadoRepository)
        {
            _repositoryAsync = repositoryAsync;
            _resultadoRepository = resultadoRepository;
        }
        public async Task<Response<bool>> Handle(DeleteResultadosByIdCommand request, CancellationToken cancellationToken)
        {
            foreach (var idResultado in request.lstResultadosId)
            {
                await _resultadoRepository.EliminarAsync(x => x.Id == idResultado);
            }

            return new Response<bool> { Succeded = true };
        }
    }
}
