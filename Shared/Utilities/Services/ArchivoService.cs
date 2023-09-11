﻿using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System.Security.Policy;
using System;
using Application.Interfaces.IRepositories;

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

        public ArchivoDto ObtenerArchivoSupervisionMuestreo(string nombreArchivo, string supervision)
        {
            var rutaBase = ObtenerRutaBaseSupervision();
            var rutaCompleta = Path.Combine(rutaBase, supervision, nombreArchivo);

            if (!File.Exists(rutaCompleta))
            {
                throw new Exception($"No se encontró el archivo: {nombreArchivo}");
            }

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(rutaCompleta, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return new ArchivoDto
            {
                Archivo = File.ReadAllBytes(rutaCompleta),
                NombreArchivo = nombreArchivo,
                ContentType = contentType
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
    }
}
