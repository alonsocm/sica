using Application.DTOs;
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

namespace Application.Features.Operacion.SupervisionMuestreo.Queries
{
    public class GetSitioPorClaveQuery: IRequest<Response<SitioSupervisionDto>>
    {
        public string claveSitio  { get; set; }
        
    }

    public class GetSitioPorClaveQueryHandler : IRequestHandler<GetSitioPorClaveQuery, Response<SitioSupervisionDto>>
    {
        private readonly IVw_SitiosRepository _sitiosRepository;
        private readonly IMapper _mapper;
        public GetSitioPorClaveQueryHandler(IVw_SitiosRepository sitiosRepository, IMapper mapper)
        {
            _sitiosRepository = sitiosRepository;
            _mapper = mapper;
        }

        public async Task<Response<SitioSupervisionDto>> Handle(GetSitioPorClaveQuery request, CancellationToken cancellationToken)
        {
            var sitios = (await _sitiosRepository.ObtenerElementosPorCriterioAsync(x => x.ClaveSitio == request.claveSitio)).FirstOrDefault();

            SitioSupervisionDto sitioDto = new SitioSupervisionDto();
            sitioDto.ClaveMuestreo = sitios.ClaveMuestreo.ToString();
            sitioDto.ClaveSitio = sitios.ClaveSitio;
            sitioDto.SitioId = sitios.SitioId;
            sitioDto.NombreSito = sitios.NombreSitio;
            sitioDto.CuencaDireccionLocalId = sitios.CuencaDireccionesLocalesId;
            sitioDto.Latitud = sitios.Latitud.ToString();
            sitioDto.Longitud = sitios.Longitud.ToString();
            sitioDto.TipoCuerpoAgua = sitios.TipoCuerpoAgua;

            return new Response<SitioSupervisionDto>(sitioDto);

        }
    }
}
