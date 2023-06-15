using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Operacion.Resultados.Comands.ValidarResultadosPorReglasCommandHandler;

namespace Application.Interfaces
{
    public interface IReglaService
    {
        bool InCumpleReglaMinimoMaximo(string regla, string valorParametro);
        bool CumpleLimitesDeteccion(LimiteDeteccion limites, decimal valorParametro);
    }
}
