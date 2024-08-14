﻿using Application.DTOs;
using Application.DTOs.Catalogos;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.ParametrosGrupo.Queries
{
    public class ParametrosQuery : IRequest<PagedResponse<IEnumerable<ParametroDTO>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filter { get; set; }
        public OrderBy? OrderBy { get; set; }
    }

    public class ParametrosQueryHandler : IRequestHandler<ParametrosQuery, PagedResponse<IEnumerable<ParametroDTO>>>
    {
        private readonly IParametroRepository _repository;

        public ParametrosQueryHandler(IParametroRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResponse<IEnumerable<ParametroDTO>>> Handle(ParametrosQuery request, CancellationToken cancellationToken)
        {
            var data = _repository.GetParametros();

            if (request.Filter.Any())
            {
                var expressions = QueryExpression<ParametroDTO>.GetExpressionList(request.Filter);

                foreach (var filter in expressions)
                {
                    data = data.AsQueryable().Where(filter);
                }
            }

            if (request.OrderBy != null)
            {
                if (request.OrderBy.Type == "asc")
                {
                    data = data.AsQueryable().OrderBy(QueryExpression<ParametroDTO>.GetOrderByExpression(request.OrderBy.Column));
                }
                else if (request.OrderBy.Type == "desc")
                {
                    data = data.AsQueryable().OrderByDescending(QueryExpression<ParametroDTO>.GetOrderByExpression(request.OrderBy.Column));
                }
            }

            return PagedResponse<ParametroDTO>.CreatePagedReponse(data, request.Page, request.PageSize);
        }
    }
}
