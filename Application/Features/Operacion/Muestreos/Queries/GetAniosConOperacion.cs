using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Muestreos.Queries
{
    public class GetAniosConOperacion : IRequest<Response<List<int?>>>
    {
    }

    public class GetAniosConOperacionHandler : IRequestHandler<GetAniosConOperacion, Response<List<int?>>>
    {
        private readonly IMuestreoRepository _repository;

        public GetAniosConOperacionHandler(IMuestreoRepository repository)
        {
            _repository=repository;
        }

        public async Task<Response<List<int?>>> Handle(GetAniosConOperacion request, CancellationToken cancellationToken)
        {          
            var anios = await _repository.GetListAniosConRegistro();
            
            return new Response<List<int?>>(anios);
        }
    }
}
