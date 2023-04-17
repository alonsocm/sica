export interface ConsultaEvidencia {
  muestreoId: any;
  claveSitioOriginal: string;
  claveSitio: string;
  claveMonitoreo: string;
  nombreSitio: string;
  organismoCuenca: string;
  direccionLocal: string;
  cuerpoAgua: string,
  tipoCuerpoAguaOriginal: string,
  tipoSitio: string,
  laboratorio: string,
  laboratorioSubrogado: string,
  fechaProgramada: string;
  fechaRealizacion: string;
  programaAnual: string;
  horaInicio: string;
  horaFin: string;
  observaciones: string;
  horaCargaEvidencias: string;
  numeroCargaEvidencias: string;

  isChecked: boolean;
  evidencias: Array<Evidencia>;
}

export interface Evidencia {
  nombreArchivo: string;
  sufijo: string;
}
