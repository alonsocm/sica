using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.TiposCuerpoAgua.Commands
{
    public class DeleteTipoCuerpoAguaCommand : IRequest<Response<long>>
    {
        public long Id { get; set; }
    }
    public class DeleteTipoCuerpoAguaCommandHandler : IRequestHandler<DeleteTipoCuerpoAguaCommand, Response<long>>
    {
        private readonly ITipoCuerpoAguaRepository _repository;
        public DeleteTipoCuerpoAguaCommandHandler(ITipoCuerpoAguaRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<long>> Handle(DeleteTipoCuerpoAguaCommand request, CancellationToken cancellationToken)
        {
            var tipoCuerpoAgua = await _repository.ObtenerElementoPorIdAsync(request.Id);
            if (tipoCuerpoAgua == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            _repository.Eliminar(tipoCuerpoAgua);
            return new Response<long>(request.Id, "TipoCuerpoAgua eliminado exitosamente.");
        }
    }
}
