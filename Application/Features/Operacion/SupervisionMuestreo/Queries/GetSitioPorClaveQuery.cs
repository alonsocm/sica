using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Operacion.SupervisionMuestreo.Queries
{
    public class GetSitioPorClaveQuery : IRequest<Response<SitioSupervisionDto>>
    {
        public string claveSitio { get; set; }

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
            var sitios = (await _sitiosRepository.ObtenerElementosPorCriterioAsync(x => x.ClaveSitio == request.claveSitio)).ToList();

            SitioSupervisionDto sitioDto = new SitioSupervisionDto();
            sitioDto.ClaveSitio = sitios.FirstOrDefault().ClaveSitio;
            sitioDto.SitioId = sitios.FirstOrDefault().SitioId;
            sitioDto.Nombre = sitios.FirstOrDefault().NombreSitio;
            sitioDto.CuencaDireccionLocalId = sitios.FirstOrDefault().CuencaDireccionesLocalesId;
            sitioDto.Latitud = sitios.FirstOrDefault().Latitud.ToString();
            sitioDto.Longitud = sitios.FirstOrDefault().Longitud.ToString();
            sitioDto.TipoCuerpoAgua = sitios.FirstOrDefault().TipoCuerpoAgua;

            sitios.ForEach(sitio => { sitioDto.ClaveMuestreo.Add(sitio.ClaveMuestreo); });

            return new Response<SitioSupervisionDto>(sitioDto);

        }
    }
}
