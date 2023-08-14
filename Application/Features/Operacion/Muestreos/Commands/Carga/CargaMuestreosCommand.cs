using Application.DTOs;
using Application.DTOs.Users;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Muestreos.Commands.Carga
{
    public class CargaMuestreosCommand : IRequest<Response<ResultadoCargaMuestreo>>
    {
        public List<CargaMuestreoDto> Muestreos { get; set; } = new List<CargaMuestreoDto>();
        public bool Validado { get; set; }
        public bool Reemplazar { get; set; }
    }

    public class CargaMasivaMuestreosCommandHandler : IRequestHandler<CargaMuestreosCommand, Response<ResultadoCargaMuestreo>>
    {
        private readonly IMuestreoRepository _repository;
        private readonly IResultado _resultadosRepository;

        public CargaMasivaMuestreosCommandHandler(IMuestreoRepository repositoryAsync, IResultado resultadosRepository)
        {
            _repository=repositoryAsync;
            _resultadosRepository=resultadosRepository;
        }

        public async Task<Response<ResultadoCargaMuestreo>> Handle(CargaMuestreosCommand request, CancellationToken cancellationToken)
        {
            var numeroEntrega = Convert.ToInt32(request.Muestreos.Select(m => m.NoEntrega).Distinct().FirstOrDefault());
            var anio = Convert.ToInt32(request.Muestreos.Select(m => m.AnioOperacion).Distinct().FirstOrDefault());
            var existeCargaPrevia = await ExisteNumeroEntrega(numeroEntrega, anio);

            var resultadoCarga = new ResultadoCargaMuestreo
            {
                Correcto = false,
                ExisteCarga = true,
                Anio = anio,
                NumeroEntrega = numeroEntrega,
            };

            if (!existeCargaPrevia)
            {
                var muestreos = _repository.ConvertToMuestreosList(request.Muestreos, request.Validado);
                _repository.InsertarRango(muestreos);
                resultadoCarga.Correcto = true;
            }
            else if (existeCargaPrevia && request.Reemplazar)
            {
                var resultadosNoEncontrados = _resultadosRepository.ActualizarValorResultado(request.Muestreos);
                var muestreos = _repository.ConvertToMuestreosList(resultadosNoEncontrados, request.Validado);
                _repository.InsertarRango(muestreos);
                resultadoCarga.Correcto = true;
            }

            return new Response<ResultadoCargaMuestreo>(resultadoCarga);
        }

        public async Task<bool> ExisteNumeroEntrega(int numeroEntrega, int anio)
        {
            return await _repository.ExisteElementoAsync(w => w.NumeroEntrega == numeroEntrega && w.AnioOperacion == anio);
        }
    }
}
