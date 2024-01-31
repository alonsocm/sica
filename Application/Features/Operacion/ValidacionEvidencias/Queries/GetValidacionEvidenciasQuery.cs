using Application.DTOs;
using Application.DTOs.EvidenciasMuestreo;
using Application.Enums;
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
    public class GetValidacionEvidenciasQuery: IRequest<Response<List<vwValidacionEvienciasDto>>>
    {
    }

    public class GetValidacionEvidenciasQueryHandler : IRequestHandler<GetValidacionEvidenciasQuery, Response<List<vwValidacionEvienciasDto>>>
    {
        private IMapper _mapper;
        private readonly IVwValidacionEvienciasRepository _datosGeneralesValidacionEvidencia;
        private readonly IMuestreoRepository _muestreoRepositiry;
        private readonly IEvidenciaMuestreoRepository _evidenciasRepository;
        public GetValidacionEvidenciasQueryHandler(IVwValidacionEvienciasRepository datosGeneralesValidacionEvidnecia, IMapper mapper, IMuestreoRepository muestreoRepository, IEvidenciaMuestreoRepository evidenciasRepository)
        {
            _datosGeneralesValidacionEvidencia = datosGeneralesValidacionEvidnecia;
            _mapper = mapper;
            _muestreoRepositiry = muestreoRepository;
            _evidenciasRepository = evidenciasRepository;

        }
        public async Task<Response<List<vwValidacionEvienciasDto>>> Handle(GetValidacionEvidenciasQuery request, CancellationToken cancellationToken)
        {
            long[] dato = { 1, 8, 7, 5 };
            var datos = _datosGeneralesValidacionEvidencia.ObtenerDatosGenerales();
            var datosDto = _mapper.Map<List<vwValidacionEvienciasDto>>(datos);
            List<InformacionEvidenciaDto> evidencias = _evidenciasRepository.GetInformacionEvidenciasAsync().Result.ToList();
            foreach (var muestreo in datosDto)
            {                
                muestreo.lstPuntosMuestreo =_mapper.Map<List<PuntosMuestreoDto>>(_muestreoRepositiry.GetPuntoPR_PMAsync(muestreo.ClaveMuestreo).Result);
                List<InformacionEvidenciaDto> evidenciasMuestreo = evidencias.Where(x => x.Muestreo == muestreo.ClaveMuestreo && dato.Contains(x.TipoEvidenciaMuestreo)).ToList();
                if (evidenciasMuestreo.Count > 0) 
                
                {
                    muestreo.lstEvidencias = evidenciasMuestreo;
                    for (int i = 0; i < evidenciasMuestreo.Count ; i++)
                    {
                        string nombrepunto = "";
                        string puntoMuestreo = "";
                        switch (evidenciasMuestreo[i].TipoEvidenciaMuestreo)
                        {
                            case 1:
                                nombrepunto = "FotodeAforo_FA";
                                puntoMuestreo = "FA";
                                break;
                            case 8:
                                nombrepunto = "FotodeMuestras_FS";
                                puntoMuestreo = "FS";
                                break;
                            case 7:
                                nombrepunto = "PuntoCercanoalTrack_TR";
                                puntoMuestreo = "TR";
                                break;
                            case 5:
                                nombrepunto = "FotodeMuestreo_FM";
                                puntoMuestreo = "FM";
                                break;

                            default:
                                break;
                        }

                        PuntosMuestreoDto punto = new PuntosMuestreoDto();
                        punto.ClaveMuestreo = muestreo.ClaveMuestreo;
                        punto.Latitud = (evidenciasMuestreo[i].Latitud == string.Empty)?0: Convert.ToDouble(evidenciasMuestreo[i].Latitud);
                        punto.Longitud = (evidenciasMuestreo[i].Longitud == string.Empty) ? 0 : Convert.ToDouble(evidenciasMuestreo[i].Longitud);
                        punto.NombrePunto = nombrepunto;
                        punto.Punto = puntoMuestreo;
                        muestreo.lstPuntosMuestreo.Add(punto);

                    }
                }
            }

            List<vwValidacionEvienciasDto> lstd = datosDto.ToList();
            return new Response<List<vwValidacionEvienciasDto>>((datos == null) ? new List<vwValidacionEvienciasDto>() : datosDto.ToList());
        }
    }
}
