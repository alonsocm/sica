export interface CorreoModel {
  destinatarios: string;
  copias: string;
  asunto: string;
  cuerpo: string;
  archivos: Array<ArchivoDat>;
  nombreArchivo?: string;
}
export interface ArchivoDat {
  nombreArchivo: string;
  ruta: string;
  extension: string;
}
