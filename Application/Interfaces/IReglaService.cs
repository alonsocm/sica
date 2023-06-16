using Application.DTOs;

namespace Application.Interfaces
{
    public interface IReglaService
    {
        bool InCumpleReglaMinimoMaximo(string regla, string valorParametro);
        bool CumpleLimitesDeteccion(LimiteDeteccionDto limites, decimal valorParametro);
    }
}
