export interface RepDiferente { 
  noEntrega: string;
  claveUnica: string;
  claveSitio: string;
  claveMonitoreo: string;
  nombreSitio: string;
  claveParametro: string;
  laboratorio: string;
  tipoCuerpoAgua: string;
  tipoCuerpoAguaOriginal: string;
  resultadoActualizadoporReplica: string;
  esCorrectoOCDL: string;
  observacionOCDL: string;
  esCorrectoSECAIA: string;
  observacionSECAIA: string;
  clasificacionObservacion: string;
  observacionSRENAMECA: string;
  apruebaResultado: string;
  comentariosAprobacionResultados: string;
  fechaObservacionSRENAMECA: string;
  seApruebaResultadodespuesdelaReplica: string;
  fechaEstatusFinal: string;
  usuarioRevision: string;
  estatusResultado: string;


  isChecked: boolean;
  isCheckedmodal: boolean;
  UsuarioRevisionId?: any;
  EstatusResultadoId:any;
}
