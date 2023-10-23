using Application.DTOs.EvidenciasMuestreo;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class EvidenciaMuestreoRepository : Repository<EvidenciaMuestreo>, IEvidenciaMuestreoRepository
    {
        public EvidenciaMuestreoRepository(SicaContext dbContext) : base(dbContext)
        {
        }

        public bool EliminarEvidenciasMuestreo(long idMuestreo)
        {
            var evidencias = _dbContext.EvidenciaMuestreo.Where(x => x.MuestreoId == idMuestreo).Select(x => new EvidenciaMuestreo { Id = x.Id }).ToList();

            if (evidencias.Any())
            {
                _dbContext.EvidenciaMuestreo.RemoveRange(evidencias);
            }

            return true;
        }

        public async Task<IEnumerable<InformacionEvidenciaDto>> GetInformacionEvidenciasAsync()
        {
            var informacionEvidencias = _dbContext.EvidenciaMuestreo;
            List<InformacionEvidenciaDto> informacionEvidenciaDtos = new();

            if (informacionEvidencias.Any())
            {
                informacionEvidenciaDtos = await informacionEvidencias.Select(x => new InformacionEvidenciaDto
                {
                    Muestreo = x.Muestreo.ProgramaMuestreo.NombreCorrectoArchivo ?? string.Empty,
                    TipoEvidencia = x.TipoEvidenciaMuestreo.Descripcion,
                    NombreArchivo = x.NombreArchivo,
                    Latitud = x.Latitud.ToString() ?? string.Empty,
                    Longitud = x.Longitud.ToString() ?? string.Empty,
                    Altitud = x.Altitud.ToString() ?? string.Empty,
                    Marca = x.MarcaCamara ?? string.Empty,
                    Modelo = x.ModeloCamara ?? string.Empty,
                    Iso = x.Iso ?? string.Empty,
                    Apertura = x.Apertura ?? string.Empty,
                    Obturador = x.Obturador ?? string.Empty,
                    Direccion = x.Direccion ?? string.Empty,
                    DistanciaFocal = x.DistanciaFocal ?? string.Empty,
                    Flash = x.Flash ?? string.Empty,
                    Tamanio = x.Tamano ?? string.Empty,
                    FechaCreacion = x.FechaCreacion == null ? string.Empty : x.FechaCreacion.Value.ToString("dd/MM/yyyy hh:mm:ss"),
                    Placas = x.Placas ?? string.Empty,
                    Laboratorio = x.Laboratorio ?? string.Empty,
                    FechaInicio = x.FechaInicio ?? string.Empty,
                    FechaFinal = x.FechaFin ?? string.Empty,
                    HoraInicio = x.HoraInicio ?? string.Empty,
                    HoraFinal = x.HoraFin ?? string.Empty
                }).ToListAsync();
            }

            return informacionEvidenciaDtos;
        }
    }
}
