﻿using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Resultados.Comands
{
    public class ValidarResultadosPorReglasCommand : IRequest<Response<bool>>
    {
        public List<int> Anios { get; set; } = new List<int>();
        public List<int> NumeroEntrega { get; set; } = new List<int>();
    }

    public class ValidarResultadosPorReglasCommandHandler : IRequestHandler<ValidarResultadosPorReglasCommand, Response<bool>>
    {
        private readonly IResultado _resultadosRepository;
        private readonly IReglasMinimoMaximoRepository _reglasMinimoMaximoRepository;
        private readonly IReglaService _regla;

        public ValidarResultadosPorReglasCommandHandler(IResultado resultadosRepository, IReglasMinimoMaximoRepository reglasMinimoMaximoRepository, IReglaService regla)
        {
            _resultadosRepository=resultadosRepository;
            _reglasMinimoMaximoRepository=reglasMinimoMaximoRepository;
            _regla=regla;
        }

        public async Task<Response<bool>> Handle(ValidarResultadosPorReglasCommand request, CancellationToken cancellationToken)
        {
            /*Obtenemos la lista de resultados correspondientes a los muestreos que cumplan con la condición de año y número de entrega*/
            var resultados = await _resultadosRepository.ObtenerElementosPorCriterioAsync(x => request.Anios.Contains((int)x.Muestreo.AnioOperacion) &&
                                                                                         request.NumeroEntrega.Contains((int)x.Muestreo.NumeroEntrega));
            var parametrosIds = resultados.Select(s => s.ParametroId).Distinct();
            /*Vamos por todas las reglas que apliquen a los resultados consultados*/
            var reglasMinimoMaximo = await _reglasMinimoMaximoRepository.ObtenerElementosPorCriterioAsync(x => parametrosIds.Contains(x.ParametroId));
            var resultadosNoValidos = new List<string>();

            /*Recorremos la lista de resultados, y con el id de parámetro (o la clave) obtenemos la correspondiente*/
            foreach (var resultado in resultados) 
            {
                /*Dentro del recorrido de los resultados, utilizamos la regla obtenida y el valor del resultado
                 para llamar a la interfaz IRegla y pasamos los argumentos al método*/
                var reglaMinimoMaximo = reglasMinimoMaximo.FirstOrDefault(x => x.ParametroId == resultado.ParametroId);

                if (reglaMinimoMaximo != null && reglaMinimoMaximo.Aplica && !resultado.Resultado.Contains('<') && !resultado.Resultado.Contains('>'))
                {
                    try
                    {
                        var incumpleRegla = _regla.InCumpleReglaMinimoMaximo(reglaMinimoMaximo.MinimoMaximo, resultado.Resultado);
                    }
                    catch (Exception ex)
                    {
                        resultadosNoValidos.Add($"La regla {reglaMinimoMaximo.MinimoMaximo} no pudo ser aplicada al resultado: {resultado.Resultado}, {ex.Message}");
                    }

                    /*Al resultado le modificamos su valor en la columna ValidacionMinimoMaximo */
                }
            }

            throw new NotImplementedException();
        }
    }
}
