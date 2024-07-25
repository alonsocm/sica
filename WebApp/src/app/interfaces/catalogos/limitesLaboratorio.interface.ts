import { Row } from "../../modules/muestreo/formatoResultado/models/row";

export interface LimitesLaboratorios extends Row {
  id: number | null,
  claveParametro: string,
  nombreParametro: string,
  parametroId: number |null,
  laboratorio: string,
  laboratorioId: number | null,
  realizaLaboratorioMuestreo: boolean,
  laboratorioMuestreo: string,
  laboratorioMuestreoId: number | null,
  periodo: number,
  activo: boolean,
  ldMaCumplir: string,
  lpCaCumplir: string,
  loMuestra: boolean,
  loSubroga: string,
  accionLaboratorio: string,
  accionLaboratorioId: number | null /*viene siendo la propiedad de LoSubrogaId*/
  laboratorioSubrogado: string,
  laboratorioSubrogadoId: number | null,
  metodoAnalitico: string,
  ldm: string,
  lpc: string,
  AnioId: number | null,
  anio: string




}
