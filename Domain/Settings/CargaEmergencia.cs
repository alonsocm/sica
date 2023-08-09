namespace Domain.Settings
{
    public static class CargaEmergencia
    {
        private static List<KeyValuePair<string, string>> columnasPropiedades = new()
        {
            new KeyValuePair<string, string>("No.","Numero"),
            new KeyValuePair<string, string>("Clave unica","ClaveUnica"),
            new KeyValuePair<string, string>("ID LABORATORIO","IdLaboratorio"),
            new KeyValuePair<string, string>("SITIO","Sitio"),
            new KeyValuePair<string, string>("Fecha_Programada","FechaProgramada"),
            new KeyValuePair<string, string>("Fecha Real de Visita","FechaRealVisita"),
            new KeyValuePair<string, string>("Hora de Muestreo","HoraMuestreo"),
            new KeyValuePair<string, string>("Tipo de Cuerpo de Agua","TipoCuerpoAgua"),
            new KeyValuePair<string, string>("Subtipo de Cuerpo de Agua","SubtipoCuerpoAgua"),
            new KeyValuePair<string, string>("LaboratorioRealizoMuestreo","LaboratorioRealizoMuestreo"),
            new KeyValuePair<string, string>("Laboratorio subrogado","LaboratorioSubrogado"),
            new KeyValuePair<string, string>("Grupo de Parametro","GrupoParametro"),
            new KeyValuePair<string, string>("Clave de Parametro","ClaveParametro"),
            new KeyValuePair<string, string>("Parametro","Parametro"),
            new KeyValuePair<string, string>("Resultado","Resultado"),
            new KeyValuePair<string, string>("Unidades","UnidadMedida"),
            new KeyValuePair<string, string>("Nombre de la Emergencia","NombreEmergencia"),
        };

        public static List<KeyValuePair<string, string>> ColumnasPropiedades { get => columnasPropiedades; set => columnasPropiedades=value; }
    }
}
