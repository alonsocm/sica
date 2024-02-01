using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IAvisoRealizacionRepository : IRepository<AvisoRealizacion>
    {
        List<AvisoRealizacion> ConvertToMuestreosList(List<AvisoRealizacionDto> cargaMuestreoDtoList);
    }
}
