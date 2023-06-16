using Application.DTOs;
using Application.Exceptions;
using Application.Models;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;

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
                        var columna = columns.SingleOrDefault(c => c.ColName.ToString() == colName);

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

                    prop.SetValue(obj, Convert.ChangeType(value, type));
                }

                if (obj != null)
                {
                    list.Add(obj);
                }
            }

            return list;
        }

        public static void ExportToExcel<T>(List<T> data, FileInfo fileInfo, bool esPlantilla = false, string nombreHoja = "ebaseca")
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var pck = new ExcelPackage(fileInfo);
            ExcelWorksheet sheet = !esPlantilla ? pck.Workbook.Worksheets.Add(nombreHoja) : pck.Workbook.Worksheets[0];
            var range = sheet.Cells[esPlantilla ? "A2" : "A1"].LoadFromCollection(data, !esPlantilla);
            pck.Save();
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

            for (int i = 2; i <=data.Count + 1; i++)
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
    }
}
