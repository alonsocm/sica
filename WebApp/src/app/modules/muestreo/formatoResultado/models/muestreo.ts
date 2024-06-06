import { Parametro } from './parametro';
import { Register } from './register';

export interface Muestreo extends Register {
  muestreoId: number;
  claveSitioOriginal: string;
  claveSitio: string;
  claveMonitoreo: string;
  fechaRealizacion: string;
  laboratorio: string;
  noEntregaOCDL: string;
  tipoCuerpoAgua: string;
  tipoHomologado: string;
  tipoSitio: string;
  parametros: Array<Parametro>;
  isChecked: boolean;
  estatus: string;
}
