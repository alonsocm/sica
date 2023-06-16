using Application.DTOs.Users;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Muestreos.Commands.Carga
{
    public class CargaMuestreosCommandValidator : AbstractValidator<CargaMuestreosCommand>
    {
        private readonly IParametroRepository _parametroRepository;
        private readonly ISitioRepository _sitioRepository;
        private readonly IRepository<VwClaveMuestreo> _claveMuestreoRepository;
        private readonly ILaboratorioRepository _laboratorioRepository;
        public CargaMuestreosCommandValidator(IParametroRepository parametroRepository, ISitioRepository sitioRepository, IVwClaveMonitoreo VwClaveMuestreo, ILaboratorioRepository laboratorioRepository)
        {
            _parametroRepository = parametroRepository;
            _sitioRepository = sitioRepository;
            _claveMuestreoRepository = VwClaveMuestreo;
            _laboratorioRepository = laboratorioRepository;

            var clavesMuestreo = ObtenerClavesMuestreo().Result.ToList();
            var sitios = ObtenerSitios().Result.ToList();
            var parametros = ObtenerParametros().Result.ToList();
            var laboratorios = ObtenerLaboratorios().Result.ToList();

            RuleForEach(x => x.Muestreos).ChildRules(muestreo =>
            {
                muestreo.RuleFor(x => x.FechaRealVisita).NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}")
                                                        .Must((FechaRealVisita) => DateTime.TryParse(FechaRealVisita, out DateTime result))
                                                        .WithMessage(muestreo => $"El campo {{PropertyName}} no cumple con el formato de fecha requerido DD/MM/YYYY. Linea: {muestreo.Linea}");

                muestreo.RuleFor(x => x.Muestreo).Cascade(CascadeMode.Stop)
                                            .NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}")
                                            .Must((muestreo, claveMuestreo) => { return clavesMuestreo.Any(a => a.ClaveMuestreo == claveMuestreo); })
                                            .WithMessage(muestreo => $"La clave de muestreo {{PropertyValue}} no se encontró en la BD. Linea:{muestreo.Linea}")
                                            .Must((muestreo, claveMuestreo) => { return clavesMuestreo.Any(a => a.ClaveMuestreo == claveMuestreo && a.Cargado == 0); })
                                            .WithMessage(muestreo => $"Los resultados del muestreo {{PropertyValue}} ya han sido cargados previamente en la BD. Linea:{muestreo.Linea}");

                muestreo.RuleFor(x => x.Claveconagua).Cascade(CascadeMode.Stop)
                                            .NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}")
                                            .Must((muestreo, claveConagua) => { return sitios.Any(a => a.ClaveSitio == claveConagua); })
                                            .WithMessage(muestreo => $"La clave CONAGUA {{PropertyValue}} no se encontró en la BD. Linea:{muestreo.Linea}");

                muestreo.RuleFor(x => x.ClaveConalab).Cascade(CascadeMode.Stop)
                                        .NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}")
                                        .Must((muestreo, claveConalab) => { return muestreo.Claveconagua == claveConalab; })
                                        .WithMessage(muestreo => $"La clave CONALAB no puede ser diferente a la clave CONAGUA. Linea: {muestreo.Linea}");

                muestreo.RuleFor(x => x.TipoCuerpoAgua).NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}");

                muestreo.RuleFor(x => x.GrupodeParametro).NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}");

                muestreo.RuleFor(x => x.ClaveParametro).Cascade(CascadeMode.Stop)
                                                  .NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}")
                                                  .Must((muestreo, resultado) => { return parametros.Any(a => a.ClaveParametro == resultado); })
                                                  .WithMessage(muestreo => $"La clave de parámetro {{PropertyValue}} no se encontró en la BD. Linea:{muestreo.Linea}");

                muestreo.RuleFor(x => x.LaboratorioRealizoMuestreo).Cascade(CascadeMode.Stop)
                                                  .NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}")
                                                  .Must((muestreo, laboratorio) => { return laboratorios.Any(a => a.Nomenclatura == laboratorio); })
                                                  .WithMessage(muestreo => $"El laboratorio {{PropertyValue}} no se encontró en la BD. Linea:{muestreo.Linea}");

                muestreo.RuleFor(x => x.Resultado).NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}");
            });
        }

        public async Task<IEnumerable<VwClaveMuestreo>> ObtenerClavesMuestreo() => await _claveMuestreoRepository.ObtenerTodosElementosAsync();
        public async Task<IEnumerable<ParametrosGrupo>> ObtenerParametros() => await _parametroRepository.ObtenerTodosElementosAsync();
        public async Task<IEnumerable<Sitio>> ObtenerSitios() => await _sitioRepository.ObtenerTodosElementosAsync();
        public async Task<IEnumerable<Laboratorios>> ObtenerLaboratorios() => await _laboratorioRepository.ObtenerTodosElementosAsync();
    }
}