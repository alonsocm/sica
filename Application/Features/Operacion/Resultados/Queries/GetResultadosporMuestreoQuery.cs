using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Resultados.Queries
{
    public class GetResultadosporMuestreoQuery : IRequest<Response<List<AcumuladosResultadoDto>>>
    {
        public List<int> Anios { get; set; } = new List<int>();
        public List<int> NumeroEntrega { get; set; } = new List<int>();
        public int estatusId { get; set; }
    }

    public class GetResultadosporMuestreoQueryHandler : IRequestHandler<GetResultadosporMuestreoQuery, Response<List<AcumuladosResultadoDto>>>
    {
        private readonly IMuestreoRepository _repositoryAsync;

        public GetResultadosporMuestreoQueryHandler(IMuestreoRepository muestreoRepository)
        {
            _repositoryAsync = muestreoRepository;

        }

        public async Task<Response<List<AcumuladosResultadoDto>>> Handle(GetResultadosporMuestreoQuery request, CancellationToken cancellationToken)
        {
            var datos = await _repositoryAsync.GetResultadosporMuestreoAsync(request.Anios, request.NumeroEntrega, request.estatusId);
            if (datos == null)
            {
                throw new KeyNotFoundException($"No se encontraron datos asociados a GetResultadosporMuestreoAsync");
            }
            return new Response<List<AcumuladosResultadoDto>>(datos.ToList());
        }
    }
}
