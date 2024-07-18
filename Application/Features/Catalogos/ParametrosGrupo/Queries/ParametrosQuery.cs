using Application.DTOs.Catalogos;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.ParametrosGrupo.Queries
{
    public class ParametrosQuery : IRequest<PagedResponse<IEnumerable<ParametroDTO>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
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

            return PagedResponse<ParametroDTO>.CreatePagedReponse(data, request.Page, request.PageSize);
        }
    }
}
