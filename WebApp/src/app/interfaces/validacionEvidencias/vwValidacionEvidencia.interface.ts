
export interface vwValidacionEvidencia {
  muestreoId: number;
  calculo: number;
  cumpleTiempoMuestreo: string;
  claveMuestreo: string;
  sitio: string;
  claveConalab: string;
  tipoCuerpoAgua: string;
  laboratorio: string;
  conEventualidades: boolean;
  fechaProgramadaVisita: string;
  fechaRealVisita: string;
  brigadaProgramaMuestreo: string;
  conQcmuestreo: boolean;
  tipoSupervision: string;
  tipoEventualidad: string;
  fechaReprogramacion: string;
  evidenciasEsperadas: number;
  totalEvidencias: number;
  cumpleEvidencias: boolean;
  fechaRealizacion: string;
  cumpleFechaRealizacion: boolean;
  horaIncioMuestreo: string;
  horaFinMuestreo: string;
  tiempoMinimoMuestreo: number;
  claveConalbaArm: string;
  cumpleClaveConalab: boolean;
  claveMuestreoArm: string;
  cumpleClaveMuestreo: boolean;
  liderBrigadaArm: string;
  liderBrigadaBase: string
  claveBrigadaArm: string;
  cumpleClaveBrigada: boolean;
  placasDeMuestreo: string;
  lat1MuestreoPrograma: string;
  log1MuestreoPrograma: string;
  latSitioResultado: string;
  longSitioResultado: string;
  lstPuntosMuestreo: any[];
  lstEvidencias: any[];


  folioBM: boolean;
  cumpleLiderBrigadaBM: boolean;
  cumpleGeocercaBM: boolean;
  calibracionVerificacionEquiposBM: boolean;
  registroResultadosCampoBM: boolean;
  firmadoyCanceladoBM: boolean;
  fotografiaGPSPuntoMuestreoBM: boolean;
  registrosVisiblesBM: boolean;
  cumpleMetadatosFM: boolean;
  liderBrigadaCuerpoAguaFM: boolean;
  fotoUnicaFM: boolean;
  cumpleMetadatosFS: boolean;
  registroRecipientesFS: boolean;
  muestrasPreservadasFS: boolean;
  fotoUnicaFS: boolean;
  cumpleMetadatosFA: boolean;
  metodologiaFA: boolean;
  fotoUnicaFA: boolean;
  formatoLlenadoCorrectoFF: boolean;
  cumpleGeocercaFF: boolean;
  registrosLegiblesFF: boolean;
  llenadoCorrectoCC: boolean;
  registrosLegiblesCC: boolean;
  rechazo: boolean;
  observacionesRechazo: boolean;
  cumplePlacasTr: boolean;
  cumpleClaveConalabTr: boolean;









}
