using Application.DTOs.Catalogos;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using MediatR;


namespace Application.Features.Catalogos.LimiteParametroLaboratorio.Queries
{
    public class GetDistinctValuesFromColumn: IRequest<Response<IEnumerable<object>>>
    {
        public string Column { get; set; }
        public List<Filter> Filters { get; set; }
    }

    public class GetDistinctValuesFromColumnHandler : IRequestHandler<GetDistinctValuesFromColumn, Response<IEnumerable<object>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.LimiteParametroLaboratorio> _repositoryAsync;
        private readonly IRepository<Domain.Entities.LimiteParametroLaboratorio> _repository;
        private readonly IMapper _mapper;

        public GetDistinctValuesFromColumnHandler(IRepositoryAsync<Domain.Entities.LimiteParametroLaboratorio> repositoryAsync, IRepository<Domain.Entities.LimiteParametroLaboratorio> repository, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<object>>> Handle(GetDistinctValuesFromColumn request, CancellationToken cancellationToken)
        {

            var sitios = await _repositoryAsync.ListAsync(new LimiteParametroLaboratorioSpecification(), cancellationToken);
            var sitiosDto = _mapper.Map<IEnumerable<LimitesParametroLaboratorioDto>>(sitios);

            if (request.Filters.Any())
            {
                var expressions = QueryExpression<Domain.Entities.LimiteParametroLaboratorio>.GetExpressionList(request.Filters);

                foreach (var filter in expressions)
                { sitiosDto = (List<LimitesParametroLaboratorioDto>)sitios.AsQueryable().Where(filter); }
            }

            var response = _repository.GetDistinctValuesFromColumn(request.Column, sitiosDto);

            return new Response<IEnumerable<object>>(response);
        }
    }
}
