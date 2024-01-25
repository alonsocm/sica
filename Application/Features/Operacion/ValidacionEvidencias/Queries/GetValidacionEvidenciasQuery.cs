using Application.Features.Operacion.SupervisionMuestreo.Queries;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.ValidacionEvidencias.Queries
{
    public class GetValidacionEvidenciasQuery: IRequest<Response<List<VwValidacionEviencias>>>
    {
    }

    public class GetValidacionEvidenciasQueryHandler : IRequestHandler<GetValidacionEvidenciasQuery, Response<List<VwValidacionEviencias>>>
    {
        private readonly IVwValidacionEvienciasRepository _datosGeneralesValidacionEvidencia;
        public GetValidacionEvidenciasQueryHandler(IVwValidacionEvienciasRepository datosGeneralesValidacionEvidnecia)
        {
            _datosGeneralesValidacionEvidencia = datosGeneralesValidacionEvidnecia;
        }

        public async Task<Response<List<VwValidacionEviencias>>> Handle(GetValidacionEvidenciasQuery request, CancellationToken cancellationToken)
        {
            
            var datos = _datosGeneralesValidacionEvidencia.ObtenerDatosGenerales();

            return new Response<List<VwValidacionEviencias>>((datos == null) ? new List<VwValidacionEviencias>() : datos.ToList());
        }
    }
}
