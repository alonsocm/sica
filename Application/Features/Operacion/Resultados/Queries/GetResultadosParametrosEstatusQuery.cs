using Application.DTOs;
using Application.Features.ResumenResultados.Queries;
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
    public class GetResultadosParametrosEstatusQuery : IRequest<Response<List<ResultadoMuestreoDto>>>
    {
        public long userId { get; set; }
        public long estatusId { get; set; }
    }

    public class GetResultadosParametrosEstatusQueryHandler : IRequestHandler<GetResultadosParametrosEstatusQuery, Response<List<ResultadoMuestreoDto>>>
    {
        private readonly IResumenResRepository _repositoryAsync;

        public GetResultadosParametrosEstatusQueryHandler(IResumenResRepository repository)
        {
            _repositoryAsync = repository;
        }

        public async Task<Response<List<ResultadoMuestreoDto>>> Handle(GetResultadosParametrosEstatusQuery request, CancellationToken cancellationToken)
        {
            var datos = await _repositoryAsync.GetResultadosParametrosEstatus(request.userId, request.estatusId); ;
            if (datos == null)
            {
                throw new KeyNotFoundException($"No se encontraron datos asociados a resultados revisados");
            }
            return new Response<List<ResultadoMuestreoDto>>(datos.ToList());
        }
    }
}
