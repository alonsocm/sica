import { Row } from "../modules/muestreo/formatoResultado/models/row";
import { Evidencia } from "./evidencia";

export interface ReplicasResultadosReglaVal extends Row {
  id: number,
  numeroCarga: number,
  resultadoMuestreoId: number,
  aceptaRechazo: boolean,
  resultadoReplica: string,
  mismoResultado: boolean,
  observacionLaboratorio: string,
  fechaReplicaLaboratorio: string,
  observacionSrenameca: string | null,
  esDatoCorrectoSrenameca: boolean | null,
  fechaObservacionSrenameca: string | null,
  observacionesReglasReplica: string | null,
  apruebaResultadoReplica: boolean | null,
  fechaEstatusFinal: string | null,
  usuarioIdReviso: number | null,
  evidencias: Array<Evidencia>,
  claveUnica: string,
  claveSitio: string,
  claveMonitoreo: string,
  nombre: string,
  claveParametro: string,
  tipoCuerpoAgua: string,
  tipoHomologado: string,
  resultado: string;
  correctoResultadoReglaValidacion: boolean,
  observacionReglaValidacion: string,
  nombreEstatus: string,
  nombreEvidencias: string,
  usuarioReviso: string,
  estatusResultadoId: number


}
