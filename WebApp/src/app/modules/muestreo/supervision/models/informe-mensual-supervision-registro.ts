export interface InformeMensualSupervisionRegistro {
  id: number;
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
  nombreFirma: string;
  puestoFirma: string;
}
