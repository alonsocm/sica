export interface vwValidacionEvidencia {

  ClaveMuestreo: string;
  Sitio: string;
  ClaveConalab: string;
  TipoCuerpoAgua: string;
  Laboratorio: string;
  ConEventualidades: boolean;
  FechaProgramadaVisita: string;
  FechaRealVisita: string;
  BrigadaProgramaMuestreo: string;
  ConQcmuestreo: boolean;
  TipoSupervision: string;
  TipoEventualidad: string;
  FechaReprogramacion: string;
  EvidenciasEsperadas: number;
  TotalEvidencias: number;
  CumpleEvidencias: boolean;
  FechaRealizacion: string;
  CumpleFechaRealizacion: boolean;
  HoraIncioMuestreo: string;
  HoraFinMuestreo: string;
  TiempoMinimoMuestreo: number;
  ClaveConalbaArm: string;
  CumpleClaveConalab: boolean;
  ClaveMuestreoArm: string;
  CumpleClaveMuestreo: boolean;
  LiderBrigadaArm: string;
  LiderBrigadaBase: string
  ClaveBrigadaArm: string;
  CumpleClaveBrigada: boolean;
  PlacasDeMuestreo: string;
  Lat1MuestreoPrograma: string;
  Log1MuestreoPrograma: string;
  LatSitioResultado: string;
  LongSitioResultado: string;


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








}
