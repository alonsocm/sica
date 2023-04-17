
using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ResumenResultados.Queries
{
    public class GetResumenRevisionResultados : IRequest<Response<List<ResultadoMuestreoDto>>>
    {
        public int EstatusId { get; set; }
        public int UserId { get; set; }
        public bool isOCDL { get; set; }

        public GetResumenRevisionResultados()
        {
            this.isOCDL = false;
        }

    }

    public class GetResumeRevisionResultadosHandler : IRequestHandler<GetResumenRevisionResultados, Response<List<ResultadoMuestreoDto>>>
    {
        private readonly IResumenResRepository _repositoryAsync;

        public GetResumeRevisionResultadosHandler(IResumenResRepository repository)
        {
            _repositoryAsync = repository;
        }

        //Obtener todos los registros de las tablas Muestreo y ResultadoMuestreo acorde al idMuestreo
        public async Task<Response<List<ResultadoMuestreoDto>>> Handle(GetResumenRevisionResultados request, CancellationToken cancellationToken)
        {
            var datos = await _repositoryAsync.GetResumenResultadosMuestreoAsync(request.EstatusId,request.UserId, request.isOCDL); 
            if (datos == null)
            {
                throw new KeyNotFoundException($"No se encontraron datos asociados a resultados revisados");
            }
            return new Response<List<ResultadoMuestreoDto>>(datos.ToList());
        }
    }
}
