export interface Correo {
  destinatarios: string;
  copias: string,
  asunto: string,
  cuerpo: string,
  archivos: Array<ArchivoDat>;
}

export interface ArchivoDat {
  nombreArchivo?: string;
  ruta?: string;
  extension?: string;
}
