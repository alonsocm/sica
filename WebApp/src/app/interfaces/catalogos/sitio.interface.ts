import { Row } from "../../modules/muestreo/formatoResultado/models/row";

export interface Sitio extends Row {
  id: number,
  claveSitio: string,
  nombreSitio: string,
  acuifero: string,
  cuenca: string,
  direccionLocal: string,
  estado: string,
  municipio: string,
  cuerpoAgua: string,
  tipoCuerpoAgua: string,
  subtipoCuerpoAgua: string,
  latitud: number,
  longitud: number,
  observaciones: string,
  acuiferoId: number | null,
  organismoCuencaId: number | null,
  direccionLocalId: number | null,
  estadoId: number,
  municipioId: number,
  cuerpoAguaId: number,
  tipoCuerpoAguaId: number,
  subtipoCuerpoAguaId: number
}
