using Application.DTOs;
using Application.DTOs.Users;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Carga
{
    public class CargaMuestreosCommand : IRequest<Response<ResultadoCargaMuestreo>>
    {
        public List<CargaMuestreoDto> Muestreos { get; set; } = new List<CargaMuestreoDto>();
        public bool Validado { get; set; }
        public bool Reemplazar { get; set; }
        public int TipoCarga { get; set; }
    }

    public class CargaMasivaMuestreosCommandHandler : IRequestHandler<CargaMuestreosCommand, Response<ResultadoCargaMuestreo>>
    {
        private readonly IMuestreoRepository _repository;
        private readonly IResultado _resultadosRepository;

        public CargaMasivaMuestreosCommandHandler(IMuestreoRepository repositoryAsync, IResultado resultadosRepository)
        {
            _repository = repositoryAsync;
            _resultadosRepository = resultadosRepository;
        }

        public async Task<Response<ResultadoCargaMuestreo>> Handle(CargaMuestreosCommand request, CancellationToken cancellationToken)
        {
            var numeroCarga = Convert.ToInt32(request.Muestreos.Select(m => m.NoCarga).Distinct().FirstOrDefault());
            var anio = Convert.ToInt32(request.Muestreos.Select(m => m.AnioOperacion).Distinct().FirstOrDefault());
            var existeCargaPrevia = await ExisteNumeroCarga(numeroCarga, anio);

            var resultadoCarga = new ResultadoCargaMuestreo
            {
                Correcto = false,
                ExisteCarga = true,
                Anio = anio,
                NumeroCarga = numeroCarga,
            };

            if (!existeCargaPrevia)
            {
                var muestreos = _repository.ConvertToMuestreosList(request.Muestreos, request.Validado, request.TipoCarga);
                _repository.InsertarRango(muestreos);
                resultadoCarga.Correcto = true;
            }
            else if (existeCargaPrevia && request.Reemplazar)
            {
                var resultadosNoEncontrados = _resultadosRepository.ActualizarValorResultado(request.Muestreos);

                if (resultadosNoEncontrados.Item2.Count > 0)
                {
                    var muestreos = _repository.ConvertToMuestreosList(resultadosNoEncontrados.Item2, request.Validado, request.TipoCarga);
                    _repository.InsertarRango(muestreos);
                }

                if (resultadosNoEncontrados.Item1.Count > 0)
                {
                    var resultados = _repository.GenerarResultados(resultadosNoEncontrados.Item1.ToList());
                    _resultadosRepository.InsertarRango(resultados);
                }

                resultadoCarga.Correcto = true;
            }

            return new Response<ResultadoCargaMuestreo>(resultadoCarga);
        }

        public async Task<bool> ExisteNumeroCarga(int numeroCarga, int anio)
        {
            return await _repository.ExisteElementoAsync(w => w.NumeroCarga == numeroCarga.ToString() && w.AnioOperacion == anio);
        }
    }
}
