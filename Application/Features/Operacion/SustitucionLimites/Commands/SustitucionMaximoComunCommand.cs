using Application.DTOs;
using Application.Enums;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using Persistence.Repository;

namespace Application.Features.Operacion.SustitucionLimites.Commands
{
    public class SustitucionMaximoComunCommand : IRequest<Response<bool>>
    {
        public ParametrosSustitucionLimitesDto ParametrosSustitucion { get; set; }
    }

    public class SustitucionMaximoComunCommandHandler : IRequestHandler<SustitucionMaximoComunCommand, Response<bool>>
    {
        private readonly IResultado _resultadosRepository;
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly ILimiteParametroLaboratorioRepository _limiteParametroLaboratorioRepository;
        private readonly IVwLimiteMaximoComunRepository _vwLimiteMaximoComunRepository;

        public SustitucionMaximoComunCommandHandler(IResultado resultadosRepository, IMuestreoRepository muestreoRepository, ILimiteParametroLaboratorioRepository limiteParametroLaboratorioRepository, IVwLimiteMaximoComunRepository vwLimiteMaximoComunRepository)
        {
            _resultadosRepository = resultadosRepository;
            _muestreoRepository = muestreoRepository;
            _limiteParametroLaboratorioRepository=limiteParametroLaboratorioRepository;
            _vwLimiteMaximoComunRepository=vwLimiteMaximoComunRepository;
        }

        public async Task<Response<bool>> Handle(SustitucionMaximoComunCommand request, CancellationToken cancellationToken)
        {
            var resultadosSustituir = await _resultadosRepository.ObtenerResultadosParaSustitucionPorPeriodo();
            var parametrosFiltrados = resultadosSustituir.Select(s => s.IdParametro).Distinct();

            //Recorremos los resultados para buscar las cadenas <LPC, <LDM, <LD
            var siglas = new List<string> { "<LPC", "<LDM", "<LD" };

            if (request.ParametrosSustitucion.OrigenLimites == (int)TipoSustitucionLimites.MaximoComun)
            {
                var limitesParametros = await _vwLimiteMaximoComunRepository.ObtenerElementosPorCriterioAsync(x => parametrosFiltrados.Contains(x.ParametroId));

                foreach (var resultado in resultadosSustituir.ToList())
                {
                    //Vamos por el límite que le corresponde al parámetro
                    var limiteParametro = limitesParametros.Where(x => x.ParametroId == resultado.IdParametro).Select(s => s.Limite).FirstOrDefault()??string.Empty;

                    if (!string.IsNullOrEmpty(limiteParametro))
                    {
                        //Convertimos el límite y el resultado a decimal
                        bool esLimiteDecimal = decimal.TryParse(limiteParametro, out decimal limiteDecimal);
                        bool esResultadoDecimal = decimal.TryParse(resultado.ValorOriginal, out decimal resultadoDecimal);

                        if (esLimiteDecimal && esResultadoDecimal && (resultadoDecimal < limiteDecimal))
                        {
                            resultado.ValorSustituido = $"<{limiteDecimal}";
                        }
                        else if (siglas.Contains(resultado.ValorOriginal))
                        {
                            resultado.ValorSustituido = $"<{limiteDecimal}";
                        }
                    }
                }
            }
            else if (request.ParametrosSustitucion.OrigenLimites == (int)TipoSustitucionLimites.TablaTemporal)
            {
                var tablaTemporalLimites = request.ParametrosSustitucion.LimitesComunes;

                foreach (var resultado in resultadosSustituir.ToList())
                {
                    //Vamos por el límite que le corresponde al parámetro
                    var limiteParametro = tablaTemporalLimites?.Where(x => x.ClaveParametro == resultado.ClaveParametro).Select(s => s.LimiteConsiderado == "LDM" ? s.LDM : s.LPC).FirstOrDefault();

                    if (!string.IsNullOrEmpty(limiteParametro))
                    {
                        //Convertimos el límite y el resultado a decimal
                        bool esLimiteDecimal = decimal.TryParse(limiteParametro, out decimal limiteDecimal);
                        bool esResultadoDecimal = decimal.TryParse(resultado.ValorOriginal, out decimal resultadoDecimal);

                        if (esLimiteDecimal && esResultadoDecimal && (resultadoDecimal < limiteDecimal))
                        {
                            resultado.ValorSustituido = $"<{limiteDecimal}";
                        }
                        else if (siglas.Contains(resultado.ValorOriginal))
                        {
                            resultado.ValorSustituido = $"<{limiteDecimal}";
                        }
                    }
                }
            }

            //Ahora actualizamos los registros, pero solo a los que se haya sustituido el valor.
            var resultadosSustituidos = resultadosSustituir.Where(x => !string.IsNullOrEmpty(x.ValorSustituido));
            _resultadosRepository.ActualizarResultadoSustituidoPorLimite(resultadosSustituidos.ToList());

            return new Response<bool>(true);
        }
    }
}
