using Application.Interfaces.IRepositories;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.Operacion.RevisionResultados.Commands
{
    public class UpdateResultadoExcelSecaiaCommandValidator : AbstractValidator<UpdateResultadoExcelSecaiaCommand>
    {
        private readonly IParametroRepository _parametroRepository;
        private readonly ISitioRepository _sitioRepository;
        private readonly IRepository<VwClaveMuestreo> _claveMuestreoRepository;
        private readonly IVwReplicaRevisionResultadoRepository _resultadorepository;

        public UpdateResultadoExcelSecaiaCommandValidator(IParametroRepository parametroRepository, ISitioRepository sitioRepository,
            IVwClaveMonitoreo VwClaveMuestreo, IVwReplicaRevisionResultadoRepository resultadorepository)
        {
            _parametroRepository = parametroRepository;
            _sitioRepository = sitioRepository;
            _claveMuestreoRepository = VwClaveMuestreo;
            _resultadorepository = resultadorepository;

            var clavesMuestreo = ObtenerClavesMuestreo().Result.ToList();
            var sitios = ObtenerSitios().Result.ToList();
            var parametros = ObtenerParametros().Result.ToList();

            RuleForEach(x => x.Parametros).ChildRules(parametro =>
            {
                parametro.RuleFor(x => x.NumeroEntrega).NotEmpty().WithMessage(parametro => $"El campo {{PropertyName}} no puede estar vacío. Linea: {parametro.Linea}");

                parametro.RuleFor(x => x.ClaveSitio).Cascade(CascadeMode.Stop)
                                            .NotEmpty().WithMessage(parametro => $"El campo {{PropertyName}} no puede estar vacío. Linea: {parametro.Linea}")
                                            .Must((parametro, claveSitio) => { return sitios.Any(a => a.ClaveSitio == claveSitio); })
                                            .WithMessage(parametro => $"La clave sitio {{PropertyValue}} no se encontró en la BD. Linea:{parametro.Linea}");

                parametro.RuleFor(x => x.ClaveParametro).Cascade(CascadeMode.Stop)
                                            .NotEmpty().WithMessage(parametro => $"El campo {{PropertyName}} no puede estar vacío. Linea: {parametro.Linea}")
                                            .Must((parametro, claveParametro) => { return parametros.Any(a => a.ClaveParametro == claveParametro); })
                                            .WithMessage(parametro => $"La clave parametro {{PropertyValue}} no se encontró en la BD. Linea:{parametro.Linea}");

                parametro.RuleFor(x => x.ClaveUnica).Cascade(CascadeMode.Stop)
                                            .NotEmpty().WithMessage(parametro => $"El campo {{PropertyName}} no puede estar vacío. Linea: {parametro.Linea}")
                                            .Must((parametro, claveUnica) => { return ExisteClaveUnica(Convert.ToInt32(parametro.NumeroEntrega), claveUnica); })
                                            .WithMessage(parametro => $"La clave unica {{PropertyValue}} no se encuentra en la tabla. Linea:{parametro.Linea}");


            });

        }


        public async Task<IEnumerable<VwClaveMuestreo>> ObtenerClavesMuestreo() => await _claveMuestreoRepository.ObtenerTodosElementosAsync();
        public async Task<IEnumerable<ParametrosGrupo>> ObtenerParametros() => await _parametroRepository.ObtenerTodosElementosAsync();
        public async Task<IEnumerable<Sitio>> ObtenerSitios() => await _sitioRepository.ObtenerTodosElementosAsync();
        public bool ExisteClaveUnica(int noEntrega, string claveUnica) => _resultadorepository.ExisteClaveUnica(noEntrega, claveUnica);
    }
}
