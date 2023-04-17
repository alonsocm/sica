export interface Revision { 
  noEntrega: string;
  claveUnica: string;
  claveSitio: string;
  claveMonitoreo: string;
  nombreSitio: string;
  claveParametro: string;
  laboratorio: string;
  tipoCuerpoAgua: string;
  tipoCuerpoAguaOriginal: string;
  resultado: string;
  esCorrectoOCDL: string;
  observacionOCDL: string;
  esCorrectoSECAIA: string;
  observacionSECAIA: string;
  clasificacionObservacion: string;
  aprobacionResulMuestreoId: string;
  apruebaResultado: string;
  comentariosAprobacionResultados: string;
  fechaAprobRechazo: string;
  usuarioRevision: string;
  estatusResultado: string;


  isChecked: boolean;
  isCheckedmodal: boolean;
  muestreoId: any;
  ResultadoMuestreoId: any;
  UsuarioRevisionId?: any;
  //estatusId: number;
  ParametroId: any;
  estatusResultadoId:any;
}
