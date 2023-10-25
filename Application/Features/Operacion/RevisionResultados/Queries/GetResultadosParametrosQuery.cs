
using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.ResumenResultados.Queries
{

    public class GetResultadosParametrosQuery : IRequest<Response<List<ResultadoMuestreoDto>>>
    {
        public int UserId { get; set; }
        public bool isOCDL { get; set; }

    }

    public class GetResultadosParametrosQueryHandler : IRequestHandler<GetResultadosParametrosQuery, Response<List<ResultadoMuestreoDto>>>
    {
        private readonly IResumenResRepository _repositoryAsync;

        public GetResultadosParametrosQueryHandler(IResumenResRepository repository)
        {
            _repositoryAsync = repository;
        }

        public async Task<Response<List<ResultadoMuestreoDto>>> Handle(GetResultadosParametrosQuery request, CancellationToken cancellationToken)
        {
            var datos = await _repositoryAsync.GetResumenResultados(request.UserId, request.isOCDL);

            if (datos == null)
            {
                throw new KeyNotFoundException($"No se encontraron datos asociados a resultados revisados");
            }

            return new Response<List<ResultadoMuestreoDto>>(datos.ToList());
        }
    }
}
