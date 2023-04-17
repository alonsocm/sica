export interface Muestreo {
  muestreoId: number;
  ocdl: string;
  claveSitio: string;
  claveMonitoreo: string;
  estado: string;
  cuerpoAgua: string,
  tipoCuerpoAgua: string;
  laboratorio: string;
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
}

export interface Evidencia {
  nombreArchivo: string;
  sufijo: string;
}
