namespace Domain.Settings
{
    public static class ExcelSettings
    {
        public static List<KeyValuePair<string, string>> keyValues = new()
        {
            new KeyValuePair<string, string>("No. Entrega","NoEntrega"),
            new KeyValuePair<string, string>("Selecciona","Id"),
            new KeyValuePair<string, string>("Muestreo","Muestreo"),
            new KeyValuePair<string, string>("Clave CONALAB","ClaveConalab"),
            new KeyValuePair<string, string>("CLAVE CONAGUA","Claveconagua"),
            new KeyValuePair<string, string>("FechaProgramadaVisita","FechaProgramadaVisita"),
            new KeyValuePair<string, string>("FechaRealVisita","FechaRealVisita"),
            new KeyValuePair<string, string>("HoraInicioMuestreo","HoraInicioMuestreo"),
            new KeyValuePair<string, string>("HoraFinMuestreo","HoraFinMuestreo"),
            new KeyValuePair<string, string>("Tipo de Cuerpo de Agua","TipoCuerpoAgua"),
            new KeyValuePair<string, string>("SubtipoCuerpoAgua","SubtipoCuerpoAgua"),
            new KeyValuePair<string, string>("Laboratorio Base de Datos","LaboratorioBasedeDatos"),
            new KeyValuePair<string, string>("LaboratorioRealizoMuestreo","LaboratorioRealizoMuestreo"),
            new KeyValuePair<string, string>("Laboratorio Subrogado","LaboratorioSubrogado"),
            new KeyValuePair<string, string>("Grupo de Parametro","GrupodeParametro"),
            new KeyValuePair<string, string>("SubgrupoParametro","SubgrupoParametro"),
            new KeyValuePair<string, string>("ClaveParametro","ClaveParametro"),
            new KeyValuePair<string, string>("Parametro","Parametro"),
            new KeyValuePair<string, string>("UnidadMedida","UnidadMedida"),
            new KeyValuePair<string, string>("Resultado","Resultado"),
            new KeyValuePair<string, string>("LPCLaboratorio","Lpclaboratorio"),
            new KeyValuePair<string, string>("LDMLaboratorio","Ldmlaboratorio"),
            new KeyValuePair<string, string>("Observaciones Laboratorio","ObservacionesLaboratorio"),
            new KeyValuePair<string, string>("NoVecesLiberadoCONAGUA","NoVecesLiberadoConagua"),
            new KeyValuePair<string, string>("Precio de Parametro","PreciodeParametro"),
            new KeyValuePair<string, string>("ObservacionesConagua","ObservacionesConagua"),
            new KeyValuePair<string, string>("ObservacionesConagua2","ObservacionesConagua2"),
            new KeyValuePair<string, string>("ObservacionesConagua3","ObservacionesConagua3"),
            new KeyValuePair<string, string>("AnioOperacion","AnioOperacion"),
            new KeyValuePair<string, string>("IdResultado","IdResultado"),
            new KeyValuePair<string, string>("FechaEntrega","FechaEntrega"),
        };
    }

    public static class ExcelResultadosTotalesSettings
    {
        public static List<KeyValuePair<string, string>> keyValues = new()
        {
            new KeyValuePair<string, string>("CLAVE PARÁMETRO","ClaveParametro"),
            new KeyValuePair<string, string>("CLAVE MONITOREO","ClaveMonitoreo"),
            new KeyValuePair<string, string>("RESULTADO","Resultado"),
            new KeyValuePair<string, string>("OBSERVACIÓN OC/DL","ObservacionOCDL")
        };
    }

    public static class ExcelResultadosTotalesSECAIASettings
    {
        public static List<KeyValuePair<string, string>> keyValues = new()
        {
            new KeyValuePair<string, string>("No. ENTREGA SECAIA","NumeroEntrega"),
            new KeyValuePair<string, string>("CLAVE ÚNICA","ClaveUnica"),
            new KeyValuePair<string, string>("CLAVE SITIO","ClaveSitio"),
            new KeyValuePair<string, string>("CLAVE MONITOREO","ClaveMonitoreo"),
            new KeyValuePair<string, string>("NOMBRE","Nombre"),
            new KeyValuePair<string, string>("CLAVE PARÁMETRO","ClaveParametro"),
            new KeyValuePair<string, string>("LABORATORIO","Laboratorio"),
            new KeyValuePair<string, string>("TIPO DE CUERPO DE AGUA","TipoCuerpoAgua"),
            new KeyValuePair<string, string>("RESULTADO","Resultado"),
            new KeyValuePair<string, string>("OBSERVACIÓN SECAIA","ObservacionSECAIA"), //ENCABEZADO OBSERVACION SECAIA (OTRO)
        };
    }

    public static class ExcelValidadosTotalesSECAIASettings
    {
        public static List<KeyValuePair<string, string>> keyValues = new()
        {
            new KeyValuePair<string, string>("No. ENTREGA SECAIA","noEntregaOCDL"),
            new KeyValuePair<string, string>("CLAVE ÚNICA","claveUnica"),
            new KeyValuePair<string, string>("CLAVE SITIO","claveSitio"),
            new KeyValuePair<string, string>("CLAVE MONITOREO","claveMonitoreo"),
            new KeyValuePair<string, string>("NOMBRE","nombreSitio"),
            new KeyValuePair<string, string>("CLAVE PARÁMETRO","claveParametro"),
            new KeyValuePair<string, string>("LABORATORIO","laboratorio"),
            new KeyValuePair<string, string>("TIPO DE CUERPO DE AGUA","tipoCuerpoAgua"),
            new KeyValuePair<string, string>("RESULTADO","resultado"),
            new KeyValuePair<string, string>("OBSERVACIÓN SECAIA","observacionSECAIA"),
            new KeyValuePair<string, string>("FECHA DE REVISIÓN","fechaRevision"),
            new KeyValuePair<string, string>("NOMBRE USUARIO QUE REVISÓ","nombreUsuario"),
            new KeyValuePair<string, string>("ESTATUS DE RESULTADO","estatusResultado"),
        };
    }

    public static class ExcelResultadosRevisionReplicaSettings
    {
        public static List<KeyValuePair<string, string>> keyValues = new()
        {
            new KeyValuePair<string, string>("No. Entrega","No_Entrega"),
            new KeyValuePair<string, string>("Clave Única","Clave_Unica"),
            new KeyValuePair<string, string>("Clave Sitio","Clave_Sitio"),
            new KeyValuePair<string, string>("Clave Monitoreo","Clave_Monitoreo"),
            new KeyValuePair<string, string>("Nombre","Nombre_Sitio"),
            new KeyValuePair<string, string>("Clave Parámetro","Clave_Parametro"),
            new KeyValuePair<string, string>("Laboratorio","Laboratorio"),
            new KeyValuePair<string, string>("Tipo de Cuerpo de Agua","Tipo_Cuerpo_Agua"),
            new KeyValuePair<string, string>("Tipo de Cuerpo de Agua Original","Tipo_Cuerpo_Agua_Original"),
            new KeyValuePair<string, string>("Resultado","Resultado"),
            new KeyValuePair<string, string>("Es Correcto el Resultado OC/DL","Es_Correcto_OCDL"),
            new KeyValuePair<string, string>("Observación OC/DL","Observacion_OCDL"),
            new KeyValuePair<string, string>("Es Correcto el Resultado por SECAIA","Es_Correcto_SECAIA"),
            new KeyValuePair<string, string>("Observación SECAIA","Observacion_SECAIA"),
            new KeyValuePair<string, string>("Clasificación Observación","Clasificacion_Observacion"),
            new KeyValuePair<string, string>("Se Aprueba el Resultado","Aprueba_Resultado"),
            new KeyValuePair<string, string>("Comentarios","Comentarios_Aprobacion_Resultados")
        };
    }

    public static class ExcelSettingsReplicaDiferente
    {
        public static List<KeyValuePair<string, string>> keyValues = new()
        {

            new KeyValuePair<string, string>("NO. ENTREGA","NoEntrega"),
            new KeyValuePair<string, string>("CLAVE ÚNICA","ClaveUnica"),
            new KeyValuePair<string, string>("CLAVE SITIO","ClaveSitio"),
            new KeyValuePair<string, string>("CLAVE MONITOREO","ClaveMonitoreo"),
            new KeyValuePair<string, string>("NOMBRE","NombreSitio"),
            new KeyValuePair<string, string>("CLAVE PARAMETRO","ClaveParametro"),
            new KeyValuePair<string, string>("LABORATORIO","Laboratorio"),
            new KeyValuePair<string, string>("TIPO DE CUERPO DE AGUA","TipoCuerpoAgua"),
            new KeyValuePair<string, string>("TIPO DE CUERPO DE AGUA ORIGINAL","TipoCuerpoAguaOriginal"),
            new KeyValuePair<string, string>("RESULTADO ACTUALIZADO POR RÉPLICA","ResultadoActualizadoporReplica"),
            new KeyValuePair<string, string>("ES CORRECTO EL RESULTADO POR OC/DL","Es_CorrectoOCDL"),
            new KeyValuePair<string, string>("OBSERVACIÓN OC/DL","ObservacionOCDL"),
            new KeyValuePair<string, string>("ES CORRECTO EL RESULTADO POR SECAIA","EsCorrectoSECAIA"),
            new KeyValuePair<string, string>("OBSERVACIÓN SECAIA","ObservacionSECAIA"),
            new KeyValuePair<string, string>("CLASIFICACIÓN OBSERVACIÓN","ClasificacionObservacion"),
            new KeyValuePair<string, string>("OBSERVACIÓN SRENAMECA","ObservacionSRENAMECA"),
            new KeyValuePair<string, string>("COMENTARIOS","ComentariosAprobacionResultados"),



        };
    }

    public static class ExcelSettingsRevisionReplicas
    {
        public static List<KeyValuePair<string, string>> KeysValues = new()
        {
            new KeyValuePair<string, string>("No. ENTREGA", "NoEntrega"),
            new KeyValuePair<string, string>("CLAVE ÚNICA", "ClaveUnica"),
            new KeyValuePair<string, string>("CLAVE SITIO", "ClaveSitio"),
            new KeyValuePair<string, string>("CLAVE MONITOREO", "ClaveMonitoreo"),
            new KeyValuePair<string, string>("NOMBRE", "Nombre"),
            new KeyValuePair<string, string>("CLAVE PARAMETRO", "ClaveParametro"),
            new KeyValuePair<string, string>("LABORATORIO", "Laboratorio"),
            new KeyValuePair<string, string>("TIPO DE CUERPO DE AGUA", "TipoCuerpoAgua"),
            new KeyValuePair<string, string>("TIPO DE CUERPO DE AGUA ORIGINAL", "TipoCuerpoAguaOriginal"),
            new KeyValuePair<string, string>("RESULTADO", "Resultado"),
            new KeyValuePair<string, string>("OBSERVACIÓN SECAIA", "ObservacionSECAIA"),
            new KeyValuePair<string, string>("CLASIFICACIÓN OBSERVACIÓN", "ClasificacionObservacion"),
            new KeyValuePair<string, string>("CAUSA DE RECHAZO", "CausaRechazo"),
            new KeyValuePair<string, string>("SE ACEPTA RECHAZO (SI/NO)", "SeAceptaRechazo"),
            new KeyValuePair<string, string>("RESULTADO RÉPLICA", "ResultadoReplica"),
            new KeyValuePair<string, string>("OBSERVACIÓN LABORATORIO", "ObservacionLaboratorio"),
            new KeyValuePair<string, string>("NOMBRE ARCHIVO EVIDENCIA", "NombreArchivoEvidencia")
        };
    }

    public static class ExcelSettingsRevisionReplicasLNR
    {
        public static List<KeyValuePair<string, string>> KeysValues = new()
        {
            new KeyValuePair<string, string>("No. ENTREGA", "NoEntrega"),
            new KeyValuePair<string, string>("CLAVE ÚNICA", "ClaveUnica"),
            new KeyValuePair<string, string>("CLAVE SITIO", "ClaveSitio"),
            new KeyValuePair<string, string>("CLAVE MONITOREO", "ClaveMonitoreo"),
            new KeyValuePair<string, string>("OBSERVACIÓN SRENAMECA", "ObservacionSRENAMECA"),
            new KeyValuePair<string, string>("COMENTARIOS", "Comentarios")
        };
    }

    public static class ExcelLimitesComunes
    {
        public static List<KeyValuePair<string, string>> keyValues = new()
        {
            new KeyValuePair<string, string>("Clave Parametro", "ClaveParametro"),
            new KeyValuePair<string, string>("LDM (Máximo)", "LDM"),
            new KeyValuePair<string, string>("LPC /CMC (Máximo)", "LPC"),
            new KeyValuePair<string, string>("Tipo de limite a considerar", "LimiteConsiderado"),
        };
    }
}
