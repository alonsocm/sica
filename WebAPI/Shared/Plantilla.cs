using Application.Exceptions;
using Microsoft.AspNetCore.StaticFiles;

namespace WebAPI.Shared
{
    public class Plantilla
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Plantilla(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration=configuration;
            _env=env;
        }

        public string ObtenerRutaPlantilla(string nombrePlantilla)
        {
            var rootPath = _env.WebRootPath;
            if (rootPath == null)
            {
                throw new ApiException("No se encontró la carpeta root");
            }

            var plantillaPath = _configuration[$"PlantillasExcel:{nombrePlantilla}"];
            if (plantillaPath == null)
            {
                throw new ApiException("No se encontró la ruta de la plantilla requerida");
            }

            var templatePath = Path.Combine(rootPath, plantillaPath);
            if (!File.Exists(templatePath))
            {
                throw new ApiException("No se encontró el archivo de la plantilla requerida");
            }

            return templatePath;
        }

        public FileInfo GenerarArchivoTemporal(string templatePath, out string temporalFilePath)
        {
            temporalFilePath = Path.GetTempFileName();
            File.Copy(templatePath, temporalFilePath, true);
            FileInfo fileInfo = new(temporalFilePath);
            
            return fileInfo;
        }

        public byte[] GenerarArchivoDescarga(string temporalFilePath, out string contentType)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(temporalFilePath, out contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes = File.ReadAllBytes(temporalFilePath);

            File.Delete(temporalFilePath);

            return bytes;
        }
    }
}
