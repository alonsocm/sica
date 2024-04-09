using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.Muestreos.Queries
{
    public class GetMuestreos : IRequest<PagedResponse<List<MuestreoDto>>>
    {
        public bool EsLiberacion { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filter { get; set; }
    }

    public class GetMuestreosHandler : IRequestHandler<GetMuestreos, PagedResponse<List<MuestreoDto>>>
    {
        private readonly IMuestreoRepository _repositoryAsync;

        public GetMuestreosHandler(IMuestreoRepository repositoryAsync)
        {
            _repositoryAsync=repositoryAsync;
        }

        public async Task<PagedResponse<List<MuestreoDto>>> Handle(GetMuestreos request, CancellationToken cancellationToken)
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
                List<Expression<Func<MuestreoDto, bool>>> expressions = new();

                foreach (var filter in request.Filter)
                {
                    if (!string.IsNullOrEmpty(filter.Conditional))
                    {
                        expressions.Add(GetExpression(filter));
                    }
                    else
                    {
                        if (filter.Values != null && filter.Values.Any())
                        {
                            expressions.Add(_repositoryAsync.GetContainsExpression(filter.Column, filter.Values));
                        }
                    }
                }

                foreach (var filter in expressions)
                {
                    data = data.AsQueryable().Where(filter);
                }
            }

            return PagedResponse<MuestreoDto>.CreatePagedReponse(data.ToList(), request.Page, request.PageSize);
        }

        private Expression<Func<MuestreoDto, bool>> GetExpression(Filter filter)
        {
            return filter.Conditional switch
            {
                "notequals" => _repositoryAsync.GetNotEqualsExpression(filter.Column, filter.Value),
                "beginswith" => _repositoryAsync.GetBeginsWithExpression(filter.Column, filter.Value),
                "notbeginswith" => _repositoryAsync.GetNotBeginsWithExpression(filter.Column, filter.Value),
                "endswith" => _repositoryAsync.GetEndsWithExpression(filter.Column, filter.Value),
                "notendswith" => _repositoryAsync.GetNotEndsWithExpression(filter.Column, filter.Value),
                "contains" => _repositoryAsync.GetContainsExpression(filter.Column, filter.Value),
                "notcontains" => _repositoryAsync.GetNotContainsExpression(filter.Column, filter.Value),
                _ => muestreo => true,
            };
        }
    }
}
