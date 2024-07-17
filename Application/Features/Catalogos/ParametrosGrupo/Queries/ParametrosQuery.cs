using Application.DTOs.Catalogos;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.ParametrosGrupo.Queries
{
    public class ParametrosQuery : IRequest<Response<IEnumerable<ParametroDTO>>>
    {
    }

    public class ParametrosQueryHandler : IRequestHandler<ParametrosQuery, Response<IEnumerable<ParametroDTO>>>
    {
        private readonly IParametroRepository _repository;

        public ParametrosQueryHandler(IParametroRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<IEnumerable<ParametroDTO>>> Handle(ParametrosQuery request, CancellationToken cancellationToken)
        {
            var parametros = _repository.GetParametros();
            return new Response<IEnumerable<ParametroDTO>>(parametros);
        }
    }
}
