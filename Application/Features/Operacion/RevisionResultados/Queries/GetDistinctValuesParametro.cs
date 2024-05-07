using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.RevisionResultados.Queries
{
    public class GetDistinctValuesParametro : IRequest<Response<List<string>>>
    {
        public string ClaveParametro { get; set; }
        public int Usuario { get; set; }
        public int CuerpoAgua { get; set; }
        public int Estatus { get; set; }
        public int Anio { get; set; }
    }

    public class GetDistinctValuesParametroHandler : IRequestHandler<GetDistinctValuesParametro, Response<List<string>>>
    {
        private readonly IResumenResRepository _repositoryAsync;

        public GetDistinctValuesParametroHandler(IResumenResRepository repositoryAsync)
        {
            _repositoryAsync=repositoryAsync;
        }

        public async Task<Response<List<string>>> Handle(GetDistinctValuesParametro request, CancellationToken cancellationToken)
        {
            IEnumerable<RegistroOriginalDto> datos = await _repositoryAsync.GetResumenResultadosTemp(request.Usuario, request.CuerpoAgua, request.Estatus, request.Anio)??throw new KeyNotFoundException($"No se encontraron datos asociados a resultados revisados");

            return new Response<List<string>>(GetResultadoParametro(datos, request.ClaveParametro).ToList());
        }

        public static IEnumerable<string> GetResultadoParametro(IEnumerable<RegistroOriginalDto> datos, string claveParametro)
        {
            var parametros = datos.Select(s => s.Parametros).SelectMany(s => s.Where(w => w.ClaveParametro == claveParametro).Select(d => d.Resultado).Distinct());
            return parametros;
        }
    }
}
