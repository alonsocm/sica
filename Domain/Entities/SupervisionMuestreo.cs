﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class SupervisionMuestreo
{
    public long Id { get; set; }

    public DateTime FehaMuestreo { get; set; }

    public TimeSpan HoraInicio { get; set; }

    public TimeSpan HoraTermino { get; set; }

    public TimeSpan HoraTomaMuestra { get; set; }

    public decimal PuntajeObtenido { get; set; }

    public long? OrganismoCuencaRealizaId { get; set; }

    public long? DireccionLocalRealizaId { get; set; }

    public long OrganismoCuencaReportaId { get; set; }

    public string SupervisorConagua { get; set; } = null!;

    public long SitioId { get; set; }

    public string ClaveMuestreo { get; set; } = null!;

    public double LatitudToma { get; set; }

    public double LongitudToma { get; set; }

    public long LaboratorioRealizaId { get; set; }

    public int ResponsableTomaId { get; set; }

    public int ResponsableMedicionesId { get; set; }

    public string? ObservacionesMuestreo { get; set; }

    public virtual DireccionLocal? DireccionLocalRealiza { get; set; }

    public virtual ICollection<EvidenciaSupervisionMuestreo> EvidenciaSupervisionMuestreo { get; set; } = new List<EvidenciaSupervisionMuestreo>();

    public virtual Laboratorios LaboratorioRealiza { get; set; } = null!;

    public virtual OrganismoCuenca? OrganismoCuencaRealiza { get; set; }

    public virtual OrganismoCuenca OrganismoCuencaReporta { get; set; } = null!;

    public virtual Muestreadores ResponsableMediciones { get; set; } = null!;

    public virtual Muestreadores ResponsableToma { get; set; } = null!;

    public virtual Sitio Sitio { get; set; } = null!;

    public virtual ICollection<ValoresSupervisionMuestreo> ValoresSupervisionMuestreo { get; set; } = new List<ValoresSupervisionMuestreo>();
}