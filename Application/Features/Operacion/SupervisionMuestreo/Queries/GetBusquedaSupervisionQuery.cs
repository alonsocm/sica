using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.SupervisionMuestreo.Queries
{
    public class GetBusquedaSupervisionQuery: IRequest<Response<List<VwDatosGeneralesSupervision>>>
    {
        public CriteriosBusquedaSupervisionDto? Busqueda { get; set; }
         
    }

    
    public class GetBusquedaSupervisionHandler : IRequestHandler<GetBusquedaSupervisionQuery, Response<List<VwDatosGeneralesSupervision>>>
    {
        private readonly IVwDatosGeneralesSupervisionRepository _datosGeneralesSupervisionRepository;
        public GetBusquedaSupervisionHandler(IVwDatosGeneralesSupervisionRepository datosGeneralesSupervisionRepository)
        {
            _datosGeneralesSupervisionRepository = datosGeneralesSupervisionRepository;

        }

        public async Task<Response<List<VwDatosGeneralesSupervision>>> Handle(GetBusquedaSupervisionQuery request, CancellationToken cancellationToken)
        {
            var datos = _datosGeneralesSupervisionRepository.ObtenerBusqueda(request.Busqueda);
            return new Response<List<VwDatosGeneralesSupervision>>((datos ==null) ? new List<VwDatosGeneralesSupervision>(): datos.ToList());

        }

      

    }
}
