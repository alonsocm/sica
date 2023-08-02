using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
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
        private readonly IProgramaAnioRepository _programAnio;

        public GetAniosConOperacionHandler(IMuestreoRepository repository, IProgramaAnioRepository programaAnio)
        {
            _repository=repository;
            _programAnio = programaAnio;
        }

        public async Task<Response<List<int?>>> Handle(GetAniosConOperacion request, CancellationToken cancellationToken)
        {          
            var anios = await _repository.GetListAniosConRegistro();
            
            return new Response<List<int?>>(anios);
        }

        public async Task<Response<List<ProgramaAnio>>> GetProgramaAnios()
        {
            var anios = await _programAnio.ObtenerTodosElementosAsync();

            return new Response<List<ProgramaAnio>>(anios.ToList());
        }



    }
}
