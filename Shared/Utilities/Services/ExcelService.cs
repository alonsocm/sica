using Application.DTOs;
using Application.Exceptions;
using Application.Models;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Shared.Utilities.Services
{
    public static class ExcelService
    {

        public static List<KeyValuePair<string, string>> Mappings;
        public static int StartColumn = 1;

        public static List<T> Import<T>(FileInfo fileInfo, string workSheetName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage package = new(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            List<T> list = new();

            if (worksheet.Name != workSheetName)
                throw new ValidationException() { Errors = { $"No se encontró la hoja {workSheetName} en el archivo." } };

            Type typeOfObject = typeof(T);
            var properties = typeof(T).GetProperties();

            var columns = worksheet.Cells[1, StartColumn, 1, worksheet.Dimension.End.Column].Select((v, i) => new { ColName = v.Value, ColIndex = i + 1 });
            columns = columns.Where(x => x.ColName != null);

            for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
            {
                T obj = (T)Activator.CreateInstance(typeOfObject);

                foreach (var prop in properties)
                {
                    string colName = string.Empty;

                    var value = new object();
                    var type = prop.PropertyType;

                    if (prop.Name != "Linea")
                    {
                        var mapping = Mappings.SingleOrDefault(m => m.Value == prop.Name);
                        colName = mapping.Key;
                        var columna = columns.FirstOrDefault(c => c.ColName.ToString() == colName);

                        if (columna is null)
                        {
                            throw new ValidationException() { Errors = { $"No se encontró la columna {colName} en el archivo." } };
                        }

                        value = worksheet.Cells[i, columna.ColIndex].Value ?? string.Empty;
                    }
                    else
                    {
                        value = i;
                    }

                    try
                    {
                        prop.SetValue(obj, Convert.ChangeType(value, type));
                    }
                    catch
                    {
                        throw new Exception($"El valor {value} no cumple con el formato requerido. Línea: {1}");
                    }
                }

                if (obj != null)
                {
                    list.Add(obj);
                }
            }

            return list;
        }

        public static List<T> ImportarDatosRango<T>(int startcell, int startcolumna, ExcelWorksheet worksheet)
        {
            var columns = worksheet.Cells[startcell, startcolumna, 1, worksheet.Dimension.End.Column].Select((v, i) => new { ColName = v.Value, ColIndex = i + 1 });
            columns = columns.Where(x => x.ColName != null);
            List<T> list = new();
            Type typeOfObject = typeof(T);

            for (int i = startcell + 1; i <= worksheet.Dimension.End.Row; i++)
            {
                var properties = typeof(T).GetProperties();

                T obj = (T)Activator.CreateInstance(typeOfObject);
                var value = new object();
                for (int j = 0; j < properties.Length; j++)
                {
                    var type = properties[j].PropertyType;
                    value = worksheet.Cells[i, j + 1].Value ?? string.Empty;
                    properties[j].SetValue(obj, Convert.ChangeType(value, type));
                }

                if (obj != null)
                { list.Add(obj); }
            }

            return list;

        }

        public static void ExportToExcel<T>(IEnumerable<T> data, FileInfo fileInfo, bool esPlantilla = false, string nombreHoja = "ebaseca")
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var pck = new ExcelPackage(fileInfo);
            ExcelWorksheet sheet = !esPlantilla ? pck.Workbook.Worksheets.Add(nombreHoja) : pck.Workbook.Worksheets[0];
            var range = sheet.Cells[esPlantilla ? "A2" : "A1"].LoadFromCollection(data, !esPlantilla);
            pck.Save();
        }

        public static void ExportToExcelResumenValidacionReglasResultado(IEnumerable<AcumuladosResultadoDto> data, FileInfo fileInfo)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var pck = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = pck.Workbook.Worksheets[0];

            var fila = 2;

            foreach (var registro in data)
            {
                worksheet.Cells[fila, 1].Value = registro.NumeroCarga;
                worksheet.Cells[fila, 2].Value = registro.ClaveUnica;
                worksheet.Cells[fila, 3].Value = registro.ClaveMonitoreo;
                worksheet.Cells[fila, 4].Value = registro.ClaveSitio;
                worksheet.Cells[fila, 5].Value = registro.NombreSitio;
                worksheet.Cells[fila, 6].Value = registro.FechaProgramada;
                worksheet.Cells[fila, 7].Value = registro.FechaRealizacion;
                worksheet.Cells[fila, 8].Value = registro.HoraInicio;
                worksheet.Cells[fila, 9].Value = registro.HoraFin;
                worksheet.Cells[fila, 10].Value = registro.ZonaEstrategica;
                worksheet.Cells[fila, 11].Value = registro.TipoCuerpoAgua;
                worksheet.Cells[fila, 12].Value = registro.SubTipoCuerpoAgua;
                worksheet.Cells[fila, 13].Value = registro.Laboratorio;
                worksheet.Cells[fila, 14].Value = registro.LaboratorioRealizoMuestreo;
                worksheet.Cells[fila, 15].Value = registro.LaboratorioSubrogado;
                worksheet.Cells[fila, 16].Value = registro.GrupoParametro;
                worksheet.Cells[fila, 17].Value = registro.SubGrupo;
                worksheet.Cells[fila, 18].Value = registro.ClaveParametro;
                worksheet.Cells[fila, 19].Value = registro.Parametro;
                worksheet.Cells[fila, 20].Value = registro.UnidadMedida;
                worksheet.Cells[fila, 21].Value = registro.Resultado;
                worksheet.Cells[fila, 22].Value = registro.NuevoResultadoReplica;
                worksheet.Cells[fila, 23].Value = registro.ProgramaAnual;
                worksheet.Cells[fila, 24].Value = registro.IdResultadoLaboratorio;
                worksheet.Cells[fila, 25].Value = registro.FechaEntrega;
                worksheet.Cells[fila, 26].Value = registro.Replica ? "SI" : "NO";
                worksheet.Cells[fila, 27].Value = "SI";
                worksheet.Cells[fila, 28].Value = registro.ResultadoReglas;
                worksheet.Cells[fila, 29].Value = registro.CostoParametro;
                worksheet.Cells[fila, 30].Value = "NO";
                worksheet.Cells[fila, 31].Value = registro.ObservacionFinal;
                fila++;
            }

            pck.SaveAs(fileInfo);
        }

        public static void ExportCargaResultadosValidadosExcel(IEnumerable<MuestreoDto> data, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Resultados validados");

            worksheet.Cells[1, 1].Value = "ESTATUS";
            worksheet.Cells[1, 2].Value = "ES REPLICA";
            worksheet.Cells[1, 3].Value = "EVIDENCIAS COMPLETAS";
            worksheet.Cells[1, 4].Value = "NÚMERO DE CARGA";
            worksheet.Cells[1, 5].Value = "CLAVE NOSEC";
            worksheet.Cells[1, 6].Value = "CLAVE 5K";
            worksheet.Cells[1, 7].Value = "CLAVE MONITOREO";
            worksheet.Cells[1, 8].Value = "TIPO SITIO";
            worksheet.Cells[1, 9].Value = "SITIO";
            worksheet.Cells[1, 10].Value = "OC/DL";
            worksheet.Cells[1, 11].Value = "TIPO CUERPO AGUA";
            worksheet.Cells[1, 12].Value = "SUBTIPO CUERPO AGUA";
            worksheet.Cells[1, 13].Value = "PROGRAMA ANUAL";
            worksheet.Cells[1, 14].Value = "LABORATORIO";
            worksheet.Cells[1, 15].Value = "LABORATORIO SUBROGADO";
            worksheet.Cells[1, 16].Value = "FECHA REALIZACIÓN";
            worksheet.Cells[1, 17].Value = "FECHA PROGRAMACIÓN";
            worksheet.Cells[1, 18].Value = "HORA INICIO MUESTREO";
            worksheet.Cells[1, 19].Value = "HORA FIN MUESTREO";
            worksheet.Cells[1, 20].Value = "FECHA CARGA SICA";
            worksheet.Cells[1, 21].Value = "FECHA ENTREGA";

            var fila = 2;

            foreach (var registro in data)
            {
                worksheet.Cells[fila, 1].Value = registro.Estatus;
                worksheet.Cells[fila, 2].Value = "NO";
                worksheet.Cells[fila, 3].Value = registro.Evidencias.Count > 1 ? "SI" : "NO";
                worksheet.Cells[fila, 4].Value = registro.NumeroCarga + '-' + registro.ProgramaAnual;
                worksheet.Cells[fila, 5].Value = registro.ClaveSitio;
                worksheet.Cells[fila, 6].Value = "CLAVE 5K";
                worksheet.Cells[fila, 7].Value = registro.ClaveMonitoreo;
                worksheet.Cells[fila, 8].Value = registro.TipoSitio;
                worksheet.Cells[fila, 9].Value = registro.NombreSitio;
                worksheet.Cells[fila, 10].Value = registro.OCDL;
                worksheet.Cells[fila, 11].Value = registro.TipoCuerpoAgua;
                worksheet.Cells[fila, 12].Value = registro.SubTipoCuerpoAgua;
                worksheet.Cells[fila, 13].Value = registro.ProgramaAnual;
                worksheet.Cells[fila, 14].Value = registro.Laboratorio;
                worksheet.Cells[fila, 15].Value = registro.LaboratorioSubrogado;
                worksheet.Cells[fila, 16].Value = registro.FechaRealizacion;
                worksheet.Cells[fila, 17].Value = registro.FechaProgramada;
                worksheet.Cells[fila, 18].Value = registro.HoraInicio;
                worksheet.Cells[fila, 19].Value = registro.HoraFin;
                worksheet.Cells[fila, 20].Value = registro.FechaCarga;
                worksheet.Cells[fila, 21].Value = registro.FechaEntregaMuestreo;
                fila++;
            }

            worksheet.Cells[1, 1, 1, 21].Style.Font.Bold = true;
            worksheet.Cells[1, 1, 1, 21].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            worksheet.Cells[1, 1, 1, 21].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[1, 1, 1, 21].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#B7DEE8"));

            package.SaveAs(new FileInfo(filePath));
        }

        public static void ExportToExcelTwoSheets<T>(List<T> dataSheet1, List<TabResumenExcel> dataSheet2, List<TabResumenExcel> dataSheet3, FileInfo fileInfo, bool bandera, bool esPlantilla)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var pack = new ExcelPackage(fileInfo);
            ExcelWorksheet sheet1 = !esPlantilla ? pack.Workbook.Worksheets.Add("Hoja1") : pack.Workbook.Worksheets[0];
            var range1 = sheet1.Cells[esPlantilla ? "A2" : "A1"].LoadFromCollection(dataSheet1, !esPlantilla);
            ExcelWorksheet sheet2 = !esPlantilla ? pack.Workbook.Worksheets.Add("Hoja2") : pack.Workbook.Worksheets[1];
            var range2 = sheet2.Cells[esPlantilla ? "A2" : "A1"].LoadFromCollection(dataSheet2, !esPlantilla);
            var range3 = sheet2.Cells[esPlantilla ? "A6" : "A5"].LoadFromCollection(dataSheet3, !esPlantilla);
            pack.Save();
        }

        public static void ExportToExcelObservaciones<T>(List<T> data, FileInfo fileInfo, List<ObservacionesDto> observaciones, bool esPlantilla = true, string nombreHoja = "ebaseca")
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var pck = new ExcelPackage(fileInfo);
            ExcelWorksheet sheet = !esPlantilla ? pck.Workbook.Worksheets.Add(nombreHoja) : pck.Workbook.Worksheets[0];
            var range = sheet.Cells[esPlantilla ? "A2" : "A1"].LoadFromCollection(data, !esPlantilla);
            AgregarDropdownObservaciones(pck, data, observaciones);
            pck.Save();
        }

        public static void AgregarDropdownObservaciones<T>(ExcelPackage package, List<T> data, List<ObservacionesDto> observaciones)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var hojaValores = package.Workbook.Worksheets.Add("Valores");
            var hojaResultados = package.Workbook.Worksheets[0];
            int fila = 1;

            foreach (var observacion in observaciones)
            {
                hojaValores.Cells[$"A{fila}"].Value = observacion.Descripcion;
                fila++;
            }

            var rangoObservaciones = hojaValores.Cells[$"A1:A{fila}"];

            for (int i = 2; i <= data.Count + 1; i++)
            {
                var validation = hojaResultados.DataValidations.AddListValidation("K" + i);
                validation.ShowErrorMessage = false;
                validation.ErrorStyle = ExcelDataValidationWarningStyle.warning;
                validation.Formula.ExcelFormula = $"{hojaValores.Name}!{rangoObservaciones}";
            }
        }

        public static void ExportToExcelRevicion<T>(List<T> data, FileInfo fileInfo, bool esPlantilla = false, string nombreHoja = "ebaseca")
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var pck = new ExcelPackage(fileInfo);
            ExcelWorksheet sheet = !esPlantilla ? pck.Workbook.Worksheets.Add(nombreHoja) : pck.Workbook.Worksheets[0];
            var range = sheet.Cells[esPlantilla ? "A2" : "A1"].LoadFromCollection(data, !esPlantilla);
            ExportToExcelRevicionOpciones(pck, data);
            pck.Save();
        }

        public static void ExportToExcelRevicionOpciones<T>(ExcelPackage package, List<T> data)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var sheet = package.Workbook.Worksheets[0];
            sheet.Cells["P1"].Style.Font.Bold = true;
            sheet.Cells["P1"].Value = "Se Aprueba el Resultado";

            sheet.Cells["Z" + 1].Value = "SI";
            sheet.Cells["Z" + 2].Value = "NO";

            for (int i = 2; i <= data.Count() + 1; i++)
            {
                var validation = sheet.DataValidations.AddListValidation("P" + i);
                validation.ShowErrorMessage = true;
                validation.ErrorStyle = ExcelDataValidationWarningStyle.warning;
                validation.Formula.ExcelFormula = "Z1:Z2";
            }
        }
        public static void ExportListToExcel(IEnumerable<MuestreoSustituidoDto> data, string filePath)
        {
            // Create a new ExcelPackage
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet to the empty workbook
                var worksheet = package.Workbook.Worksheets.Add("Data");
                var parametros = data.FirstOrDefault()?.Resultados;

                worksheet.Cells[1, 1].Value = "CLAVE SITIO";
                worksheet.Cells[1, 2].Value = "CLAVE MONITOREO";
                worksheet.Cells[1, 3].Value = "NOMBRE DEL SITIO";
                worksheet.Cells[1, 4].Value = "TIPO CUERPO DE AGUA";
                worksheet.Cells[1, 5].Value = "FECHA REALIZACIÓN";
                worksheet.Cells[1, 6].Value = "AÑO";

                using (ExcelRange range = worksheet.Cells[1, 1, 1, parametros.Count + 6])
                {
                    var border = range.Style.Border;
                    border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Font.Bold = true;
                    range.Style.Font.Size = 11;
                }

                var celda = 7;
                //Agregamos dinámicamente los encabezados
                foreach (var parametro in parametros.OrderBy(x => x.Orden))
                {
                    worksheet.Cells[1, celda].Value = parametro.ClaveParametro;
                    celda++;
                }

                var fila = 2;
                //Agregamos dinámicamente los encabezados
                foreach (var muestreo in data)
                {
                    worksheet.Cells[fila, 1].Value = muestreo.ClaveSitio;
                    worksheet.Cells[fila, 2].Value = muestreo.ClaveMonitoreo;
                    worksheet.Cells[fila, 3].Value = muestreo.NombreSitio;
                    worksheet.Cells[fila, 4].Value = muestreo.TipoCuerpoAgua;
                    worksheet.Cells[fila, 5].Value = muestreo.FechaRealizacion;

                    var columna = 7;
                    foreach (var parametro in muestreo.Resultados.OrderBy(x => x.Orden))
                    {
                        worksheet.Cells[fila, columna].Value = parametro.Valor;
                        columna++;
                    }

                    fila++;
                }

                // Save the workbook to the specified file path
                package.SaveAs(new FileInfo(filePath));
            }
        }

        public static void ExportConsultaRegistroOriginalExcel(IEnumerable<RegistroOriginalDto> registros, List<ParametrosDto> parametros, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            // Add a new worksheet to the empty workbook
            var worksheet = package.Workbook.Worksheets.Add("Registro original");

            worksheet.Cells[1, 1].Value = "ESTATUS";
            worksheet.Cells[1, 2].Value = "AÑO";
            worksheet.Cells[1, 3].Value = "NÚMERO DE CARGA";
            worksheet.Cells[1, 4].Value = "TUVO REPLICA";
            worksheet.Cells[1, 5].Value = "CLAVE SITIO ORIGINAL";
            worksheet.Cells[1, 6].Value = "CLAVE SITIO";
            worksheet.Cells[1, 7].Value = "CLAVE MONITOREO";
            worksheet.Cells[1, 8].Value = "FECHA REALIZACIÓN";
            worksheet.Cells[1, 9].Value = "LABORATORIO";
            worksheet.Cells[1, 10].Value = "TIPO CUERPO AGUA ORIGINAL";
            worksheet.Cells[1, 11].Value = "TIPO CUERPO AGUA";
            worksheet.Cells[1, 12].Value = "TIPO SITIO";

            int inicioParametros = 13;

            foreach (var parametro in parametros)
            {
                worksheet.Cells[1, inicioParametros].Value = parametro.ClaveParametro;
                inicioParametros++;
            }

            var fila = 2;

            foreach (var registro in registros)
            {
                worksheet.Cells[fila, 1].Value = registro.Estatus;
                worksheet.Cells[fila, 2].Value = registro.Anio;
                worksheet.Cells[fila, 3].Value = registro.NumeroCarga;
                worksheet.Cells[fila, 4].Value = "NO";
                worksheet.Cells[fila, 5].Value = registro.ClaveSitioOriginal;
                worksheet.Cells[fila, 6].Value = registro.ClaveSitio;
                worksheet.Cells[fila, 7].Value = registro.ClaveMonitoreo;
                worksheet.Cells[fila, 8].Value = registro.FechaRealizacion;
                worksheet.Cells[fila, 9].Value = registro.Laboratorio;
                worksheet.Cells[fila, 10].Value = registro.TipoCuerpoAgua;
                worksheet.Cells[fila, 11].Value = registro.TipoHomologado;
                worksheet.Cells[fila, 12].Value = registro.TipoSitio;

                foreach (var parametro in registro.Parametros)
                {
                    //Buscamos la celda del parámetro
                    // Usa LINQ para buscar el valor en todas las celdas
                    var query = from cell in worksheet.Cells[1, 1, 1, parametros.Count + inicioParametros]
                                where cell.Value?.ToString().Contains(parametro.ClaveParametro) == true
                                select cell;

                    var parameterCell = query.FirstOrDefault();
                    var column = parameterCell?.Start.Column;

                    worksheet.Cells[fila, column.Value].Value = parametro.Resultado;
                }

                fila++;
            }

            worksheet.Cells[1, 1, 1, parametros.Count + inicioParametros].Style.Font.Bold = true;
            worksheet.Cells[1, 1, 1, parametros.Count + inicioParametros].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            worksheet.Cells[1, 1, 1, parametros.Count + inicioParametros].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[1, 1, 1, parametros.Count + inicioParametros].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#B7DEE8"));
            // Save the workbook to the specified file path
            package.SaveAs(new FileInfo(filePath));
        }

        public static void ExportAcumulacionResultadosExcel(IEnumerable<AcumuladosResultadoDto> data, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(filePath);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            var fila = 2;

            foreach (var registro in data)
            {
                worksheet.Cells[fila, 1].Value = "NO";
                worksheet.Cells[fila, 2].Value = registro.NumeroCarga;
                worksheet.Cells[fila, 3].Value = registro.ClaveUnica;
                worksheet.Cells[fila, 4].Value = registro.ClaveMonitoreo;
                worksheet.Cells[fila, 5].Value = registro.ClaveSitio;
                worksheet.Cells[fila, 6].Value = registro.NombreSitio;
                worksheet.Cells[fila, 7].Value = registro.FechaProgramada;
                worksheet.Cells[fila, 8].Value = registro.FechaRealizacion;
                worksheet.Cells[fila, 9].Value = registro.HoraInicio;
                worksheet.Cells[fila, 10].Value = registro.HoraFin;
                worksheet.Cells[fila, 11].Value = registro.TipoSitio;
                worksheet.Cells[fila, 12].Value = registro.TipoCuerpoAgua;
                worksheet.Cells[fila, 13].Value = registro.SubTipoCuerpoAgua;
                worksheet.Cells[fila, 14].Value = registro.Laboratorio;
                worksheet.Cells[fila, 15].Value = registro.LaboratorioRealizoMuestreo;
                worksheet.Cells[fila, 16].Value = registro.LaboratorioSubrogado;
                worksheet.Cells[fila, 17].Value = registro.GrupoParametro;
                worksheet.Cells[fila, 18].Value = registro.SubGrupo;
                worksheet.Cells[fila, 19].Value = registro.ClaveParametro;
                worksheet.Cells[fila, 20].Value = registro.Parametro;
                worksheet.Cells[fila, 21].Value = registro.UnidadMedida;
                worksheet.Cells[fila, 22].Value = registro.Resultado;
                worksheet.Cells[fila, 23].Value = registro.NuevoResultadoReplica;
                worksheet.Cells[fila, 24].Value = registro.ProgramaAnual;
                worksheet.Cells[fila, 25].Value = registro.IdResultadoLaboratorio;
                worksheet.Cells[fila, 26].Value = registro.FechaEntrega;
                worksheet.Cells[fila, 27].Value = registro.Replica;
                worksheet.Cells[fila, 28].Value = registro.CambioResultado;
                fila++;
            }

            package.SaveAs(new FileInfo(filePath));
        }
    }
}
