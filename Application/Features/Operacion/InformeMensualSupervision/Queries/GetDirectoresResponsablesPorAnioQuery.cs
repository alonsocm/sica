using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Operacion.InformeMensualSupervision.Queries
{
    public class GetDirectoresResponsablesPorAnioQuery : IRequest<Response<List<VwDirectoresResponsablesOc>>>
    {
        public string anio { get; set; }
    }

    public class GetDirectoresResponsablesHandler : IRequestHandler<GetDirectoresResponsablesPorAnioQuery, Response<List<VwDirectoresResponsablesOc>>>
    {
        private readonly IVwDirectoresResponsablesRepository _repository;
        public GetDirectoresResponsablesHandler(IVwDirectoresResponsablesRepository repository)
        {
            _repository = repository;

        }

        public async Task<Response<List<VwDirectoresResponsablesOc>>> Handle(GetDirectoresResponsablesPorAnioQuery request, CancellationToken cancellationToken)
        {

            return new Response<List<VwDirectoresResponsablesOc>>(_repository.ObtenerElementosPorCriterioAsync(x => x.Anio.Equals(request.anio)).Result.ToList());
        }
    }
}
