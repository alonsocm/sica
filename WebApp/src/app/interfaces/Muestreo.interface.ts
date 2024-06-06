import { Row } from '../modules/muestreo/formatoResultado/models/row';

export interface Muestreo extends Row {
  muestreoId: number;
  ocdl: string;
  claveSitio: string;
  claveMonitoreo: string;
  estado: string;
  cuerpoAgua: string;
  tipoCuerpoAgua: string;
  subTipoCuerpoAgua: string;
  laboratorio: string;
  laboratorioSubrogado: string;
  fechaRealizacion: string;
  fechaLimiteRevision: string;
  numeroEntrega: string;
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
}

export interface Evidencia {
  nombreArchivo: string;
  sufijo: string;
}
