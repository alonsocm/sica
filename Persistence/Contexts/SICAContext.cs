﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Domain.Entities;

namespace Persistence.Contexts
{
    public partial class SICAContext : DbContext
    {
        public SICAContext()
        {
        }

        public SICAContext(DbContextOptions<SICAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accion> Accion { get; set; } = null!;
        public virtual DbSet<AprobacionResultadoMuestreo> AprobacionResultadoMuestreo { get; set; } = null!;
        public virtual DbSet<CuencaDireccionesLocales> CuencaDireccionesLocales { get; set; } = null!;
        public virtual DbSet<CuerpoAgua> CuerpoAgua { get; set; } = null!;
        public virtual DbSet<CuerpoTipoSubtipoAgua> CuerpoTipoSubtipoAgua { get; set; } = null!;
        public virtual DbSet<DireccionLocal> DireccionLocal { get; set; } = null!;
        public virtual DbSet<Estado> Estado { get; set; } = null!;
        public virtual DbSet<EstatusMuestreo> EstatusMuestreo { get; set; } = null!;
        public virtual DbSet<EvidenciaMuestreo> EvidenciaMuestreo { get; set; } = null!;
        public virtual DbSet<EvidenciaReplica> EvidenciaReplica { get; set; } = null!;
        public virtual DbSet<Laboratorios> Laboratorios { get; set; } = null!;
        public virtual DbSet<Localidad> Localidad { get; set; } = null!;
        public virtual DbSet<Muestreo> Muestreo { get; set; } = null!;
        public virtual DbSet<Municipio> Municipio { get; set; } = null!;
        public virtual DbSet<Observaciones> Observaciones { get; set; } = null!;
        public virtual DbSet<OrganismoCuenca> OrganismoCuenca { get; set; } = null!;
        public virtual DbSet<Pagina> Pagina { get; set; } = null!;
        public virtual DbSet<ParametrosGrupo> ParametrosGrupo { get; set; } = null!;
        public virtual DbSet<Perfil> Perfil { get; set; } = null!;
        public virtual DbSet<PerfilPagina> PerfilPagina { get; set; } = null!;
        public virtual DbSet<PerfilPaginaAccion> PerfilPaginaAccion { get; set; } = null!;
        public virtual DbSet<ProgramaMuestreo> ProgramaMuestreo { get; set; } = null!;
        public virtual DbSet<ProgramaSitio> ProgramaSitio { get; set; } = null!;
        public virtual DbSet<ResultadoMuestreo> ResultadoMuestreo { get; set; } = null!;
        public virtual DbSet<Sitio> Sitio { get; set; } = null!;
        public virtual DbSet<SubgrupoAnalitico> SubgrupoAnalitico { get; set; } = null!;
        public virtual DbSet<SubtipoCuerpoAgua> SubtipoCuerpoAgua { get; set; } = null!;
        public virtual DbSet<TipoAprobacion> TipoAprobacion { get; set; } = null!;
        public virtual DbSet<TipoCuerpoAgua> TipoCuerpoAgua { get; set; } = null!;
        public virtual DbSet<TipoEvidenciaMuestreo> TipoEvidenciaMuestreo { get; set; } = null!;
        public virtual DbSet<TipoHomologado> TipoHomologado { get; set; } = null!;
        public virtual DbSet<TipoSitio> TipoSitio { get; set; } = null!;
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; } = null!;
        public virtual DbSet<Usuario> Usuario { get; set; } = null!;
        public virtual DbSet<VwClaveMuestreo> VwClaveMuestreo { get; set; } = null!;
        public virtual DbSet<VwReplicaRevisionResultado> VwReplicaRevisionResultado { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DbConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accion>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AprobacionResultadoMuestreo>(entity =>
            {
                entity.Property(e => e.FechaAprobRechazo).HasColumnType("datetime");

                entity.HasOne(d => d.ResultadoMuestreo)
                    .WithMany(p => p.AprobacionResultadoMuestreo)
                    .HasForeignKey(d => d.ResultadoMuestreoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AprobacionResultadoMuestreo_Muestreo");

                entity.HasOne(d => d.UsuarioRevision)
                    .WithMany(p => p.AprobacionResultadoMuestreo)
                    .HasForeignKey(d => d.UsuarioRevisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AprobacionResultadoMuestreo_Usuario");
            });

            modelBuilder.Entity<CuencaDireccionesLocales>(entity =>
            {
                entity.Property(e => e.DlocalId).HasColumnName("DLocalId");

                entity.Property(e => e.OcuencaId).HasColumnName("OCuencaId");

                entity.HasOne(d => d.Dlocal)
                    .WithMany(p => p.CuencaDireccionesLocales)
                    .HasForeignKey(d => d.DlocalId)
                    .HasConstraintName("FK_CuencaDireccionesLocales_DireccionLocal");

                entity.HasOne(d => d.Ocuenca)
                    .WithMany(p => p.CuencaDireccionesLocales)
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
                entity.HasOne(d => d.CuerpoAgua)
                    .WithMany(p => p.CuerpoTipoSubtipoAgua)
                    .HasForeignKey(d => d.CuerpoAguaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CuerpoTipoSubtipoAgua_CuerpoAgua");

                entity.HasOne(d => d.SubtipoCuerpoAgua)
                    .WithMany(p => p.CuerpoTipoSubtipoAgua)
                    .HasForeignKey(d => d.SubtipoCuerpoAguaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CuerpoTipoSubtipoAgua_SubtipoCuerpoAgua");

                entity.HasOne(d => d.TipoCuerpoAgua)
                    .WithMany(p => p.CuerpoTipoSubtipoAgua)
                    .HasForeignKey(d => d.TipoCuerpoAguaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CuerpoTipoSubtipoAgua_TipoCuerpoAgua");
            });

            modelBuilder.Entity<DireccionLocal>(entity =>
            {
                entity.Property(e => e.Clave).HasMaxLength(10);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);
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
                entity.Property(e => e.NombreArchivo).IsUnicode(false);

                entity.HasOne(d => d.Muestreo)
                    .WithMany(p => p.EvidenciaMuestreo)
                    .HasForeignKey(d => d.MuestreoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvidenciaMuestreo_Muestreo");

                entity.HasOne(d => d.TipoEvidenciaMuestreo)
                    .WithMany(p => p.EvidenciaMuestreo)
                    .HasForeignKey(d => d.TipoEvidenciaMuestreoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvidenciaMuestreo_TipoEvidenciaMuestreo");
            });

            modelBuilder.Entity<EvidenciaReplica>(entity =>
            {
                entity.Property(e => e.ClaveUnica)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombreArchivo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ResultadoMuestreo)
                    .WithMany(p => p.EvidenciaReplica)
                    .HasForeignKey(d => d.ResultadoMuestreoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvidenciaReplica_ResultadoMuestreo");
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

            modelBuilder.Entity<Localidad>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Localidad)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Localidad__Estad__571DF1D5");

                entity.HasOne(d => d.Municipio)
                    .WithMany(p => p.Localidad)
                    .HasForeignKey(d => d.MunicipioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Localidad__Munic__5812160E");
            });

            modelBuilder.Entity<Muestreo>(entity =>
            {
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

                entity.HasOne(d => d.Estatus)
                    .WithMany(p => p.MuestreoEstatus)
                    .HasForeignKey(d => d.EstatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Muestreo_EstatusMuestreo");

                entity.HasOne(d => d.EstatusOcdlNavigation)
                    .WithMany(p => p.MuestreoEstatusOcdlNavigation)
                    .HasForeignKey(d => d.EstatusOcdl)
                    .HasConstraintName("FK_Muestreo_EstatusMuestreo1");

                entity.HasOne(d => d.EstatusSecaiaNavigation)
                    .WithMany(p => p.MuestreoEstatusSecaiaNavigation)
                    .HasForeignKey(d => d.EstatusSecaia)
                    .HasConstraintName("FK_Muestreo_EstatusMuestreo2");

                entity.HasOne(d => d.ProgramaMuestreo)
                    .WithMany(p => p.Muestreo)
                    .HasForeignKey(d => d.ProgramaMuestreoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Muestreo_ProgramaMuestreo");

                entity.HasOne(d => d.TipoAprobacion)
                    .WithMany(p => p.Muestreo)
                    .HasForeignKey(d => d.TipoAprobacionId)
                    .HasConstraintName("FK_Muestreo_TipoAprobacion");

                entity.HasOne(d => d.UsuarioRevisionOcdl)
                    .WithMany(p => p.MuestreoUsuarioRevisionOcdl)
                    .HasForeignKey(d => d.UsuarioRevisionOcdlid)
                    .HasConstraintName("FK_Muestreo_Usuario");

                entity.HasOne(d => d.UsuarioRevisionSecaia)
                    .WithMany(p => p.MuestreoUsuarioRevisionSecaia)
                    .HasForeignKey(d => d.UsuarioRevisionSecaiaid)
                    .HasConstraintName("FK_Muestreo_Usuario1");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Municipio)
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
            });

            modelBuilder.Entity<Pagina>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Url).IsUnicode(false);

                entity.HasOne(d => d.IdPaginaPadreNavigation)
                    .WithMany(p => p.InverseIdPaginaPadreNavigation)
                    .HasForeignKey(d => d.IdPaginaPadre)
                    .HasConstraintName("FK_Pagina_Pagina");
            });

            modelBuilder.Entity<ParametrosGrupo>(entity =>
            {
                entity.Property(e => e.ClaveParametro).HasMaxLength(30);

                entity.Property(e => e.Descripcion).HasMaxLength(100);

                entity.HasOne(d => d.IdSubgrupoNavigation)
                    .WithMany(p => p.ParametrosGrupo)
                    .HasForeignKey(d => d.IdSubgrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ParametrosGrupo_SubtipoCuerpoAgua");

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.ParametrosGrupo)
                    .HasForeignKey(d => d.IdUnidadMedida)
                    .HasConstraintName("FK_ParametrosGrupo_UnidadMedida");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PerfilPagina>(entity =>
            {
                entity.HasOne(d => d.IdPaginaNavigation)
                    .WithMany(p => p.PerfilPagina)
                    .HasForeignKey(d => d.IdPagina)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PerfilPag__IdPag__37A5467C");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.PerfilPagina)
                    .HasForeignKey(d => d.IdPerfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PerfilPag__IdPer__36B12243");
            });

            modelBuilder.Entity<PerfilPaginaAccion>(entity =>
            {
                entity.HasOne(d => d.IdAccionNavigation)
                    .WithMany(p => p.PerfilPaginaAccion)
                    .HasForeignKey(d => d.IdAccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PerfilPag__IdAcc__3B75D760");

                entity.HasOne(d => d.IdPerfilPaginaNavigation)
                    .WithMany(p => p.PerfilPaginaAccion)
                    .HasForeignKey(d => d.IdPerfilPagina)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PerfilPag__IdPer__3A81B327");
            });

            modelBuilder.Entity<ProgramaMuestreo>(entity =>
            {
                entity.Property(e => e.DiaProgramado).HasColumnType("date");

                entity.Property(e => e.DomingoSemanaProgramada).HasColumnType("date");

                entity.Property(e => e.NombreCorrectoArchivo).HasMaxLength(100);

                entity.HasOne(d => d.ProgramaSitio)
                    .WithMany(p => p.ProgramaMuestreo)
                    .HasForeignKey(d => d.ProgramaSitioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProgramaMuestreo_ProgramaSitio");
            });

            modelBuilder.Entity<ProgramaSitio>(entity =>
            {
                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.HasOne(d => d.Laboratorio)
                    .WithMany(p => p.ProgramaSitio)
                    .HasForeignKey(d => d.LaboratorioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProgramaSitio_Laboratorio");

                entity.HasOne(d => d.Sitio)
                    .WithMany(p => p.ProgramaSitio)
                    .HasForeignKey(d => d.SitioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProgramaSitio_Sitio");

                entity.HasOne(d => d.TipoSitio)
                    .WithMany(p => p.ProgramaSitio)
                    .HasForeignKey(d => d.TipoSitioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProgramaSitio_TipoSitio");
            });

            modelBuilder.Entity<ResultadoMuestreo>(entity =>
            {
                entity.Property(e => e.CausaRechazo).IsUnicode(false);

                entity.Property(e => e.Comentarios).IsUnicode(false);

                entity.Property(e => e.EsCorrectoOcdl).HasColumnName("EsCorrectoOCDL");

                entity.Property(e => e.EsCorrectoSecaia).HasColumnName("EsCorrectoSECAIA");

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

                entity.Property(e => e.ResultadoReplica).IsUnicode(false);

                entity.HasOne(d => d.EstatusResultadoNavigation)
                    .WithMany(p => p.ResultadoMuestreo)
                    .HasForeignKey(d => d.EstatusResultado)
                    .HasConstraintName("FK_ResultadoMuestreo_EstatusMuestreo");

                entity.HasOne(d => d.Muestreo)
                    .WithMany(p => p.ResultadoMuestreo)
                    .HasForeignKey(d => d.MuestreoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResultadoMuestreo_Muestreo");

                entity.HasOne(d => d.ObservacionSrenamecaNavigation)
                    .WithMany(p => p.ResultadoMuestreoObservacionSrenamecaNavigation)
                    .HasForeignKey(d => d.ObservacionSrenamecaid)
                    .HasConstraintName("FK_ResultadoMuestreo_ObservacionesSRENAMECA");

                entity.HasOne(d => d.ObservacionesOcdlNavigation)
                    .WithMany(p => p.ResultadoMuestreoObservacionesOcdlNavigation)
                    .HasForeignKey(d => d.ObservacionesOcdlid)
                    .HasConstraintName("FK_ResultadoMuestreo_ObservacionesOCDL");

                entity.HasOne(d => d.ObservacionesSecaiaNavigation)
                    .WithMany(p => p.ResultadoMuestreoObservacionesSecaiaNavigation)
                    .HasForeignKey(d => d.ObservacionesSecaiaid)
                    .HasConstraintName("FK_ResultadoMuestreo_Observaciones");

                entity.HasOne(d => d.Parametro)
                    .WithMany(p => p.ResultadoMuestreo)
                    .HasForeignKey(d => d.ParametroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResultadoMuestreo_Parametro");
            });

            modelBuilder.Entity<Sitio>(entity =>
            {
                entity.Property(e => e.ClaveSitio).HasMaxLength(150);

                entity.Property(e => e.DireccionLrevisionId).HasColumnName("DireccionLRevisionId");

                entity.Property(e => e.NombreSitio).HasMaxLength(250);

                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.HasOne(d => d.CuencaDireccionesLocales)
                    .WithMany(p => p.Sitio)
                    .HasForeignKey(d => d.CuencaDireccionesLocalesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sitios_CuencaDireccionesLocales1");

                entity.HasOne(d => d.CuencaRevision)
                    .WithMany(p => p.Sitio)
                    .HasForeignKey(d => d.CuencaRevisionId)
                    .HasConstraintName("FK_Sitio_OrganismoCuenca");

                entity.HasOne(d => d.CuerpoTipoSubtipoAgua)
                    .WithMany(p => p.Sitio)
                    .HasForeignKey(d => d.CuerpoTipoSubtipoAguaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sitios_CuerpoTipoSubtipoAgua");

                entity.HasOne(d => d.DireccionLrevision)
                    .WithMany(p => p.Sitio)
                    .HasForeignKey(d => d.DireccionLrevisionId)
                    .HasConstraintName("FK_Sitio_DireccionLocal");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Sitio)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sitios_Estado1");

                entity.HasOne(d => d.Municipio)
                    .WithMany(p => p.Sitio)
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

            modelBuilder.Entity<TipoAprobacion>(entity =>
            {
                entity.Property(e => e.Descripcion).HasMaxLength(20);
            });

            modelBuilder.Entity<TipoCuerpoAgua>(entity =>
            {
                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.HasOne(d => d.TipoHomologado)
                    .WithMany(p => p.TipoCuerpoAgua)
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

            modelBuilder.Entity<TipoSitio>(entity =>
            {
                entity.Property(e => e.TipoSitio1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TipoSitio");
            });

            modelBuilder.Entity<UnidadMedida>(entity =>
            {
                entity.Property(e => e.Descripcion).HasMaxLength(30);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
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

                entity.HasOne(d => d.Cuenca)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.CuencaId)
                    .HasConstraintName("FK_Usuario_OrganismoCuenca");

                entity.HasOne(d => d.DireccionLocal)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.DireccionLocalId)
                    .HasConstraintName("FK_Usuario_DireccionLocal");

                entity.HasOne(d => d.Perfil)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.PerfilId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Perfil");
            });

            modelBuilder.Entity<VwClaveMuestreo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Vw_ClaveMuestreo");

                entity.Property(e => e.ClaveMuestreo).HasMaxLength(4000);
            });

            modelBuilder.Entity<VwReplicaRevisionResultado>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Vw_ReplicaRevisionResultado");

                entity.Property(e => e.ClasificacionObservacion).IsUnicode(false);

                entity.Property(e => e.ClaveMonitoreo).HasMaxLength(4000);

                entity.Property(e => e.ClaveParametro).HasMaxLength(30);

                entity.Property(e => e.ClaveSitio).HasMaxLength(150);

                entity.Property(e => e.ClaveUnica).HasMaxLength(4000);

                entity.Property(e => e.ComentariosReplicaDiferente).IsUnicode(false);

                entity.Property(e => e.EsCorrectoOcdl).HasColumnName("EsCorrectoOCDL");

                entity.Property(e => e.EsCorrectoSecaia).HasColumnName("EsCorrectoSECAIA");

                entity.Property(e => e.Estatus)
                    .HasMaxLength(150)
                    .IsUnicode(false);

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

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(252)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroEntrega)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.ObservacionSrenameca)
                    .IsUnicode(false)
                    .HasColumnName("ObservacionSRENAMECA");

                entity.Property(e => e.ObservacionesOcdl)
                    .HasMaxLength(100)
                    .HasColumnName("ObservacionesOCDL");

                entity.Property(e => e.ObservacionesSecaia)
                    .HasMaxLength(100)
                    .HasColumnName("ObservacionesSECAIA");

                entity.Property(e => e.Resultado)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ResultadoActualizadoReplica).IsUnicode(false);

                entity.Property(e => e.TipoCuerpoAgua).HasMaxLength(150);

                entity.Property(e => e.TipoCuerpoAguaOriginal).HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
