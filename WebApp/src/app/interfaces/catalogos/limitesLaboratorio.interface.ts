import { Row } from "../../modules/muestreo/formatoResultado/models/row";

export interface LimitesLaboratorios extends Row {
  id: number | null,
  claveParametro: string,
  nombreParametro: string,
  parametroId: number |null,
  laboratorio: string,
  laboratorioId: number | null,
  realizaLaboratorioMuestreoId: number | null,
  realizaLaboratorioMuestreo: string,
  laboratorioMuestreo: string,
  laboratorioMuestreoId: number | null,
  periodoId: number | null,
  mes: string,
  activo: boolean,
  ldMaCumplir: string,
  lpCaCumplir: string,
  loMuestra: boolean | null,
  loSubroga: string,
  loSubrogaId: number|null,
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
