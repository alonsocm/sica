export interface InformeMensualSupervisionGeneral {
  oficio: string;
  lugar: string;
  fecha: string;
  responsableId: number;
  anio: number;
  mes: number;
  copias: Array<{ nombre: string; puesto: string }>;
  personasInvolucradas: string;
  archivo: any;
}
