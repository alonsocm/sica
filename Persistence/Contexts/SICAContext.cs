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

    public virtual DbSet<AprobacionResultadoMuestreo> AprobacionResultadoMuestreo { get; set; }

    public virtual DbSet<ArchivoInformeMensualSupervision> ArchivoInformeMensualSupervision { get; set; }

    public virtual DbSet<BrigadaMuestreo> BrigadaMuestreo { get; set; }

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

    public virtual DbSet<EvidenciaMuestreo> EvidenciaMuestreo { get; set; }

    public virtual DbSet<EvidenciaReplica> EvidenciaReplica { get; set; }

    public virtual DbSet<EvidenciaSupervisionMuestreo> EvidenciaSupervisionMuestreo { get; set; }

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

    public virtual DbSet<ResultadoMuestreo> ResultadoMuestreo { get; set; }

    public virtual DbSet<Sitio> Sitio { get; set; }

    public virtual DbSet<SubgrupoAnalitico> SubgrupoAnalitico { get; set; }

    public virtual DbSet<SubtipoCuerpoAgua> SubtipoCuerpoAgua { get; set; }

    public virtual DbSet<SupervisionMuestreo> SupervisionMuestreo { get; set; }

    public virtual DbSet<TipoAprobacion> TipoAprobacion { get; set; }

    public virtual DbSet<TipoArchivoInformeMensualSupervision> TipoArchivoInformeMensualSupervision { get; set; }

    public virtual DbSet<TipoCuerpoAgua> TipoCuerpoAgua { get; set; }

    public virtual DbSet<TipoEvidenciaMuestreo> TipoEvidenciaMuestreo { get; set; }

    public virtual DbSet<TipoHomologado> TipoHomologado { get; set; }

    public virtual DbSet<TipoRegla> TipoRegla { get; set; }

    public virtual DbSet<TipoSitio> TipoSitio { get; set; }

    public virtual DbSet<TipoSustitucion> TipoSustitucion { get; set; }

    public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<ValoresSupervisionMuestreo> ValoresSupervisionMuestreo { get; set; }

    public virtual DbSet<VwClaveMuestreo> VwClaveMuestreo { get; set; }

    public virtual DbSet<VwDatosGeneralesSupervision> VwDatosGeneralesSupervision { get; set; }

    public virtual DbSet<VwDirectoresResponsablesOc> VwDirectoresResponsablesOc { get; set; }

    public virtual DbSet<VwIntervalosTotalesOcDl> VwIntervalosTotalesOcDl { get; set; }

    public virtual DbSet<VwLimiteLaboratorio> VwLimiteLaboratorio { get; set; }

    public virtual DbSet<VwLimiteMaximoComun> VwLimiteMaximoComun { get; set; }

    public virtual DbSet<VwOrganismosDirecciones> VwOrganismosDirecciones { get; set; }

    public virtual DbSet<VwReplicaRevisionResultado> VwReplicaRevisionResultado { get; set; }

    public virtual DbSet<VwResultadosInicialReglas> VwResultadosInicialReglas { get; set; }

    public virtual DbSet<VwSitios> VwSitios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DbConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accion>(entity =>
        {
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AccionLaboratorio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AccionSubrogado");

            entity.Property(e => e.Descripcion).HasMaxLength(30);
            entity.Property(e => e.LoSubroga).HasMaxLength(10);
        });

        modelBuilder.Entity<AprobacionResultadoMuestreo>(entity =>
        {
            entity.HasIndex(e => e.ResultadoMuestreoId, "IX_AprobacionResultadoMuestreo_ResultadoMuestreoId");

            entity.HasIndex(e => e.UsuarioRevisionId, "IX_AprobacionResultadoMuestreo_UsuarioRevisionId");

            entity.Property(e => e.FechaAprobRechazo).HasColumnType("datetime");

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
            entity.Property(e => e.FechaCarga).HasColumnType("datetime");
            entity.Property(e => e.NombreArchivo).IsUnicode(false);

            entity.HasOne(d => d.InformeMensualSupervision).WithMany(p => p.ArchivoInformeMensualSupervision)
                .HasForeignKey(d => d.InformeMensualSupervisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArchivoInformeMensualSupervision_InformeMensualSupervision");

            entity.HasOne(d => d.TipoArchivoInformeMensualSupervision).WithMany(p => p.ArchivoInformeMensualSupervision)
                .HasForeignKey(d => d.TipoArchivoInformeMensualSupervisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArchivoInformeMensualSupervision_TipoArchivoInformeMensualSupervision");

            entity.HasOne(d => d.UsuarioCarga).WithMany(p => p.ArchivoInformeMensualSupervision)
                .HasForeignKey(d => d.UsuarioCargaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArchivoInformeMensualSupervision_Usuario");
        });

        modelBuilder.Entity<BrigadaMuestreo>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<ClasificacionCriterio>(entity =>
        {
            entity.ToTable("ClasificacionCriterio", "cat");

            entity.Property(e => e.Descripcion).HasMaxLength(100);
        });

        modelBuilder.Entity<ClasificacionRegla>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CloasificacionRegla");

            entity.Property(e => e.Descripcion).HasMaxLength(30);
        });

        modelBuilder.Entity<CopiaInformeMensualSupervision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CopiaReporteInformeMensual");

            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Puesto).HasMaxLength(100);

            entity.HasOne(d => d.InformeMensualSupervision).WithMany(p => p.CopiaInformeMensualSupervision)
                .HasForeignKey(d => d.InformeMensualSupervisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CopiaReporteInformeMensual_ReporteInformeMensual");
        });

        modelBuilder.Entity<CriteriosSupervisionMuestreo>(entity =>
        {
            entity.ToTable("CriteriosSupervisionMuestreo", "cat");

            entity.Property(e => e.Descripcion).HasMaxLength(250);
            entity.Property(e => e.Valor).HasColumnType("decimal(7, 1)");

            entity.HasOne(d => d.ClasificacionCriterio).WithMany(p => p.CriteriosSupervisionMuestreo)
                .HasForeignKey(d => d.ClasificacionCriterioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CriteriosSupervisionMuestreo_ClasificacionCriterio");
        });

        modelBuilder.Entity<CuencaDireccionesLocales>(entity =>
        {
            entity.HasIndex(e => e.DlocalId, "IX_CuencaDireccionesLocales_DLocalId");

            entity.HasIndex(e => e.OcuencaId, "IX_CuencaDireccionesLocales_OCuencaId");

            entity.Property(e => e.DlocalId).HasColumnName("DLocalId");
            entity.Property(e => e.OcuencaId).HasColumnName("OCuencaId");

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
            entity.Property(e => e.Descripcion).HasMaxLength(150);
        });

        modelBuilder.Entity<CuerpoTipoSubtipoAgua>(entity =>
        {
            entity.HasIndex(e => e.CuerpoAguaId, "IX_CuerpoTipoSubtipoAgua_CuerpoAguaId");

            entity.HasIndex(e => e.SubtipoCuerpoAguaId, "IX_CuerpoTipoSubtipoAgua_SubtipoCuerpoAguaId");

            entity.HasIndex(e => e.TipoCuerpoAguaId, "IX_CuerpoTipoSubtipoAgua_TipoCuerpoAguaId");

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

            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<DireccionLocal>(entity =>
        {
            entity.Property(e => e.Clave).HasMaxLength(10);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Directorio>(entity =>
        {
            entity.ToTable("Directorio", "cat");

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Sexo).HasMaxLength(2);

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

            entity.Property(e => e.ClaveMunicipio).HasMaxLength(50);
            entity.Property(e => e.ClaveSitio).HasMaxLength(200);
            entity.Property(e => e.Cuenca).HasMaxLength(100);
            entity.Property(e => e.CuerpoAgua).HasMaxLength(100);
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FechaRealizacion).HasColumnType("datetime");
            entity.Property(e => e.Latitud).HasMaxLength(50);
            entity.Property(e => e.Longitud).HasMaxLength(50);
            entity.Property(e => e.Municipio).HasMaxLength(100);
            entity.Property(e => e.NombreEmergencia).HasMaxLength(200);
            entity.Property(e => e.NombreSitio).HasMaxLength(200);
            entity.Property(e => e.OrganismoCuenca).HasMaxLength(100);
            entity.Property(e => e.SubTipoCuerpoAgua).HasMaxLength(100);
            entity.Property(e => e.TipoCuerpoAgua).HasMaxLength(100);
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.Property(e => e.Abreviatura)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstatusMuestreo>(entity =>
        {
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EvidenciaMuestreo>(entity =>
        {
            entity.HasIndex(e => e.MuestreoId, "IX_EvidenciaMuestreo_MuestreoId");

            entity.HasIndex(e => e.TipoEvidenciaMuestreoId, "IX_EvidenciaMuestreo_TipoEvidenciaMuestreoId");

            entity.Property(e => e.NombreArchivo).IsUnicode(false);

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

            entity.Property(e => e.ClaveUnica)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreArchivo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.ResultadoMuestreo).WithMany(p => p.EvidenciaReplica)
                .HasForeignKey(d => d.ResultadoMuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EvidenciaReplica_ResultadoMuestreo");
        });

        modelBuilder.Entity<EvidenciaSupervisionMuestreo>(entity =>
        {
            entity.Property(e => e.NombreArchivo).IsUnicode(false);

            entity.HasOne(d => d.SupervisionMuestreo).WithMany(p => p.EvidenciaSupervisionMuestreo)
                .HasForeignKey(d => d.SupervisionMuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EvidenciaSupervisionMuestreo_SupervisionMuestreo");

            entity.HasOne(d => d.TipoEvidencia).WithMany(p => p.EvidenciaSupervisionMuestreo)
                .HasForeignKey(d => d.TipoEvidenciaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EvidenciaSupervisionMuestreo_TipoEvidenciaMuestreo");
        });

        modelBuilder.Entity<FormaReporteEspecifica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FormaReporte");

            entity.Property(e => e.Descripcion).HasMaxLength(60);

            entity.HasOne(d => d.Parametro).WithMany(p => p.FormaReporteEspecifica)
                .HasForeignKey(d => d.ParametroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FormaReporteEspecifica_ParametrosGrupo");
        });

        modelBuilder.Entity<GrupoParametro>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(15);
        });

        modelBuilder.Entity<HistorialSustitucionEmergencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_HistorialSustitucionEmergencia");

            entity.Property(e => e.Fecha).HasColumnType("datetime");

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

            entity.Property(e => e.Fecha).HasColumnType("datetime");

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

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Iniciales).HasMaxLength(200);
            entity.Property(e => e.Lugar).HasMaxLength(200);
            entity.Property(e => e.Memorando).HasMaxLength(50);

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

            entity.Property(e => e.Descripcion).HasMaxLength(7);
        });

        modelBuilder.Entity<Laboratorios>(entity =>
        {
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nomenclatura)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LimiteParametroLaboratorio>(entity =>
        {
            entity.Property(e => e.Ldm)
                .HasMaxLength(30)
                .HasColumnName("LDM");
            entity.Property(e => e.LdmaCumplir)
                .HasMaxLength(30)
                .HasColumnName("LDMaCumplir");
            entity.Property(e => e.Lpc)
                .HasMaxLength(30)
                .HasColumnName("LPC");
            entity.Property(e => e.LpcaCumplir)
                .HasMaxLength(30)
                .HasColumnName("LPCaCumplir");
            entity.Property(e => e.MetodoAnalitico).HasMaxLength(250);

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

            entity.HasOne(d => d.RealizaLaboratorioMuestreo).WithMany(p => p.LimiteParametroLaboratorioRealizaLaboratorioMuestreo)
                .HasForeignKey(d => d.RealizaLaboratorioMuestreoId)
                .HasConstraintName("FK_LimiteParametroLaboratorio_AccionSubrogado1");
        });

        modelBuilder.Entity<Localidad>(entity =>
        {
            entity.HasIndex(e => e.EstadoId, "IX_Localidad_EstadoId");

            entity.HasIndex(e => e.MunicipioId, "IX_Localidad_MunicipioId");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

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

            entity.Property(e => e.Descripcion).HasMaxLength(10);
        });

        modelBuilder.Entity<Muestreadores>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Muestradores");

            entity.Property(e => e.ApellidoMaterno).HasMaxLength(50);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(50);
            entity.Property(e => e.Iniciales).HasMaxLength(5);
            entity.Property(e => e.Nombre).HasMaxLength(150);
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

            entity.Property(e => e.EstatusOcdl).HasColumnName("EstatusOCDL");
            entity.Property(e => e.EstatusSecaia).HasColumnName("EstatusSECAIA");
            entity.Property(e => e.FechaCarga).HasColumnType("datetime");
            entity.Property(e => e.FechaCargaEvidencias).HasColumnType("datetime");
            entity.Property(e => e.FechaLimiteRevision).HasColumnType("date");
            entity.Property(e => e.FechaRealVisita).HasColumnType("date");
            entity.Property(e => e.FechaRevisionOcdl)
                .HasColumnType("date")
                .HasColumnName("FechaRevisionOCDL");
            entity.Property(e => e.FechaRevisionSecaia)
                .HasColumnType("date")
                .HasColumnName("FechaRevisionSECAIA");
            entity.Property(e => e.UsuarioRevisionOcdlid).HasColumnName("UsuarioRevisionOCDLId");
            entity.Property(e => e.UsuarioRevisionSecaiaid).HasColumnName("UsuarioRevisionSECAIAId");

            entity.HasOne(d => d.Estatus).WithMany(p => p.MuestreoEstatus)
                .HasForeignKey(d => d.EstatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Muestreo_EstatusMuestreo");

            entity.HasOne(d => d.EstatusOcdlNavigation).WithMany(p => p.MuestreoEstatusOcdlNavigation)
                .HasForeignKey(d => d.EstatusOcdl)
                .HasConstraintName("FK_Muestreo_EstatusMuestreo1");

            entity.HasOne(d => d.EstatusSecaiaNavigation).WithMany(p => p.MuestreoEstatusSecaiaNavigation)
                .HasForeignKey(d => d.EstatusSecaia)
                .HasConstraintName("FK_Muestreo_EstatusMuestreo2");

            entity.HasOne(d => d.ProgramaMuestreo).WithMany(p => p.Muestreo)
                .HasForeignKey(d => d.ProgramaMuestreoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Muestreo_ProgramaMuestreo");

            entity.HasOne(d => d.TipoAprobacion).WithMany(p => p.Muestreo)
                .HasForeignKey(d => d.TipoAprobacionId)
                .HasConstraintName("FK_Muestreo_TipoAprobacion");

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

            entity.Property(e => e.ClaveUnica).HasMaxLength(150);
            entity.Property(e => e.FechaProgramada).HasColumnType("date");
            entity.Property(e => e.FechaRealVisita).HasColumnType("date");
            entity.Property(e => e.HoraMuestreo).HasMaxLength(20);
            entity.Property(e => e.IdLaboratorio).HasMaxLength(10);
            entity.Property(e => e.LaboratorioRealizoMuestreo).HasMaxLength(100);
            entity.Property(e => e.LaboratorioSubrogado).HasMaxLength(100);
            entity.Property(e => e.NombreEmergencia).HasMaxLength(250);
            entity.Property(e => e.Numero).HasMaxLength(10);
            entity.Property(e => e.Resultado).HasMaxLength(50);
            entity.Property(e => e.ResultadoSustituidoPorLimite).HasMaxLength(50);
            entity.Property(e => e.Sitio).HasMaxLength(150);
            entity.Property(e => e.SubtipoCuerpoAgua).HasMaxLength(100);
            entity.Property(e => e.TipoCuerpoAgua).HasMaxLength(100);

            entity.HasOne(d => d.Parametro).WithMany(p => p.MuestreoEmergencia)
                .HasForeignKey(d => d.ParametroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MuestreoEmergencia_ParametroGrupo");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasIndex(e => e.EstadoId, "IX_Municipio_EstadoId");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Estado).WithMany(p => p.Municipio)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Municipio__Estad__4E88ABD4");
        });

        modelBuilder.Entity<Observaciones>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(100);
        });

        modelBuilder.Entity<OrganismoCuenca>(entity =>
        {
            entity.Property(e => e.Clave).HasMaxLength(10);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(300)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.Telefono)
                .HasMaxLength(14)
                .HasDefaultValueSql("('')");
        });

        modelBuilder.Entity<Pagina>(entity =>
        {
            entity.HasIndex(e => e.IdPaginaPadre, "IX_Pagina_IdPaginaPadre");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Url).IsUnicode(false);

            entity.HasOne(d => d.IdPaginaPadreNavigation).WithMany(p => p.InverseIdPaginaPadreNavigation)
                .HasForeignKey(d => d.IdPaginaPadre)
                .HasConstraintName("FK_Pagina_Pagina");
        });

        modelBuilder.Entity<ParametrosCostos>(entity =>
        {
            entity.Property(e => e.Precio).HasColumnType("decimal(6, 2)");

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

            entity.Property(e => e.ClaveParametro).HasMaxLength(30);
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.EsLdm).HasColumnName("EsLDM");

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

            entity.HasOne(d => d.Parametro).WithMany(p => p.ParametrosReglasNoRelacion)
                .HasForeignKey(d => d.ParametroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParametrosReglasNORelacion_ParametrosGrupo");
        });

        modelBuilder.Entity<ParametrosSitioTipoCuerpoAgua>(entity =>
        {
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
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PerfilPagina>(entity =>
        {
            entity.HasIndex(e => e.IdPagina, "IX_PerfilPagina_IdPagina");

            entity.HasIndex(e => e.IdPerfil, "IX_PerfilPagina_IdPerfil");

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

            entity.Property(e => e.Anio).HasMaxLength(4);
            entity.Property(e => e.Contrato).HasMaxLength(200);
            entity.Property(e => e.DenominacionContrato).HasMaxLength(300);
            entity.Property(e => e.ImagenEncabezado).IsUnicode(false);
            entity.Property(e => e.ImagenPiePagina).IsUnicode(false);
            entity.Property(e => e.SitiosMiniMax).HasMaxLength(30);
        });

        modelBuilder.Entity<ProgramaAnio>(entity =>
        {
            entity.Property(e => e.Anio).HasMaxLength(4);
        });

        modelBuilder.Entity<ProgramaMuestreo>(entity =>
        {
            entity.HasIndex(e => e.ProgramaSitioId, "IX_ProgramaMuestreo_ProgramaSitioId");

            entity.Property(e => e.DiaProgramado).HasColumnType("date");
            entity.Property(e => e.DomingoSemanaProgramada).HasColumnType("date");
            entity.Property(e => e.NombreCorrectoArchivo).HasMaxLength(100);

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

            entity.Property(e => e.Observaciones).HasMaxLength(500);

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

            entity.Property(e => e.Descripcion).HasMaxLength(120);
        });

        modelBuilder.Entity<ReglaReporteResultadoTca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ResultadoTCAReglaReporte");

            entity.ToTable("ReglaReporteResultadoTCA");

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

            entity.Property(e => e.ClaveUnicaLabParametro).HasMaxLength(50);
            entity.Property(e => e.EsLdm).HasColumnName("EsLDM");
            entity.Property(e => e.Ldm)
                .HasMaxLength(20)
                .HasColumnName("LDM");
            entity.Property(e => e.Lpc)
                .HasMaxLength(20)
                .HasColumnName("LPC");

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
            entity.Property(e => e.ClaveRegla).HasMaxLength(10);
            entity.Property(e => e.Leyenda).HasMaxLength(80);
            entity.Property(e => e.MinimoMaximo).HasMaxLength(35);

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
            entity.Property(e => e.ClaveRegla).HasMaxLength(10);
            entity.Property(e => e.Leyenda).HasMaxLength(110);
            entity.Property(e => e.RelacionRegla).HasMaxLength(200);
        });

        modelBuilder.Entity<ReglasRelacionParametro>(entity =>
        {
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
            entity.Property(e => e.ClaveRegla).HasMaxLength(10);
            entity.Property(e => e.EsValidoResultadoIm).HasColumnName("EsValidoResultadoIM");
            entity.Property(e => e.EsValidoResultadoMenorCmc).HasColumnName("EsValidoResultadoMenorCMC");
            entity.Property(e => e.EsValidoResultadoMenorLd).HasColumnName("EsValidoResultadoMenorLD");
            entity.Property(e => e.EsValidoResultadoMenorLpc).HasColumnName("EsValidoResultadoMenorLPC");
            entity.Property(e => e.EsValidoResultadoNa).HasColumnName("EsValidoResultadoNA");
            entity.Property(e => e.EsValidoResultadoNd).HasColumnName("EsValidoResultadoND");
            entity.Property(e => e.EsValidoResultadoNe).HasColumnName("EsValidoResultadoNE");

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
            entity.Property(e => e.Leyenda).HasMaxLength(50);
            entity.Property(e => e.ReglaReporte).HasMaxLength(50);
        });

        modelBuilder.Entity<ResultadoMuestreo>(entity =>
        {
            entity.HasIndex(e => e.EstatusResultado, "IX_ResultadoMuestreo_EstatusResultado");

            entity.HasIndex(e => e.MuestreoId, "IX_ResultadoMuestreo_MuestreoId");

            entity.HasIndex(e => e.ObservacionSrenamecaid, "IX_ResultadoMuestreo_ObservacionSRENAMECAId");

            entity.HasIndex(e => e.ObservacionesOcdlid, "IX_ResultadoMuestreo_ObservacionesOCDLId");

            entity.HasIndex(e => e.ObservacionesSecaiaid, "IX_ResultadoMuestreo_ObservacionesSECAIAId");

            entity.HasIndex(e => e.ParametroId, "IX_ResultadoMuestreo_ParametroId");

            entity.Property(e => e.CausaRechazo).IsUnicode(false);
            entity.Property(e => e.Comentarios).IsUnicode(false);
            entity.Property(e => e.EsCorrectoOcdl).HasColumnName("EsCorrectoOCDL");
            entity.Property(e => e.EsCorrectoSecaia).HasColumnName("EsCorrectoSECAIA");
            entity.Property(e => e.FechaEntrega).HasColumnType("datetime");
            entity.Property(e => e.FechaEstatusFinal).HasColumnType("datetime");
            entity.Property(e => e.FechaObservacionSrenameca)
                .HasColumnType("datetime")
                .HasColumnName("FechaObservacionSRENAMECA");
            entity.Property(e => e.FechaReplicaLaboratorio).HasColumnType("datetime");
            entity.Property(e => e.ObservacionLaboratorio).IsUnicode(false);
            entity.Property(e => e.ObservacionSrenameca)
                .IsUnicode(false)
                .HasColumnName("ObservacionSRENAMECA");
            entity.Property(e => e.ObservacionSrenamecaid).HasColumnName("ObservacionSRENAMECAId");
            entity.Property(e => e.ObservacionesOcdl)
                .IsUnicode(false)
                .HasColumnName("ObservacionesOCDL");
            entity.Property(e => e.ObservacionesOcdlid).HasColumnName("ObservacionesOCDLId");
            entity.Property(e => e.ObservacionesSecaia)
                .IsUnicode(false)
                .HasColumnName("ObservacionesSECAIA");
            entity.Property(e => e.ObservacionesSecaiaid).HasColumnName("ObservacionesSECAIAId");
            entity.Property(e => e.Resultado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ResultadoActualizadoReplica).IsUnicode(false);
            entity.Property(e => e.ResultadoReglas)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ResultadoReplica).IsUnicode(false);
            entity.Property(e => e.ResultadoSustituidoPorLaboratorio).HasMaxLength(50);
            entity.Property(e => e.ResultadoSustituidoPorLimite).HasMaxLength(50);

            entity.HasOne(d => d.EstatusResultadoNavigation).WithMany(p => p.ResultadoMuestreo)
                .HasForeignKey(d => d.EstatusResultado)
                .HasConstraintName("FK_ResultadoMuestreo_EstatusMuestreo");

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

            entity.Property(e => e.ClaveSitio).HasMaxLength(150);
            entity.Property(e => e.DireccionLrevisionId).HasColumnName("DireccionLRevisionId");
            entity.Property(e => e.NombreSitio).HasMaxLength(250);
            entity.Property(e => e.Observaciones).HasMaxLength(500);

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
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<SubtipoCuerpoAgua>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<SupervisionMuestreo>(entity =>
        {
            entity.Property(e => e.ClaveMuestreo).HasMaxLength(50);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.FehaMuestreo).HasColumnType("datetime");
            entity.Property(e => e.PuntajeObtenido).HasColumnType("decimal(4, 1)");
            entity.Property(e => e.SupervisorConagua).HasMaxLength(100);

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
            entity.Property(e => e.Descripcion).HasMaxLength(20);
        });

        modelBuilder.Entity<TipoArchivoInformeMensualSupervision>(entity =>
        {
            entity.ToTable("TipoArchivoInformeMensualSupervision", "cat");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoCuerpoAgua>(entity =>
        {
            entity.HasIndex(e => e.TipoHomologadoId, "IX_TipoCuerpoAgua_TipoHomologadoId");

            entity.Property(e => e.Descripcion).HasMaxLength(50);

            entity.HasOne(d => d.TipoHomologado).WithMany(p => p.TipoCuerpoAgua)
                .HasForeignKey(d => d.TipoHomologadoId)
                .HasConstraintName("FK_TipoCuerpoAgua_TipoHomologado");
        });

        modelBuilder.Entity<TipoEvidenciaMuestreo>(entity =>
        {
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Extension)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Sufijo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<TipoHomologado>(entity =>
        {
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoRegla>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(10);
        });

        modelBuilder.Entity<TipoSitio>(entity =>
        {
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoSustitucion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoSust__3214EC0762844A01");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UnidadMedida>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(30);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasIndex(e => e.CuencaId, "IX_Usuario_CuencaId");

            entity.HasIndex(e => e.DireccionLocalId, "IX_Usuario_DireccionLocalId");

            entity.HasIndex(e => e.PerfilId, "IX_Usuario_PerfilId");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false);

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

        modelBuilder.Entity<ValoresSupervisionMuestreo>(entity =>
        {
            entity.Property(e => e.Resultado).HasMaxLength(8);

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

        modelBuilder.Entity<VwIntervalosTotalesOcDl>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwIntervalosTotalesOC_DL");

            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Ocdlid).HasColumnName("OCDLId");
            entity.Property(e => e.Ocid).HasColumnName("OCId");
            entity.Property(e => e.OrganismoCuencaDireccionLocal)
                .HasMaxLength(201)
                .IsUnicode(false);
            entity.Property(e => e.PuntajeObtenido).HasColumnType("decimal(4, 1)");
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

            entity.Property(e => e.NombreOrganismoCuenca)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OrganismoCuencaDireccionLocal)
                .HasMaxLength(201)
                .IsUnicode(false);
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
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.MuestreoCompletoPorResultados)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("Muestreo Completo por resultados");
            entity.Property(e => e.NombreSitio).HasMaxLength(250);
            entity.Property(e => e.NumDatosEsperados).HasColumnName("Num datos esperados");
            entity.Property(e => e.NumDatosReportados).HasColumnName("Num datos reportados");
            entity.Property(e => e.SubtipoCuerpoDeAgua)
                .HasMaxLength(50)
                .HasColumnName("Subtipo cuerpo de agua");
            entity.Property(e => e.TipoCuerpoAgua)
                .HasMaxLength(50)
                .HasColumnName("Tipo cuerpo agua");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
