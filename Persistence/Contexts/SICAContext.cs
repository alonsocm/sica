using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

public partial class SicaContext : DbContext
{
    public SicaContext()
    {
    }

    public SicaContext(DbContextOptions<SicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accion> Accion { get; set; }

    public virtual DbSet<AccionLaboratorio> AccionLaboratorio { get; set; }

    public virtual DbSet<Acuifero> Acuifero { get; set; }

    public virtual DbSet<AprobacionResultadoMuestreo> AprobacionResultadoMuestreo { get; set; }

    public virtual DbSet<ArchivoInformeMensualSupervision> ArchivoInformeMensualSupervision { get; set; }

    public virtual DbSet<AvisoRealizacion> AvisoRealizacion { get; set; }

    public virtual DbSet<BrigadaMuestreo> BrigadaMuestreo { get; set; }

    public virtual DbSet<CargaSitiosBase7145> CargaSitiosBase7145 { get; set; }

    public virtual DbSet<CatCuerpoaguaFinal> CatCuerpoaguaFinal { get; set; }

    public virtual DbSet<ClasificacionCriterio> ClasificacionCriterio { get; set; }

    public virtual DbSet<ClasificacionRegla> ClasificacionRegla { get; set; }

    public virtual DbSet<CopiaInformeMensualSupervision> CopiaInformeMensualSupervision { get; set; }

    public virtual DbSet<CriteriosSupervisionMuestreo> CriteriosSupervisionMuestreo { get; set; }

    public virtual DbSet<CuencaDireccionesLocales> CuencaDireccionesLocales { get; set; }

    public virtual DbSet<CuerpoAgua> CuerpoAgua { get; set; }

    public virtual DbSet<CuerpoTipoSubtipoAgua> CuerpoTipoSubtipoAgua { get; set; }

    public virtual DbSet<DestinatariosAtencion> DestinatariosAtencion { get; set; }

    public virtual DbSet<DireccionLocal> DireccionLocal { get; set; }

    public virtual DbSet<Directorio> Directorio { get; set; }

    public virtual DbSet<Emergencia> Emergencia { get; set; }

    public virtual DbSet<Estado> Estado { get; set; }

    public virtual DbSet<EstatusMuestreo> EstatusMuestreo { get; set; }

    public virtual DbSet<EstatusMuestreo1> EstatusMuestreo1 { get; set; }

    public virtual DbSet<EstatusOcdlSecaia> EstatusOcdlSecaia { get; set; }

    public virtual DbSet<EstatusResultado> EstatusResultado { get; set; }

    public virtual DbSet<EvidenciaMuestreo> EvidenciaMuestreo { get; set; }

    public virtual DbSet<EvidenciaReplica> EvidenciaReplica { get; set; }

    public virtual DbSet<EvidenciaSupervisionMuestreo> EvidenciaSupervisionMuestreo { get; set; }

    public virtual DbSet<EvidenciasReplicasResultadoReglasValidacion> EvidenciasReplicasResultadoReglasValidacion { get; set; }

    public virtual DbSet<FormaReporteEspecifica> FormaReporteEspecifica { get; set; }

    public virtual DbSet<GrupoParametro> GrupoParametro { get; set; }

    public virtual DbSet<HistorialSustitucionEmergencia> HistorialSustitucionEmergencia { get; set; }

    public virtual DbSet<HistorialSustitucionLimites> HistorialSustitucionLimites { get; set; }

    public virtual DbSet<InformeMensualSupervision> InformeMensualSupervision { get; set; }

    public virtual DbSet<IntervalosPuntajeSupervision> IntervalosPuntajeSupervision { get; set; }

    public virtual DbSet<Laboratorios> Laboratorios { get; set; }

    public virtual DbSet<LimiteParametroLaboratorio> LimiteParametroLaboratorio { get; set; }

    public virtual DbSet<Localidad> Localidad { get; set; }

    public virtual DbSet<Mes> Mes { get; set; }

    public virtual DbSet<Muestreadores> Muestreadores { get; set; }

    public virtual DbSet<Muestreo> Muestreo { get; set; }

    public virtual DbSet<MuestreoEmergencia> MuestreoEmergencia { get; set; }

    public virtual DbSet<Municipio> Municipio { get; set; }

    public virtual DbSet<Observaciones> Observaciones { get; set; }

    public virtual DbSet<OrganismoCuenca> OrganismoCuenca { get; set; }

    public virtual DbSet<Pagina> Pagina { get; set; }

    public virtual DbSet<ParametrosCostos> ParametrosCostos { get; set; }

    public virtual DbSet<ParametrosGrupo> ParametrosGrupo { get; set; }

    public virtual DbSet<ParametrosReglasNoRelacion> ParametrosReglasNoRelacion { get; set; }

    public virtual DbSet<ParametrosSitioTipoCuerpoAgua> ParametrosSitioTipoCuerpoAgua { get; set; }

    public virtual DbSet<Perfil> Perfil { get; set; }

    public virtual DbSet<PerfilPagina> PerfilPagina { get; set; }

    public virtual DbSet<PerfilPaginaAccion> PerfilPaginaAccion { get; set; }

    public virtual DbSet<PlantillaInformeMensualSupervision> PlantillaInformeMensualSupervision { get; set; }

    public virtual DbSet<ProgramaAnio> ProgramaAnio { get; set; }

    public virtual DbSet<ProgramaMuestreo> ProgramaMuestreo { get; set; }

    public virtual DbSet<ProgramaSitio> ProgramaSitio { get; set; }

    public virtual DbSet<Puestos> Puestos { get; set; }

    public virtual DbSet<ReglaReporteResultadoTca> ReglaReporteResultadoTca { get; set; }

    public virtual DbSet<ReglasLaboratorioLdmLpc> ReglasLaboratorioLdmLpc { get; set; }

    public virtual DbSet<ReglasMinimoMaximo> ReglasMinimoMaximo { get; set; }

    public virtual DbSet<ReglasRelacion> ReglasRelacion { get; set; }

    public virtual DbSet<ReglasRelacionParametro> ReglasRelacionParametro { get; set; }

    public virtual DbSet<ReglasReporte> ReglasReporte { get; set; }

    public virtual DbSet<ReglasReporteLeyendas> ReglasReporteLeyendas { get; set; }

    public virtual DbSet<ReplicasResultadosReglasValidacion> ReplicasResultadosReglasValidacion { get; set; }

    public virtual DbSet<ResultadoMuestreo> ResultadoMuestreo { get; set; }

    public virtual DbSet<Sitio> Sitio { get; set; }

    public virtual DbSet<SubgrupoAnalitico> SubgrupoAnalitico { get; set; }

    public virtual DbSet<SubtipoCuerpoAgua> SubtipoCuerpoAgua { get; set; }

    public virtual DbSet<SupervisionMuestreo> SupervisionMuestreo { get; set; }

    public virtual DbSet<TipoAprobacion> TipoAprobacion { get; set; }

    public virtual DbSet<TipoArchivoInformeMensualSupervision> TipoArchivoInformeMensualSupervision { get; set; }

    public virtual DbSet<TipoCarga> TipoCarga { get; set; }

    public virtual DbSet<TipoCuerpoAgua> TipoCuerpoAgua { get; set; }

    public virtual DbSet<TipoEvidenciaMuestreo> TipoEvidenciaMuestreo { get; set; }

    public virtual DbSet<TipoHomologado> TipoHomologado { get; set; }

    public virtual DbSet<TipoRegla> TipoRegla { get; set; }

    public virtual DbSet<TipoSitio> TipoSitio { get; set; }

    public virtual DbSet<TipoSupervision> TipoSupervision { get; set; }

    public virtual DbSet<TipoSustitucion> TipoSustitucion { get; set; }

    public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<ValidacionEvidencia> ValidacionEvidencia { get; set; }

    public virtual DbSet<ValoresSupervisionMuestreo> ValoresSupervisionMuestreo { get; set; }

    public virtual DbSet<VwClaveMuestreo> VwClaveMuestreo { get; set; }

    public virtual DbSet<VwDatosGeneralesSupervision> VwDatosGeneralesSupervision { get; set; }

    public virtual DbSet<VwDirectoresResponsablesOc> VwDirectoresResponsablesOc { get; set; }

    public virtual DbSet<VwEstatusMuestreosAdministracion> VwEstatusMuestreosAdministracion { get; set; }

    public virtual DbSet<VwIntervalosTotalesOcDl> VwIntervalosTotalesOcDl { get; set; }

    public virtual DbSet<VwLimiteLaboratorio> VwLimiteLaboratorio { get; set; }

    public virtual DbSet<VwLimiteMaximoComun> VwLimiteMaximoComun { get; set; }

    public virtual DbSet<VwOrganismosDirecciones> VwOrganismosDirecciones { get; set; }

    public virtual DbSet<VwReplicaRevisionResultado> VwReplicaRevisionResultado { get; set; }

    public virtual DbSet<VwResultadosInicialReglas> VwResultadosInicialReglas { get; set; }

    public virtual DbSet<VwResultadosNoCumplenFechaEntrega> VwResultadosNoCumplenFechaEntrega { get; set; }

    public virtual DbSet<VwSitios> VwSitios { get; set; }

    public virtual DbSet<VwValidacionEvidenciaRealizada> VwValidacionEvidenciaRealizada { get; set; }

    public virtual DbSet<VwValidacionEvidenciaTotales> VwValidacionEvidenciaTotales { get; set; }

    public virtual DbSet<VwValidacionEviencias> VwValidacionEviencias { get; set; }    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DbConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accion>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal del catálogo Acción");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Descripción de Acción");
        });

        modelBuilder.Entity<AccionLaboratorio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AccionSubrogado");

            entity.Property(e => e.Id).HasComment("Identificador principal del catálogo AccionLaboratorio ");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .HasComment("Campo que describe el significado para las opciones NA y NRL");
            entity.Property(e => e.LoSubroga)
                .HasMaxLength(10)
                .HasComment("Campo que describe si subroga");
        });

        modelBuilder.Entity<Acuifero>(entity =>
        {
            entity.ToTable("Acuifero", "cat");

            entity.Property(e => e.Id).HasComment("Identificador principal del catálogo Acuífero");
            entity.Property(e => e.Clave).HasComment("Campo que describe la clave del acuífero");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasComment("Campo que describe el Acuífero");
        });

        modelBuilder.Entity<AprobacionResultadoMuestreo>(entity =>
        {
            entity.HasIndex(e => e.ResultadoMuestreoId, "IX_AprobacionResultadoMuestreo_ResultadoMuestreoId");

            entity.HasIndex(e => e.UsuarioRevisionId, "IX_AprobacionResultadoMuestreo_UsuarioRevisionId");

            entity.Property(e => e.Id).HasComment("Identificador principal de tabla donde se guardara la aprobación de resultado de muestreo");
            entity.Property(e => e.ApruebaResultado).HasComment("Estatus de aprobación de resultados");
            entity.Property(e => e.ComentariosAprobacionResultados).HasComment("Comentarios porque no fue aprobado");
            entity.Property(e => e.FechaAprobRechazo)
                .HasComment("Fecha de aprobación o rechazo")
                .HasColumnType("datetime");
            entity.Property(e => e.ResultadoMuestreoId).HasComment("Identificador de llave foránea que hace referencia a la tabla de ResultadoMuestreo");
            entity.Property(e => e.UsuarioRevisionId).HasComment("Identificador de llave foránea de usuario que realizo la aprobación/rechazo");

            entity.HasOne(d => d.ResultadoMuestreo).WithMany(p => p.AprobacionResultadoMuestreo)
                .HasForeignKey(d => d.ResultadoMuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AprobacionResultadoMuestreo_ResultadoMuestreo");

            entity.HasOne(d => d.UsuarioRevision).WithMany(p => p.AprobacionResultadoMuestreo)
                .HasForeignKey(d => d.UsuarioRevisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AprobacionResultadoMuestreo_Usuario");
        });

        modelBuilder.Entity<ArchivoInformeMensualSupervision>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla ArchivoInformeMensualSupervision");
            entity.Property(e => e.Archivo).HasComment("Campo que describe el archivo en formato varbinary");
            entity.Property(e => e.FechaCarga)
                .HasComment("Campo que describe la fecha en la que se cargo el archivo")
                .HasColumnType("datetime");
            entity.Property(e => e.InformeMensualSupervisionId).HasComment("Llave foránea que hace referencia al catálogo de TipoArchivoInformeMensualSupervision ");
            entity.Property(e => e.NombreArchivo)
                .IsUnicode(false)
                .HasComment("Campo que describe el nombre del archivo");
            entity.Property(e => e.TipoArchivoInformeMensualSupervisionId).HasComment("Llave foránea que hace referencia a la catálogo de TipoArchivoInformeMensualSupervision ");
            entity.Property(e => e.UsuarioCargaId).HasComment("Llave foránea que hace relación a la tabla de Usuario describiendo el usuario que cargo el archivo");

            entity.HasOne(d => d.InformeMensualSupervision).WithMany(p => p.ArchivoInformeMensualSupervision)
                .HasForeignKey(d => d.InformeMensualSupervisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArchivoInformeMensualSupervision_InformeMensualSupervision");

            entity.HasOne(d => d.UsuarioCarga).WithMany(p => p.ArchivoInformeMensualSupervision)
                .HasForeignKey(d => d.UsuarioCargaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArchivoInformeMensualSupervision_Usuario");
        });

        modelBuilder.Entity<AvisoRealizacion>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador de la tabla AvisoRealizacion");
            entity.Property(e => e.BrigadaMuestreoId).HasComment("Llave foránea que hace referencia al catalogo de Brigada de muestreo");
            entity.Property(e => e.ClaveMuestreo)
                .HasMaxLength(100)
                .HasComment("Campo que describe la clave muestreo");
            entity.Property(e => e.ClaveSitio)
                .HasMaxLength(150)
                .HasComment("Campo que describe la clave sitio");
            entity.Property(e => e.ConEventualidades).HasComment("Campo que describe si cuenta con eventualidades");
            entity.Property(e => e.ConQcmuestreo)
                .HasComment("Campo que describe si cuenta con QC el muestreo")
                .HasColumnName("ConQCMuestreo");
            entity.Property(e => e.DocumentoEventualidad)
                .HasMaxLength(100)
                .HasComment("Campo que describe el documento de la eventualidad");
            entity.Property(e => e.FechaAprobacionEventualidad)
                .HasComment("Campo que describe la fecha de aprobación de la eventualidad")
                .HasColumnType("date");
            entity.Property(e => e.FechaProgramada)
                .HasComment("Campo que describe la fecha programada")
                .HasColumnType("date");
            entity.Property(e => e.FechaRealVisita)
                .HasComment("Campo que describe la fecha real de visita")
                .HasColumnType("date");
            entity.Property(e => e.FechaReprogramacion)
                .HasComment("Campo que describe la fecha de reprogramación")
                .HasColumnType("date");
            entity.Property(e => e.FolioEventualidad)
                .HasMaxLength(30)
                .HasComment("Campo que describe el folio de la eventualidad");
            entity.Property(e => e.LaboratorioId).HasComment("Llave foránea que hace referencia al catálogo de Laboratorios");
            entity.Property(e => e.TipoEventualidad)
                .HasMaxLength(100)
                .HasComment("Llave foránea que hace referencia al catálogo de Tipo de eventualidad");
            entity.Property(e => e.TipoSitioId).HasComment("Llave foránea que hace relación al catálogo tipo de sitio");
            entity.Property(e => e.TipoSupervisionId).HasComment("Llave foránea que hace referencia al catálogo de Tipo de supervisión");

            entity.HasOne(d => d.BrigadaMuestreo).WithMany(p => p.AvisoRealizacion)
                .HasForeignKey(d => d.BrigadaMuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AvisoRealizacion_BrigadaMuestreo");

            entity.HasOne(d => d.Laboratorio).WithMany(p => p.AvisoRealizacion)
                .HasForeignKey(d => d.LaboratorioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AvisoRealizacion_TipoSupervision");

            entity.HasOne(d => d.TipoSitio).WithMany(p => p.AvisoRealizacion)
                .HasForeignKey(d => d.TipoSitioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AvisoRealizacion_TipoSitio");

            entity.HasOne(d => d.TipoSupervision).WithMany(p => p.AvisoRealizacion)
                .HasForeignKey(d => d.TipoSupervisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AvisoRealizacion_TipoSupervision1");
        });

        modelBuilder.Entity<BrigadaMuestreo>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador de tabla  BrigadaMuestreo");
            entity.Property(e => e.Activo).HasComment("Estatus de activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasComment("Descripción de la Brigada");
            entity.Property(e => e.LaboratorioId).HasComment("Llave foranea que hace relación al catalogo de Laboratorios para indicar que laboratorio tiene la brigada");
            entity.Property(e => e.Lider)
                .HasMaxLength(100)
                .HasComment("Campo que describe el nombre del lider");
            entity.Property(e => e.Placas)
                .HasMaxLength(10)
                .HasComment("Campo que describe las placas");

            entity.HasOne(d => d.Laboratorio).WithMany(p => p.BrigadaMuestreo)
                .HasForeignKey(d => d.LaboratorioId)
                .HasConstraintName("FK_BrigadaMuestreo_Laboratorios");
        });        

        modelBuilder.Entity<ClasificacionCriterio>(entity =>
        {
            entity.ToTable("ClasificacionCriterio", "cat");

            entity.Property(e => e.Id).HasComment("Identificador principal de catálogo de clasificaciones de criterio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasComment("Descripción de la clasificación del criterio");
        });

        modelBuilder.Entity<ClasificacionRegla>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CloasificacionRegla");

            entity.Property(e => e.Id).HasComment("Identificador de catalogo ClasificacionRegla");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .HasComment("Descripción de la clasificación de regla");
        });

        modelBuilder.Entity<CopiaInformeMensualSupervision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CopiaReporteInformeMensual");

            entity.Property(e => e.Id).HasComment("Identificador principal de tabla de CopiaReporteInformeMensual");
            entity.Property(e => e.InformeMensualSupervisionId).HasComment("Llave foránea que hace relación a la tabla de InformeMensualSupervision");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasComment("Campo que describe el nombre de la persona que se enviara copia");
            entity.Property(e => e.Puesto)
                .HasMaxLength(100)
                .HasComment("Campo que describe el puesto del usuario que se enviara copia");

            entity.HasOne(d => d.InformeMensualSupervision).WithMany(p => p.CopiaInformeMensualSupervision)
                .HasForeignKey(d => d.InformeMensualSupervisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CopiaReporteInformeMensual_ReporteInformeMensual");
        });

        modelBuilder.Entity<CriteriosSupervisionMuestreo>(entity =>
        {
            entity.ToTable("CriteriosSupervisionMuestreo", "cat");

            entity.Property(e => e.Id).HasComment("Identificador principal de catalogo de criterios de supervisión de muestreo");
            entity.Property(e => e.ClasificacionCriterioId).HasComment("Llave foránea que hace relación al catálogo de ClasificacionCriterios");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .HasComment("Descripción del criterio de muestreo");
            entity.Property(e => e.EsExcepcionNoAplica).HasComment("Campo que describe si puede ser un criterio con observación \"no aplica\"");
            entity.Property(e => e.Obligatorio).HasComment("Campo que indica si el criterio es obligatorio");
            entity.Property(e => e.Valor)
                .HasComment("Campo que indica el valor del criterio")
                .HasColumnType("decimal(7, 1)");

            entity.HasOne(d => d.ClasificacionCriterio).WithMany(p => p.CriteriosSupervisionMuestreo)
                .HasForeignKey(d => d.ClasificacionCriterioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CriteriosSupervisionMuestreo_ClasificacionCriterio");
        });

        modelBuilder.Entity<CuencaDireccionesLocales>(entity =>
        {
            entity.HasIndex(e => e.DlocalId, "IX_CuencaDireccionesLocales_DLocalId");

            entity.HasIndex(e => e.OcuencaId, "IX_CuencaDireccionesLocales_OCuencaId");

            entity.Property(e => e.Id).HasComment("Identificador principal de catálogo CuencaDireccionesLocales donde describe la relación entre las Cuencas y Direcciones Locales");
            entity.Property(e => e.Activo).HasComment("Campo que indica si se encuentra activo el registro");
            entity.Property(e => e.DlocalId)
                .HasComment("Llave foránea que hace referencia al catálogo de Direccones Locales")
                .HasColumnName("DLocalId");
            entity.Property(e => e.OcuencaId)
                .HasComment("Llave foránea que hace referencia al catálogo de Organismos de Cuenca")
                .HasColumnName("OCuencaId");

            entity.HasOne(d => d.Dlocal).WithMany(p => p.CuencaDireccionesLocales)
                .HasForeignKey(d => d.DlocalId)
                .HasConstraintName("FK_CuencaDireccionesLocales_DireccionLocal");

            entity.HasOne(d => d.Ocuenca).WithMany(p => p.CuencaDireccionesLocales)
                .HasForeignKey(d => d.OcuencaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CuencaDireccionesLocales_OrganismoCuenca");
        });

        modelBuilder.Entity<CuerpoAgua>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador prinicpal del catálogo CuerpoAgua");
            entity.Property(e => e.Activo).HasComment("Estatus de  Cuerpo Agua");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .HasComment("Descripción Cuerpo Agua");
        });

        modelBuilder.Entity<CuerpoTipoSubtipoAgua>(entity =>
        {
            entity.HasIndex(e => e.CuerpoAguaId, "IX_CuerpoTipoSubtipoAgua_CuerpoAguaId");

            entity.HasIndex(e => e.SubtipoCuerpoAguaId, "IX_CuerpoTipoSubtipoAgua_SubtipoCuerpoAguaId");

            entity.HasIndex(e => e.TipoCuerpoAguaId, "IX_CuerpoTipoSubtipoAgua_TipoCuerpoAguaId");

            entity.Property(e => e.Id).HasComment("Identificador de Cuerpo Tipo Subtipo Agua");
            entity.Property(e => e.CuerpoAguaId).HasComment("Llave foránea que hace referencia al catálogo de CuerpoAgua");
            entity.Property(e => e.SubtipoCuerpoAguaId).HasComment("Llave foránea que hace referencia al catálogo de SubtipoCuerpoAgua");
            entity.Property(e => e.TipoCuerpoAguaId).HasComment("Llave foránea que hace referencia al catálogo de TipoCuerpoAgua");

            entity.HasOne(d => d.CuerpoAgua).WithMany(p => p.CuerpoTipoSubtipoAgua)
                .HasForeignKey(d => d.CuerpoAguaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CuerpoTipoSubtipoAgua_CuerpoAgua");

            entity.HasOne(d => d.SubtipoCuerpoAgua).WithMany(p => p.CuerpoTipoSubtipoAgua)
                .HasForeignKey(d => d.SubtipoCuerpoAguaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CuerpoTipoSubtipoAgua_SubtipoCuerpoAgua");

            entity.HasOne(d => d.TipoCuerpoAgua).WithMany(p => p.CuerpoTipoSubtipoAgua)
                .HasForeignKey(d => d.TipoCuerpoAguaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CuerpoTipoSubtipoAgua_TipoCuerpoAgua");
        });
        
        modelBuilder.Entity<DestinatariosAtencion>(entity =>
        {
            entity.ToTable("DestinatariosAtencion", "cat");

            entity.Property(e => e.Id).HasComment("Identificador principal del catalogo DestinatariosAtencion");
            entity.Property(e => e.Activo).HasComment("Campo que describe si se encuentra activo el destinatario");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasComment("Campo que describe el destinatario de atención");
        });

        modelBuilder.Entity<DireccionLocal>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de tabla Dirección Local");
            entity.Property(e => e.Clave)
                .HasMaxLength(10)
                .HasComment("Campo que indica la clave de la Dirección Local");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Descripción de Dirección Local");
        });

        modelBuilder.Entity<Directorio>(entity =>
        {
            entity.ToTable("Directorio", "cat");

            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla Directorio");
            entity.Property(e => e.Activo).HasComment("Campo que describe si se encuentra activo el personal");
            entity.Property(e => e.DireccionLocalId).HasComment("Llave foránea que hace relación al catálogo de Direcciones locales");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasComment("Campo que describe el nombre del personal");
            entity.Property(e => e.OrganismoCuencaId).HasComment("Llave foránea que hace relación al catálogo de Organismos de cuenca");
            entity.Property(e => e.ProgramaAnioId).HasComment("Llave foránea que hace relación al catálogo de ProgramaAnio");
            entity.Property(e => e.PuestoId).HasComment("Llave foránea que hace relación al catálogo de Puestos");
            entity.Property(e => e.Sexo)
                .HasMaxLength(2)
                .HasComment("Campo que describe el sexo del personal");

            entity.HasOne(d => d.DireccionLocal).WithMany(p => p.Directorio)
                .HasForeignKey(d => d.DireccionLocalId)
                .HasConstraintName("FK_Directorio_DireccionLocal");

            entity.HasOne(d => d.OrganismoCuenca).WithMany(p => p.Directorio)
                .HasForeignKey(d => d.OrganismoCuencaId)
                .HasConstraintName("FK_Directorio_OrganismoCuenca");

            entity.HasOne(d => d.ProgramaAnio).WithMany(p => p.Directorio)
                .HasForeignKey(d => d.ProgramaAnioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directorio_ProgramaAnio");

            entity.HasOne(d => d.Puesto).WithMany(p => p.Directorio)
                .HasForeignKey(d => d.PuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Directorio_Puestos");
        });        

        modelBuilder.Entity<Emergencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_MuestreoEmergencia");

            entity.ToTable("Emergencia", "cat");

            entity.Property(e => e.Id).HasComment("Identificador de la tabla de Emergencias");
            entity.Property(e => e.Anio).HasComment("Campo que indica el año de la emergencia");
            entity.Property(e => e.ClaveMunicipio)
                .HasMaxLength(50)
                .HasComment("Campo que indica la clave del municipio");
            entity.Property(e => e.ClaveSitio)
                .HasMaxLength(200)
                .HasComment("Campo que indica la clave sitio");
            entity.Property(e => e.Cuenca)
                .HasMaxLength(100)
                .HasComment("Campo que indica el nombre de la cuenca");
            entity.Property(e => e.CuerpoAgua)
                .HasMaxLength(100)
                .HasComment("Campo que indica el cuerpo de agua ");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasComment("Campo que indica el estado de la emergencia");
            entity.Property(e => e.FechaRealizacion)
                .HasComment("Campo que indica la fecha de realización")
                .HasColumnType("datetime");
            entity.Property(e => e.Latitud)
                .HasMaxLength(50)
                .HasComment("Campo que indica la latitud");
            entity.Property(e => e.Longitud)
                .HasMaxLength(50)
                .HasComment("Campo que indica la longitud");
            entity.Property(e => e.Municipio)
                .HasMaxLength(100)
                .HasComment("Campo que indica el municipio");
            entity.Property(e => e.NombreEmergencia)
                .HasMaxLength(200)
                .HasComment("Campo que indica el nombre de la emergencia");
            entity.Property(e => e.NombreSitio)
                .HasMaxLength(200)
                .HasComment("Campo que indica el nombre del sitio");
            entity.Property(e => e.OrganismoCuenca)
                .HasMaxLength(100)
                .HasComment("Campo que indica el organismo de cuenca");
            entity.Property(e => e.SubTipoCuerpoAgua)
                .HasMaxLength(100)
                .HasComment("Campo que indica el subtipo del cuerpo de agua");
            entity.Property(e => e.TipoCuerpoAgua)
                .HasMaxLength(100)
                .HasComment("Campo que indica el tipo de cuerpo de agua");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador de Estado");
            entity.Property(e => e.Abreviatura)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Abreviatura del Estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Nombre de Estado");
        });

        modelBuilder.Entity<EstatusMuestreo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EstatusMuestreos");

            entity.ToTable("EstatusMuestreo", "cat");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .HasComment("Campo que indica en que etapa se encuentra el muestreo respecto al flujo");
            entity.Property(e => e.Etiqueta)
                .HasMaxLength(300)
                .HasComment("Campo que india el nombre del estatus como lo solicita el usuario que se muestre");
        });

        modelBuilder.Entity<EstatusMuestreo1>(entity =>
        {
            entity.ToTable("EstatusMuestreo");

            entity.Property(e => e.Id).HasComment("Identificador de Estatus Muestreo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasComment("Campo que describe el estatus del muestreo");
        });

        modelBuilder.Entity<EstatusOcdlSecaia>(entity =>
        {
            entity.ToTable("EstatusOcdlSecaia", "cat");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .HasComment("Campo que indica en que estatus se encuntra la revision de OC/DL o SECAIA");
            entity.Property(e => e.Etiqueta)
                .HasMaxLength(300)
                .HasComment("Campo que india el nombre del estatus como lo solicita el usuario describiendo en que etapa se encuntra la revsión de OC/DL o SECAIA");
        });

        modelBuilder.Entity<EstatusResultado>(entity =>
        {
            entity.ToTable("EstatusResultado", "cat");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .HasComment("Campo que indica el estatus del resultado del muestreo");
            entity.Property(e => e.Etiqueta)
                .HasMaxLength(300)
                .HasComment("Campo que indica el estatus del resultado del muestreo pero nombrada como lo requiere ver el usuario");
        });

        modelBuilder.Entity<EvidenciaMuestreo>(entity =>
        {
            entity.HasIndex(e => e.MuestreoId, "IX_EvidenciaMuestreo_MuestreoId");

            entity.HasIndex(e => e.TipoEvidenciaMuestreoId, "IX_EvidenciaMuestreo_TipoEvidenciaMuestreoId");

            entity.Property(e => e.Id).HasComment("Identificador principal de EvidenciaMuestreo");
            entity.Property(e => e.Altitud)
                .HasComment("Campo que describe la altitud de la evidencia")
                .HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Apertura)
                .HasMaxLength(50)
                .HasComment("Campo que describe la apertura");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .HasComment("Campo que describe la dirección");
            entity.Property(e => e.DistanciaFocal)
                .HasMaxLength(50)
                .HasComment("Campo que describe la distancia focal");
            entity.Property(e => e.FechaCreacion)
                .HasComment("Campo que describe la fecha de creación de la evidencia")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaFin)
                .HasMaxLength(25)
                .HasComment("Campo que describe la fecha fin");
            entity.Property(e => e.FechaInicio)
                .HasMaxLength(25)
                .HasComment("Campo que describe la fecha de inicio");
            entity.Property(e => e.Flash)
                .HasMaxLength(50)
                .HasComment("Campo que describe el flash");
            entity.Property(e => e.HoraFin)
                .HasMaxLength(25)
                .HasComment("Campo que describe la hora fin");
            entity.Property(e => e.HoraInicio)
                .HasMaxLength(25)
                .HasComment("Campo que describe la hora inicio");
            entity.Property(e => e.Iso)
                .HasMaxLength(50)
                .HasComment("Campo  que describe el iso");
            entity.Property(e => e.Laboratorio)
                .HasMaxLength(25)
                .HasComment("Campo que india el laboratorio");
            entity.Property(e => e.Latitud)
                .HasComment("Campo que describe la latitud de la evidencia")
                .HasColumnType("decimal(12, 9)");
            entity.Property(e => e.Longitud)
                .HasComment("Campo que describe la longitud de la evidencia")
                .HasColumnType("decimal(12, 9)");
            entity.Property(e => e.MarcaCamara)
                .HasMaxLength(50)
                .HasComment("Campo que describe la marca de la cámara de la evidencia");
            entity.Property(e => e.ModeloCamara)
                .HasMaxLength(50)
                .HasComment("Campo que describe el Modelo de la cámara de la toma de la evidencia");
            entity.Property(e => e.MuestreoId).HasComment("Llave foránea que hace referencia a la tabla de Muestreo");
            entity.Property(e => e.NombreArchivo)
                .IsUnicode(false)
                .HasComment("Campo que describe el nombre del archivo");
            entity.Property(e => e.Obturador)
                .HasMaxLength(50)
                .HasComment("Campo que describe el obturador");
            entity.Property(e => e.Placas)
                .HasMaxLength(25)
                .HasComment("Campo que indica las placas");
            entity.Property(e => e.Tamano)
                .HasMaxLength(50)
                .HasComment("Campo que describe el tamaño de la evidencia");
            entity.Property(e => e.TipoEvidenciaMuestreoId).HasComment("Llave foránea que hace referencia al catálogo de TipoEvidenciaMuestreo");

            entity.HasOne(d => d.Muestreo).WithMany(p => p.EvidenciaMuestreo)
                .HasForeignKey(d => d.MuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EvidenciaMuestreo_Muestreo");

            entity.HasOne(d => d.TipoEvidenciaMuestreo).WithMany(p => p.EvidenciaMuestreo)
                .HasForeignKey(d => d.TipoEvidenciaMuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EvidenciaMuestreo_TipoEvidenciaMuestreo");
        });

        modelBuilder.Entity<EvidenciaReplica>(entity =>
        {
            entity.HasIndex(e => e.ResultadoMuestreoId, "IX_EvidenciaReplica_ResultadoMuestreoId");

            entity.Property(e => e.Id).HasComment("Identificador principal de tabla EvidenciaReplica");
            entity.Property(e => e.Archivo).HasComment("Campo que describe el archivo");
            entity.Property(e => e.ClaveUnica)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Campo que indica la clave única");
            entity.Property(e => e.NombreArchivo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Campo de nombre de archivo");
            entity.Property(e => e.ResultadoMuestreoId).HasComment("Llave foránea que hace referencia a la tabla de  ResultadoMuestreo");

            entity.HasOne(d => d.ResultadoMuestreo).WithMany(p => p.EvidenciaReplica)
                .HasForeignKey(d => d.ResultadoMuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EvidenciaReplica_ResultadoMuestreo");
        });

        modelBuilder.Entity<EvidenciaSupervisionMuestreo>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de tabla para guardado de evidencias de supervisión ");
            entity.Property(e => e.NombreArchivo)
                .IsUnicode(false)
                .HasComment("Campo que describe el nombre del archivo");
            entity.Property(e => e.SupervisionMuestreoId).HasComment("Llave foránea que hace referencia a la tabla de SupervisionMuestreo");
            entity.Property(e => e.TipoEvidenciaId).HasComment("Llave foránea que hace referencia al catálogo de tipo de evidencia");

            entity.HasOne(d => d.SupervisionMuestreo).WithMany(p => p.EvidenciaSupervisionMuestreo)
                .HasForeignKey(d => d.SupervisionMuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EvidenciaSupervisionMuestreo_SupervisionMuestreo");
        });

        modelBuilder.Entity<EvidenciasReplicasResultadoReglasValidacion>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla EvidenciasReplicasResultadoReglasValidacion");
            entity.Property(e => e.NombreArchivo)
                .IsUnicode(false)
                .HasComment("Campo que indica el nombre del archivo de la evidencia");
            entity.Property(e => e.ReplicasResultadoReglasValidacionId).HasComment("Llave foranea que hace referencia a la tabla de ReplicasResultadoReglasValidacion");

            entity.HasOne(d => d.ReplicasResultadoReglasValidacion).WithMany(p => p.EvidenciasReplicasResultadoReglasValidacion)
                .HasForeignKey(d => d.ReplicasResultadoReglasValidacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EvidenciasReplicasResultadoReglasValidacion_ReplicasResultadosReglasValidacion");
        });

        modelBuilder.Entity<FormaReporteEspecifica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FormaReporte");

            entity.Property(e => e.Id).HasComment("Identificador del catalogo de FormaReporteEspecifica");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(60)
                .HasComment("Descripción de la forma reporte especifica");
            entity.Property(e => e.ParametroId).HasComment("Llave foránea haciendo referencia al catalogo ParametroGrupo");

            entity.HasOne(d => d.Parametro).WithMany(p => p.FormaReporteEspecifica)
                .HasForeignKey(d => d.ParametroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FormaReporteEspecifica_ParametrosGrupo");
        });

        modelBuilder.Entity<GrupoParametro>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal del catalogo que indica el grupo al que pertenece el parámetro");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(15)
                .HasComment("Campo que describe el grupo del parámetro");
        });

        modelBuilder.Entity<HistorialSustitucionEmergencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_HistorialSustitucionEmergencia");

            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla HistorialSustitucionEmergencia");
            entity.Property(e => e.Anio).HasComment("Campo que describe el año");
            entity.Property(e => e.Fecha)
                .HasComment("Campo que describe la fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.MuestreoEmergenciaId).HasComment("Llave foránea que hace referencia a la tabla de MuestreoEmergencia");
            entity.Property(e => e.UsuarioId).HasComment("Llave foránea que hace referencia a la tabla de Usuario indicando el usuario que realizo la sustitución");

            entity.HasOne(d => d.MuestreoEmergencia).WithMany(p => p.HistorialSustitucionEmergencia)
                .HasForeignKey(d => d.MuestreoEmergenciaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MuestreoEmergencia");

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistorialSustitucionEmergencia)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario");
        });

        modelBuilder.Entity<HistorialSustitucionLimites>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3214EC07E25AC404");

            entity.Property(e => e.Id).HasComment("Identificador principal del catalogo del historial de limites");
            entity.Property(e => e.Fecha)
                .HasComment("Campo que indica la fecha en la que se esta realizando la sustitución del limite")
                .HasColumnType("datetime");
            entity.Property(e => e.MuestreoId).HasComment("Llave foránea que hace relación a la tabla de Muestreo");
            entity.Property(e => e.TipoSustitucionId).HasComment("Llave foránea que hace relación al catálogo de TipoSustitucion");
            entity.Property(e => e.UsuarioId).HasComment("Llave foránea que hace relación a la tabla de Usuario indicando el usuario que esta realizando la sustitución del limite");

            entity.HasOne(d => d.Muestreo).WithMany(p => p.HistorialSustitucionLimites)
                .HasForeignKey(d => d.MuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistorialSustitucion_Muestreo");

            entity.HasOne(d => d.TipoSustitucion).WithMany(p => p.HistorialSustitucionLimites)
                .HasForeignKey(d => d.TipoSustitucionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistorialSustitucion_TipoSustitucion");

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistorialSustitucionLimites)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistorialSustitucion_Usuario");
        });

        modelBuilder.Entity<InformeMensualSupervision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ReporteInformeMensual");

            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla InformeMensualSupervisión");
            entity.Property(e => e.Anio).HasComment("Campo que indica el año al que pertenece el informe");
            entity.Property(e => e.DirectorioFirmaId).HasComment("Llave foránea que hace relación al catálogo de Directorio para describir el director o responsable de calidad del organismo de cuenca o dirección local");
            entity.Property(e => e.Fecha)
                .HasComment("Campo que describe la fecha que se mostrara en el reporte")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasComment("Campo que describe la fecha en la que se capturo el reporte")
                .HasColumnType("datetime");
            entity.Property(e => e.Iniciales)
                .HasMaxLength(200)
                .HasComment("Campo que describe las iniciales de las personas involucradas separadas por una \"/\"");
            entity.Property(e => e.Lugar)
                .HasMaxLength(200)
                .HasComment("Campo que describe el lugar del informe mensual");
            entity.Property(e => e.Memorando)
                .HasMaxLength(50)
                .HasComment("Campo que describe el número de memorando de la plantilla");
            entity.Property(e => e.MesId).HasComment("Llave foránea que hace relación al catálogo de Mes el cual describe el mes que se reporta el informe mensual");
            entity.Property(e => e.UsuarioRegistroId).HasComment("Llave foránea que hace relación a la tabla de Usuario registrando el usuario que creo el reporte");

            entity.HasOne(d => d.DirectorioFirma).WithMany(p => p.InformeMensualSupervision)
                .HasForeignKey(d => d.DirectorioFirmaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReporteInformeMensual_Directorio");

            entity.HasOne(d => d.Mes).WithMany(p => p.InformeMensualSupervision)
                .HasForeignKey(d => d.MesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReporteInformeMensual_Mes");

            entity.HasOne(d => d.UsuarioRegistro).WithMany(p => p.InformeMensualSupervision)
                .HasForeignKey(d => d.UsuarioRegistroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReporteInformeMensual_Usuario");
        });

        modelBuilder.Entity<IntervalosPuntajeSupervision>(entity =>
        {
            entity.ToTable("IntervalosPuntajeSupervision", "cat");

            entity.Property(e => e.Id).HasComment("Identificador principal del catalogo de IntervalosPuntajeSupervision");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(7)
                .HasComment("Campo que describe el intervalo");
        });

        modelBuilder.Entity<Laboratorios>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador de catálogo de Laboratorios");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasComment("Campo que describe el laboratorio");
            entity.Property(e => e.Nomenclatura)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Campo que describe la nomenclatura del laboratorio");
        });

        modelBuilder.Entity<LimiteParametroLaboratorio>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla LimiteParametrosLaboratorio");
            entity.Property(e => e.Activo).HasComment("Campo que indica si es activo, es una bandera del histórico");
            entity.Property(e => e.AnioId).HasComment("Llave foránea que hace relación al catálogo ProgramaAnio indicando el año al que pertenece el limite de dicho parámetro");
            entity.Property(e => e.LaboratorioId).HasComment("Llave foránea que hace relación al catálogo de Laboratorios indicando el laboratorio que debería de revisar dicho parámetro");
            entity.Property(e => e.LaboratorioMuestreoId).HasComment("Llave foránea que hace relación al catálogo de Laboratorios indicando el laboratorio que realiza el muestreo");
            entity.Property(e => e.LaboratorioSubrogaId).HasComment("Llave foránea que hace relación al catálogo de Laboratorios indicando el Laboratorio subrogado");
            entity.Property(e => e.Ldm)
                .HasMaxLength(30)
                .HasComment("Campo que indica el limite de LDM")
                .HasColumnName("LDM");
            entity.Property(e => e.LdmaCumplir)
                .HasMaxLength(30)
                .HasComment("Campo que indica el LDMA a cumplir")
                .HasColumnName("LDMaCumplir");
            entity.Property(e => e.LoMuestra).HasComment("Campo que indica si muestra");
            entity.Property(e => e.LoSubrogaId).HasComment("Llave foránea que hace relación al catálogo de AccionLaboratorio indicando si lo subroga");
            entity.Property(e => e.Lpc)
                .HasMaxLength(30)
                .HasComment("Campo que indica el limite del LPC")
                .HasColumnName("LPC");
            entity.Property(e => e.LpcaCumplir)
                .HasMaxLength(30)
                .HasComment("Campo que indica el LPC a cumplir")
                .HasColumnName("LPCaCumplir");
            entity.Property(e => e.MetodoAnalitico)
                .HasMaxLength(250)
                .HasComment("Campo que indica el método analítico");
            entity.Property(e => e.ParametroId).HasComment("Llave foránea que hace relación al catálogo de Parámetros indicando el parámetro");
            entity.Property(e => e.PeriodoId).HasComment("Llave foranea que hace relación al catalogo de Mes donde describe el periodo ");
            entity.Property(e => e.RealizaLaboratorioMuestreoId).HasComment("Llave foránea que hace relación al catálogo de AccionLaboratorio");

            entity.HasOne(d => d.Anio).WithMany(p => p.LimiteParametroLaboratorio)
                .HasForeignKey(d => d.AnioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LimiteParametroLaboratorio_ProgramaAnio");

            entity.HasOne(d => d.Laboratorio).WithMany(p => p.LimiteParametroLaboratorioLaboratorio)
                .HasForeignKey(d => d.LaboratorioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LimiteParametroLaboratorio_Laboratorios");

            entity.HasOne(d => d.LaboratorioMuestreo).WithMany(p => p.LimiteParametroLaboratorioLaboratorioMuestreo)
                .HasForeignKey(d => d.LaboratorioMuestreoId)
                .HasConstraintName("FK_LimiteParametroLaboratorio_LaboratorioMuestreo");

            entity.HasOne(d => d.LaboratorioSubroga).WithMany(p => p.LimiteParametroLaboratorioLaboratorioSubroga)
                .HasForeignKey(d => d.LaboratorioSubrogaId)
                .HasConstraintName("FK_LimiteParametroLaboratorio_LaboratorioSubroga");

            entity.HasOne(d => d.LoSubroga).WithMany(p => p.LimiteParametroLaboratorioLoSubroga)
                .HasForeignKey(d => d.LoSubrogaId)
                .HasConstraintName("FK_LimiteParametroLaboratorio_AccionSubrogado");

            entity.HasOne(d => d.Parametro).WithMany(p => p.LimiteParametroLaboratorio)
                .HasForeignKey(d => d.ParametroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LimiteParametroLaboratorio_ParametrosGrupo");

            entity.HasOne(d => d.Periodo).WithMany(p => p.LimiteParametroLaboratorio)
                .HasForeignKey(d => d.PeriodoId)
                .HasConstraintName("FK_LimiteParametroLaboratorio_Mes");

            entity.HasOne(d => d.RealizaLaboratorioMuestreo).WithMany(p => p.LimiteParametroLaboratorioRealizaLaboratorioMuestreo)
                .HasForeignKey(d => d.RealizaLaboratorioMuestreoId)
                .HasConstraintName("FK_LimiteParametroLaboratorio_AccionSubrogado1");
        });

        modelBuilder.Entity<Localidad>(entity =>
        {
            entity.HasIndex(e => e.EstadoId, "IX_Localidad_EstadoId");

            entity.HasIndex(e => e.MunicipioId, "IX_Localidad_MunicipioId");

            entity.Property(e => e.Id).HasComment("Identificador prncipal del catálogo Localidad");
            entity.Property(e => e.EstadoId).HasComment("Llave foránea que hace referencia al catálogo de Estado indicand el estado de la localidad");
            entity.Property(e => e.MunicipioId).HasComment("Llave foránea que hace referencia al catálogo de Municipio indicando el municipio de la localidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Campo que describe la Localidad");

            entity.HasOne(d => d.Estado).WithMany(p => p.Localidad)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Localidad__Estad__571DF1D5");

            entity.HasOne(d => d.Municipio).WithMany(p => p.Localidad)
                .HasForeignKey(d => d.MunicipioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Localidad__Munic__5812160E");
        });

        modelBuilder.Entity<Mes>(entity =>
        {
            entity.ToTable("Mes", "cat");

            entity.Property(e => e.Id).HasComment("Identificador principal de tabla de la tabla Mes");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(10)
                .HasComment("Campo que describe el mes");
        });

        modelBuilder.Entity<Muestreadores>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Muestradores");

            entity.Property(e => e.Id).HasComment("Identificador principal de tabla de la tabla Muestradores");
            entity.Property(e => e.Activo).HasComment("Campo que describe si es activo el muestrador");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .HasComment("Campo que describe el apellido materno del muestrador");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .HasComment("Campo que describe el apellido paterno del muestrador");
            entity.Property(e => e.BrigadaId).HasComment("Llave foránea que hace relación al catálogo de BrigadaMuestreo");
            entity.Property(e => e.Iniciales)
                .HasMaxLength(5)
                .HasComment("Campo que describe las iniciales del usuario");
            entity.Property(e => e.LaboratorioId).HasComment("Llave foránea que hace relación al catálogo de Laboratorios");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasComment("Campo que describe el nombre del muestrador");
        });

        modelBuilder.Entity<Muestreo>(entity =>
        {
            entity.HasIndex(e => e.EstatusId, "IX_Muestreo_EstatusId");

            entity.HasIndex(e => e.EstatusOcdl, "IX_Muestreo_EstatusOCDL");

            entity.HasIndex(e => e.EstatusSecaia, "IX_Muestreo_EstatusSECAIA");

            entity.HasIndex(e => e.ProgramaMuestreoId, "IX_Muestreo_ProgramaMuestreoId");

            entity.HasIndex(e => e.TipoAprobacionId, "IX_Muestreo_TipoAprobacionId");

            entity.HasIndex(e => e.UsuarioRevisionOcdlid, "IX_Muestreo_UsuarioRevisionOCDLId");

            entity.HasIndex(e => e.UsuarioRevisionSecaiaid, "IX_Muestreo_UsuarioRevisionSECAIAId");

            entity.Property(e => e.Id).HasComment("Identificador  de Muestreo");
            entity.Property(e => e.AnioOperacion).HasComment("Campo que indica el año de operación");
            entity.Property(e => e.AutorizacionCondicionantes).HasComment("Autorizacion cuando no cumple con los resultados de condicionantes, esta autorizacion se realiza en la pantalla de módulo de reglas donde se autorizara en dado caso para aplicar las reglas ");
            entity.Property(e => e.AutorizacionFechaEntrega).HasComment("Campo que indica si se autorizo ya que la fecha de entrega no se cumplio");
            entity.Property(e => e.AutorizacionIncompleto).HasComment("Campo que indica si fue autorizado el muestreo estando incompletos los resultados del muestreo");
            entity.Property(e => e.EstatusId).HasComment("Llave foránea que hace referencia al catálogo de EstatusMuestreo, indicando el estatus del muestreo");
            entity.Property(e => e.EstatusOcdl)
                .HasComment("Llave foranea que hace referencia al catalogo de EstatusOcdlSecaia indicando el estatus de la revisión de OCDL")
                .HasColumnName("EstatusOCDL");
            entity.Property(e => e.EstatusSecaia)
                .HasComment("Llave foranea que hace referencia al catalogo de EstatusOcdlSecaia indicando el estatus de la revisión de SECAIA")
                .HasColumnName("EstatusSECAIA");
            entity.Property(e => e.FechaCarga)
                .HasComment("Campo que describe la fecha en la que se cargo el muesreo a traves del archivo ebaseca")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaCargaEvidencias)
                .HasComment("Campo que indica la fecha en la que se realizo la carga de las evidencias de muestreo")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaLimiteRevision)
                .HasComment("Campo que describe la fecha limite de revisión")
                .HasColumnType("date");
            entity.Property(e => e.FechaRealVisita)
                .HasComment("Campo que describe la fecha real de visita proviene de ebaseca")
                .HasColumnType("date");
            entity.Property(e => e.FechaRevisionOcdl)
                .HasComment("Campo que indca la fecha de revisión por OCDL")
                .HasColumnType("date")
                .HasColumnName("FechaRevisionOCDL");
            entity.Property(e => e.FechaRevisionSecaia)
                .HasComment("Campo que indca la fecha de revisión por SECAIA")
                .HasColumnType("date")
                .HasColumnName("FechaRevisionSECAIA");
            entity.Property(e => e.HoraFin).HasComment("Campo que describe la hora fin, proviene de ebseca");
            entity.Property(e => e.HoraInicio).HasComment("Campo que indica la hora de inicio proviene de ebaseca");
            entity.Property(e => e.NumeroCarga)
                .HasMaxLength(10)
                .HasComment("Campo que indica el número de carga al que pertenece el muestreo al ser cargado desde ebaseca");
            entity.Property(e => e.NumeroEntrega).HasComment("Campo que indica el numero de entrega, este es consecutivo cada vez que se envie un bloque de muestreos a liberación despues de haber sido aplicado las reglas y haber tenido una validación final en \"OK\"");
            entity.Property(e => e.ProgramaMuestreoId).HasComment("Llave foránea que hace referencia a la tabla tabla de ProgramaMuestreo");
            entity.Property(e => e.TipoAprobacionId).HasComment("Llave foránea que hace relación al catálogo de TipoAprobacion");
            entity.Property(e => e.UsuarioRevisionOcdlid)
                .HasComment("Llave foránea que hace relación a la tabla de Usuario indicando quien fue el que reviso a nivel OCDL")
                .HasColumnName("UsuarioRevisionOCDLId");
            entity.Property(e => e.UsuarioRevisionSecaiaid)
                .HasComment("Llave foránea que hace relación a la tabla de Usuario indicando quien fue el que reviso a nivel SECAIA")
                .HasColumnName("UsuarioRevisionSECAIAId");
            entity.Property(e => e.ValidacionEvidencias).HasComment("Campo que indica si se envia a la etapa de validación de evidencias");

            entity.HasOne(d => d.Estatus).WithMany(p => p.Muestreo)
                .HasForeignKey(d => d.EstatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Muestreo_EstatusMuestreo");

            entity.HasOne(d => d.EstatusOcdlNavigation).WithMany(p => p.MuestreoEstatusOcdlNavigation)
                .HasForeignKey(d => d.EstatusOcdl)
                .HasConstraintName("FK_Muestreo_EstatusOcdlSecaia");

            entity.HasOne(d => d.EstatusSecaiaNavigation).WithMany(p => p.MuestreoEstatusSecaiaNavigation)
                .HasForeignKey(d => d.EstatusSecaia)
                .HasConstraintName("FK_Muestreo_EstatusOcdlSecaia1");

            entity.HasOne(d => d.ProgramaMuestreo).WithMany(p => p.Muestreo)
                .HasForeignKey(d => d.ProgramaMuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Muestreo_ProgramaMuestreo");

            entity.HasOne(d => d.TipoAprobacion).WithMany(p => p.Muestreo)
                .HasForeignKey(d => d.TipoAprobacionId)
                .HasConstraintName("FK_Muestreo_TipoAprobacion");

            entity.HasOne(d => d.TipoCarga).WithMany(p => p.Muestreo)
                .HasForeignKey(d => d.TipoCargaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Muestreo_TipoCarga");

            entity.HasOne(d => d.UsuarioRevisionOcdl).WithMany(p => p.MuestreoUsuarioRevisionOcdl)
                .HasForeignKey(d => d.UsuarioRevisionOcdlid)
                .HasConstraintName("FK_Muestreo_Usuario");

            entity.HasOne(d => d.UsuarioRevisionSecaia).WithMany(p => p.MuestreoUsuarioRevisionSecaia)
                .HasForeignKey(d => d.UsuarioRevisionSecaiaid)
                .HasConstraintName("FK_Muestreo_Usuario1");
        });

        modelBuilder.Entity<MuestreoEmergencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Muestreo__3214EC078DE004E5");

            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla de MuestreoEmergencia");
            entity.Property(e => e.ClaveUnica)
                .HasMaxLength(150)
                .HasComment("Campo que indica la clave única");
            entity.Property(e => e.FechaProgramada)
                .HasComment("Campo que indica la fecha programada del muestreo de emergencia")
                .HasColumnType("date");
            entity.Property(e => e.FechaRealVisita)
                .HasComment("Campo que indica la fecha real visita del muestreo de emergencia")
                .HasColumnType("date");
            entity.Property(e => e.HoraMuestreo)
                .HasMaxLength(20)
                .HasComment("Campo que indica la hora del muestreo de emergencia");
            entity.Property(e => e.IdLaboratorio)
                .HasMaxLength(10)
                .HasComment("Campo que indica el IdLaboratorio, dato de laboratorio");
            entity.Property(e => e.LaboratorioRealizoMuestreo)
                .HasMaxLength(100)
                .HasComment("Campo que indica el laboratorio que realizo el muestreo de emergencia");
            entity.Property(e => e.LaboratorioSubrogado)
                .HasMaxLength(100)
                .HasComment("Campo que indica el laboratorio subrogado del muestreo de emergencia");
            entity.Property(e => e.NombreEmergencia)
                .HasMaxLength(250)
                .HasComment("Campo que indica el nombre de la emergencia");
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .HasComment("Campo que indica el número de muestreo de emergencia");
            entity.Property(e => e.ParametroId).HasComment("Llave foránea que hace relación al catálogo de Parámetros indicando el parámetro del muestreo de emergencia");
            entity.Property(e => e.Resultado)
                .HasMaxLength(50)
                .HasComment("Campo que indica el resultado");
            entity.Property(e => e.ResultadoSustituidoPorLimite)
                .HasMaxLength(50)
                .HasComment("Campo que indica el resultado sustituido por el limite");
            entity.Property(e => e.Sitio)
                .HasMaxLength(150)
                .HasComment("Campo que indica el sitio del muestreo de emergencia");
            entity.Property(e => e.SubtipoCuerpoAgua)
                .HasMaxLength(100)
                .HasComment("Campo que indica el subtipo de cuerpo de agua del muestreo de emergencia");
            entity.Property(e => e.TipoCuerpoAgua)
                .HasMaxLength(100)
                .HasComment("Campo que indica el tipo de cuerpo de agua del muestreo de emergencia");

            entity.HasOne(d => d.Parametro).WithMany(p => p.MuestreoEmergencia)
                .HasForeignKey(d => d.ParametroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MuestreoEmergencia_ParametroGrupo");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasIndex(e => e.EstadoId, "IX_Municipio_EstadoId");

            entity.Property(e => e.Id).HasComment("Identificador del catálogo de Municipio");
            entity.Property(e => e.EstadoId).HasComment("Llave foránea que hace referencia al catálogo de Estado indicando a que estado pertenece el municipio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Campo que describe el municipio");

            entity.HasOne(d => d.Estado).WithMany(p => p.Municipio)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Municipio__Estad__4E88ABD4");
        });

        modelBuilder.Entity<Observaciones>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal del catálogo de Observaciones");
            entity.Property(e => e.Activo).HasComment("Campo que indica si se encuentra activa la observación");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasComment("Campo que describe la observación");
        });

        modelBuilder.Entity<OrganismoCuenca>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identifiacador principal del catálogo OrganismoCuenca");
            entity.Property(e => e.Clave)
                .HasMaxLength(10)
                .HasComment("Campo que describe la clave del Organismo de Cuenca");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Campo que describe el Organismo de la Cuenca");
            entity.Property(e => e.Direccion)
                .HasMaxLength(300)
                .HasDefaultValueSql("('')")
                .HasComment("Campo que describe la dirección del Organismo de Cuenca");
            entity.Property(e => e.Telefono)
                .HasMaxLength(14)
                .HasDefaultValueSql("('')")
                .HasComment("Campoq ue indica el teléfono del Organismo de Cuenca");
        });

        modelBuilder.Entity<Pagina>(entity =>
        {
            entity.HasIndex(e => e.IdPaginaPadre, "IX_Pagina_IdPaginaPadre");

            entity.Property(e => e.Id).HasComment("Identificador principal del catálogo Pagina");
            entity.Property(e => e.Activo).HasComment("Campo qe indica si se encuentra activa la página");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Campo que indica el nombre de la página, nombre que se mostrara en el menú");
            entity.Property(e => e.IdPaginaPadre).HasComment("Llave foránea que hace referencia a esta misma tabla indicando la página padre");
            entity.Property(e => e.Orden).HasComment("Campo que indica el orden de la página");
            entity.Property(e => e.Url)
                .IsUnicode(false)
                .HasComment("Campoq ue indica la url de la página");

            entity.HasOne(d => d.IdPaginaPadreNavigation).WithMany(p => p.InverseIdPaginaPadreNavigation)
                .HasForeignKey(d => d.IdPaginaPadre)
                .HasConstraintName("FK_Pagina_Pagina");
        });

        modelBuilder.Entity<ParametrosCostos>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal del catálogo ParametrosCostos");
            entity.Property(e => e.ParametroId).HasComment("Llave foránea que hace referencia al catálogo de ParametrosGrupo indicando el parámetro");
            entity.Property(e => e.Precio)
                .HasComment("Campo que describe el precio del parámetro")
                .HasColumnType("decimal(6, 2)");
            entity.Property(e => e.ProgramaAnioId).HasComment("Llave foránea que hace referencia la catálogo de ProgramaAnio indicando el año al que pertenece el costo de este parámetro.");

            entity.HasOne(d => d.Parametro).WithMany(p => p.ParametrosCostos)
                .HasForeignKey(d => d.ParametroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParametrosCostos_ParametrosGrupo");

            entity.HasOne(d => d.ProgramaAnio).WithMany(p => p.ParametrosCostos)
                .HasForeignKey(d => d.ProgramaAnioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParametrosCostos_ProgramaAnio");
        });

        modelBuilder.Entity<ParametrosGrupo>(entity =>
        {
            entity.HasIndex(e => e.IdSubgrupo, "IX_ParametrosGrupo_IdSubgrupo");

            entity.HasIndex(e => e.IdUnidadMedida, "IX_ParametrosGrupo_IdUnidadMedida");

            entity.Property(e => e.Id).HasComment("Identificador principal de catálogo ParametrosGrupo\r\n");
            entity.Property(e => e.ClaveParametro)
                .HasMaxLength(30)
                .HasComment("Campo que describe la clave de parámetro");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasComment("Campo que describe el nombre del parámetro");
            entity.Property(e => e.EsLdm)
                .HasComment("Campo que indica si se tomara el limite de LDM")
                .HasColumnName("EsLDM");
            entity.Property(e => e.GrupoParametroId).HasComment("Llave foránea que hace referencia al catálogo de GrupoParametro");
            entity.Property(e => e.IdSubgrupo).HasComment("Llave foránea que hace referencia al catálogo de SubgrupoAnalitico");
            entity.Property(e => e.IdUnidadMedida).HasComment("Llave foránea que hace referencia al catálogo de UnidadMedida");
            entity.Property(e => e.Orden).HasComment("Campo que indica el orden del parámetro, en este orden se mostrara en la tabla en forma horizontal");
            entity.Property(e => e.ParametroPadreId).HasComment("Llave foránea que hace relación a la misma tabla indicando si este parámetro pertenece a un parámetro padre para la relación de suma de parámetros ");

            entity.HasOne(d => d.GrupoParametro).WithMany(p => p.ParametrosGrupo)
                .HasForeignKey(d => d.GrupoParametroId)
                .HasConstraintName("FK_ParametrosGrupo_GrupoParametro");

            entity.HasOne(d => d.IdSubgrupoNavigation).WithMany(p => p.ParametrosGrupo)
                .HasForeignKey(d => d.IdSubgrupo)
                .HasConstraintName("FK_ParametrosGrupo_SubtipoCuerpoAgua");

            entity.HasOne(d => d.IdUnidadMedidaNavigation).WithMany(p => p.ParametrosGrupo)
                .HasForeignKey(d => d.IdUnidadMedida)
                .HasConstraintName("FK_ParametrosGrupo_UnidadMedida");

            entity.HasOne(d => d.ParametroPadre).WithMany(p => p.InverseParametroPadre)
                .HasForeignKey(d => d.ParametroPadreId)
                .HasConstraintName("FK_ParametrosGrupo_ParametrosGrupo");
        });

        modelBuilder.Entity<ParametrosReglasNoRelacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ParametrosReglasNORelacion");

            entity.Property(e => e.Id).HasComment("Identificador de llave primaria de la tabla de ParametrosReglasNORelacion");
            entity.Property(e => e.ParametroId).HasComment("Llave foránea que hace referencia al catálogo de ParametrosGrupo indicando el parametro que tiene regla de no relación\r\n");

            entity.HasOne(d => d.Parametro).WithMany(p => p.ParametrosReglasNoRelacion)
                .HasForeignKey(d => d.ParametroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParametrosReglasNORelacion_ParametrosGrupo");
        });

        modelBuilder.Entity<ParametrosSitioTipoCuerpoAgua>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de tabla que hace referencia a los parámetros por sitio y tipo cuerpo de agua");
            entity.Property(e => e.ParametroId).HasComment("Llave foránea que hace referencia al catálogo de  ParametrosGrpo");
            entity.Property(e => e.TipoCuerpoAguaId).HasComment("Llave foránea que hace referencia al catálogo de TipoCuerpoAgua");
            entity.Property(e => e.TipoSitioId).HasComment("Llave foránea que hace referencia al catálogo TipoSitio");

            entity.HasOne(d => d.Parametro).WithMany(p => p.ParametrosSitioTipoCuerpoAgua)
                .HasForeignKey(d => d.ParametroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParametrosSitioTipoCuerpoAgua_ParametrosGrupo");

            entity.HasOne(d => d.TipoCuerpoAgua).WithMany(p => p.ParametrosSitioTipoCuerpoAgua)
                .HasForeignKey(d => d.TipoCuerpoAguaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParametrosSitioTipoCuerpoAgua_TipoCuerpoAgua");

            entity.HasOne(d => d.TipoSitio).WithMany(p => p.ParametrosSitioTipoCuerpoAgua)
                .HasForeignKey(d => d.TipoSitioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParametrosSitioTipoCuerpoAgua_TipoSitio");
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal del catálogo Perfil");
            entity.Property(e => e.Estatus).HasComment("Campo que describe el estatus del perfil");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Campo que describe el nombre del perfil");
        });

        modelBuilder.Entity<PerfilPagina>(entity =>
        {
            entity.HasIndex(e => e.IdPagina, "IX_PerfilPagina_IdPagina");

            entity.HasIndex(e => e.IdPerfil, "IX_PerfilPagina_IdPerfil");

            entity.Property(e => e.Id).HasComment("Identificador principal del catálogo PerfilPagina\r\n");
            entity.Property(e => e.Estatus).HasComment("Campo que describe el estatus del registro");
            entity.Property(e => e.IdPagina).HasComment("Llave foránea que hace referencia al catálogo de Pagina");
            entity.Property(e => e.IdPerfil).HasComment("Llave foránea que hace referencia al catálogo de Perfil");

            entity.HasOne(d => d.IdPaginaNavigation).WithMany(p => p.PerfilPagina)
                .HasForeignKey(d => d.IdPagina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PerfilPag__IdPag__37A5467C");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.PerfilPagina)
                .HasForeignKey(d => d.IdPerfil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PerfilPag__IdPer__36B12243");
        });

        modelBuilder.Entity<PerfilPaginaAccion>(entity =>
        {
            entity.HasIndex(e => e.IdAccion, "IX_PerfilPaginaAccion_IdAccion");

            entity.HasIndex(e => e.IdPerfilPagina, "IX_PerfilPaginaAccion_IdPerfilPagina");

            entity.Property(e => e.Id).HasComment("Identificador principal del catálogo PerfilPaginaAccion\r\n");
            entity.Property(e => e.Estatus).HasComment("Campo que indica el estatus del registro");
            entity.Property(e => e.IdAccion).HasComment("Llave foránea que hace refrencia al catálogo de Accion");
            entity.Property(e => e.IdPerfilPagina).HasComment("Llave foránea que hace refrencia al catálogo de PerfilPagina");

            entity.HasOne(d => d.IdAccionNavigation).WithMany(p => p.PerfilPaginaAccion)
                .HasForeignKey(d => d.IdAccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PerfilPag__IdAcc__3B75D760");

            entity.HasOne(d => d.IdPerfilPaginaNavigation).WithMany(p => p.PerfilPaginaAccion)
                .HasForeignKey(d => d.IdPerfilPagina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PerfilPag__IdPer__3A81B327");
        });

        modelBuilder.Entity<PlantillaInformeMensualSupervision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Plantilla");

            entity.ToTable("PlantillaInformeMensualSupervision", "cat");

            entity.Property(e => e.Id).HasComment("Identificador principal del catálogo Plantilla");
            entity.Property(e => e.Anio)
                .HasMaxLength(4)
                .HasComment("Campo que indica el año");
            entity.Property(e => e.Contrato)
                .HasMaxLength(200)
                .HasComment("Campo que describe el contrato");
            entity.Property(e => e.DenominacionContrato)
                .HasMaxLength(300)
                .HasComment("Campo que describe la denominación del contrato");
            entity.Property(e => e.ImagenEncabezado)
                .IsUnicode(false)
                .HasComment("Campo que indica la imagen del encabezado");
            entity.Property(e => e.ImagenPiePagina)
                .IsUnicode(false)
                .HasComment("Campo que indica la imagen de pie de página");
            entity.Property(e => e.Indicaciones).HasComment("Campo que indica las indicaciones");
            entity.Property(e => e.SitiosMiniMax)
                .HasMaxLength(30)
                .HasComment("Campo que describe el mínimo y máximo de sitios");
        });

        modelBuilder.Entity<ProgramaAnio>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador del catálogo ProgramaAnio");
            entity.Property(e => e.Anio)
                .HasMaxLength(4)
                .HasComment("Campo que describe el año");
        });

        modelBuilder.Entity<ProgramaMuestreo>(entity =>
        {
            entity.HasIndex(e => e.ProgramaSitioId, "IX_ProgramaMuestreo_ProgramaSitioId");

            entity.Property(e => e.Id).HasComment("Identificador prinicpal de la tabla ProgramaMuestreo");
            entity.Property(e => e.BrigadaMuestreoId).HasComment("Llave foránea que hace referencia al catálogo de BrigadaMuestreo");
            entity.Property(e => e.DiaProgramado)
                .HasComment("Campo describe la fecha del dia programado")
                .HasColumnType("date");
            entity.Property(e => e.DomingoSemanaProgramada)
                .HasComment("Campo que describe el número del domingo de semana programada")
                .HasColumnType("date");
            entity.Property(e => e.NombreCorrectoArchivo)
                .HasMaxLength(100)
                .HasComment("Campo que indica el nombre correcto del archivo");
            entity.Property(e => e.ProgramaSitioId).HasComment("Llave foránea que hace referencia a la tabla ProgramaSitio");
            entity.Property(e => e.SemanaProgramada).HasComment("Campo que describe el número de la semana programada");

            entity.HasOne(d => d.BrigadaMuestreo).WithMany(p => p.ProgramaMuestreo)
                .HasForeignKey(d => d.BrigadaMuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProgramaMuestreo_BrigadaMuestreo");

            entity.HasOne(d => d.ProgramaSitio).WithMany(p => p.ProgramaMuestreo)
                .HasForeignKey(d => d.ProgramaSitioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProgramaMuestreo_ProgramaSitio");
        });

        modelBuilder.Entity<ProgramaSitio>(entity =>
        {
            entity.HasIndex(e => e.LaboratorioId, "IX_ProgramaSitio_LaboratorioId");

            entity.HasIndex(e => e.SitioId, "IX_ProgramaSitio_SitioId");

            entity.HasIndex(e => e.TipoSitioId, "IX_ProgramaSitio_TipoSitioId");

            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla Programa sitio\r\n");
            entity.Property(e => e.LaboratorioId).HasComment("Llave foránea que hace referencia al catálogo de Laboratorio");
            entity.Property(e => e.NumMuestreosRealizarAnio).HasComment("Campo que indica el número de muestreos a realizar al año");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .HasComment("Campo que describe las observaciones");
            entity.Property(e => e.ProgramaAnioId).HasComment("Llave foránea que hace referencia a la tabla de ProgramaAnio");
            entity.Property(e => e.SitioId).HasComment("Llave foránea que hace referencia al catálogo de Sitio");
            entity.Property(e => e.TipoSitioId).HasComment("Llave foránea que hace referencia al catálogo de TipoSitio");

            entity.HasOne(d => d.Laboratorio).WithMany(p => p.ProgramaSitio)
                .HasForeignKey(d => d.LaboratorioId)
                .HasConstraintName("FK_ProgramaSitio_Laboratorio");

            entity.HasOne(d => d.ProgramaAnio).WithMany(p => p.ProgramaSitio)
                .HasForeignKey(d => d.ProgramaAnioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProgramaSitio_ProgramaAnio");

            entity.HasOne(d => d.Sitio).WithMany(p => p.ProgramaSitio)
                .HasForeignKey(d => d.SitioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProgramaSitio_Sitio");

            entity.HasOne(d => d.TipoSitio).WithMany(p => p.ProgramaSitio)
                .HasForeignKey(d => d.TipoSitioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProgramaSitio_TipoSitio");
        });

        modelBuilder.Entity<Puestos>(entity =>
        {
            entity.ToTable("Puestos", "cat");

            entity.Property(e => e.Id).HasComment("Identificador principal del catálogo Puestos");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(120)
                .HasComment("Campo que describe el puesto");
        });

        modelBuilder.Entity<ReglaReporteResultadoTca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ResultadoTCAReglaReporte");

            entity.ToTable("ReglaReporteResultadoTCA");

            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla ReglaReporteResultadoTCA");
            entity.Property(e => e.ReglaReporteId).HasComment("Llave foránea que hace referencia al catálogo de ReglaReporte");
            entity.Property(e => e.TipoCuerpoAguaId).HasComment("Llave foránea que hace referencia al catálogo de TipoCuerpoAgua");

            entity.HasOne(d => d.ReglaReporte).WithMany(p => p.ReglaReporteResultadoTca)
                .HasForeignKey(d => d.ReglaReporteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReglaReporteResultadoTCA_ReglasReporte");

            entity.HasOne(d => d.TipoCuerpoAgua).WithMany(p => p.ReglaReporteResultadoTca)
                .HasForeignKey(d => d.TipoCuerpoAguaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResultadoTCAReglaReporte_TipoCuerpoAgua");
        });

        modelBuilder.Entity<ReglasLaboratorioLdmLpc>(entity =>
        {
            entity.ToTable("ReglasLaboratorioLDM_LPC");

            entity.Property(e => e.Id).HasComment("Identificador principal de tabla de ReglasLaboratorioLDM_LPC");
            entity.Property(e => e.ClaveUnicaLabParametro)
                .HasMaxLength(50)
                .HasComment("Clave conformada por la concatenación del laboratorio con el parámetro");
            entity.Property(e => e.EsLdm)
                .HasComment("Campo que describe si es LDM")
                .HasColumnName("EsLDM");
            entity.Property(e => e.LaboratorioId).HasComment("Llave foránea que hace referencia al catálogo Laboratorios");
            entity.Property(e => e.Ldm)
                .HasMaxLength(20)
                .HasComment("Campo que describe el LDM")
                .HasColumnName("LDM");
            entity.Property(e => e.Lpc)
                .HasMaxLength(20)
                .HasComment("Campo que describe el LPC")
                .HasColumnName("LPC");
            entity.Property(e => e.ParametroId).HasComment("Llave foránea que hace referencia al catálogo ParametrosGrupo");

            entity.HasOne(d => d.Laboratorio).WithMany(p => p.ReglasLaboratorioLdmLpc)
                .HasForeignKey(d => d.LaboratorioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReglasLaboratorioLDM_LPC_Laboratorios");

            entity.HasOne(d => d.Parametro).WithMany(p => p.ReglasLaboratorioLdmLpc)
                .HasForeignKey(d => d.ParametroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReglasLaboratorioLDM_LPC_ParametrosGrupo");
        });

        modelBuilder.Entity<ReglasMinimoMaximo>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla ReglasMinimoMaximo");
            entity.Property(e => e.Aplica).HasComment("Campo que describe si aplica la regla");
            entity.Property(e => e.ClasificacionReglaId).HasComment("Llave foránea que hace referencia al catálogo ClasificaciónRegla");
            entity.Property(e => e.ClaveRegla)
                .HasMaxLength(10)
                .HasComment("Campo que describe la clave de la regla\r\n");
            entity.Property(e => e.Leyenda)
                .HasMaxLength(80)
                .HasComment("Campo que describe la leyenda que se mostrara en caso de que no se cumpla la regla");
            entity.Property(e => e.MinimoMaximo)
                .HasMaxLength(35)
                .HasComment("Campo que describe el mínimo máximo");
            entity.Property(e => e.ParametroId).HasComment("Llave foránea que hace referencia al catálogo ParametrosGrupo");
            entity.Property(e => e.TipoReglaId).HasComment("Llave foránea que hace referencia al catálogo TipoRegla");

            entity.HasOne(d => d.ClasificacionRegla).WithMany(p => p.ReglasMinimoMaximo)
                .HasForeignKey(d => d.ClasificacionReglaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReglasMinimoMaximo_ClasificacionRegla");

            entity.HasOne(d => d.Parametro).WithMany(p => p.ReglasMinimoMaximo)
                .HasForeignKey(d => d.ParametroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReglasMinimoMaximo_ParametrosGrupo");

            entity.HasOne(d => d.TipoRegla).WithMany(p => p.ReglasMinimoMaximo)
                .HasForeignKey(d => d.TipoReglaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReglasMinimoMaximo_TipoRegla");
        });

        modelBuilder.Entity<ReglasRelacion>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla ReglasRelacion");
            entity.Property(e => e.ClasificacionReglaId).HasComment("Llave foránea que hace referencia al catálogo ClasificacionRegla");
            entity.Property(e => e.ClaveRegla)
                .HasMaxLength(10)
                .HasComment("Capo que describe la clave de la regla");
            entity.Property(e => e.Leyenda)
                .HasMaxLength(110)
                .HasComment("Campo que describe a leenda que se mostrara en caso de que no se cumple la regla");
            entity.Property(e => e.RelacionRegla)
                .HasMaxLength(200)
                .HasComment("Campo que describe la formula de la regla");
            entity.Property(e => e.TipoReglaId).HasComment("Llave foránea que hace referencia al catálogo TipoRegla");
        });

        modelBuilder.Entity<ReglasRelacionParametro>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla ReglasRelacionParametro");
            entity.Property(e => e.ParametroId).HasComment("Llave foránea que hace referencia al catalogo ParametrosGrupo");
            entity.Property(e => e.ReglasRelacionId).HasComment("Llave foránea que hace referencia al  catalogo ReglasRelacion");

            entity.HasOne(d => d.Parametro).WithMany(p => p.ReglasRelacionParametro)
                .HasForeignKey(d => d.ParametroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReglasRelacionParametro_ParametrosGrupo");

            entity.HasOne(d => d.ReglasRelacion).WithMany(p => p.ReglasRelacionParametro)
                .HasForeignKey(d => d.ReglasRelacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReglasRelacionParametro_ReglasRelacion");
        });

        modelBuilder.Entity<ReglasReporte>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla ReglasReporte\r\n");
            entity.Property(e => e.ClasificacionReglaId).HasComment("Llave foránea que hace referencia al catalogo ClasificacionRegla");
            entity.Property(e => e.ClaveRegla)
                .HasMaxLength(10)
                .HasComment("Campo que describe la clave de la regla");
            entity.Property(e => e.EsValidoEspaciosBlanco).HasComment("Bandera que indica si el resultado es valido a espacios en blanco");
            entity.Property(e => e.EsValidoMenorCero).HasComment("Bandera que indica si el resultado es valido a <0");
            entity.Property(e => e.EsValidoResultadoCero).HasComment("Bandera que indica si el resultado es valido a cero\r\n");
            entity.Property(e => e.EsValidoResultadoIm)
                .HasComment("Bandera que indica si el resultado es valido a un IM")
                .HasColumnName("EsValidoResultadoIM");
            entity.Property(e => e.EsValidoResultadoMenorCmc)
                .HasComment("Bandera que indica si el resultado es valido a que sea <CMC")
                .HasColumnName("EsValidoResultadoMenorCMC");
            entity.Property(e => e.EsValidoResultadoMenorLd)
                .HasComment("Bandera que indica si el resultado es valido a que sea <LD")
                .HasColumnName("EsValidoResultadoMenorLD");
            entity.Property(e => e.EsValidoResultadoMenorLpc)
                .HasComment("Bandera que indica si el resultado es valido a que sea <LPC")
                .HasColumnName("EsValidoResultadoMenorLPC");
            entity.Property(e => e.EsValidoResultadoNa)
                .HasComment("Bandera que indica si el resultado es valido a un NA")
                .HasColumnName("EsValidoResultadoNA");
            entity.Property(e => e.EsValidoResultadoNd)
                .HasComment("Bandera que indica si el resultado es valido a un ND")
                .HasColumnName("EsValidoResultadoND");
            entity.Property(e => e.EsValidoResultadoNe)
                .HasComment("Bandera que indica si el resultado es valido a un NE")
                .HasColumnName("EsValidoResultadoNE");
            entity.Property(e => e.ParametroId).HasComment("Llave foránea que hace relación al catalogo de ParametroGrupo");

            entity.HasOne(d => d.ClasificacionRegla).WithMany(p => p.ReglasReporte)
                .HasForeignKey(d => d.ClasificacionReglaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReglasReporte_ClasificacionRegla");

            entity.HasOne(d => d.Parametro).WithMany(p => p.ReglasReporte)
                .HasForeignKey(d => d.ParametroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReglasReporte_ParametrosGrupo");
        });

        modelBuilder.Entity<ReglasReporteLeyendas>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla ReglasReporteLeyendas");
            entity.Property(e => e.Leyenda)
                .HasMaxLength(50)
                .HasComment("Campo que hace referencia a la leyenda");
            entity.Property(e => e.ReglaReporte)
                .HasMaxLength(50)
                .HasComment("Campo que describe la leyenda de la regla de reporte");
        });

        modelBuilder.Entity<ReplicasResultadosReglasValidacion>(entity =>
        {
            entity.Property(e => e.EsDatoCorrectoSrenameca).HasColumnName("EsDatoCorrectoSRENAMECA");
            entity.Property(e => e.FechaEstatusFinal)
                .HasDefaultValueSql("('')")
                .HasColumnType("date");
            entity.Property(e => e.FechaObservacionSrenameca)
                .HasColumnType("date")
                .HasColumnName("FechaObservacionSRENAMECA");
            entity.Property(e => e.FechaReplicaLaboratorio).HasColumnType("date");
            entity.Property(e => e.ObservacionLaboratorio).HasDefaultValueSql("('')");
            entity.Property(e => e.ObservacionSrenameca).HasColumnName("ObservacionSRENAMECA");
            entity.Property(e => e.ObservacionesReglasReplica)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ResultadoReplica)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.HasOne(d => d.ResultadoMuestreo).WithMany(p => p.ReplicasResultadosReglasValidacion)
                .HasForeignKey(d => d.ResultadoMuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReplicasResultadosReglasValidacion_ResultadoMuestreo");
        });

        modelBuilder.Entity<ResultadoMuestreo>(entity =>
        {
            entity.HasIndex(e => e.EstatusResultado, "IX_ResultadoMuestreo_EstatusResultado");

            entity.HasIndex(e => e.MuestreoId, "IX_ResultadoMuestreo_MuestreoId");

            entity.HasIndex(e => e.ObservacionSrenamecaid, "IX_ResultadoMuestreo_ObservacionSRENAMECAId");

            entity.HasIndex(e => e.ObservacionesOcdlid, "IX_ResultadoMuestreo_ObservacionesOCDLId");

            entity.HasIndex(e => e.ObservacionesSecaiaid, "IX_ResultadoMuestreo_ObservacionesSECAIAId");

            entity.HasIndex(e => e.ParametroId, "IX_ResultadoMuestreo_ParametroId");

            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla ResultadoMuestreo");
            entity.Property(e => e.CausaRechazo)
                .IsUnicode(false)
                .HasComment("Campo que indica las causas de rechazo");
            entity.Property(e => e.Comentarios)
                .IsUnicode(false)
                .HasComment("Camp que describe los comentarios");
            entity.Property(e => e.EsCorrectoOcdl)
                .HasComment("Campo que indica si es correcto por parte de OCDL")
                .HasColumnName("EsCorrectoOCDL");
            entity.Property(e => e.EsCorrectoSecaia)
                .HasComment("Campo que indica si es correcto por parte de SECAIA")
                .HasColumnName("EsCorrectoSECAIA");
            entity.Property(e => e.EsMismoResultado).HasComment("Campo que indica si es el mismo resultado");
            entity.Property(e => e.EstatusResultado).HasComment("Llave foránea que hace referencia al catálogo de Estatus descibiendo el estatus del resultado");
            entity.Property(e => e.EstatusResultadoId).HasComment("Llave foranea que hace referencia al catalogo de EstatusResultado indicando el estatus en el que se encuentra el resultado del muestreo");
            entity.Property(e => e.FechaEntrega)
                .HasComment("Campo que describe la fecha de entrega")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaEstatusFinal)
                .HasComment("Campo que indica la fecha del estatus final")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaObservacionSrenameca)
                .HasComment("Campo que indica la fecha en la que se realizaron las observaciones por parte de SRENAMECA")
                .HasColumnType("datetime")
                .HasColumnName("FechaObservacionSRENAMECA");
            entity.Property(e => e.FechaReplicaLaboratorio)
                .HasComment("Campo que describe la fecha de replica de laboratorio")
                .HasColumnType("datetime");
            entity.Property(e => e.IdResultadoLaboratorio).HasComment("Campo que describe el id resultado de laboratorio es carado mediante ebaseca");
            entity.Property(e => e.LaboratorioId).HasComment("Llave foránea que hace referencia al catalogo de Laboratorio");
            entity.Property(e => e.LaboratorioSubrogadoId).HasComment("Llave foránea que hace referencia al catalogo de LaboratorioSubrogado");
            entity.Property(e => e.MuestreoId).HasComment("Llave foránea que hace relación a la Tabla de muestreo");
            entity.Property(e => e.ObservacionFinal).HasComment("Campo que describe las observaciones de validación final que se realiza despues de haber aplicado las reglas");
            entity.Property(e => e.ObservacionLaboratorio)
                .IsUnicode(false)
                .HasComment("Campo que describe las observaciones por parte del laboratorio");
            entity.Property(e => e.ObservacionSrenameca)
                .IsUnicode(false)
                .HasComment("Campo que describe las observaciones por parte de SRENAMECA")
                .HasColumnName("ObservacionSRENAMECA");
            entity.Property(e => e.ObservacionSrenamecaid)
                .HasComment("Llave foránea hace referencia al catalogo de observaciones indicando las observaciones de SRENAMECA")
                .HasColumnName("ObservacionSRENAMECAId");
            entity.Property(e => e.ObservacionesOcdl)
                .IsUnicode(false)
                .HasComment("Campo que indica las observaciones de OCDL")
                .HasColumnName("ObservacionesOCDL");
            entity.Property(e => e.ObservacionesOcdlid)
                .HasComment("Llave foránea hace referencia al catalogo de observaciones indicando las observaciones de OCDL")
                .HasColumnName("ObservacionesOCDLId");
            entity.Property(e => e.ObservacionesSecaia)
                .IsUnicode(false)
                .HasComment("Campo que describe las observaciones por parte de SECAIA")
                .HasColumnName("ObservacionesSECAIA");
            entity.Property(e => e.ObservacionesSecaiaid)
                .HasComment("Llave foránea hace referencia al catalogo de observaciones indicando las observaciones de SECAIA")
                .HasColumnName("ObservacionesSECAIAId");
            entity.Property(e => e.ParametroId).HasComment("Llave foránea que hace relación al catalogo de ParametroGrupo indicando el parámetro\r\n");
            entity.Property(e => e.Resultado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Campo que describe el resultado del parametro del muestreo, cargado mediante ebaseca");
            entity.Property(e => e.ResultadoActualizadoReplica)
                .IsUnicode(false)
                .HasComment("Campo que indica el resultado actualizado de replica");
            entity.Property(e => e.ResultadoReglas)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasComment("Campo que describe el resultado de las reglas");
            entity.Property(e => e.ResultadoReplica)
                .IsUnicode(false)
                .HasComment("Campo que describe el resultado de la replica");
            entity.Property(e => e.ResultadoSustituidoPorLaboratorio)
                .HasMaxLength(50)
                .HasComment("Campo que describe el resultado sustituido por laboratorio");
            entity.Property(e => e.ResultadoSustituidoPorLimite)
                .HasMaxLength(50)
                .HasComment("Campo que describe el resultado sustituido por limite");
            entity.Property(e => e.SeAceptaRechazoSiNo).HasComment("Campo que describe si se acepta el rechazo");
            entity.Property(e => e.SeApruebaResultadoReplica).HasComment("Campo que describe si se aprueba el resultado de replica");
            entity.Property(e => e.ValidacionFinal).HasComment("Campo que describe si aprueba la validación final que se realiza despues de haber aplicado las reglas");

            entity.HasOne(d => d.EstatusResultadoNavigation).WithMany(p => p.ResultadoMuestreo)
                .HasForeignKey(d => d.EstatusResultado)
                .HasConstraintName("FK_ResultadoMuestreo_EstatusMuestreo");

            entity.HasOne(d => d.EstatusResultado1).WithMany(p => p.ResultadoMuestreo)
                .HasForeignKey(d => d.EstatusResultadoId)
                .HasConstraintName("FK_ResultadoMuestreo_EstatusResultado");

            entity.HasOne(d => d.Laboratorio).WithMany(p => p.ResultadoMuestreoLaboratorio)
                .HasForeignKey(d => d.LaboratorioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResultadoMuestreo_Laboratorios");

            entity.HasOne(d => d.LaboratorioSubrogado).WithMany(p => p.ResultadoMuestreoLaboratorioSubrogado)
                .HasForeignKey(d => d.LaboratorioSubrogadoId)
                .HasConstraintName("FK_ResultadoMuestreo_LaboratorioSub");

            entity.HasOne(d => d.Muestreo).WithMany(p => p.ResultadoMuestreo)
                .HasForeignKey(d => d.MuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResultadoMuestreo_Muestreo");

            entity.HasOne(d => d.ObservacionSrenamecaNavigation).WithMany(p => p.ResultadoMuestreoObservacionSrenamecaNavigation)
                .HasForeignKey(d => d.ObservacionSrenamecaid)
                .HasConstraintName("FK_ResultadoMuestreo_ObservacionesSRENAMECA");

            entity.HasOne(d => d.ObservacionesOcdlNavigation).WithMany(p => p.ResultadoMuestreoObservacionesOcdlNavigation)
                .HasForeignKey(d => d.ObservacionesOcdlid)
                .HasConstraintName("FK_ResultadoMuestreo_ObservacionesOCDL");

            entity.HasOne(d => d.ObservacionesSecaiaNavigation).WithMany(p => p.ResultadoMuestreoObservacionesSecaiaNavigation)
                .HasForeignKey(d => d.ObservacionesSecaiaid)
                .HasConstraintName("FK_ResultadoMuestreo_ObservacionesSECAIA");

            entity.HasOne(d => d.Parametro).WithMany(p => p.ResultadoMuestreo)
                .HasForeignKey(d => d.ParametroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResultadoMuestreo_ParametrosGrupo");
        });

        modelBuilder.Entity<Sitio>(entity =>
        {
            entity.HasIndex(e => e.CuencaDireccionesLocalesId, "IX_Sitio_CuencaDireccionesLocalesId");

            entity.HasIndex(e => e.CuencaRevisionId, "IX_Sitio_CuencaRevisionId");

            entity.HasIndex(e => e.CuerpoTipoSubtipoAguaId, "IX_Sitio_CuerpoTipoSubtipoAguaId");

            entity.HasIndex(e => e.DireccionLrevisionId, "IX_Sitio_DireccionLRevisionId");

            entity.HasIndex(e => e.EstadoId, "IX_Sitio_EstadoId");

            entity.HasIndex(e => e.MunicipioId, "IX_Sitio_MunicipioId");

            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla Sitio");
            entity.Property(e => e.AcuiferoId).HasComment("Llave foránea no obligatoria que hace relación al catálogo de Acuifero");
            entity.Property(e => e.ClaveSitio)
                .HasMaxLength(150)
                .HasComment("Campo que describe la clave sitio");
            entity.Property(e => e.CuencaDireccionesLocalesId).HasComment("Llave foránea que hace referencia al catalogo de CuencaDireccionLocal");
            entity.Property(e => e.CuencaRevisionId).HasComment("Llave foránea que hace referencia al catalogo de OrganismosCuenca");
            entity.Property(e => e.CuerpoTipoSubtipoAguaId).HasComment("Llave foránea que hace referencia al catalogo de CuerpoTipoSubtipoAgua");
            entity.Property(e => e.DireccionLrevisionId)
                .HasComment("Llave foránea que hace referencia al catalogo de DireccionLocal")
                .HasColumnName("DireccionLRevisionId");
            entity.Property(e => e.EstadoId).HasComment("Llave foránea que hace referencia al catalogo de Estado");
            entity.Property(e => e.Latitud).HasComment("Campo que describe la latitud del sitio");
            entity.Property(e => e.Longitud).HasComment("Campo que describe la longitud del sitio");
            entity.Property(e => e.MunicipioId).HasComment("Llave foránea que hace referencia al catalogo de Municipio");
            entity.Property(e => e.NombreSitio)
                .HasMaxLength(250)
                .HasComment("Campo que describe el nombre del sitio");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .HasComment("Campo que describe las observaciones");

            entity.HasOne(d => d.Acuifero).WithMany(p => p.Sitio)
                .HasForeignKey(d => d.AcuiferoId)
                .HasConstraintName("FK_Sitio_Acuifero");

            entity.HasOne(d => d.CuencaDireccionesLocales).WithMany(p => p.Sitio)
                .HasForeignKey(d => d.CuencaDireccionesLocalesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sitios_CuencaDireccionesLocales1");

            entity.HasOne(d => d.CuencaRevision).WithMany(p => p.Sitio)
                .HasForeignKey(d => d.CuencaRevisionId)
                .HasConstraintName("FK_Sitio_OrganismoCuenca");

            entity.HasOne(d => d.CuerpoTipoSubtipoAgua).WithMany(p => p.Sitio)
                .HasForeignKey(d => d.CuerpoTipoSubtipoAguaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sitios_CuerpoTipoSubtipoAgua");

            entity.HasOne(d => d.DireccionLrevision).WithMany(p => p.Sitio)
                .HasForeignKey(d => d.DireccionLrevisionId)
                .HasConstraintName("FK_Sitio_DireccionLocal");

            entity.HasOne(d => d.Estado).WithMany(p => p.Sitio)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sitios_Estado1");

            entity.HasOne(d => d.Municipio).WithMany(p => p.Sitio)
                .HasForeignKey(d => d.MunicipioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sitios_Municipio1");
        });

        modelBuilder.Entity<SubgrupoAnalitico>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal se la tabla SubGrupoAnalitico");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasComment("Campo que describe el subgrupo analítico");
        });

        modelBuilder.Entity<SubtipoCuerpoAgua>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal se la tabla SubtipoCuerpoAgua");
            entity.Property(e => e.Activo).HasComment("Bandera que indica si se encuentra activo el subtipo cuerpo de agua");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasComment("Campo que describe el subtipo cuepo de agua");
        });

        modelBuilder.Entity<SupervisionMuestreo>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal se la tabla SupervisionMuestreo");
            entity.Property(e => e.ClaveMuestreo)
                .HasMaxLength(50)
                .HasComment("Campo que describe la clave de muestreo");
            entity.Property(e => e.FechaRegistro)
                .HasComment("Campo que describe la fecha en la que se realizo el registro")
                .HasColumnType("datetime");
            entity.Property(e => e.FehaMuestreo)
                .HasComment("Campo que describe la fecha muestreo")
                .HasColumnType("datetime");
            entity.Property(e => e.HoraInicio).HasComment("Campo que indica la hora de inicio");
            entity.Property(e => e.HoraTermino).HasComment("Campo que indica la hora de termino");
            entity.Property(e => e.HoraTomaMuestra).HasComment("Campo que indica la hora de la toma de muestra");
            entity.Property(e => e.LaboratorioRealizaId).HasComment("Llave foránea que hace referencia al catalogo Laboratorios describiendo el laboratorio que realizo la supervisión");
            entity.Property(e => e.LatitudToma).HasComment("Campo que describe la latitud de toma del muestreo");
            entity.Property(e => e.LongitudToma).HasComment("Campo que describe la longitud de toma del muestreo");
            entity.Property(e => e.ObservacionesMuestreo).HasComment("Campo que describe las observaciones del muestreo");
            entity.Property(e => e.OrganismoCuencaReportaId).HasComment("Llave foránea que hace referencia al catalogo de OrganismosCuenca");
            entity.Property(e => e.OrganismosDireccionesRealizaId).HasComment("Llave foránea que hace referencia al catalogo de CuencasDireccionesLocales describiendo el organismo dirección que reporta el muestreo");
            entity.Property(e => e.PuntajeObtenido)
                .HasComment("Campo que indica el puntaje")
                .HasColumnType("decimal(4, 1)");
            entity.Property(e => e.ResponsableMedicionesId).HasComment("Llave foránea que indica el responsable de las mediciones del muestreo");
            entity.Property(e => e.ResponsableTomaId).HasComment("Llave foránea que hace referencia al catálogo de Muestradores indicando el responsable de la toma");
            entity.Property(e => e.SitioId).HasComment("Llave foránea que hace referencia al catalogo de Sitio");
            entity.Property(e => e.SupervisorConagua)
                .HasMaxLength(100)
                .HasComment("Campo que describe el supervisor de Conagua");
            entity.Property(e => e.UsuarioRegistroId).HasComment("Llave foránea que hace relación a la tabla de Usuario describiendo el usuario que registro la supervisión");

            entity.HasOne(d => d.LaboratorioRealiza).WithMany(p => p.SupervisionMuestreo)
                .HasForeignKey(d => d.LaboratorioRealizaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupervisionMuestreo_SupervisionMuestreo");

            entity.HasOne(d => d.OrganismoCuencaReporta).WithMany(p => p.SupervisionMuestreo)
                .HasForeignKey(d => d.OrganismoCuencaReportaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupervisionMuestreo_OrganismoCuenca");

            entity.HasOne(d => d.OrganismosDireccionesRealiza).WithMany(p => p.SupervisionMuestreo)
                .HasForeignKey(d => d.OrganismosDireccionesRealizaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupervisionMuestreo_CuencaDireccionesLocales");

            entity.HasOne(d => d.ResponsableMediciones).WithMany(p => p.SupervisionMuestreoResponsableMediciones)
                .HasForeignKey(d => d.ResponsableMedicionesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupervisionMuestreo_MuestradoresMediciones");

            entity.HasOne(d => d.ResponsableToma).WithMany(p => p.SupervisionMuestreoResponsableToma)
                .HasForeignKey(d => d.ResponsableTomaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupervisionMuestreo_MuestradoresToma");

            entity.HasOne(d => d.Sitio).WithMany(p => p.SupervisionMuestreo)
                .HasForeignKey(d => d.SitioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupervisionMuestreo_Sitio");

            entity.HasOne(d => d.UsuarioRegistro).WithMany(p => p.SupervisionMuestreo)
                .HasForeignKey(d => d.UsuarioRegistroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupervisionMuestreo_Usuario");
        });

        modelBuilder.Entity<TipoAprobacion>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla TipoAprobacion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .HasComment("Campo que describe el tipo de aprobación");
        });

        modelBuilder.Entity<TipoArchivoInformeMensualSupervision>(entity =>
        {
            entity.ToTable("TipoArchivoInformeMensualSupervision", "cat");

            entity.Property(e => e.Id).HasComment("Identificador principal del catálogo TipoArchivoInformeMensualSupervision");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasComment("Campo que describe el tipo de archivo de informe mensual de supervisión");
        });

        modelBuilder.Entity<TipoCarga>(entity =>
        {
            entity.ToTable("TipoCarga", "cat");

            entity.Property(e => e.Id).HasComment("Identificador principal del catalogo TipoCarga");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(10)
                .HasComment("Descripcion del tipo de carga de archivo ebaseca");
        });

        modelBuilder.Entity<TipoCuerpoAgua>(entity =>
        {
            entity.HasIndex(e => e.TipoHomologadoId, "IX_TipoCuerpoAgua_TipoHomologadoId");

            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla TipoCuerpoAgua");
            entity.Property(e => e.Activo).HasComment("Campo que describe si se encuentra activo el tipo cuerpo de agua");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasComment("Campo que describe el tipo de cuerpo de agua ");
            entity.Property(e => e.EvidenciasEsperadas).HasComment("Campo que describe las evidencias esperadas conforme al tipo de cuerpo de agua");
            entity.Property(e => e.Frecuencia)
                .HasMaxLength(10)
                .HasComment("Campo que describe la frecuencia del tipo de cuerpo de agua para el muestreo");
            entity.Property(e => e.TiempoMinimoMuestreo).HasComment("Campo que describe el tiempo mínimo del muestreo en minutos");
            entity.Property(e => e.TipoHomologadoId).HasComment("Llave foránea que hace relación a la tabla de TipoHomologado");

            entity.HasOne(d => d.TipoHomologado).WithMany(p => p.TipoCuerpoAgua)
                .HasForeignKey(d => d.TipoHomologadoId)
                .HasConstraintName("FK_TipoCuerpoAgua_TipoHomologado");
        });

        modelBuilder.Entity<TipoEvidenciaMuestreo>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla TipoEvidenciaMuestreo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Campo que describe el tipo de evidencia del muestreo");
            entity.Property(e => e.Extension)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("Campo que describe la extensión de la evidencia");
            entity.Property(e => e.Sufijo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("Campo que describe el sufijo ");
        });

        modelBuilder.Entity<TipoHomologado>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla TipoHomologado");
            entity.Property(e => e.Activo).HasComment("Camo que describe si se enceuntra activo el tipo homologado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Campo que describe el tipo homologado");
        });

        modelBuilder.Entity<TipoRegla>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla TipoRegla");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(10)
                .HasComment("Campo que describe el tipo de regla");
        });

        modelBuilder.Entity<TipoSitio>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla TipoSitio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Campo que describe el tipo sitio");
        });

        modelBuilder.Entity<TipoSupervision>(entity =>
        {
            entity.ToTable("TipoSupervision", "cat");

            entity.Property(e => e.Id).HasComment("Identificador principal de tabla");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .HasComment("Campo que describe el tipo de supervisión");
        });

        modelBuilder.Entity<TipoSustitucion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoSust__3214EC0762844A01");

            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla TipoSustitucion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Campo que describe el tipo de sustitución");
        });

        modelBuilder.Entity<UnidadMedida>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla UnidadMedida");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .HasComment("Campo que describe la unidad de medida");
        });        

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasIndex(e => e.CuencaId, "IX_Usuario_CuencaId");

            entity.HasIndex(e => e.DireccionLocalId, "IX_Usuario_DireccionLocalId");

            entity.HasIndex(e => e.PerfilId, "IX_Usuario_PerfilId");

            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla Usuario");
            entity.Property(e => e.Activo).HasComment("Campoq ue describe si se encuentra activo el usuario");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Campo que describe el apellido materno del usuario");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Campo que describe el apellido paterno del usuario");
            entity.Property(e => e.CuencaId).HasComment("Llave foránea que hace referencia al catálogo de OrganismosCuenca");
            entity.Property(e => e.DireccionLocalId).HasComment("Llave foránea que hace referencia al catálogo de DireccionesLocales");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Campo que describe el correo electrónico del usuario");
            entity.Property(e => e.FechaRegistro)
                .HasComment("Campo que describe la fecha registro del usuario")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasComment("Campo que describe el nombre del usuario");
            entity.Property(e => e.PerfilId).HasComment("Llave foránea que hace relación al catálogo de Perfil");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Camp que describe el username del usuario");

            entity.HasOne(d => d.Cuenca).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.CuencaId)
                .HasConstraintName("FK_Usuario_OrganismoCuenca");

            entity.HasOne(d => d.DireccionLocal).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.DireccionLocalId)
                .HasConstraintName("FK_Usuario_DireccionLocal");

            entity.HasOne(d => d.Perfil).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.PerfilId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Perfil");
        });

        modelBuilder.Entity<ValidacionEvidencia>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla ValidacionEvidencia");
            entity.Property(e => e.AvisoRealizacionId).HasComment("Llave foánea que hace referencia a la tabla de AvisoRealizacion");
            entity.Property(e => e.CalibracionVerificacionEquiposBm)
                .HasComment("Campo que describe si se cumplió con la calibración y/o verificación de equipos al criterio de bitácora de muestreo (BM)")
                .HasColumnName("CalibracionVerificacionEquiposBM");
            entity.Property(e => e.CumpleClaveBrigadaBm)
                .HasComment("Campo que describe si se cumplió con la clave brigada referente al criterio de bitácora de muestreo (BM)")
                .HasColumnName("CumpleClaveBrigadaBM");
            entity.Property(e => e.CumpleClaveConalabbm)
                .HasComment("Campo que describe si se cumplió con la clave CONALAB referente al criterio de bitácora de muestreo (BM)")
                .HasColumnName("CumpleClaveCONALABBM");
            entity.Property(e => e.CumpleClaveConalabtr)
                .HasComment("Campo que indica si se cumple con la clave CONALAB referente al criterio Track de ruta (TR)")
                .HasColumnName("CumpleClaveCONALABTR");
            entity.Property(e => e.CumpleClaveMuestreoBm)
                .HasComment("Campo que describe si se cumplió con la clave muestreo referente al criterio de bitácora de muestreo (BM)")
                .HasColumnName("CumpleClaveMuestreoBM");
            entity.Property(e => e.CumpleEvidenciasEsperadas).HasComment("Campo que describe si se cumplieron las evidencias esperadas");
            entity.Property(e => e.CumpleFechaRealizacionBm)
                .HasComment("Campo que describe si se cumplió la fecha de realización referente al criterio de bitácora de muestreo (BM)")
                .HasColumnName("CumpleFechaRealizacionBM");
            entity.Property(e => e.CumpleGeocercaBm)
                .HasComment("Campo que describe si se cumplió con la geocerca referente al criterio de bitácora de muestreo (BM)")
                .HasColumnName("CumpleGeocercaBM");
            entity.Property(e => e.CumpleGeocercaFf)
                .HasComment("Campo indica si se cumple la geocerca referente al criterio formato de aforo (FF)")
                .HasColumnName("CumpleGeocercaFF");
            entity.Property(e => e.CumpleLiderBrigadaBm)
                .HasComment("Campo que describe si se cumplió con el líder de la brigada referente al criterio de bitácora de muestreo (BM)")
                .HasColumnName("CumpleLiderBrigadaBM");
            entity.Property(e => e.CumpleMetadatosFa)
                .HasComment("Campo que indica si se cumplió con los metadatos referente a los criterios foto de aforo (FA)")
                .HasColumnName("CumpleMetadatosFA");
            entity.Property(e => e.CumpleMetadatosFm)
                .HasComment("Campo que describe si se cumple los metadatos referente a los criterios foto de muestreo (FM)")
                .HasColumnName("CumpleMetadatosFM");
            entity.Property(e => e.CumpleMetadatosFs)
                .HasComment("Campo que indica si se cumple los metadatos referente a los criterios foto de muestras (FS)")
                .HasColumnName("CumpleMetadatosFS");
            entity.Property(e => e.CumplePlacasTr)
                .HasComment("Campo que indica si se cumple con las placas de la unidad referente al criterio Track de ruta (TR)")
                .HasColumnName("CumplePlacasTR");
            entity.Property(e => e.CumpleTiempoMuestreoBm)
                .HasComment("Campo que describe si se cumplió el Tiempo de muestreo referente al criterio de bitácora de muestreo (BM)")
                .HasColumnName("CumpleTiempoMuestreoBM");
            entity.Property(e => e.FechaRegistro)
                .HasComment("Campo que registra la fecha en la que se realizó la validación")
                .HasColumnType("datetime");
            entity.Property(e => e.FirmadoyCanceladoBm)
                .HasComment("Campo que describe si es firmado y cancelado referente al criterio de bitácora de muestreo (BM)")
                .HasColumnName("FirmadoyCanceladoBM");
            entity.Property(e => e.FolioBm)
                .HasComment("Campo que describe si existe el folio en el criterio de bitácora de muestreo (BM)")
                .HasColumnName("FolioBM");
            entity.Property(e => e.FormatoLlenadoCorrectoFf)
                .HasComment("Campo indica si fue llenado el formato correctamente referente al criterio formato de aforo (FF)")
                .HasColumnName("FormatoLlenadoCorrectoFF");
            entity.Property(e => e.FotoUnicaFa)
                .HasComment("Campo que verifica la foto única referente a los criterios foto de aforo (FA)")
                .HasColumnName("FotoUnicaFA");
            entity.Property(e => e.FotoUnicaFm)
                .HasComment("Campo que indica si existe foto única referente a los criterios foto de muestreo (FM)")
                .HasColumnName("FotoUnicaFM");
            entity.Property(e => e.FotoUnicaFs)
                .HasComment("Campo que verifica la foto única referente a los criterios foto de muestras (FS)")
                .HasColumnName("FotoUnicaFS");
            entity.Property(e => e.FotografiaGpspuntoMuestreoBm)
                .HasComment("Campo que describe si existe fotografía GPS punto de muestreo referente al criterio de bitácora de muestreo (BM)")
                .HasColumnName("FotografiaGPSPuntoMuestreoBM");
            entity.Property(e => e.LiderBrigadaCuerpoAguaFm)
                .HasComment("Campo que verifica el líder de brigada y cuerpo de agua referente a los criterios foto de muestreo (FM)")
                .HasColumnName("LiderBrigadaCuerpoAguaFM");
            entity.Property(e => e.LlenadoCorrectoCc)
                .HasComment("Campo que indica si el llenado fue correcto referente al criterio cadena de custodia (CC)")
                .HasColumnName("LlenadoCorrectoCC");
            entity.Property(e => e.MetodologiaFa)
                .HasComment("Campo que indica si se cumplió la metodología referente a los criterios foto de aforo (FA)")
                .HasColumnName("MetodologiaFA");
            entity.Property(e => e.MuestrasPreservadasFs)
                .HasComment("Campo que verifica las muestras preservadas  referente a los criterios foto de muestras (FS)")
                .HasColumnName("MuestrasPreservadasFS");
            entity.Property(e => e.MuestreoId).HasComment("Llave foránea que hace relación a la tabla de Muestreo");
            entity.Property(e => e.ObservacionesRechazo).HasComment("Campo que describe las observaciones de rechazo");
            entity.Property(e => e.PorcentajePago).HasComment("Campo que describe el porcentaje de pago");
            entity.Property(e => e.Rechazo).HasComment("Campo que describe si es rechazado ");
            entity.Property(e => e.RegistroRecipientesFs)
                .HasComment("Campo que verifica el registro de los recipientes referente a los criterios foto de muestras (FS)")
                .HasColumnName("RegistroRecipientesFS");
            entity.Property(e => e.RegistroResultadosCampoBm)
                .HasComment("Campo que describe si se cumplió con el registro de resultados de campo al criterio de bitácora de muestreo (BM)")
                .HasColumnName("RegistroResultadosCampoBM");
            entity.Property(e => e.RegistrosLegiblesCc)
                .HasComment("Capo que indica si los registros son legibles referente al criterio cadena de custodia (CC)")
                .HasColumnName("RegistrosLegiblesCC");
            entity.Property(e => e.RegistrosLegiblesFf)
                .HasComment("Campo verifica si los registros son legibles referente al criterio formato de aforo (FF)")
                .HasColumnName("RegistrosLegiblesFF");
            entity.Property(e => e.RegistrosVisiblesBm)
                .HasComment("Campo que describe si existe registros visibles referente al criterio de bitácora de muestreo (BM)")
                .HasColumnName("RegistrosVisiblesBM");
            entity.Property(e => e.UsuarioValidoId).HasComment("Llave foránea que hace relación a la tabla de Usuarios describiendo el usuario que realizo dicha validación");

            entity.HasOne(d => d.AvisoRealizacion).WithMany(p => p.ValidacionEvidencia)
                .HasForeignKey(d => d.AvisoRealizacionId)
                .HasConstraintName("FK_ValidacionEvidencia_AvisoRealizacion");

            entity.HasOne(d => d.Muestreo).WithMany(p => p.ValidacionEvidencia)
                .HasForeignKey(d => d.MuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ValidacionEvidencia_Muestreo");

            entity.HasOne(d => d.UsuarioValido).WithMany(p => p.ValidacionEvidencia)
                .HasForeignKey(d => d.UsuarioValidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ValidacionEvidencia_Usuario");
        });

        modelBuilder.Entity<ValoresSupervisionMuestreo>(entity =>
        {
            entity.Property(e => e.Id).HasComment("Identificador principal de la tabla ValoresSupervisionMuestreo");
            entity.Property(e => e.CriterioSupervisionId).HasComment("Llave foránea que hace relación al catálogo de CriteriosSupervision");
            entity.Property(e => e.ObservacionesCriterio).HasComment("Campo que describe las observaciones del criterio");
            entity.Property(e => e.Resultado)
                .HasMaxLength(8)
                .HasComment("Campo que describe el resultado");
            entity.Property(e => e.SupervisionMuestreoId).HasComment("Llave foránea que hace relación al atabla de SupervisiónMuestreo");

            entity.HasOne(d => d.SupervisionMuestreo).WithMany(p => p.ValoresSupervisionMuestreo)
                .HasForeignKey(d => d.SupervisionMuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ValoresSupervisionMuestreo_SupervisionMuestreo");
        });

        modelBuilder.Entity<VwClaveMuestreo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_ClaveMuestreo");

            entity.Property(e => e.ClaveMuestreo).HasMaxLength(100);
        });

        modelBuilder.Entity<VwDatosGeneralesSupervision>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwDatosGeneralesSupervision");

            entity.Property(e => e.ClaveMuestreo).HasMaxLength(50);
            entity.Property(e => e.FechaMuestreo).HasColumnType("datetime");
            entity.Property(e => e.Laboratorio)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.NombreSitio).HasMaxLength(250);
            entity.Property(e => e.OcdlRealiza)
                .HasMaxLength(201)
                .IsUnicode(false);
            entity.Property(e => e.PuntajeObtenido).HasColumnType("decimal(4, 1)");
            entity.Property(e => e.TipoCuerpoAgua).HasMaxLength(50);
        });

        modelBuilder.Entity<VwDirectoresResponsablesOc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_DirectoresResponsablesOC");

            entity.Property(e => e.Anio).HasMaxLength(4);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Oc)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("OC");
            entity.Property(e => e.Ocid).HasColumnName("OCId");
            entity.Property(e => e.Puesto).HasMaxLength(224);
        });

        modelBuilder.Entity<VwEstatusMuestreosAdministracion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_EstatusMuestreosAdministracion");

            entity.Property(e => e.Etapa)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("ETAPA");
            entity.Property(e => e.TotalMuestreo).HasColumnName("TOTAL MUESTREO");
            entity.Property(e => e.TotalResultados).HasColumnName("TOTAL RESULTADOS");
        });

        modelBuilder.Entity<VwIntervalosTotalesOcDl>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwIntervalosTotalesOC_DL");

            entity.Property(e => e.Direccion).HasMaxLength(300);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.FehaMuestreo).HasColumnType("datetime");
            entity.Property(e => e.Ocdlid).HasColumnName("OCDLId");
            entity.Property(e => e.Ocid).HasColumnName("OCId");
            entity.Property(e => e.OrganismoCuencaDireccionLocal)
                .HasMaxLength(201)
                .IsUnicode(false);
            entity.Property(e => e.PuntajeObtenido).HasColumnType("decimal(4, 1)");
            entity.Property(e => e.Telefono).HasMaxLength(14);
            entity.Property(e => e._50).HasColumnName("<50");
            entity.Property(e => e._5160).HasColumnName("51-60");
            entity.Property(e => e._6170).HasColumnName("61-70");
            entity.Property(e => e._7180).HasColumnName("71-80");
            entity.Property(e => e._8185).HasColumnName("81-85");
            entity.Property(e => e._8690).HasColumnName("86-90");
            entity.Property(e => e._9195).HasColumnName("91-95");
            entity.Property(e => e._96100).HasColumnName("96-100");
        });

        modelBuilder.Entity<VwLimiteLaboratorio>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwLimiteLaboratorio");

            entity.Property(e => e.Anio)
                .HasMaxLength(4)
                .HasColumnName("anio");
            entity.Property(e => e.EsLdm).HasColumnName("EsLDM");
            entity.Property(e => e.LabMuestreo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LabSubrogado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Ldm)
                .HasMaxLength(30)
                .HasColumnName("LDM");
            entity.Property(e => e.Limite).HasMaxLength(30);
            entity.Property(e => e.Lpc)
                .HasMaxLength(30)
                .HasColumnName("LPC");
        });

        modelBuilder.Entity<VwLimiteMaximoComun>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwLimiteMaximoComun");

            entity.Property(e => e.Limite).HasMaxLength(30);
        });

        modelBuilder.Entity<VwOrganismosDirecciones>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwOrganismosDirecciones");

            entity.Property(e => e.Direccion).HasMaxLength(300);
            entity.Property(e => e.NombreOrganismoCuenca)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OrganismoCuencaDireccionLocal)
                .HasMaxLength(201)
                .IsUnicode(false);
            entity.Property(e => e.Telefono).HasMaxLength(14);
        });

        modelBuilder.Entity<VwReplicaRevisionResultado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwReplicaRevisionResultado");

            entity.Property(e => e.ClaveParametro).HasMaxLength(30);
            entity.Property(e => e.ClaveSitio).HasMaxLength(150);
            entity.Property(e => e.ClaveUnica)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ComentariosReplicaDiferente).IsUnicode(false);
            entity.Property(e => e.EsCorrectoOcdl).HasColumnName("EsCorrectoOCDL");
            entity.Property(e => e.EsCorrectoSecaia).HasColumnName("EsCorrectoSECAIA");
            entity.Property(e => e.EstatusSecaia).HasColumnName("EstatusSECAIA");
            entity.Property(e => e.FechaAprobRechazo).HasColumnType("datetime");
            entity.Property(e => e.FechaEstatusFinal).HasColumnType("datetime");
            entity.Property(e => e.FechaObservacionSrenameca)
                .HasColumnType("datetime")
                .HasColumnName("FechaObservacionSRENAMECA");
            entity.Property(e => e.Laboratorio)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.NombreSitio).HasMaxLength(250);
            entity.Property(e => e.ObservacionSrenameca)
                .IsUnicode(false)
                .HasColumnName("ObservacionSRENAMECA");
            entity.Property(e => e.ObservacionesOcdl)
                .IsUnicode(false)
                .HasColumnName("ObservacionesOCDL");
            entity.Property(e => e.ObservacionesSecaia)
                .IsUnicode(false)
                .HasColumnName("ObservacionesSECAIA");
            entity.Property(e => e.Resultado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ResultadoActualizadoReplica).IsUnicode(false);
            entity.Property(e => e.TipoCuerpoAgua).HasMaxLength(50);
            entity.Property(e => e.TipoCuerpoAguaOriginal)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwResultadosInicialReglas>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwResultadosInicialReglas");

            entity.Property(e => e.ClaveMuestreo).HasMaxLength(100);
            entity.Property(e => e.ClaveSitio).HasMaxLength(150);
            entity.Property(e => e.CuerpoDeAgua)
                .HasMaxLength(150)
                .HasColumnName("Cuerpo de agua");
            entity.Property(e => e.CumpleConLasReglasCondicionantes)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("Cumple con las Reglas condicionantes");
            entity.Property(e => e.DiferenciaEnDias).HasColumnName("Diferencia en dias");
            entity.Property(e => e.FechaEntregaTeorica)
                .HasColumnType("date")
                .HasColumnName("Fecha entrega teorica");
            entity.Property(e => e.FechaProgramada)
                .HasColumnType("date")
                .HasColumnName("Fecha programada");
            entity.Property(e => e.FechaRealizacion)
                .HasColumnType("date")
                .HasColumnName("Fecha Realizacion");
            entity.Property(e => e.Laboratorio)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MuestreoCompletoPorResultados)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("Muestreo Completo por resultados");
            entity.Property(e => e.NombreSitio).HasMaxLength(250);
            entity.Property(e => e.NumDatosEsperados).HasColumnName("Num datos esperados");
            entity.Property(e => e.NumDatosReportados).HasColumnName("Num datos reportados");
            entity.Property(e => e.NumeroCarga).HasMaxLength(10);
            entity.Property(e => e.SubtipoCuerpoDeAgua)
                .HasMaxLength(50)
                .HasColumnName("Subtipo cuerpo de agua");
            entity.Property(e => e.TipoCuerpoAgua)
                .HasMaxLength(50)
                .HasColumnName("Tipo cuerpo agua");
            entity.Property(e => e.UsuarioValido)
                .HasMaxLength(252)
                .IsUnicode(false)
                .HasColumnName("Usuario Valido");
        });

        modelBuilder.Entity<VwResultadosNoCumplenFechaEntrega>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_ResultadosNoCumplenFechaEntrega");

            entity.Property(e => e.ClaveMuestreo).HasMaxLength(100);
            entity.Property(e => e.ClaveParametro).HasMaxLength(30);
            entity.Property(e => e.FechaEntrega).HasColumnType("datetime");
            entity.Property(e => e.FechaMaxima)
                .HasColumnType("date")
                .HasColumnName("Fecha Maxima");
            entity.Property(e => e.FechaRealVisita).HasColumnType("date");
        });

        modelBuilder.Entity<VwSitios>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_Sitios");

            entity.Property(e => e.ClaveMuestreo).HasMaxLength(100);
            entity.Property(e => e.ClaveSitio).HasMaxLength(150);
            entity.Property(e => e.NombreSitio).HasMaxLength(250);
            entity.Property(e => e.TipoCuerpoAgua).HasMaxLength(50);
        });

        modelBuilder.Entity<VwValidacionEvidenciaRealizada>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_ValidacionEvidenciaRealizada");

            entity.Property(e => e.Brigada).HasMaxLength(50);
            entity.Property(e => e.ClaveMuestreo).HasMaxLength(100);
            entity.Property(e => e.ClaveSitio).HasMaxLength(150);
            entity.Property(e => e.ConQcmuestreo).HasColumnName("ConQCMuestreo");
            entity.Property(e => e.FechaProgramada).HasColumnType("date");
            entity.Property(e => e.FechaRealVisita).HasColumnType("date");
            entity.Property(e => e.FechaReprogramacion).HasColumnType("date");
            entity.Property(e => e.FechaValidacion).HasColumnType("datetime");
            entity.Property(e => e.Laboratorio)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LaboratorioMuestreo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TipoCuerpoAgua).HasMaxLength(50);
            entity.Property(e => e.TipoEventualidad).HasMaxLength(100);
            entity.Property(e => e.TipoSupervision).HasMaxLength(30);
        });

        modelBuilder.Entity<VwValidacionEvidenciaTotales>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_ValidacionEvidenciaTotales");

            entity.Property(e => e.MuestreosAprobados).HasColumnName("muestreosAprobados");
            entity.Property(e => e.MuestreosRechazados).HasColumnName("muestreosRechazados");
            entity.Property(e => e.MuestreosTotales).HasColumnName("muestreosTotales");
            entity.Property(e => e.Nomenclatura)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwValidacionEviencias>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_ValidacionEviencias");

            entity.Property(e => e.BrigadaProgramaMuestreo)
                .HasMaxLength(50)
                .HasColumnName("BRIGADA PROGRAMA MUESTREO");
            entity.Property(e => e.ClaveBrigadaArm)
                .HasMaxLength(50)
                .HasColumnName("CLAVE BRIGADA ARM");
            entity.Property(e => e.ClaveConalab)
                .HasMaxLength(150)
                .HasColumnName("Clave Conalab");
            entity.Property(e => e.ClaveConalbaArm)
                .HasMaxLength(150)
                .HasColumnName("Clave CONALBA ARM");
            entity.Property(e => e.ClaveMuestreo)
                .HasMaxLength(100)
                .HasColumnName("Clave Muestreo");
            entity.Property(e => e.ClaveMuestreoArm)
                .HasMaxLength(100)
                .HasColumnName("CLAVE MUESTREO ARM");
            entity.Property(e => e.ConQcmuestreo).HasColumnName("ConQCMuestreo");
            entity.Property(e => e.CumpleClaveBrigada)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CUMPLE CLAVE BRIGADA");
            entity.Property(e => e.CumpleClaveConalab)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CUMPLE CLAVE CONALAB");
            entity.Property(e => e.CumpleClaveMuestreo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CUMPLE CLAVE MUESTREO");
            entity.Property(e => e.CumpleEvidencias)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CUMPLE EVIDENCIAS");
            entity.Property(e => e.CumpleFechaRealizacion)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("CUMPLE FECHA REALIZACION");
            entity.Property(e => e.CumpleTiempoMuestreo)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.EvidenciasEsperadas).HasColumnName("Evidencias esperadas");
            entity.Property(e => e.FechaProgramadaVisita)
                .HasColumnType("date")
                .HasColumnName("Fecha Programada Visita");
            entity.Property(e => e.FechaRealVisita)
                .HasColumnType("date")
                .HasColumnName("Fecha Real Visita");
            entity.Property(e => e.FechaRealizacion)
                .HasColumnType("date")
                .HasColumnName("Fecha Realizacion");
            entity.Property(e => e.FechaReprogramacion).HasColumnType("date");
            entity.Property(e => e.HoraFinMuestreo).HasColumnName("Hora Fin Muestreo");
            entity.Property(e => e.HoraIncioMuestreo).HasColumnName("Hora Incio Muestreo");
            entity.Property(e => e.Laboratorio)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Lat1MuestreoPrograma).HasColumnName("LAT 1 MUESTREO PROGRAMA");
            entity.Property(e => e.LatSitioResultado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LAT SITIO RESULTADO");
            entity.Property(e => e.LiderBrigadaArm)
                .HasMaxLength(100)
                .HasColumnName("LIDER BRIGADA ARM");
            entity.Property(e => e.LiderBrigadaBase)
                .HasMaxLength(100)
                .HasColumnName("LIDER BRIGADA BASE");
            entity.Property(e => e.Log1MuestreoPrograma).HasColumnName("LOG 1 MUESTREO PROGRAMA");
            entity.Property(e => e.LongSitioResultado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LONG SITIO RESULTADO");
            entity.Property(e => e.PlacasDeMuestreo)
                .HasMaxLength(10)
                .HasColumnName("PLACAS DE MUESTREO");
            entity.Property(e => e.Sitio).HasMaxLength(250);
            entity.Property(e => e.TiempoMinimoMuestreo).HasColumnName("Tiempo Minimo Muestreo");
            entity.Property(e => e.TipoCuerpoAgua)
                .HasMaxLength(50)
                .HasColumnName("Tipo Cuerpo Agua");
            entity.Property(e => e.TipoEventualidad).HasMaxLength(100);
            entity.Property(e => e.TipoSupervision)
                .HasMaxLength(30)
                .HasColumnName("Tipo Supervision");
            entity.Property(e => e.TotalEvidencias).HasColumnName("Total evidencias");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
