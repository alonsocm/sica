﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AcumuladosResultadoDto :MuestreoDto
    {
       
        public string claveUnica { get; set; }
        public string SubTipoCuerpoAgua { get; set; }
        public string laboratorioRealizoMuestreo { get; set; }
        public string grupoParametro { get; set; }
        public string subGrupo { get; set; }
        public string claveParametro { get; set; }
        public string parametro { get; set; }
        public string? unidadMedida { get; set; }
        public string resultado { get; set; }
        public string zonaEstrategica { get; set; }
        public long idResultadoLaboratorio { get; set; }
        public string fechaEntrega { get; set; }
        public string nuevoResultadoReplica { get; set; }
        public bool replica { get; set; }
        public bool cambioResultado { get; set; }
        public int diferenciaDias { get; set; }
        public string fechaEntregaTeorica { get; set; }
        public int numParametrosEsperados { get; set; }
        public int numParametrosCargados { get; set; }
        public string muestreoCompletoPorResultados { get; set; }           
        public bool validadoReglas { get; set; }       
        public decimal costoParametro { get; set; }
        public int anioOperacion { get; set; }
        public string cumpleReglasCondic { get; set; }

        public long tipoCuerpoAguaId { get; set; }
        public long tipoSitioId { get; set; }

        public string cumpleFechaEntrega { get; set; }
        public string resultadoReglas { get; set; }

        public List<ParametrosDto> lstParametros { get; set; }



        public AcumuladosResultadoDto()
        { this.claveUnica = string.Empty;
            this.SubTipoCuerpoAgua = string.Empty;
            this.laboratorioRealizoMuestreo = string.Empty;
            this.subGrupo = string.Empty;
            this.claveParametro = string.Empty;
            this.parametro = string.Empty;
            this.unidadMedida = string.Empty;
            this.resultado = string.Empty;
            this.zonaEstrategica = string.Empty;
            this.grupoParametro = string.Empty;
            this.idResultadoLaboratorio = 0;
            this.fechaEntrega = string.Empty;
            this.nuevoResultadoReplica = string.Empty;
            this.replica = false;
            this.cambioResultado = false;
            this.diferenciaDias = 0;
            this.fechaEntregaTeorica = string.Empty;
            this.numParametrosEsperados = 0;
            this.numParametrosCargados = 0;
            this.muestreoCompletoPorResultados = string.Empty;
            this.validadoReglas = false;
            this.costoParametro = 0;
            this.anioOperacion = 0;
            this.cumpleReglasCondic = string.Empty;

            this.tipoCuerpoAguaId = 0;
            this.tipoSitioId = 0;

            this.cumpleFechaEntrega = string.Empty;
            this.resultadoReglas = string.Empty;

            this.lstParametros = new List<ParametrosDto>();
        }
    }
}
