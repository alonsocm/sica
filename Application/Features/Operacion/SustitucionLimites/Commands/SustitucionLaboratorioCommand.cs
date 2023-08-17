using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Application.Features.Operacion.SustitucionLimites.Commands
{
    public class SustitucionLaboratorioCommand : IRequest<Response<List<ResultadoParaSustitucionLimitesDto>>>
    {

        public List<int> anios { get; set; } = new List<int>();
    }

    public class SustitucionLaboratorioCommandHandler : IRequestHandler<SustitucionLaboratorioCommand, Response<List<ResultadoParaSustitucionLimitesDto>>>
    {
        private readonly IResultado _resultadosRepository;
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly ILimiteParametroLaboratorioRepository _limiteParametroLaboratorioRepository;
        private readonly IVwLimiteLaboratorioRepository _vwLimiteLaboratorioRepository;

        public SustitucionLaboratorioCommandHandler(IResultado resultadosRepository, IMuestreoRepository muestreoRepository, ILimiteParametroLaboratorioRepository limiteParametroLaboratorioRepository, IVwLimiteLaboratorioRepository vwLaboratorioRepository)
        {
            _resultadosRepository = resultadosRepository;
            _muestreoRepository = muestreoRepository;
            _limiteParametroLaboratorioRepository = limiteParametroLaboratorioRepository;
            _vwLimiteLaboratorioRepository = vwLaboratorioRepository;
        }

        public async Task<Response<List<ResultadoParaSustitucionLimitesDto>>> Handle(SustitucionLaboratorioCommand request, CancellationToken cancellationToken)
        {
            List<ResultadoParaSustitucionLimitesDto> lstResultadosSinLimite = new List<ResultadoParaSustitucionLimitesDto>();
            List<ResultadoParaSustitucionLimitesDto> lstResultadosMultiplesLimites = new List<ResultadoParaSustitucionLimitesDto>();

            var siglas = new List<string> { "<LPC", "<LDM", "<LD" };
            var resultadosSustituir = await _resultadosRepository.ObtenerResultadosParaSustitucionPorAnios(request.anios);            

            List<ResultadoParaSustitucionLimitesDto> lstResultadosaSustituir = resultadosSustituir.Where(x => siglas.Contains(x.ValorOriginal.ToString())).ToList();

            var limites = await _vwLimiteLaboratorioRepository.ObtenerElementosPorCriterioAsync(x => request.anios.Contains(Convert.ToInt32(x.Anio)));

            foreach (var resultado in lstResultadosaSustituir)
            {
                List<VwLimiteLaboratorio> valr = new List<VwLimiteLaboratorio>();

                valr = limites.Where(x => x.LaboratorioMuestreoId == resultado.LaboratorioId && x.Anio == resultado.Anio.ToString()
                          && x.ParametroId == resultado.IdParametro).ToList();

                if (valr.Count == 0)
                {
                    ResultadoParaSustitucionLimitesDto resultadoSinLimite = new ResultadoParaSustitucionLimitesDto();
                    resultadoSinLimite.ClaveParametro = resultado.ClaveParametro;
                    resultadoSinLimite.Anio = resultado.Anio;
                    lstResultadosSinLimite.Add(resultadoSinLimite);
                }

                else if (valr.Count == 1)
                {
                    bool esLimiteDecimal = decimal.TryParse(valr.FirstOrDefault().Limite, out decimal limiteDecimal);
                    resultado.ValorSustituido = $"<{limiteDecimal}";
                }

                else

                {
                    ResultadoParaSustitucionLimitesDto MultiplesLimites = new ResultadoParaSustitucionLimitesDto();
                    MultiplesLimites.ClaveParametro = resultado.ClaveParametro;
                    MultiplesLimites.Anio = resultado.Anio;
                    MultiplesLimites.LaboratorioId = resultado.LaboratorioId;
                    MultiplesLimites.IdMuestreo = resultado.IdMuestreo;
                    lstResultadosMultiplesLimites.Add(MultiplesLimites);
                }               
            }           

            var lstSinlimite = lstResultadosSinLimite.Select(x => new { x.ClaveParametro, x.Anio }).Distinct().ToList();
            return new Response<List<ResultadoParaSustitucionLimitesDto>>(lstResultadosSinLimite);
        }
    }
}
