using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ReplicaResumenDto
    {
        public string? NoEntrega { get; set; }
        public string? ClaveUnica { get; set; }
        public string? ClaveSitio { get; set; }
        public string? ClaveMonitoreo { get; set; }
        public string? NombreSitio { get; set; }
        public string? ClaveParametro { get; set; }
        public string? Laboratorio { get; set; }
        public string? TipoCuerpoAgua { get; set; }
        public string? TipoCuerpoAguaOriginal { get; set; }
        public string? Resultado { get; set; }
        public string? estatusResultado { get; set; }
    }

    public class GeneralDescargaResumenExcel
    {
        public string? NoEntrega { get; set; }
        public string? ClaveUnica { get; set; }
        public string? ClaveSitio { get; set; }
        public string? ClaveMonitoreo { get; set; }
        public string? NombreSitio { get; set; }
        public string? ClaveParametro { get; set; }
        public string?Laboratorio { get; set; }
        public string? TipoCuerpoAgua { get; set; }
        public string? TipoCuerpoAguaOriginal { get; set; }
        public string?Resultado { get; set; }
        public string? estatusResultado { get; set; }
    }
}