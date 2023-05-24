using Application.DTOs;
using Application.Features.Muestreos.Queries;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Resultados.Queries
{
    public class GetResumenValidacionReglas : IRequest<Response<List<ResultadoValidacionReglasDto>>>
    {
        public List<int> Anios { get; set; } = new List<int>();
        public List<int> NumeroEntrega { get; set; } = new List<int>();
    }

    public class GetResumenValidacionReglasHandler : IRequestHandler<GetResumenValidacionReglas, Response<List<ResultadoValidacionReglasDto>>>
    {
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly IResultado _resultadosRepository;

        public GetResumenValidacionReglasHandler(IMuestreoRepository muestreoRepository, IResultado repositoryAsync)
        {
            _muestreoRepository = muestreoRepository;
            _resultadosRepository=repositoryAsync;
        }

        public async Task<Response<List<ResultadoValidacionReglasDto>>> Handle(GetResumenValidacionReglas request, CancellationToken cancellationToken)
        {
            var muestreos = await _muestreoRepository.ObtenerElementosPorCriterioAsync(x => request.Anios.Contains((int)x.AnioOperacion) &&
                                                                                         request.NumeroEntrega.Contains((int)x.NumeroEntrega));

            var resultados = await _resultadosRepository.ObtenerResultadosValidacion(muestreos.Select(s => s.Id).ToList());

            return new Response<List<ResultadoValidacionReglasDto>>(resultados);
        }
    }
}
