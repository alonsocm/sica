import { DecimalPipe } from "@angular/common";
import { Muestreo } from "./Muestreo.interface";

export interface acumuladosMuestreo extends Muestreo {
  claveUnica: string,
  subTipoCuerpoAgua: string,
  laboratorioRealizoMuestreo: string,
  laboratorioSubrogado: string,
  subGrupo: string,
  claveParametro: string,
  parametro: string,
  unidadMedida: string,
  resultado: string,
  grupoParametro: string,
  zonaEstrategica : string,
  idResultadoLaboratorio: number
  nuevoResultadoReplica: string,
  replica: boolean,
  cambioResultado: boolean,
  fechaEntrega: string,
  diferenciaDias :number,
  fechaEntregaTeorica: string,
  numParametrosEsperados: number,
  numParametrosCargados: number,
  muestreoCompletoPorResultados: string,  
  validadoReglas: boolean,
  costoParametro: number,
  numeroEntrega: string,
  isChecked: boolean;
  resultadoMuestreoId: number;
  cumpleReglasCondic: string;
  observaciones: string;
  cumpleFechaEntrega: string;
  validacionEvidencias: boolean;
  cumpleTodosCriterios: boolean;
  resultadoReglas: string;

  validacionFinal: string;
  observacionFinal: string;



  
}
