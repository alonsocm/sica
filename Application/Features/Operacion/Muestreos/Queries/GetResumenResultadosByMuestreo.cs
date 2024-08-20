using Application.DTOs;
using Application.DTOs.Users;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Muestreos.Queries
{
    public class GetResumenResultadosByMuestreo : IRequest<Response<ResumenDTO>>
    {
        public IEnumerable<long> Muestreos { get; set; }
        public List<Filter> Filters { get; set; }
        public bool SelectAll { get; set; }
    }

    public class GetResumenResultadosByMuestreoHandler : IRequestHandler<GetResumenResultadosByMuestreo, Response<ResumenDTO>>
    {
        private readonly IMuestreoRepository _repository;

        public GetResumenResultadosByMuestreoHandler(IMuestreoRepository repository)
        {
            _repository=repository;
        }

        public async Task<Response<ResumenDTO>> Handle(GetResumenResultadosByMuestreo request, CancellationToken cancellationToken)
        {
            var response = new ResumenDTO();

            var data = await _repository.GetResumenMuestreosAsync(new List<long> { (long)Enums.EstatusMuestreo.CargaResultados, (long)Enums.EstatusMuestreo.EvidenciasCargadas });

            if (request.Filters.Any() || request.SelectAll)
            {
                data = data.AsQueryable();
                var expressions = MuestreoExpression.GetExpressionList(request.Filters);
                List<MuestreoDto> lstMuestreo = new();

                foreach (var filter in expressions)
                {
                    if (request.Filters.Count == 2 && request.Filters[0].Conditional == "equals" && request.Filters[1].Conditional == "equals")
                    {
                        var dataFinal = data;
                        dataFinal = dataFinal.AsQueryable().Where(filter);
                        lstMuestreo.AddRange(dataFinal);
                    }
                    else
                    {
                        data = data.AsQueryable().Where(filter);
                    }
                }

                data = (lstMuestreo.Count > 0) ? lstMuestreo.AsQueryable() : data;

                response.ResumenResultado = await _repository.GetResumenResultados(data.Select(s => s.MuestreoId).Distinct());
                response.ResumenMuestreo = GetTotalesPorFiltro(data);

                return new Response<ResumenDTO>(response);
            }
            else
            {
                response.ResumenResultado = await _repository.GetResumenResultados(request.Muestreos);
                response.ResumenMuestreo = GetTotalesPorFiltro(data.Where(w => request.Muestreos.Contains(w.MuestreoId)));
                return new Response<ResumenDTO>(response);
            }
        }

        public IEnumerable<ResumenMuestreoDTO> GetTotalesPorFiltro(IEnumerable<MuestreoDto> muestreos)
        {
            var listaResumen = new List<ResumenMuestreoDTO>();

            var resumen = new ResumenMuestreoDTO();
            var oc = muestreos.Where(w => w.OCDL.StartsWith("OC")).Select(s => s.OCDL).GroupBy(p => p)
            .Select(g => new CriterioDTO { Nombre = g.Key, Cantidad = g.Count() })
            .ToList();

            resumen.Tipo= "OC";
            resumen.Criterios = oc;
            listaResumen.Add(resumen);

            var resumenDL = new ResumenMuestreoDTO();
            var dl = muestreos.Where(w => w.OCDL.StartsWith("DL")).Select(s => s.OCDL).GroupBy(p => p)
            .Select(g => new CriterioDTO { Nombre = g.Key, Cantidad = g.Count() })
            .ToList();

            resumenDL.Tipo= "DL";
            resumenDL.Criterios = dl;
            listaResumen.Add(resumenDL);

            var resumenFecha = new ResumenMuestreoDTO();
            var fechaRealizacion = muestreos.Select(s => s.FechaRealizacion).GroupBy(p => p)
            .Select(g => new CriterioDTO { Nombre = g.Key, Cantidad = g.Count() })
            .ToList();

            resumenFecha.Tipo= "Fecha";
            resumenFecha.Criterios = fechaRealizacion;
            listaResumen.Add(resumenFecha);

            var resumenEstado = new ResumenMuestreoDTO();
            var estado = muestreos.Select(s => s.Estado).GroupBy(p => p)
            .Select(g => new CriterioDTO { Nombre = g.Key, Cantidad = g.Count() })
            .ToList();

            resumenEstado.Tipo= "Estado";
            resumenEstado.Criterios = estado;
            listaResumen.Add(resumenEstado);

            var resumenTipoCuerpoAgua = new ResumenMuestreoDTO();
            var tipoCuerpoAgua = muestreos.Select(s => s.TipoCuerpoAgua).GroupBy(p => p)
            .Select(g => new CriterioDTO { Nombre = g.Key, Cantidad = g.Count() })
            .ToList();

            resumenTipoCuerpoAgua.Tipo= "TipoCuerpoAgua";
            resumenTipoCuerpoAgua.Criterios = tipoCuerpoAgua;
            listaResumen.Add(resumenTipoCuerpoAgua);

            var resumenLaboratorio = new ResumenMuestreoDTO();
            var laboratorio = muestreos.Select(s => s.Laboratorio).GroupBy(p => p)
            .Select(g => new CriterioDTO { Nombre = g.Key, Cantidad = g.Count() })
            .ToList();

            resumenLaboratorio.Tipo= "Laboratorio";
            resumenLaboratorio.Criterios = laboratorio;
            listaResumen.Add(resumenLaboratorio);

            return listaResumen;
        }
    }
}
