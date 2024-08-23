using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enums
{
    public enum EstatusResultado
    {
        IncidenciasResultados = 1,
        EnvíoLaboratorioExterno = 2,
        CargaRéplicasLaboratorioExterno = 3,
        EnvíoaSRENAMECA = 4,
        CargaValidaciónSRENAMECA = 5,
        AcumulaciónResultadosReplica = 6,
        MóduloInicialReglasReplica = 7,
        MóduloReglasReplica = 8,
        ResumenValidaciónReglasReplica = 9,
        RechazoResultadosPorArchivo = 10,
        AprobaciónResultadosPorArchivo = 11,
        LiberaciónResultadosReplica = 12,
        EnviadoaPenalizaciónReplica = 13
    }
}
