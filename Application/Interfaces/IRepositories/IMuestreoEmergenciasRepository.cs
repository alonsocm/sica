using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IMuestreoEmergenciasRepository : IRepository<MuestreoEmergencia>
    {
        List<MuestreoEmergencia> ConvertToMuestreosList(List<CargaMuestreoEmergenciaDto> cargaMuestreoDtoList);
    }
}
