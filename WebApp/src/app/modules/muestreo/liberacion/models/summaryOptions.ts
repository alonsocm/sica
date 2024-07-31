import { Muestreo } from 'src/app/interfaces/Muestreo.interface';

export interface SummaryOptions {
  muestreos: Muestreo[];
  filter: string;
  selectAll: boolean;
  total: number;
}
