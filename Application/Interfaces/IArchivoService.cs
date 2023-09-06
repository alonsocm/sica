using Application.DTOs;
using Application.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IArchivoService
    {
        string ObtenerRutaBase();
        List<EvidenciasMuestreo> OrdenarEvidenciasPorMuestreo(List<IFormFile> archivos);
        ArchivoDto ObtenerEvidencia(string nombreArchivo);
        List<ArchivoDto> ObtenerEvidenciasPorMuestreo(string muestreo);
        bool GuardarEvidencias(EvidenciasMuestreo evidenciasMuestreo);
        bool EliminarEvidencias(string muestreo);
        bool GuardarEvidenciasSupervision(ArchivosSupervisionDto evidenciasMuestreo);
    }
}
