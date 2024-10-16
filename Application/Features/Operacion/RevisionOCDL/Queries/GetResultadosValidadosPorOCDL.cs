﻿using Application.DTOs;
using Application.DTOs.RevisionOCDL;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.RevisionOCDL.Queries
{
    public class GetResultadosValidadosPorOCDL : IRequest<PagedResponse<IEnumerable<ResultadosValidadosPorOCDLDTO>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter>? Filter { get; set; }
        public OrderBy? OrderBy { get; set; }
    }
    public class GetResultadosValidadosPorOCDLHandler : IRequestHandler<GetResultadosValidadosPorOCDL, PagedResponse<IEnumerable<ResultadosValidadosPorOCDLDTO>>>
    {
        private readonly IResultado _repositoryAsync;

        public GetResultadosValidadosPorOCDLHandler(IResultado repository)
        {
            _repositoryAsync = repository;
        }
        public async Task<PagedResponse<IEnumerable<ResultadosValidadosPorOCDLDTO>>> Handle(GetResultadosValidadosPorOCDL request, CancellationToken cancellationToken)
        {
            var datos = await _repositoryAsync.GetResultadosValidadosPorOCDLAsync();
            
            if (request.Filter != null && request.Filter.Any())
            {
                var filteredDatos = datos.AsQueryable();
                var expressions = QueryExpression<ResultadosValidadosPorOCDLDTO>.GetExpressionList(request.Filter);

                foreach (var filter in expressions)
                {
                    filteredDatos = filteredDatos.Where(filter);
                }

                datos = filteredDatos.ToList();
            }

            if (request.OrderBy != null)
            {

                if (request.OrderBy.Type == "asc")
                {
                    datos = datos.AsQueryable().OrderBy(QueryExpression<ResultadosValidadosPorOCDLDTO>.GetOrderByExpression(request.OrderBy.Column)).ToList();
                }
                else if (request.OrderBy.Type == "desc")
                {
                    datos = datos.AsQueryable().OrderByDescending(QueryExpression<ResultadosValidadosPorOCDLDTO>.GetOrderByExpression(request.OrderBy.Column)).ToList();
                }

            }
            return PagedResponse<ResultadosValidadosPorOCDLDTO>.CreatePagedReponse(datos, request.Page, request.PageSize);
        }
    }
}