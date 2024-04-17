using Application.DTOs;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Muestreos.Queries
{
    public class GetMuestreosPaginados : IRequest<PagedResponse<List<MuestreoDto>>>
    {
        public bool EsLiberacion { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filter { get; set; }
    }

    public class GetMuestreosHandler : IRequestHandler<GetMuestreosPaginados, PagedResponse<List<MuestreoDto>>>
    {
        private readonly IMuestreoRepository _repositoryAsync;

        public GetMuestreosHandler(IMuestreoRepository repositoryAsync)
        {
            _repositoryAsync=repositoryAsync;
        }

        public async Task<PagedResponse<List<MuestreoDto>>> Handle(GetMuestreosPaginados request, CancellationToken cancellationToken)
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

            return PagedResponse<MuestreoDto>.CreatePagedReponse(data.ToList(), request.Page, request.PageSize);
        }
    }
}
