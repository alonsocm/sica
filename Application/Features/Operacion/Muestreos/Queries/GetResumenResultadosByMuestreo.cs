using Application.DTOs.Users;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Muestreos.Queries
{
    public class GetResumenResultadosByMuestreo : IRequest<Response<List<ResumenResultadosDto>>>
    {
        public List<int> Muestreos { get; set; }
    }

    public class GetResumenResultadosByMuestreoHandler : IRequestHandler<GetResumenResultadosByMuestreo, Response<List<ResumenResultadosDto>>>
    {
        private readonly IMuestreoRepository _repository;

        public GetResumenResultadosByMuestreoHandler(IMuestreoRepository repository)
        {
            _repository=repository;
        }

        public async Task<Response<List<ResumenResultadosDto>>> Handle(GetResumenResultadosByMuestreo request, CancellationToken cancellationToken)
        {
            var resumen = await _repository.GetResumenResultados(request.Muestreos);
            return new Response<List<ResumenResultadosDto>>(resumen.ToList());

        }
    }
}
