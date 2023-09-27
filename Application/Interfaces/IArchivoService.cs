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
        public ArchivoDto ObtenerArchivoSupervisionMuestreo(string nombreArchivo, string supervision);
        public bool GuardarInformeSupervision(string informe, IFormFile archivo);
        bool GuardarEvidencias(EvidenciasMuestreo evidenciasMuestreo);
        bool EliminarEvidencias(string muestreo);
        bool EliminarArchivoSupervisionMuestreo(string nombreArchivo, string supervision);
        List<string> GuardarEvidenciasSupervision(ArchivosSupervisionDto evidenciasMuestreo);
    }
}
