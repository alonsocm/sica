using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;



namespace Application.Features.Catalogos.Laboratorios.Queries
{
    public class GetDistinctValuesFromColumn : IRequest<Response<IEnumerable<object>>>
    {
        public string Column { get; set; }
        public List<Filter> Filters { get; set; }
    }

    public class GetDistinctValuesFromColumnHandler : IRequestHandler<GetDistinctValuesFromColumn, Response<IEnumerable<object>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.Laboratorios> _repositoryAsync;
        private readonly IRepository<Domain.Entities.Laboratorios> _repository;
        private readonly IMapper _mapper;

        public GetDistinctValuesFromColumnHandler(IRepositoryAsync<Domain.Entities.Laboratorios> repositoryAsync, IRepository<Domain.Entities.Laboratorios> repository)
        {
            _repositoryAsync = repositoryAsync;
            _repository = repository;
        }

        public async Task<Response<IEnumerable<object>>> Handle(GetDistinctValuesFromColumn request, CancellationToken cancellationToken)
        {
            var laboratorios = await _repositoryAsync.ListAsync();
            if (request.Filters.Any())
            {
                var expressions = QueryExpression<Domain.Entities.Laboratorios>.GetExpressionList(request.Filters);

                foreach (var filter in expressions)
                { laboratorios = (List<Domain.Entities.Laboratorios>)laboratorios.AsQueryable().Where(filter); }
            }

            var response = _repository.GetDistinctValuesFromColumn(request.Column, laboratorios);

            return new Response<IEnumerable<object>>(response);
        }
    }
}
