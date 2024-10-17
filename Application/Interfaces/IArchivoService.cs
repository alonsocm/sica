using Application.DTOs;
using Application.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IArchivoService
    {
        string ObtenerRutaBase();
        List<EvidenciasMuestreo> OrdenarEvidenciasPorMuestreo(List<IFormFile> archivos);
        ArchivoDto ObtenerEvidencia(string nombreArchivo);
        List<ArchivoDto> ObtenerEvidenciasPorMuestreo(string muestreo);
        List<ArchivoDto> ObtenerEvidenciasPorReplica();
        public ArchivoDto ObtenerArchivoSupervisionMuestreo(string nombreArchivo, string supervision);
        public bool GuardarInformeSupervision(string informe, IFormFile archivo);
        bool GuardarEvidencias(EvidenciasMuestreo evidenciasMuestreo);
        bool GuardarEvidencias(List<IFormFile> evidenciasMuestreo);
        bool EliminarEvidencias(string muestreo);
        bool EliminarArchivoSupervisionMuestreo(string nombreArchivo, string supervision);
        List<string> GuardarEvidenciasSupervision(ArchivosSupervisionDto evidenciasMuestreo);
        public Task<byte[]> ConvertIFormFileToByteArray(IFormFile file);      
    }
}
