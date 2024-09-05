using Application.DTOs;
using Application.DTOs.Catalogos;
using Application.Expressions;
using Application.Features.Operacion.Resultados.Queries;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.ReplicasResultadosReglasValidacion.Queries
{
    public class GetReplicasResultadosReglaValByEstatus: IRequest<PagedResponse<IEnumerable<ReplicasResultadosReglasValidacionDto>>>
    {

        public List<int> EstatusId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filter { get; set; }
        public OrderBy? OrderBy { get; set; }
    }

    public class GetReplicasResultadosReglaValByEstatusHandler : IRequestHandler<GetReplicasResultadosReglaValByEstatus, PagedResponse<IEnumerable<ReplicasResultadosReglasValidacionDto>>>
    {
        private readonly IMuestreoRepository _repositoryAsync;

        public GetReplicasResultadosReglaValByEstatusHandler(IMuestreoRepository repository)
        {
            _repositoryAsync = repository;
        }

        public async Task<PagedResponse<IEnumerable<ReplicasResultadosReglasValidacionDto>>> Handle(GetReplicasResultadosReglaValByEstatus request, CancellationToken cancellationToken)
        {
            var data = await _repositoryAsync.GetReplicasResultadosReglaValidacion(request.EstatusId);

            if (request.Filter.Any())
            {
                var expressions = QueryExpression<ReplicasResultadosReglasValidacionDto>.GetExpressionList(request.Filter);
                List<ReplicasResultadosReglasValidacionDto> lstMuestreo = new();

                foreach (var filter in expressions)
                {
                    if (request.Filter.Count == 2 && request.Filter[0].Conditional == "equals" && request.Filter[1].Conditional == "equals")
                    {
                        var dataFinal = data;
                        dataFinal = dataFinal.AsQueryable().Where(filter);
                        lstMuestreo.AddRange(dataFinal);
                        data = lstMuestreo;
                    }
                    else
                    {
                        data = data.AsQueryable().Where(filter);
                    }
                }
            }
            if (request.OrderBy != null)
            {
                if (request.OrderBy.Type == "asc")
                {
                    data = data.AsQueryable().OrderBy(QueryExpression<ReplicasResultadosReglasValidacionDto>.GetOrderByExpression(request.OrderBy.Column));
                }
                else if (request.OrderBy.Type == "desc")
                {
                    data = data.AsQueryable().OrderByDescending(QueryExpression<ReplicasResultadosReglasValidacionDto>.GetOrderByExpression(request.OrderBy.Column));
                }
            }

            if (data == null)
            {
                throw new KeyNotFoundException($"No se encontraron datos asociados a resultados revisados");
            }

            return PagedResponse<ReplicasResultadosReglasValidacionDto>.CreatePagedReponse(data, request.Page, request.PageSize);
        }
    }
}
