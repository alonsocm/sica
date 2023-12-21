using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Queries
{
    public class GetNumeroEntrega : IRequest<Response<List<int?>>>
    {
    }

    public class GetNumeroEntregaHandler : IRequestHandler<GetNumeroEntrega, Response<List<int?>>>
    {
        private readonly IMuestreoRepository _repository;

        public GetNumeroEntregaHandler(IMuestreoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<int?>>> Handle(GetNumeroEntrega request, CancellationToken cancellationToken)
        {
            var anios = await _repository.GetListNumeroEntrega();
            return new Response<List<int?>>(anios);
        }
    }
}





