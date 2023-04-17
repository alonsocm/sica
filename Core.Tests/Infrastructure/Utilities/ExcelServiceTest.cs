using Shared.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Infrastructure.Utilities
{
    public class ExcelServiceTest
    {
        [Fact]
        public void CanReadData()
        {
            ExcelService.Mappings = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Nombre", "Nombre"),
                new KeyValuePair<string, string>("Apellido Paterno", "ApellidoPaterno"),
                new KeyValuePair<string, string>("Apellido Materno", "ApellidoMaterno"),
            };

            FileInfo fileInfo = new(@"C:\Users\Alonso\Desktop\prueba_carga.xlsx");

            var result = ExcelService.Import<Persona>(fileInfo, "Hoja1");

            Assert.True(result is not null);
        }

        [Fact]
        public void ExcelFileIsCreate()
        {
            FileInfo fileInfo = new(@"C:\Users\Alonso\Desktop\resultados.xlsx");
                
            var personas = new List<Persona>
            {
                new Persona { Nombre = "Alonso", ApellidoPaterno = "Castro", ApellidoMaterno = "Maximo" }
            };

            ExcelService.ExportToExcel(personas, fileInfo);
        }

        public class Persona
        {
            public string Nombre { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
        }
    }
}
