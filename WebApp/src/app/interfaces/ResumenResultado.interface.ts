export interface ResumenResultados {
  id: number;
  muestreoId: number;
  parametroId: number;
  resultado: string;
  observaciones: string;
  noEntregaOCDL: string;
  claveUnica: string;
  claveSitio: string;
  claveMonitoreo: string;
  nombreSitio: string;
  claveParametro: string;
  laboratorio: string;
  tipoCuerpoAgua: string;
  tipoCuerpoAguaOriginal: string;
  tipoAprobacion: string;
  esCorrectoResultado: string;
  fechaRevision: string;
  nombreUsuario: string;
  estatusResultado: string;
  isChecked: boolean;
  fechaLimiteRevision?: string;

}








