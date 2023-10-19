using Application.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Features.CargaMasivaEvidencias.Commands
{
    public class CargaEvidenciasCommandValidator : AbstractValidator<CargaEvidenciasCommand>
    {
        private readonly IMuestreoRepository _muestreoRepository;
        readonly string[] sufijosObligatorios = { "E", "M", "S", "D", "R" };
        readonly string[] sufijosObligatoriosLotico = { "O", "A" };

        public CargaEvidenciasCommandValidator(IMuestreoRepository muestreoRepository)
        {
            _muestreoRepository = muestreoRepository;

            RuleForEach(x => x.Archivos).ChildRules(archivo =>
            {
                archivo.RuleFor(x => x.FileName)
                       .Matches("[a-zA-Z0-9]\\-[0-9]{6}\\-[EMSDROAVC]\\.(jpg|JPG|PDF|pdf|XLS|xls|XLSX|xlsx|XLMS|xmls|XLSM)")
                       .WithMessage(archivo => $"El nombre del archivo {archivo.FileName} no cumple con el formato requerido");
            }).DependentRules(() =>
            {
                RuleFor(x => x.Archivos).Custom((archivos, context) =>
                {
                    var nombresArchivos = archivos.Select(s => s.FileName).ToList();
                    var clavesMuestreos = new List<string>();

                    nombresArchivos.ForEach(nombreArchivo =>
                    {
                        var claveMuestreo = nombreArchivo[..nombreArchivo.LastIndexOf('-')];
                        clavesMuestreos.Add(claveMuestreo);
                    });

                    clavesMuestreos.Distinct().ToList().ForEach(claveMuestreo =>
                    {
                        var evidenciasMonitoreo = nombresArchivos.Where(nombre => nombre.StartsWith(claveMuestreo)).ToList();
                        var sufijosMonitoreo = new List<string>();

                        foreach (var evidencia in evidenciasMonitoreo)
                        {
                            var sufijo = evidencia[(evidencia.LastIndexOf('-') + 1)..evidencia.LastIndexOf('.')];
                            sufijosMonitoreo.Add(sufijo);
                        }

                        var tipoCuerpoAgua = _muestreoRepository.GetTipoCuerpoAguaHomologado(claveMuestreo);

                        if (tipoCuerpoAgua is null)
                        {
                            context.AddFailure($"Los resultados del monitoreo {claveMuestreo} no han sido cargados en el sistema. No fue posible cargar evidencias.");
                        }
                        else
                        {
                            bool contieneSufijosObligatorios = tipoCuerpoAgua.ToUpper().Contains("LÓTICO")
                                ? sufijosObligatorios.Union(sufijosObligatoriosLotico).All(x => sufijosMonitoreo.Any(y => x == y))
                                : sufijosObligatorios.All(x => sufijosMonitoreo.Any(y => x == y));

                            if (!contieneSufijosObligatorios)
                            {
                                context.AddFailure($"No se encontraron todas las evidencias requeridas para el muestreo {claveMuestreo}");
                            }
                        }
                    });
                });
            });
        }
    }
}
