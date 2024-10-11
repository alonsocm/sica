using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;

namespace Shared.Utilities.Services
{
    internal class ArchivoService : IArchivoService
    {
        private readonly IConfiguration _configuration;
        private readonly IEvidenciaSupervisionMuestreoRepository _evidenciasupervisionrepository;

        public ArchivoService(IConfiguration configuration, IEvidenciaSupervisionMuestreoRepository evidenciasupervisionrepository)
        {
            _configuration=configuration;
            _evidenciasupervisionrepository = evidenciasupervisionrepository;
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

        //CAMBIAR POR METODO GENERAL
        public bool GuardarEvidencias(List<IFormFile> evidenciasReplicas)
        {
            var ruta = ObtenerRutaBaseEvidenciaReplica();
            var directorioMuestreo = Directory.CreateDirectory(Path.Combine(ruta, DateTime.Now.ToShortDateString().Replace('/', '_')));

            foreach (var archivo in evidenciasReplicas)
            {
                using var stream = File.Create(Path.Combine(directorioMuestreo.FullName, archivo.FileName));
                archivo.CopyTo(stream);
            }

            return true;
        }


        public List<string> GuardarEvidenciasSupervision(ArchivosSupervisionDto evidenciasMuestreo)
        {
            var ruta = ObtenerRutaBaseSupervision();
            List<string> lstNombreArchivos = new List<string>();
            var directorioMuestreo = Directory.CreateDirectory(Path.Combine(ruta, evidenciasMuestreo.SupervisionId.ToString()));

            var datosEvidencias = _evidenciasupervisionrepository.ObtenerElementosPorCriterioAsync(x => x.SupervisionMuestreoId == evidenciasMuestreo.SupervisionId && x.TipoEvidenciaId == Convert.ToInt64(Application.Enums.TipoEvidencia.EvidenciaSupervisión));
            var index = (datosEvidencias.Result.ToList().Count > 0) ? datosEvidencias.Result.ToList().Count + 1 : 1;

            foreach (var archivo in evidenciasMuestreo.Archivos)
            {

                string NombreArchivo = (archivo.ContentType == "application/pdf") ? evidenciasMuestreo.ClaveMuestreo + ".pdf" :
                evidenciasMuestreo.ClaveMuestreo + "_" + index + archivo.FileName.Substring(archivo.FileName.IndexOf('.'), archivo.FileName.Length - archivo.FileName.IndexOf('.'));
                lstNombreArchivos.Add(NombreArchivo);
                using var stream = File.Create(Path.Combine(directorioMuestreo.FullName, NombreArchivo));
                archivo.CopyTo(stream);
                if (archivo.ContentType != "application/pdf") { index++; }
            }
            return lstNombreArchivos;
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

        public string ObtenerRutaBaseEvidenciaReplica()
        {
            return _configuration.GetValue<string>("Archivos:EvidenciasReplicas");
        }

        public string ObtenerRutaBaseSupervision()
        {
            return _configuration.GetValue<string>("Archivos:EvidenciasSupervisionMuestreo");
        }

        public string ObtenerRutaBaseInformeSupervision()
        {
            return _configuration.GetValue<string>("Archivos:InformeSupervisionMuestreo");
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
                NombreArchivo = nombreArchivo,
                ContentType = GetContentType(rutaEvidencia)
            };
        }

        private static string GetContentType(string rutaEvidencia)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(rutaEvidencia, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
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

       //Se puede realizar un metodo general para descargar conforme al nombre de la carpeta
        public List<ArchivoDto> ObtenerEvidenciasPorReplica(string nombreCarpeta)
        {
            var rutaBase = ObtenerRutaBaseEvidenciaReplica();
            var rutaEvidenciasReplicas = Path.Combine(rutaBase, nombreCarpeta);
            DirectoryInfo carpetaEvidenciasMuestreo = new(rutaEvidenciasReplicas);
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

        public ArchivoDto ObtenerArchivoSupervisionMuestreo(string nombreArchivo, string supervision)
        {
            var rutaBase = ObtenerRutaBaseSupervision();
            var rutaCompleta = Path.Combine(rutaBase, supervision, nombreArchivo);

            if (!File.Exists(rutaCompleta))
            {
                throw new Exception($"No se encontró el archivo: {nombreArchivo}");
            }


            return new ArchivoDto
            {
                Archivo = File.ReadAllBytes(rutaCompleta),
                NombreArchivo = nombreArchivo,
                ContentType = GetContentType(rutaCompleta)
            };
        }

        public bool EliminarArchivoSupervisionMuestreo(string nombreArchivo, string supervision)
        {
            var rutaBase = ObtenerRutaBaseSupervision();
            var rutaCompleta = Path.Combine(rutaBase, supervision, nombreArchivo);

            if (File.Exists(rutaCompleta))
            {
                File.Delete(rutaCompleta);
            }
            else
            {
                throw new Exception($"No se encontró el archivo: {nombreArchivo}");
            }

            return true;
        }

        public bool GuardarInformeSupervision(string informe, IFormFile archivo)
        {
            var ruta = ObtenerRutaBaseInformeSupervision();
            var directorioInforme = Directory.CreateDirectory(Path.Combine(ruta, informe));

            using var stream = File.Create(Path.Combine(directorioInforme.FullName, archivo.FileName));
            archivo.CopyTo(stream);

            return true;
        }

        public async Task<byte[]> ConvertIFormFileToByteArray(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        //public List<EvidenciasReplicasResultado> OrdenarEvidenciasClaveUnica(List<IFormFile> archivos)
        //{
        //    var evidencias = new List<EvidenciasReplicasResultado>();
        //    foreach (var archivo in archivos)
        //    {
        //        var claveUnica = archivo.FileName[..archivo.FileName.LastIndexOf("_")];
              
        //        if (!evidencias.Any(a => a.ClaveUnica == claveUnica))
        //        {
        //            evidencias.Add(new EvidenciasReplicasResultado { ClaveUnica = claveUnica });
        //        }
        //        var evidencia = evidencias.Find(a => a.ClaveUnica == claveUnica);
        //        evidencia?.Archivos.Add(archivo);
        //    }
        //    return evidencias;
        //}
    }
}
