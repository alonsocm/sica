using Application.DTOs;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;


namespace Application.Features.Catalogos.TiposCuerpoAgua.Queries
{
    // se crea la clase llamada GetTipoCuerpoAguaPQuery  indicando que es publica ,
    // se hace la peticion IRequest  se ocupa la clase PagedResponse para traer datos paginados 
    // IEnumerable se obtines los datos 
    public class GetTipoCuerpoAguaPQuery : IRequest<PagedResponse<IEnumerable<TipoCuerpoAguaDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filter { get; set; }
    }

    // manejador GetTipoCuerpoAguaPQuertyHandler 
    // hace la solicitud IRequestHandler
    public class GetTipoCuerpoAguaPQuertyHandler : IRequestHandler<GetTipoCuerpoAguaPQuery, PagedResponse<IEnumerable<TipoCuerpoAguaDto>>>
    {
        private readonly ITipoCuerpoAguaRepository _repository;
        // se declara el nombre  
        public GetTipoCuerpoAguaPQuertyHandler(ITipoCuerpoAguaRepository repository)
        {
            _repository = repository;
        }
        public async Task<PagedResponse<IEnumerable<TipoCuerpoAguaDto>>> Handle(GetTipoCuerpoAguaPQuery request, CancellationToken cancellationToken)
        {
            var data = _repository.GetTipoCuerpoAgua(); // trae todo los datos 

            if (request.Filter.Any()) // se relizan los filtros 
            {
                var e = QueryExpression<TipoCuerpoAguaDto>.GetExpressionList(request.Filter);

                foreach (var filter in e)
                {
                    data = data.AsQueryable().Where(filter);
                }
            }

            return PagedResponse<TipoCuerpoAguaDto>.CreatePagedReponse(data.OrderBy(o => o.Descripcion), request.Page, request.PageSize);
        }
    }
}
