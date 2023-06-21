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
    //GetResultadosMuestreoEstatusMuestreoAsync
    public class GetResultadosMuestreoEstatusMuestreoQuery : IRequest<Response<List<AcumuladosResultadoDto>>>
    {
        public long estatusId { get; set; }
        

    }

    public class GetResultadosMuestreoEstatusMuestreoQueryHandler : IRequestHandler<GetResultadosMuestreoEstatusMuestreoQuery, Response<List<AcumuladosResultadoDto>>>
    {
        private readonly IMuestreoRepository _repositoryAsync;

        public GetResultadosMuestreoEstatusMuestreoQueryHandler(IMuestreoRepository repository)
        {
            _repositoryAsync = repository;
        }

        public async Task<Response<List<AcumuladosResultadoDto>>> Handle(GetResultadosMuestreoEstatusMuestreoQuery request, CancellationToken cancellationToken)
        {
            var datos = await _repositoryAsync.GetResultadosMuestreoEstatusMuestreoAsync(request.estatusId); ;
            if (datos == null)
            {
                throw new KeyNotFoundException($"No se encontraron datos asociados a resultados revisados");
            }
            return new Response<List<AcumuladosResultadoDto>>(datos.ToList());
        }
    }
}
