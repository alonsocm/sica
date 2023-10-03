export interface InformeMensualSupervisionRegistro {
  id: number;
  oficio: string;
  lugar: string;
  fechaRegistro: string;
  fechaRegistroFin: string;
  direccionTecnica: string;
  gerenteCalidadAgua: string;
  mesReporte: string;
  contrato: string;
  denominacionContrato: string;
  numeroSitios: string;
  indicaciones: string;
  nombreFirma: string;
  puestoFirma: string;
  existeInformeFirmado?: boolean;
}
