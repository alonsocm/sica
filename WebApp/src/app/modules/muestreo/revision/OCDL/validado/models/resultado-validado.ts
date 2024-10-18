import { Row } from "src/app/modules/muestreo/formatoResultado/models/row";

export interface ResultadoValidado extends Row {
  noEntregaOCDL: string;
  claveUnica: string;
  claveSitio: string;
  claveMonitoreo: string;
  nombreSitio: string;
  claveParametro: string;
  laboratorio: string;
  tipoCuerpoAgua: string;
  tipoCuerpoAguaOriginal: string;
  resultado: string;
  tipoAprobacion: string;
  esCorrectoResultado: string;
  observacionOCDL: string;
  fechaRevision: string;
  nombreUsuario: string;
  fechaLimiteRevision?: string;
  fechaRealizacion: string;
  estatusResultado: string;
  ocdl?: string;
  muestreoId: number;
}
