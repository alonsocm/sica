using Application.DTOs;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
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
        public OrderBy? OrderBy { get; set; }
    }

    // manejador GetTipoCuerpoAguaPQuertyHandler 
    // hace la solicitud IRequestHandler
    public class GetTipoCuerpoAguaPQuertyHandler : IRequestHandler<GetTipoCuerpoAguaPQuery, PagedResponse<IEnumerable<TipoCuerpoAguaDto>>>
    {
        private readonly ITipoCuerpoAguaRepository _repository;
        private readonly IMapper _mapper;
        // se declara el nombre  
        public GetTipoCuerpoAguaPQuertyHandler(ITipoCuerpoAguaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<TipoCuerpoAguaDto>>> Handle(GetTipoCuerpoAguaPQuery request, CancellationToken cancellationToken)
        {
            var data = _repository.GetTipoCuerpoAgua(); // trae todo los datos 
            var tipoCuerpoAguaDTO = _mapper.Map<IEnumerable<TipoCuerpoAguaDto>>(data);

            if (request.Filter.Any()) // se relizan los filtros 
            {
                var e = QueryExpression<TipoCuerpoAguaDto>.GetExpressionList(request.Filter);

                foreach (var filter in e)
                {
                    data = data.AsQueryable().Where(filter);
                }
            }
            if (request.OrderBy != null)
            {
                if (request.OrderBy.Type == "asc")
                {
                    data = data.AsQueryable().OrderBy(QueryExpression<TipoCuerpoAguaDto>.GetOrderByExpression(request.OrderBy.Column));
                }
                else if (request.OrderBy.Type == "desc")
                {
                    data = data.AsQueryable().OrderByDescending(QueryExpression<TipoCuerpoAguaDto>.GetOrderByExpression(request.OrderBy.Column));
                }
            }

            return PagedResponse<TipoCuerpoAguaDto>.CreatePagedReponse(data, request.Page, request.PageSize);
        }
    }
}
