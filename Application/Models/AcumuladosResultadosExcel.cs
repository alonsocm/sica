using Application.Interfaces.IRepositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace Application.Models
{
    public class AcumuladosResultadosExcel
    {
        public string claveUnica { get; set; }
        public string claveMonitoreo { get; set; }
        public string claveSitio { get; set; }
        public string nombreSitio { get; set; }
        public string fechaProgramada { get; set; }
        public string fechaRealizacion { get; set; }
        public string horaInicio { get; set; }
        public string horaFin { get; set; }
        public string zonaEstrategica { get; set; }
        public string tipoCuerpoAgua { get; set; }
        public string subTipoCuerpoAgua { get; set; }
        public string laboratorio { get; set; }
        public string laboratorioRealizoMuestreo { get; set; }
        public string laboratorioSubrogado { get; set; }
        public string grupoParametro { get; set; }
        public string subGrupo { get; set; }
        public string claveParametro { get; set; }
        public string parametro { get; set; }
        public string unidadMedida { get; set; }
        public string resultado { get; set; }
        public string nuevoResultadoReplica { get; set; }
        public string programaAnual { get; set; }
        public long idResultadoLaboratorio { get; set; }
        public string fechaEntrega { get; set; }
        public bool replica { get; set; }
        public bool cambioResultado { get; set; }
        public AcumuladosResultadosExcel()
        {
            this.claveUnica = string.Empty;
            this.claveMonitoreo = string.Empty;
            this.claveSitio = string.Empty;
            this.nombreSitio = string.Empty;
            this.fechaProgramada = string.Empty;
            this.fechaRealizacion = string.Empty;
            this.horaInicio = string.Empty;
            this.horaFin = string.Empty;
            this.zonaEstrategica = string.Empty;
            this.tipoCuerpoAgua = string.Empty;
            this.subTipoCuerpoAgua = string.Empty;
            this.laboratorio = string.Empty;
            this.laboratorioRealizoMuestreo = string.Empty;
            this.laboratorioSubrogado = string.Empty;
            this.grupoParametro = string.Empty;
            this.subGrupo = string.Empty;
            this.claveParametro = string.Empty;
            this.parametro = string.Empty;
            this.unidadMedida = string.Empty;
            this.resultado = string.Empty;
            this.nuevoResultadoReplica = string.Empty;
            this.programaAnual = string.Empty;
            this.idResultadoLaboratorio = 0;
            this.fechaEntrega = string.Empty;
            this.replica = false;
            this.cambioResultado = false;

        }

    }
}
