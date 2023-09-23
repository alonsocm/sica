export interface InformeMensualSupervision {
  oficio: string;
  lugar: string;
  fecha: string;
  direccionTecnica: string;
  gerenteCalidadAgua: string;
  mesReporte: string;
  atencion: Array<string>;
  contrato: string;
  denominacionContrato: string;
  numeroSitios: string;
  indicaciones: string;
  resultados: Array<Resultado>;
  nombreFirma: string;
  puestoFirma: string;
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
