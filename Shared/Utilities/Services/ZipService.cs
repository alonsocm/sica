using Application.DTOs;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.Services
{
    public static class ZipService
    {
        public static byte[] GenerarZip(List<ArchivoDto> archivos)
        {
            var rutaCarpetaTemporal = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "CarpetaOrigen"));
            var rutaZipTemporal = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "CarpetaDestino"));

            foreach (var evidencia in archivos)
            {
                using var stream = File.Create(Path.Combine(rutaCarpetaTemporal.FullName, evidencia.NombreArchivo));
                stream.Write(evidencia.Archivo);
            }

            ZipFile.CreateFromDirectory(rutaCarpetaTemporal.FullName, Path.Combine(rutaZipTemporal.FullName, "archivos.zip"));

            var bytes = File.ReadAllBytes(Path.Combine(rutaZipTemporal.FullName, "archivos.zip"));
            Directory.Delete(rutaCarpetaTemporal.FullName, true);
            Directory.Delete(rutaZipTemporal.FullName, true);

            return bytes;
        }
    }
}
