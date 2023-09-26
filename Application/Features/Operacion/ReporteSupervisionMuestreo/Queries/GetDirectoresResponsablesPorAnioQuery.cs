using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Operacion.ReporteSupervisionMuestreo.Queries
{
    public class GetDirectoresResponsablesPorAnioQuery : IRequest<Response<List<VwDirectoresResponsables>>>
    {
        public string anio { get; set; }
    }

    public class GetDirectoresResponsablesHandler : IRequestHandler<GetDirectoresResponsablesPorAnioQuery, Response<List<VwDirectoresResponsables>>>
    {
        private readonly IVwDirectoresResponsablesRepository _repository;
        public GetDirectoresResponsablesHandler(IVwDirectoresResponsablesRepository repository)
        {
            _repository = repository;

        }

        public async Task<Response<List<VwDirectoresResponsables>>> Handle(GetDirectoresResponsablesPorAnioQuery request, CancellationToken cancellationToken)
        {

            return new Response<List<VwDirectoresResponsables>>(_repository.ObtenerElementosPorCriterioAsync(x => x.Anio.Equals(request.anio)).Result.ToList());
        }
    }
}
