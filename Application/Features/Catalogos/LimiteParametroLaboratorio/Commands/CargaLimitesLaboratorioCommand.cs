using Application.Features.Catalogos.Sitios.Commands;
using Application.Interfaces.IRepositories;
using Application.Models;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Catalogos.LimiteParametroLaboratorio.Commands
{
    public class CargaLimitesLaboratorioCommand : IRequest<Response<bool>>
    {
        public List<LimiteParametrosLaboratorioExcel> LimitesLaboratorios { get; set; }
        public bool Actualizar { get; set; }
    }

    public class CargaLimitesLaboratorioHandler : IRequestHandler<CargaLimitesLaboratorioCommand, Response<bool>>
    {
        private readonly IParametroRepository _parametrosRepository;
        private readonly ILaboratorioRepository _laboratoriosRepository;
        private readonly IRepositoryAsync<Domain.Entities.AccionLaboratorio> _accionesLabRepositoyAsync;
        private readonly IRepositoryAsync<Domain.Entities.Mes> _mesesLabRepositoyAsync;
        private readonly IProgramaAnioRepository _progrmasAnioRepository;
        private readonly ILimiteParametroLaboratorioRepository _limiteParametroLabRepository;


        public CargaLimitesLaboratorioHandler(
            IParametroRepository parametrosRepository,
            ILaboratorioRepository laboratoriosRepository,
            IRepositoryAsync<Domain.Entities.AccionLaboratorio> accionesLabRepositoyAsync,
            IRepositoryAsync<Domain.Entities.Mes> mesesLabRepositoyAsync,
            IProgramaAnioRepository progrmasAnioRepository,
            ILimiteParametroLaboratorioRepository limiteParametroLabRepository)
        {

            _parametrosRepository = parametrosRepository;
            _laboratoriosRepository = laboratoriosRepository;
            _accionesLabRepositoyAsync = accionesLabRepositoyAsync;
            _mesesLabRepositoyAsync = mesesLabRepositoyAsync;
            _progrmasAnioRepository = progrmasAnioRepository;
            _limiteParametroLabRepository = limiteParametroLabRepository;

        }

        public async Task<Response<bool>> Handle(CargaLimitesLaboratorioCommand request, CancellationToken cancellationToken)
        {
            var parametros = _parametrosRepository.ObtenerTodosElementosAsync();
            var laboratorios = await _laboratoriosRepository.ObtenerTodosElementosAsync();
            var accionesLaboratorio = await _accionesLabRepositoyAsync.ListAsync();
            var meses = await _mesesLabRepositoyAsync.ListAsync();
            var anios = _progrmasAnioRepository.ObtenerTodosElementosAsync();

            foreach (var limite in request.LimitesLaboratorios)
            {
                var parametro = parametros.Result.Where(x => x.ClaveParametro.ToUpper() == limite.ClaveParametro.ToUpper());
                var laboratorio = laboratorios.Where(x => x.Nomenclatura.ToUpper() == limite.Laboratorio.ToUpper());
                var realizaLabMuestreo = accionesLaboratorio.Where(x => x.LoSubroga == limite.RealizaLaboratorioMuestreo.ToUpper());
                var laboratorioMuestreo = laboratorio.Where(x => x.Nomenclatura.ToUpper() == limite.LaboratorioMuestreo.ToUpper());
                var mes = meses.Where(x => x.Descripcion.ToUpper() == limite.Mes.ToUpper());
                var anio = anios.Result.Where(x => x.Anio == limite.Anio);
                var loSubroga = accionesLaboratorio.Where(x => x.LoSubroga == limite.LoSubroga.ToUpper());
                var laboratorioSubrogado = laboratorios.Where(x => x.Nomenclatura.ToUpper() == limite.LaboratorioSubrogado.ToUpper());

                var existeLimiteLaboratorio = _limiteParametroLabRepository.ObtenerElementosPorCriterioAsync(x => x.ParametroId == parametro.FirstOrDefault().Id &&
                x.LaboratorioId == laboratorio.FirstOrDefault().Id && x.PeriodoId == mes.FirstOrDefault().Id && x.AnioId == anio.FirstOrDefault().Id).Result.FirstOrDefault();

                if (existeLimiteLaboratorio != null && !request.Actualizar)
                {
                    return new Response<bool> { Succeded = false, Message = "Se encontraron límites registrados previamente" };
                }
                else if (existeLimiteLaboratorio != null && request.Actualizar)
                {
                    existeLimiteLaboratorio.ParametroId = parametro.FirstOrDefault().Id;
                    existeLimiteLaboratorio.LaboratorioId = laboratorio.FirstOrDefault().Id;
                    existeLimiteLaboratorio.RealizaLaboratorioMuestreoId = realizaLabMuestreo.FirstOrDefault().Id;
                    existeLimiteLaboratorio.LaboratorioMuestreoId = laboratorioMuestreo.FirstOrDefault().Id;
                    existeLimiteLaboratorio.PeriodoId = mes.FirstOrDefault().Id;
                    existeLimiteLaboratorio.LdmaCumplir = limite.LDMaCumplir;
                    existeLimiteLaboratorio.LpcaCumplir = limite.LPCaCumplir;
                    existeLimiteLaboratorio.LoMuestra = (limite.LoMuestra.ToUpper() == "SI") ? true : false;
                    existeLimiteLaboratorio.LoSubrogaId = loSubroga.FirstOrDefault().Id;
                    existeLimiteLaboratorio.LaboratorioSubrogaId = laboratorioSubrogado.FirstOrDefault().Id;
                    existeLimiteLaboratorio.MetodoAnalitico = limite.MetodoAnalitico;
                    existeLimiteLaboratorio.Ldm = limite.LDM;
                    existeLimiteLaboratorio.Lpc = limite.LPC;
                    existeLimiteLaboratorio.AnioId = anio.FirstOrDefault().Id;
                    _limiteParametroLabRepository.Actualizar(existeLimiteLaboratorio);
                }
                else
                {
                    var nuevoRegistro = new Domain.Entities.LimiteParametroLaboratorio()
                    {
                        ParametroId = parametro.FirstOrDefault().Id,
                        LaboratorioId = laboratorio.FirstOrDefault().Id,
                        RealizaLaboratorioMuestreoId = realizaLabMuestreo.FirstOrDefault().Id,
                        LaboratorioMuestreoId = laboratorioMuestreo.FirstOrDefault().Id,
                        PeriodoId = mes.FirstOrDefault().Id,
                        LdmaCumplir = limite.LDMaCumplir,
                        LpcaCumplir = limite.LPCaCumplir,
                        LoMuestra = (limite.LoMuestra.ToUpper() == "SI") ? true : false,
                        LoSubrogaId = loSubroga.FirstOrDefault().Id,
                        LaboratorioSubrogaId = laboratorioSubrogado.FirstOrDefault().Id,
                        MetodoAnalitico = limite.MetodoAnalitico,
                        Ldm = limite.LDM,
                        Lpc = limite.LPC,
                        AnioId = anio.FirstOrDefault().Id,
                        Activo = true

                    };
                    _limiteParametroLabRepository.Insertar(nuevoRegistro);
                }
            }
            return new Response<bool>(true);
        }
    }
}
