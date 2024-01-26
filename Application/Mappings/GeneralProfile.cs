using Application.DTOs;
using Application.DTOs.EvidenciasMuestreo;
using Application.DTOs.Users;
using Application.Features.Sitios.Commands.CreateSitioCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Commands
            CreateMap<CreateSitioCommand, Sitio>();
            #endregion

            #region DTOs
            CreateMap<Sitio, SitioDto>();
            CreateMap<Perfil, PerfilDto>();
            CreateMap<DireccionLocal, DireccionLocalDto>();
            CreateMap<Observaciones, ObservacionesDto>();
            CreateMap<OrganismoCuenca, OrganismoCuencaDto>();
            CreateMap<Usuario, UserDto>()
                .ForMember(x => x.PerfilId, o => o.MapFrom(src => src.Perfil.Id))
                .ForMember(x => x.NombrePerfil, o => o.MapFrom(src => src.Perfil.Nombre));
            CreateMap<PerfilPagina, PaginaDto>()
                .ForMember(x => x.Id, o => o.MapFrom(src => src.IdPaginaNavigation.Id))
                .ForMember(x => x.IdPaginaPadre, o => o.MapFrom(src => src.IdPaginaNavigation.IdPaginaPadre))
                .ForMember(x => x.Descripcion, o => o.MapFrom(src => src.IdPaginaNavigation.Descripcion))
                .ForMember(x => x.Url, o => o.MapFrom(src => src.IdPaginaNavigation.Url))
                .ForMember(x => x.Orden, o => o.MapFrom(src => src.IdPaginaNavigation.Orden));
            //CreateMap<CargaMuestreoDto, CargaMuestreos>();
            CreateMap<ResultadoMuestreo, ResultadoMuestreoDto>()
                .ForMember(x => x.MuestreoId, o => o.MapFrom(src => src.Id));
            CreateMap<Estado, EstadoDto>();
            CreateMap<Municipio, MunicipioDto>();
            CreateMap<Localidad, LocalidadDto>();
            //CreateMap<AprobacionResultadoMuestreo, AprobacionResultadoMuestreoDto>();

            CreateMap<ParametrosGrupo, ParametrosDto>();
            CreateMap<TipoHomologado, TipoHomologadoDto>();
            CreateMap<SupervisionMuestreo, SupervisionMuestreoDto>();
            CreateMap<Laboratorios, LaboratoriosDto>();
            CreateMap<EvidenciaSupervisionMuestreo, EvidenciaSupervisionDto>();
            CreateMap<TipoCuerpoAgua, TipoCuerpoAguaDto>();
            CreateMap<VwValidacionEviencias, vwValidacionEvienciasDto>();

            #endregion
        }
    }
}
