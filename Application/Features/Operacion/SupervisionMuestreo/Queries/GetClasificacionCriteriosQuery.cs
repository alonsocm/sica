using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.SupervisionMuestreo.Queries
{
    public class GetClasificacionCriteriosQuery: IRequest<Response<List<ClasificacionCriterioDto>>>
    {
    }

    public class GetClasificacionCriteriosHandler : IRequestHandler<GetClasificacionCriteriosQuery, Response<List<ClasificacionCriterioDto>>>
    {
        private readonly ISupervisionMuestreoRepository _repository;
        public GetClasificacionCriteriosHandler(ISupervisionMuestreoRepository repository)
        {
            _repository = repository;
         
        }

        public async Task<Response<List<ClasificacionCriterioDto>>> Handle(GetClasificacionCriteriosQuery request, CancellationToken cancellationToken)
        {
            
            return new Response<List<ClasificacionCriterioDto>>(_repository.ObtenerCriterios().Result.ToList());
        }
    }
}
