export interface InformeMensualSupervision {
  oficio: string;
  lugar: string;
  fecha: string;
  direccionTecnica: string;
  gerenteCalidadAgua: string; //llega
  mesReporte: string;
  atencion: Array<string>; //llega
  contrato: string; //llega
  denominacionContrato: string; //llega
  numeroSitios: string; //llega
  indicaciones: string; //llega
  resultados: Array<Resultado>; //llega
  nombreFirma: string;
  puestoFirma: string;
  copias: Array<{ nombre: string; puesto: string }>;
  personasInvolucradas: string;
  direccionOC: string;
  telefonoOC: string;
}

export interface Resultado {
  ocDl: string;
  totalSitios: string;
  intervalos: Array<Intervalo>;
}

export interface Intervalo {
  calificacion: string;
  numeroSitios: string;
  porcentaje: string;
}
