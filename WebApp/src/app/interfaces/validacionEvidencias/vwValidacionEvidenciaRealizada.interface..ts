export interface vwValidacionEvidenciaRealizada {
  claveMuestreo: string;
  claveSitio: string;
  tipoCuerpoAgua: string;
  laboratorio: string;
  laboratorioMuestreo: string; 
  fonEventualidades: boolean;
  fechaProgramada: string;
  fechaRealVisita: string;
  brigada: string;
  conQcmuestreo: boolean;
  tipoSupervision: string;
  tipoEventualidad: string;
  fechaReprogramacion: string;
  fechaValidacion: string;
  porcentajePago: number;
  rechazo: boolean;
  conEventualidades: boolean;
}
