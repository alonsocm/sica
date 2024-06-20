using Application.DTOs;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Resultados.Queries
{
    public class GetDistinctValuesFromColumn : IRequest<Response<IEnumerable<object>>>
    {
        public string Column { get; set; }
        public int UserId { get; set; }
        public int Estatus { get; set; }
        public List<Filter> Filters { get; set; }
    }

    public class GetDistinctValuesFromColumnHandler : IRequestHandler<GetDistinctValuesFromColumn, Response<IEnumerable<object>>>
    {
        private readonly IResumenResRepository _repositoryAsync;

        public GetDistinctValuesFromColumnHandler(IResumenResRepository repositoryAsync)
        {
            _repositoryAsync=repositoryAsync;
        }

        public async Task<Response<IEnumerable<object>>> Handle(GetDistinctValuesFromColumn request, CancellationToken cancellationToken)
        {
            IEnumerable<RegistroOriginalDto> data = await _repositoryAsync.GetResumenResultadosTemp(request.UserId, request.Estatus)??throw new KeyNotFoundException($"No se encontraron datos asociados a resultados revisados");
            data = data.AsQueryable();

            if (request.Filters.Any())
            {
                var expressions = RegistroOriginalExpression.GetExpressionList(request.Filters);

                foreach (var filter in expressions)
                {
                    data = data.AsQueryable().Where(filter);
                }
            }

            var response = _repositoryAsync.GetDistinctValuesFromColumn(request.Column, data);

            return new Response<IEnumerable<object>>(response);
        }
    }
}
