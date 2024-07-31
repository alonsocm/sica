using Application.DTOs;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public GetDistinctValuesFromColumnHandler(IRepositoryAsync<Sitio> repositoryAsync, IRepository<Sitio> repository, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<object>>> Handle(GetDistinctValuesFromColumn request, CancellationToken cancellationToken)
        {

            var sitios = await _repositoryAsync.ListAsync(new PagedSitiosSpecification(), cancellationToken);
            var sitiosDto = _mapper.Map<IEnumerable<SitioDto>>(sitios);

            if (request.Filters.Any())
            {
                var expressions = QueryExpression<Sitio>.GetExpressionList(request.Filters);

                foreach (var filter in expressions)
                { sitiosDto = (List<SitioDto>)sitios.AsQueryable().Where(filter); }
            }

            var response = _repository.GetDistinctValuesFromColumn(request.Column, sitiosDto);

            return new Response<IEnumerable<object>>(response);
        }
    }
}
