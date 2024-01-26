using Application.DTOs;
using Application.DTOs.EvidenciasMuestreo;
using Application.Features.Operacion.SupervisionMuestreo.Queries;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
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
        private IMapper _mapper;
        private readonly IVwValidacionEvienciasRepository _datosGeneralesValidacionEvidencia;
        private readonly IMuestreoRepository _muestreoRepositiry;
        public GetValidacionEvidenciasQueryHandler(IVwValidacionEvienciasRepository datosGeneralesValidacionEvidnecia, IMapper mapper, IMuestreoRepository muestreoRepository)
        {
            _datosGeneralesValidacionEvidencia = datosGeneralesValidacionEvidnecia;
            _mapper = mapper;
            _muestreoRepositiry = muestreoRepository;
        }

        public async Task<Response<List<VwValidacionEviencias>>> Handle(GetValidacionEvidenciasQuery request, CancellationToken cancellationToken)
        {
            
            var datos = _datosGeneralesValidacionEvidencia.ObtenerDatosGenerales();
            var datosDto = _mapper.Map<List<vwValidacionEvienciasDto>>(datos);
            foreach (var muestreo in datosDto)
            {
                //var puntos = _muestreoRepositiry.GetPuntoPR_PMAsync(muestreo.ClaveMuestreo).Result;
                muestreo.lstPuntosMuestreo =_mapper.Map<List<PuntosMuestreoDto>>(_muestreoRepositiry.GetPuntoPR_PMAsync(muestreo.ClaveMuestreo).Result);
            }


            return new Response<List<VwValidacionEviencias>>((datos == null) ? new List<VwValidacionEviencias>() : datos.ToList());
        }
    }
}
