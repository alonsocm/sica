export interface ReporteMensualSupervision {
  oficio: string;
  lugar: string;
  fecha: string;
  direccionTecnica: string;
  mesReporte: string;
  atencion: Array<string>;
  contrato: string;
  denominacionContrato: string;
  numeroSitios: string;
  indicaciones: string;
  resultados: Array<Resultado>;
}

export interface Resultado {
  ocdl: string;
  totalSitios: string;
  intervalos: Array<Intervalo>;
}

export interface Intervalo {
  calificacion: string;
  numeroSitios: string;
  porcentaje: string;
}
