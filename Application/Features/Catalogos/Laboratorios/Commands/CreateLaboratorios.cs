using Application.DTOs.Catalogos;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.Laboratorios.Commands
{
    public class CreateLaboratorios : IRequest<Response<bool>>
    {
        public List<ExcelLaboratorioDTO> Laboratorios { get; set; }
        public bool Actualizar { get; set; }
    }

    public class CreateLaboratoriosHandler : IRequestHandler<CreateLaboratorios, Response<bool>>
    {
        private readonly ILaboratorioRepository _laboratorioRepository;

        public CreateLaboratoriosHandler(ILaboratorioRepository laboratorioRepository)
        {
            _laboratorioRepository = laboratorioRepository;
        }

        public async Task<Response<bool>> Handle(CreateLaboratorios request, CancellationToken cancellationToken)
        {
            foreach (var laboratorio in request.Laboratorios)
            {
                var laboratorioBD = _laboratorioRepository.ObtenerElementosPorCriterioAsync(x => x.Descripcion == laboratorio.Descripcion).Result.FirstOrDefault();

                if (laboratorioBD != null && !request.Actualizar)
                {
                    return new Response<bool> { Succeded = false, Message = "Se encontraron parámetros registrados previamente" };
                }
                else if (laboratorioBD != null && request.Actualizar)
                {
                    laboratorioBD.Descripcion = laboratorio.Descripcion;
                    laboratorioBD.Nomenclatura = laboratorio.Nomenclatura;

                    _laboratorioRepository.Actualizar(laboratorioBD);
                }
                else
                {
                    var nuevoRegistro = new Domain.Entities.Laboratorios()
                    {
                        Descripcion = laboratorio.Descripcion,
                        Nomenclatura= laboratorio.Nomenclatura,
                    };

                    _laboratorioRepository.Insertar(nuevoRegistro);
                }
            }

            return new Response<bool>(true);
        }
    }
}
