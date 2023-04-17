
using Application.DTOs;
using Application.Features.ResumenResultados.Queries;
using Application.Interfaces.IRepositories;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
namespace Application.Features.Operacion.RevisionResultados.Queries
{
    public class GetResultadosParametrosTempQuery : IRequest<Response<List<ResultadoMuestreoDto>>>
    {
        public int UserId { get; set; }
        public int CuerpoAgua { get; set; }
        public int Estatus { get; set; }
        public int Anio { get; set; }
    }
    public class GetResultadosParametrosQueryHandler : IRequestHandler<GetResultadosParametrosTempQuery, Response<List<ResultadoMuestreoDto>>>
    {
        private readonly IResumenResRepository _repositoryAsync;

        public GetResultadosParametrosQueryHandler(IResumenResRepository repository)
        {
            _repositoryAsync = repository;
        }

        public async Task<Response<List<ResultadoMuestreoDto>>> Handle(GetResultadosParametrosTempQuery request, CancellationToken cancellationToken)
        {
            var datos = await _repositoryAsync.GetResumenResultadosTemp(request.UserId, request.CuerpoAgua, request.Estatus, request.Anio); 
            if (datos == null)
            {
                throw new KeyNotFoundException($"No se encontraron datos asociados a resultados revisados");
            }
            return new Response<List<ResultadoMuestreoDto>>(datos.ToList());
        }
    }
}
