export interface Usuario {
  id: number,
  activo: boolean,
  userName: string, 
  nombre: string,
  apellidoPaterno: string,
  apellidoMaterno: string,
  email: string,
  perfilId: number,
  cuencaId: number |null,
  direccionLocalId: number | null,
  nombrePerfil: string|null
}      
