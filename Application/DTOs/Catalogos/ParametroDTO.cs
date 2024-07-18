﻿namespace Application.DTOs.Catalogos
{
    public class ParametroDTO
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string Grupo { get; set; }
        public string SubGrupo { get; set; }
        public string UnidadMedida { get; set; }
        public int Orden { get; set; }
        public string ParametroPadre { get; set; }
    }
}
