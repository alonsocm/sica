using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.Services
{
    internal class ArchivoService : IArchivoService
    {
        private readonly IConfiguration _configuration;

        public ArchivoService(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        public bool EliminarEvidencias(string muestreo)
        {
            string rutaBase = ObtenerRutaBase();
            string directorioEvidencias = Path.Combine(rutaBase, muestreo);

            if (Directory.Exists(directorioEvidencias))
            {
                Directory.Delete(directorioEvidencias, true);
            }

            return true;
        }

        public bool GuardarEvidencias(EvidenciasMuestreo evidenciasMuestreo)
        {
            var ruta = ObtenerRutaBase();
            var directorioMuestreo = Directory.CreateDirectory(Path.Combine(ruta, evidenciasMuestreo.Muestreo));

            foreach (var archivo in evidenciasMuestreo.Archivos)
            {
                using var stream = File.Create(Path.Combine(directorioMuestreo.FullName, archivo.FileName));
                archivo.CopyTo(stream);
            }

            return true;
        }


        public bool GuardarEvidenciasSupervision(ArchivosSupervisionDto evidenciasMuestreo)
        {
            var ruta = ObtenerRutaBaseSupervision();
            var directorioMuestreo = Directory.CreateDirectory(Path.Combine(ruta, evidenciasMuestreo.SupervisionId.ToString()));

            foreach (var archivo in evidenciasMuestreo.Archivos)
            {
                using var stream = File.Create(Path.Combine(directorioMuestreo.FullName, archivo.FileName));
                archivo.CopyTo(stream);
            }

            return true;
        }


        public List<EvidenciasMuestreo> OrdenarEvidenciasPorMuestreo(List<IFormFile> archivos)
        {
            var evidencias = new List<EvidenciasMuestreo>();

            foreach (var archivo in archivos)
            {
                var muestreo = archivo.FileName[..archivo.FileName.LastIndexOf("-")];

                if (!evidencias.Any(a => a.Muestreo == muestreo))
                {
                    evidencias.Add(new EvidenciasMuestreo { Muestreo = muestreo });
                }

                var evidencia = evidencias.Find(a => a.Muestreo == muestreo);
                evidencia?.Archivos.Add(archivo);
            }

            return evidencias;
        }

        public string ObtenerRutaBase()
        {
            return _configuration.GetValue<string>("Archivos:EvidenciasMuestreo");
        }

        public string ObtenerRutaBaseSupervision()
        {
            return _configuration.GetValue<string>("Archivos:EvidenciasSupervisionMuestreo");
        }

        public ArchivoDto ObtenerEvidencia(string nombreArchivo)
        {
            var rutaBase = ObtenerRutaBase();
            var muestreo = nombreArchivo[..nombreArchivo.LastIndexOf("-")];
            var rutaEvidencia = Path.Combine(rutaBase, muestreo, nombreArchivo);

            if (!File.Exists(rutaEvidencia))
            {
                throw new Exception($"No se encontró el archivo: {nombreArchivo}");
            }

            return new ArchivoDto
            {
                Archivo = File.ReadAllBytes(rutaEvidencia),
                NombreArchivo = nombreArchivo
            };
        }

        public List<ArchivoDto> ObtenerEvidenciasPorMuestreo(string muestreo)
        {
            var rutaBase = ObtenerRutaBase();
            var rutaEvidenciasMuestreo = Path.Combine(rutaBase, muestreo);
            DirectoryInfo carpetaEvidenciasMuestreo = new(rutaEvidenciasMuestreo);
            List<ArchivoDto> archivos = new();

            if (carpetaEvidenciasMuestreo.Exists)
            {
                foreach (var archivo in carpetaEvidenciasMuestreo.GetFiles())
                {
                    archivos.Add(new ArchivoDto
                    {
                        Archivo = File.ReadAllBytes(archivo.FullName),
                        NombreArchivo = archivo.Name
                    });
                }
            }

            return archivos;
        }
    }
}
