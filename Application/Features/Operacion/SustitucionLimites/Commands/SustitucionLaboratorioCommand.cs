using Application.Enums;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.SustitucionLimites.Commands
{
    public class SustitucionLaboratorioCommand: IRequest<Response<bool>>
    {
        public int AnioOperacion { get; set; }
    }

    public class SustitucionLaboratorioCommandHandler : IRequestHandler<SustitucionLaboratorioCommand, Response<bool>>
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

        public async Task<Response<bool>> Handle(SustitucionLaboratorioCommand request, CancellationToken cancellationToken)
        {

            var resultadosSustituir = await _resultadosRepository.ObtenerResultadosParaSustitucionPorPeriodo();
            var limites = await _vwLimiteLaboratorioRepository.ObtenerElementosPorCriterioAsync(x => request.AnioOperacion.Equals(x.Anio));
            foreach (var resultado in resultadosSustituir.ToList())
            {

            }
            return new Response<bool>(true);
        }
    }




}
