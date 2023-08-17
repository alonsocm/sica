using Application.DTOs;
using Application.Enums;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using Persistence.Repository;

namespace Application.Features.Operacion.SustitucionLimites.Commands
{
    public class SustitucionEmergenciasCommand : IRequest<Response<List<string>>>
    {
        public ParametrosSustitucionLimitesDto ParametrosSustitucion { get; set; }
    }

    public class SustitucionEmergenciasCommandHandler : IRequestHandler<SustitucionEmergenciasCommand, Response<List<string>>>
    {
        private readonly IMuestreoEmergenciasRepository _muestreoEmergenciaRepository;
        private readonly ILimiteParametroLaboratorioRepository _limiteParametroLaboratorioRepository;
        private readonly IVwLimiteMaximoComunRepository _vwLimiteMaximoComunRepository;
        private readonly IHistorialSusticionEmergenciaRepository _historialSustitucionLimiteRepository;

        public SustitucionEmergenciasCommandHandler(
            IMuestreoEmergenciasRepository muestreoEmergenciasRepository,
            ILimiteParametroLaboratorioRepository limiteParametroLaboratorioRepository,
            IVwLimiteMaximoComunRepository vwLimiteMaximoComunRepository,
            IHistorialSusticionEmergenciaRepository historialSusticionLimiteRepository)
        {
            _muestreoEmergenciaRepository = muestreoEmergenciasRepository;
            _limiteParametroLaboratorioRepository=limiteParametroLaboratorioRepository;
            _vwLimiteMaximoComunRepository=vwLimiteMaximoComunRepository;
            _historialSustitucionLimiteRepository=historialSusticionLimiteRepository;
        }

        public async Task<Response<List<string>>> Handle(SustitucionEmergenciasCommand request, CancellationToken cancellationToken)
        {
            var resultadosSustituir = await _muestreoEmergenciaRepository.ObtenerResultadosParaSustitucion();

            if (!resultadosSustituir.Any())
            {
                return new Response<List<string>>() { Succeded = false, Message = "No se encontraron resultados para el período seleccionado." };
            }

            var parametrosFiltrados = resultadosSustituir.Select(s => s.IdParametro).Distinct();

            //Recorremos los resultados para buscar las cadenas <LPC, <LDM, <LD
            var siglas = new List<string> { "<LPC", "<LDM", "<LD" };

            if (request.ParametrosSustitucion.OrigenLimites == (int)TipoSustitucionLimites.CatalogoLimites)
            {
                var limitesParametros = await _vwLimiteMaximoComunRepository.ObtenerElementosPorCriterioAsync(x => parametrosFiltrados.Contains(x.ParametroId));

                var parametrosSinLimites = resultadosSustituir.Select(s => new { s.IdParametro, s.ClaveParametro }).Distinct().Where(r => !limitesParametros.Select(s => s.ParametroId).Contains(r.IdParametro));

                if (parametrosSinLimites.Any())
                {
                    return new Response<List<string>>() { Data = parametrosSinLimites.Select(s => s.ClaveParametro).ToList(), Succeded=false, Message = "No se encontraron los límites de algunos parámetros" };
                }

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
                var parametrosSinLimites = resultadosSustituir.Select(s => s.ClaveParametro).Distinct().Where(r => !request.ParametrosSustitucion.LimitesComunes.Select(s => s.ClaveParametro).Contains(r));

                if (parametrosSinLimites.Any())
                {
                    return new Response<List<string>>() { Data = parametrosSinLimites.ToList(), Succeded=false, Message = "La tabla temporal no contiene los límites de los parámetros indicados" };
                }

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
            _muestreoEmergenciaRepository.ActualizarResultadoSustituidoPorLimite(resultadosSustituidos.ToList());

            //Insertamos en el historial
            var fechaSustitucion = DateTime.Now;

            foreach (var resultado in resultadosSustituidos)
            {
                _historialSustitucionLimiteRepository.Insertar(new Domain.Entities.HistorialSustitucionEmergencia
                {
                    MuestreoEmergenciaId = resultado.IdMuestreo,
                    Anio = resultado.Anio,
                    UsuarioId = request.ParametrosSustitucion.Usuario,
                    Fecha = fechaSustitucion
                }); ;
            }

            return new Response<List<string>>() { Succeded=true };
        }
    }
}
