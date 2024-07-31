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
  oCuencaId: number | null,
  dLocalId: number | null,
  estadoId: number | null,
  municipioId: number | null,
  cuerpoAguaId: number | null,
  tipoCuerpoAguaId: number|null,
  subtipoCuerpoAguaId: number | null,
  cuerpoTipoSubtipoAguaId: number | null,
  cuencaDireccionesLocalesId: number | null
}
