export interface Usuario {
  Id: number,
  Activo: boolean,
  UserName: string, 
  Nombre: string,
  ApellidoPaterno: string,
  ApellidoMaterno: string,
  Email: string,
  PerfilId: number ,
  CuencaId: number |null,
  DireccionLocalId: number |null
}
