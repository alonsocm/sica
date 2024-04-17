using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Models;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Muestreos.Queries
{
    public class GetMuestreosExcel : IRequest<List<CargaResultadosEbaseca>>
    {
        public bool EsLiberacion { get; set; }
        public List<Filter> Filter { get; set; }
    }

    public class GetMuestreosExcelHandler : IRequestHandler<GetMuestreosExcel, List<CargaResultadosEbaseca>>
    {
        private readonly IMuestreoRepository _repositoryAsync;

        public GetMuestreosExcelHandler(IMuestreoRepository repositoryAsync)
        {
            _repositoryAsync=repositoryAsync;
        }

        public async Task<List<CargaResultadosEbaseca>> Handle(GetMuestreosExcel request, CancellationToken cancellationToken)
        {
            var estatus = new List<long>();

            if (!request.EsLiberacion)
            {
                estatus.Add((long)Enums.EstatusMuestreo.Cargado);
                estatus.Add((long)Enums.EstatusMuestreo.EvidenciasCargadas);
            }
            else
            {
                //Revisar porque se tiene los demas estatus
                estatus.Add((long)Enums.EstatusMuestreo.NoEnviado);

                //se comenta porque ya no es necesario este estatus aqui ya que despues de ser evidencias cargadas deben de pasar por la
                //validación de reglas
                //estatus.Add((long)Enums.EstatusMuestreo.EvidenciasCargadas);

                estatus.Add((long)Enums.EstatusMuestreo.Enviado);
                estatus.Add((long)Enums.EstatusMuestreo.EnviadoConExtensionFecha);
                estatus.Add((long)Enums.EstatusMuestreo.Validado);
            }

            var data = await _repositoryAsync.GetResumenMuestreosAsync(estatus);

            if (request.Filter.Any())
            {
                var expressions = MuestreoExpression.GetExpressionList(request.Filter);

                foreach (var filter in expressions)
                {
                    data = data.AsQueryable().Where(filter);
                }
            }

            List<CargaResultadosEbaseca> muestreosExcel = new();

            data.ToList().ForEach(muestreo =>
                muestreosExcel.Add(new CargaResultadosEbaseca
                {
                    Estatus = muestreo.Estatus,
                    EvidenciasCompletas = (muestreo.Evidencias.Count > 0) ? "SI" : "NO",
                    NumeroCarga = muestreo.NumeroEntrega,
                    ClaveNOSEC = muestreo.ClaveSitio,
                    Clave5K = string.Empty,
                    ClaveMonitoreo = muestreo.ClaveMonitoreo,
                    TipoSitio = muestreo.TipoSitio,
                    NombreSitio = muestreo.NombreSitio,
                    OCDL = muestreo.OCDL,
                    TipoCuerpoAgua = muestreo.TipoCuerpoAgua,
                    SubtipoCuerpoAgua = muestreo.SubTipoCuerpoAgua,
                    ProgramaAnual = muestreo.ProgramaAnual,
                    Laboratorio = muestreo.Laboratorio,
                    LaboratorioSubrogado = muestreo.LaboratorioSubrogado,
                    FechaProgramacion = muestreo.FechaProgramada,
                    FechaRealizacion = muestreo.FechaRealizacion,
                    HoraInicioMuestreo = muestreo.HoraInicio,
                    HoraFinMuestreo = muestreo.HoraFin,
                    FechaCargaSica = muestreo.FechaCarga,
                    FechaEntrega = muestreo.FechaEntregaMuestreo
                }
            ));

            return muestreosExcel;
        }
    }
}
