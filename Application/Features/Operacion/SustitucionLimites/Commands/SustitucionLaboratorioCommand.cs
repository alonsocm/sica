using Application.DTOs;
using Application.Enums;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Application.Features.Operacion.SustitucionLimites.Commands
{
    public class SustitucionLaboratorioCommand : IRequest<Response<string>>
    {

        public List<int> anios { get; set; } = new List<int>();
    }

    public class SustitucionLaboratorioCommandHandler : IRequestHandler<SustitucionLaboratorioCommand, Response<string>>
    {
        private readonly IResultado _resultadosRepository;
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly ILimiteParametroLaboratorioRepository _limiteParametroLaboratorioRepository;
        private readonly IVwLimiteLaboratorioRepository _vwLimiteLaboratorioRepository;
        private readonly IHistorialSusticionLimiteRepository _historialSustitucionLimiteRepository;

        public SustitucionLaboratorioCommandHandler(IResultado resultadosRepository, IMuestreoRepository muestreoRepository, ILimiteParametroLaboratorioRepository limiteParametroLaboratorioRepository, 
                                                    IVwLimiteLaboratorioRepository vwLaboratorioRepository, IHistorialSusticionLimiteRepository historialSustitucionLimiteRepository)
        {
            _resultadosRepository = resultadosRepository;
            _muestreoRepository = muestreoRepository;
            _limiteParametroLaboratorioRepository = limiteParametroLaboratorioRepository;
            _vwLimiteLaboratorioRepository = vwLaboratorioRepository;
            _historialSustitucionLimiteRepository = historialSustitucionLimiteRepository;
        }

        public async Task<Response<string>> Handle(SustitucionLaboratorioCommand request, CancellationToken cancellationToken)
        {
            List<ResultadoParaSustitucionLimitesDto> lstResultadosSinLimite = new List<ResultadoParaSustitucionLimitesDto>();
            List<ResultadoParaSustitucionLimitesDto> lstResultadosMultiplesLimites = new List<ResultadoParaSustitucionLimitesDto>();
            string parametros = "";
            List<VwLimiteLaboratorio> vwMultiplesLimites = new List<VwLimiteLaboratorio>();

            var siglas = new List<string> { "<LPC", "<LDM", "<LD", "<CMC" };
            var resultadosSustituir = await _resultadosRepository.ObtenerResultadosParaSustitucionPorAnios(request.anios);
            List<ResultadoParaSustitucionLimitesDto> lstResultadosaSustituir = resultadosSustituir.Where(x => siglas.Contains(x.ValorOriginal.ToString())).ToList();


            if (lstResultadosaSustituir.Count > 0)
            {
                var limites = await _vwLimiteLaboratorioRepository.ObtenerElementosPorCriterioAsync(x => request.anios.Contains(Convert.ToInt32(x.Anio)));

                foreach (var resultado in lstResultadosaSustituir)
                {
                    resultado.esSustitucionLaboratorio = true;
                    List<VwLimiteLaboratorio> valr = new List<VwLimiteLaboratorio>();
                    valr = limites.Where(x => x.LaboratorioId == resultado.LaboratorioId && x.Anio == resultado.Anio.ToString()
                              && x.ParametroId == resultado.IdParametro).ToList();

                    if (valr.Count == 0)
                    {
                        ResultadoParaSustitucionLimitesDto resultadoSinLimite = new ResultadoParaSustitucionLimitesDto();
                        resultadoSinLimite.ClaveParametro = resultado.ClaveParametro;
                        resultadoSinLimite.Anio = resultado.Anio;
                        resultadoSinLimite.LaboratorioMuestreo = resultado.LaboratorioMuestreo;
                        lstResultadosSinLimite.Add(resultadoSinLimite);
                    }

                    else if (valr.Count == 1)
                    {
                        if (valr[0].LaboratorioSubrogaId != null)
                        {
                            resultado.LaboratorioSubrogadoId = valr[0].LaboratorioSubrogaId;
                            var limitesubrogado = limites.Where(x => x.LaboratorioId == valr[0].LaboratorioSubrogaId && x.Anio == resultado.Anio.ToString()
                                                  && x.ParametroId == resultado.IdParametro).ToList();
                            bool esLimiteDecimalsubrogado = decimal.TryParse(limitesubrogado.FirstOrDefault().Limite, out decimal limiteDecimalsubrogado);
                            resultado.ValorSustituido = (esLimiteDecimalsubrogado) ? $"<{limiteDecimalsubrogado}" : $"<{limitesubrogado.FirstOrDefault().Limite}";
                        }
                        else
                        {
                            bool esLimiteDecimal = decimal.TryParse(valr.FirstOrDefault().Limite, out decimal limiteDecimal);
                            resultado.ValorSustituido = (esLimiteDecimal) ? $"<{limiteDecimal}" : $"<{valr.FirstOrDefault().Limite}";
                        }
                    }

                    else
                    {
                        ResultadoParaSustitucionLimitesDto MultiplesLimites = new ResultadoParaSustitucionLimitesDto();
                        MultiplesLimites.ClaveParametro = resultado.ClaveParametro;
                        MultiplesLimites.Anio = resultado.Anio;
                        MultiplesLimites.LaboratorioId = resultado.LaboratorioId;
                        MultiplesLimites.IdMuestreo = resultado.IdMuestreo;
                        lstResultadosMultiplesLimites.Add(MultiplesLimites);
                        vwMultiplesLimites.AddRange(valr);
                    }

                }


                if (lstResultadosSinLimite.Count > 0)
                {
                    var lstSinlimite = lstResultadosSinLimite.Select(x => new { x.ClaveParametro, x.Anio, x.LaboratorioMuestreo }).Distinct().ToList();
                    foreach (var clave in lstSinlimite.ToList())
                    { parametros += "No existe limite para el parámetro " + clave.ClaveParametro + " del año " + clave.Anio + " y laboratorio " + clave.LaboratorioMuestreo + ","; }
                }
                else
                {
                    //Actualización historico              
                    var fechaSustitucion = DateTime.Now;
                    var historialSustitucionLimites = lstResultadosaSustituir.Select(s => s.IdMuestreo).Distinct().Select(x =>
                        new HistorialSustitucionLimites()
                        {
                            MuestreoId = x,
                            TipoSustitucionId = (int)TipoSustitucionLimites.Laboratorio,
                            UsuarioId = 43,
                            Fecha = fechaSustitucion

                        }).ToList();
                    _resultadosRepository.ActualizarResultadoSustituidoPorLimite(lstResultadosaSustituir);
                    _historialSustitucionLimiteRepository.InsertarRango(historialSustitucionLimites);                   
                }
            }
            else { parametros = "No existen resultados a sustituir para los años seleccionados";  }


            return new Response<string>(parametros);

        }



    }
}
