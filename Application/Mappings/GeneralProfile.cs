using Application.DTOs;
using Application.DTOs.Catalogos;
using Application.DTOs.EvidenciasMuestreo;
using Application.DTOs.Users;
using Application.Features.Sitios.Commands.CreateSitioCommand;
using Application.Models;
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
            CreateMap<LimiteParametroLaboratorio, LimitesParametroLaboratorioDto>()
                .ForMember(x => x.ClaveParametro, o => o.MapFrom(src => src.Parametro.ClaveParametro))
                .ForMember(x => x.NombreParametro, o => o.MapFrom(src => src.Parametro.Descripcion))
                .ForMember(x => x.Laboratorio, o => o.MapFrom(src => src.Laboratorio.Nomenclatura))
                .ForMember(x => x.RealizaLaboratorioMuestreo, o => o.MapFrom(src => src.RealizaLaboratorioMuestreo.Descripcion))
                .ForMember(x => x.LaboratorioMuestreo, o => o.MapFrom(src => src.LaboratorioMuestreo.Nomenclatura))
                .ForMember(x => x.Periodo, o => o.MapFrom(src => src.Periodo))
                .ForMember(x => x.Activo, o => o.MapFrom(src => src.Activo))
                .ForMember(x => x.LDMaCumplir, o => o.MapFrom(src => src.LdmaCumplir))
                .ForMember(x => x.LPCaCumplir, o => o.MapFrom(src => src.LpcaCumplir))
                .ForMember(x => x.LoMuestra, o => o.MapFrom(src => src.LoMuestra))
                .ForMember(x => x.LoSubroga, o => o.MapFrom(src => src.LoSubroga.Descripcion))
                .ForMember(x => x.LaboratorioSubrogado, o => o.MapFrom(src => src.LaboratorioSubroga.Nomenclatura))
                ;
            CreateMap<Acuifero, AcuiferoDto>();
            CreateMap<Sitio, SitioDto>()
                .ForMember(x => x.NombreSitio, o => o.MapFrom(src => src.NombreSitio))
                .ForMember(x => x.ClaveSitio, o => o.MapFrom(src => src.ClaveSitio))
                .ForMember(x => x.Acuifero, o => o.MapFrom(src => src.Acuifero.Descripcion))
                .ForMember(x => x.Cuenca, o => o.MapFrom(src => src.CuencaDireccionesLocales.Ocuenca.Descripcion))
                .ForMember(x => x.DireccionLocal, o => o.MapFrom(src => src.CuencaDireccionesLocales.Dlocal.Descripcion))
                .ForMember(x => x.Estado, o => o.MapFrom(src => src.Estado.Nombre))
                .ForMember(x => x.Municipio, o => o.MapFrom(src => src.Municipio.Nombre))
                .ForMember(x => x.CuerpoAgua, o => o.MapFrom(src => src.CuerpoTipoSubtipoAgua.CuerpoAgua.Descripcion))
                .ForMember(x => x.TipoCuerpoAgua, o => o.MapFrom(src => src.CuerpoTipoSubtipoAgua.TipoCuerpoAgua.Descripcion))
                .ForMember(x => x.subtipoCuerpoAgua, o => o.MapFrom(src => src.CuerpoTipoSubtipoAgua.SubtipoCuerpoAgua.Descripcion))
                .ForMember(x => x.Latitud, o => o.MapFrom(src => src.Latitud))
                .ForMember(x => x.Longitud, o => o.MapFrom(src => src.Longitud))
                .ForMember(x => x.Observaciones, o => o.MapFrom(src => src.Observaciones));
            CreateMap<CuencaDireccionesLocales, CuencaDireccionesLocalesDto>()
                .ForMember(x => x.OrganismoCuenca, o => o.MapFrom(src => src.Ocuenca.Descripcion))
                .ForMember(x => x.DieccionLocal, o => o.MapFrom(src => src.Dlocal.Descripcion))
            .ForMember(x => x.OrganismoCuencaId, o => o.MapFrom(src => src.Ocuenca.Id))
       .ForMember(x => x.DieccionLocalId, o => o.MapFrom(src => src.Dlocal.Id));

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
            CreateMap<TipoCuerpoAgua, TipoCuerpoAguaDto>()
                .ForMember(x => x.TipoHomologadoDescripcion, o => o.MapFrom(src => src.TipoHomologado.Descripcion));
            CreateMap<VwValidacionEviencias, vwValidacionEvienciasDto>();
            CreateMap<VwValidacionEvidenciaRealizada, EventualidadesMuestreoAprobados>();
            CreateMap<VwValidacionEvidenciaRealizada, EvidenciasMuestreosAprobados>();
            #endregion
        }
    }
}
