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
                    if (filter.Values.Any())
                    {
                        expressions.Add(_repositoryAsync.GetContainsExpression(filter.Column, filter.Values));
                    }
                }

                foreach (Expression<Func<MuestreoDto, bool>> filter in expressions)
                {
                    data = data.AsQueryable().Where(filter);
                }
            }

            var response = PagedResponse<MuestreoDto>.CreatePagedReponse(data.ToList(), request.Page, request.PageSize);

            return response;
        }
    }
}
