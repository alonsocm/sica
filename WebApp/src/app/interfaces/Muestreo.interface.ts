import { Row } from '../modules/muestreo/formatoResultado/models/row';

export interface Muestreo extends Row {
  muestreoId: number;
  ocdl: string;
  claveSitio: string;
  claveSitioOriginal: string;
  claveMonitoreo: string;
  estado: string;
  cuerpoAgua: string;
  tipoCuerpoAgua: string;
  tipoCuerpoAguaHomologado: string;
  subTipoCuerpoAgua: string;
  laboratorio: string;
  laboratorioSubrogado: string;
  fechaRealizacion: string;
  fechaLimiteRevision: string;
  numeroEntrega: string;
  numeroCarga: string;
  estatus: string;
  tipoCargaResultados: string;
  isChecked: boolean;
  horaInicio: string;
  horaFin: string;
  tipoSitio: string;
  nombreSitio: string;
  fechaProgramada: string;
  fechaCarga: string;
  programaAnual: string;
  evidencias: Array<Evidencia>;
  estatusSECAIA: number;
  isupdate: boolean;
  programaMuestreoId: number;
  estatusId: number;
  fechaEntregaMuestreo: string;
  fechaCargaEvidencias: string;
  autorizacionIncompleto: boolean;
  autorizacionFechaEntrega: boolean;
  correReglaValidacion: string;
  autorizacionCondicionantes: boolean;
  cumpleNumeroEvidencias: string;
  usuarioValido: string;
  porcentajePago: string;
  esReplica: boolean;
}

export interface Evidencia {
  nombreArchivo: string;
  sufijo: string;
}
