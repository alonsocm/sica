using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Catalogos.Sitios.Queries
{
    public class GetDistinctValuesFromColumn : IRequest<Response<IEnumerable<object>>>
    {
        public string Column { get; set; }
        public List<Filter> Filters { get; set; }
    }

    public class GetDistinctValuesFromColumnHandler : IRequestHandler<GetDistinctValuesFromColumn, Response<IEnumerable<object>>>
    {
        private readonly IRepositoryAsync<Sitio> _repositoryAsync;
        private readonly IRepository<Sitio> _repository;

        public GetDistinctValuesFromColumnHandler(IRepositoryAsync<Sitio> repositoryAsync, IRepository<Sitio> repository)
        {
            _repositoryAsync = repositoryAsync;
            _repository = repository;
        }

        public async Task<Response<IEnumerable<object>>> Handle(GetDistinctValuesFromColumn request, CancellationToken cancellationToken)
        {
            var data = await _repositoryAsync.ListAsync();
            var date = await _repository.ObtenerTodosElementosAsync();
            //data = data.AsQueryable();

            if (request.Filters.Any())
            {
                var expressions = QueryExpression<Sitio>.GetExpressionList(request.Filters);

                //foreach (var filter in expressions)
                //{
                //    data = data.Where(filter);
                //}
            }

            var response = _repository.GetDistinctValuesFromColumn(request.Column, data);

            return new Response<IEnumerable<object>>(response);
        }
    }
}
