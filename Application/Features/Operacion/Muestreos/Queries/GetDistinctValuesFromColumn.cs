using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Muestreos.Queries
{
    public class GetDistinctValuesFromColumn : IRequest<Response<IEnumerable<object>>>
    {
        public string Column { get; set; }
    }

    public class GetDistinctValuesFromColumnHandler : IRequestHandler<GetDistinctValuesFromColumn, Response<IEnumerable<object>>>
    {
        private readonly IMuestreoRepository _repositoryAsync;

        public GetDistinctValuesFromColumnHandler(IMuestreoRepository repositoryAsync)
        {
            _repositoryAsync=repositoryAsync;
        }

        public async Task<Response<IEnumerable<object>>> Handle(GetDistinctValuesFromColumn request, CancellationToken cancellationToken)
        {
            var muestreos = await _repositoryAsync.GetResumenMuestreosAsync(new List<long> { (long)Enums.EstatusMuestreo.Cargado });
            var response = _repositoryAsync.GetDistinctValuesFromColumn(request.Column, muestreos);

            return new Response<IEnumerable<object>>(response);
        }
    }
}
