using Application.DTOs;
using Application.Expressions;
using Application.Features.ResumenResultados.Queries;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Resultados.Queries
{
    public class GetResultadosMuestreoEstatusMuestreoPaginadosQuery : IRequest<PagedResponse<List<AcumuladosResultadoDto>>>
    {
        public int estatusId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filter { get; set; }
        public OrderBy? OrderBy { get; set; }

    }

    public class GetResultadosMuestreoEstatusMuestreoQueryHandler : IRequestHandler<GetResultadosMuestreoEstatusMuestreoPaginadosQuery, PagedResponse<List<AcumuladosResultadoDto>>>
    {
        private readonly IMuestreoRepository _repositoryAsync;

        public GetResultadosMuestreoEstatusMuestreoQueryHandler(IMuestreoRepository repository)
        {
            _repositoryAsync = repository;
        }
        public async Task<PagedResponse<List<AcumuladosResultadoDto>>> Handle(GetResultadosMuestreoEstatusMuestreoPaginadosQuery request, CancellationToken cancellationToken)
        {
            var data = await _repositoryAsync.GetResultadosMuestreoEstatusMuestreoAsync(request.estatusId);
            data = data.AsQueryable();

            if (request.Filter.Any())
            {
                var expressions = MuestreoExpression.GetExpressionList(request.Filter);
                List<AcumuladosResultadoDto> lstMuestreo = new List<AcumuladosResultadoDto>();

                foreach (var filter in expressions)
                {
                    if (request.Filter.Count == 2 && request.Filter[0].Conditional == "equals" && request.Filter[1].Conditional == "equals")
                    {
                        var dataFinal = data;
                        dataFinal = (IEnumerable<AcumuladosResultadoDto>)dataFinal.AsQueryable().Where(filter);
                        lstMuestreo.AddRange(dataFinal);

                    }
                    else
                    { data = (IEnumerable<AcumuladosResultadoDto>)data.AsQueryable().Where(filter); }
                }
                data = (lstMuestreo.Count > 0) ? lstMuestreo.AsQueryable() : data;
            }
            if (request.OrderBy != null)
            {
                if (request.OrderBy.Type == "asc")
                {
                    data = (IEnumerable<AcumuladosResultadoDto>)data.AsQueryable().OrderBy(MuestreoExpression.GetOrderByExpression(request.OrderBy.Column));
                }
                else if (request.OrderBy.Type == "desc")
                {
                    data = (IEnumerable<AcumuladosResultadoDto>)data.AsQueryable().OrderByDescending(MuestreoExpression.GetOrderByExpression(request.OrderBy.Column));
                }
            }

            if (data == null)
            {
                throw new KeyNotFoundException($"No se encontraron datos asociados a resultados revisados");
            }
            
            return PagedResponse<AcumuladosResultadoDto>.CreatePagedReponse(data.ToList(), request.Page, request.PageSize);
        }
    }
}
