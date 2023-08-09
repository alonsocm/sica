using Application.Interfaces.IRepositories;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.Operacion.Muestreos.Commands.Carga
{
    public class CargaMuestreosEmergenciasCommandValidator : AbstractValidator<CargaMuestreosEmergenciaCommand>
    {
        private readonly IParametroRepository _parametroRepository;
        private readonly ISitioRepository _sitioRepository;
        private readonly IRepository<VwClaveMuestreo> _claveMuestreoRepository;
        private readonly ILaboratorioRepository _laboratorioRepository;
        public CargaMuestreosEmergenciasCommandValidator(IParametroRepository parametroRepository, ISitioRepository sitioRepository, IVwClaveMonitoreo VwClaveMuestreo, ILaboratorioRepository laboratorioRepository)
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
                muestreo.RuleFor(x => x.NombreEmergencia).NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}");

                muestreo.RuleFor(x => x.ClaveUnica).NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}");

                muestreo.RuleFor(x => x.IdLaboratorio).NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}");

                muestreo.RuleFor(x => x.Sitio).NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}");

                muestreo.RuleFor(x => x.TipoCuerpoAgua).NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}");

                muestreo.RuleFor(x => x.LaboratorioRealizoMuestreo).NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}");

                muestreo.RuleFor(x => x.GrupoParametro).NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}");

                muestreo.RuleFor(x => x.Parametro).NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}");

                muestreo.RuleFor(x => x.ClaveParametro).Cascade(CascadeMode.Stop)
                                                  .NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}")
                                                  .Must((muestreo, resultado) => { return parametros.Any(a => a.ClaveParametro == resultado); })
                                                  .WithMessage(muestreo => $"La clave de parámetro {{PropertyValue}} no se encontró en la BD. Linea:{muestreo.Linea}");

                muestreo.RuleFor(x => x.Resultado).NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}");

                muestreo.RuleFor(x => x.FechaProgramada).NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}")
                                                        .Must((FechaProgramada) => DateTime.TryParse(FechaProgramada, out DateTime result))
                                                        .WithMessage(muestreo => $"El campo {{PropertyName}} no cumple con el formato de fecha requerido DD/MM/YYYY. Linea: {muestreo.Linea}");

                muestreo.RuleFor(x => x.FechaRealVisita).NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}")
                                                        .Must((FechaRealVisita) => DateTime.TryParse(FechaRealVisita, out DateTime result))
                                                        .WithMessage(muestreo => $"El campo {{PropertyName}} no cumple con el formato de fecha requerido DD/MM/YYYY. Linea: {muestreo.Linea}");

                muestreo.RuleFor(x => x.LaboratorioRealizoMuestreo).Cascade(CascadeMode.Stop)
                                                  .NotEmpty().WithMessage(muestreo => $"El campo {{PropertyName}} no puede estar vacío. Linea: {muestreo.Linea}")
                                                  .Must((muestreo, laboratorio) => { return laboratorios.Any(a => a.Nomenclatura == laboratorio); })
                                                  .WithMessage(muestreo => $"El laboratorio {{PropertyValue}} no se encontró en la BD. Linea:{muestreo.Linea}");
            });
        }

        public async Task<IEnumerable<VwClaveMuestreo>> ObtenerClavesMuestreo() => await _claveMuestreoRepository.ObtenerTodosElementosAsync();
        public async Task<IEnumerable<ParametrosGrupo>> ObtenerParametros() => await _parametroRepository.ObtenerTodosElementosAsync();
        public async Task<IEnumerable<Sitio>> ObtenerSitios() => await _sitioRepository.ObtenerTodosElementosAsync();
        public async Task<IEnumerable<Laboratorios>> ObtenerLaboratorios() => await _laboratorioRepository.ObtenerTodosElementosAsync();
    }
}