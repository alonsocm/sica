import { Parametro } from './parametro';
import { Row } from './row';

export interface Muestreo extends Row {
  muestreoId: number;
  claveSitioOriginal: string;
  claveSitio: string;
  claveMonitoreo: string;
  fechaRealizacion: string;
  laboratorio: string;
  numeroEntrega: string;
  tipoCuerpoAgua: string;
  tipoHomologado: string;
  tipoSitio: string;
  parametros: Array<Parametro>;
  isChecked: boolean;
  estatus: string;
  anio: string;
}
