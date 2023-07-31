﻿using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.SustitucionLimites.Queries
{
    public class ResultadosSustituidosQuery : IRequest<Response<List<MuestreoSustituidoDto>>>
    {
    }

    public class ResultadosSustituidosQueryHandler : IRequestHandler<ResultadosSustituidosQuery, Response<List<MuestreoSustituidoDto>>>
    {
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly IResultado _resultadosRepository;

        public ResultadosSustituidosQueryHandler(IMuestreoRepository muestreoRepository, IResultado repositoryAsync)
        {
            _muestreoRepository = muestreoRepository;
            _resultadosRepository=repositoryAsync;
        }

        public async Task<Response<List<MuestreoSustituidoDto>>> Handle(ResultadosSustituidosQuery request, CancellationToken cancellationToken)
        {
            var muestreos = await _resultadosRepository.ObtenerResultadosSustituidos();

            return new Response<List<MuestreoSustituidoDto>>(muestreos);
        }
    }
}
