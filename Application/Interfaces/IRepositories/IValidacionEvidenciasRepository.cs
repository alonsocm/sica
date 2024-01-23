using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IValidacionEvidenciasRepository : IRepository<AvisoRealizacion>
    {
        List<AvisoRealizacion> ConvertToMuestreosList(List<AvisoRealizacionDto> cargaMuestreoDtoList);
    }
}
