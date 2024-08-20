using Application.DTOs;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Muestreos.Queries
{
    public class GetMuestreosPaginados : IRequest<PagedResponse<IEnumerable<MuestreoDto>>>
    {
        public bool EsLiberacion { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filter { get; set; }
        public OrderBy? OrderBy { get; set; }
    }

    public class GetMuestreosHandler : IRequestHandler<GetMuestreosPaginados, PagedResponse<IEnumerable<MuestreoDto>>>
    {
        private readonly IMuestreoRepository _repositoryAsync;

        public GetMuestreosHandler(IMuestreoRepository repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<MuestreoDto>>> Handle(GetMuestreosPaginados request, CancellationToken cancellationToken)
        {
            var estatus = new List<long>();

            if (!request.EsLiberacion)
            {
                estatus.Add((long)Enums.EstatusMuestreo.CargaResultados);
                estatus.Add((long)Enums.EstatusMuestreo.EvidenciasCargadas);
            }
            else
            {
                //Revisar porque se tiene los demas estatus
                estatus.Add((long)Enums.EstatusMuestreo.Liberaciondemonitoreos);

                //se comenta porque ya no es necesario este estatus aqui ya que despues de ser evidencias cargadas deben de pasar por la
                //validación de reglas
                //estatus.Add((long)Enums.EstatusMuestreo.EvidenciasCargadas);

                estatus.Add((long)Enums.EstatusMuestreo.RevisiónOCDLSECAIA);
                estatus.Add((long)Enums.EstatusMuestreo.Liberaciondemonitoreosconextencióndefecha);
                estatus.Add((long)Enums.EstatusOcdlSEcaia.Validado);
            }

            var data = await _repositoryAsync.GetResumenMuestreosAsync(estatus);
            data = data.AsQueryable();

            if (request.Filter.Any())
            {
                var expressions = MuestreoExpression.GetExpressionList(request.Filter);
                List<MuestreoDto> lstMuestreo = new List<MuestreoDto>();

                foreach (var filter in expressions)
                {
                    if (request.Filter.Count == 2 && request.Filter[0].Conditional == "equals" && request.Filter[1].Conditional == "equals")
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
            }

            if (request.OrderBy != null)
            {
                if (request.OrderBy.Type == "asc")
                {
                    data = data.AsQueryable().OrderBy(MuestreoExpression.GetOrderByExpression(request.OrderBy.Column));
                }
                else if (request.OrderBy.Type == "desc")
                {
                    data = data.AsQueryable().OrderByDescending(MuestreoExpression.GetOrderByExpression(request.OrderBy.Column));
                }
            }

            return PagedResponse<MuestreoDto>.CreatePagedReponse(data, request.Page, request.PageSize);
        }
    }
}
