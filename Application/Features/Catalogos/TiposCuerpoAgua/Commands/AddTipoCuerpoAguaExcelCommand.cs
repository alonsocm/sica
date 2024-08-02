using Application.DTOs.Catalogos;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using FluentValidation;
using MediatR;

namespace Application.Features.Catalogos.TiposCuerpoAgua.Commands
{
    public class AddTipoCuerpoAguaExcelCommand : IRequest<Response<bool>>
    {
        public List<ExcelTipocuerpoAguaDTO> TipoCuerpoAgua { get; set; }
        public bool Actualizar { get; set; }

    }
    public class AddTipoCuerpoAguaExcelCommandHandler : IRequestHandler<AddTipoCuerpoAguaExcelCommand, Response<bool>>
    {
        private readonly ITipoCuerpoAguaRepository _repository;

        public AddTipoCuerpoAguaExcelCommandHandler(ITipoCuerpoAguaRepository repository)
        {
            _repository = repository;

        }

        public async Task<Response<bool>> Handle(AddTipoCuerpoAguaExcelCommand request, CancellationToken cancellationToken)
        {
            var homologado = _repository.GetTipoCuerpoAgua();

            foreach (var item in request.TipoCuerpoAgua)
            {
                var thomologado = homologado.Where(w => w.Descripcion == item.TipoHomologadoDescripcion).FirstOrDefault();

                var tipoCuerpoAguaBD = _repository.ObtenerElementosPorCriterioAsync(x => x.Descripcion == item.Descripcion).Result.FirstOrDefault();
                if (tipoCuerpoAguaBD != null && !request.Actualizar)
                {
                    return new Response<bool> { Succeded = false, Message = "Se encontraron parámetros registrados previamente" };
                }
                else if (tipoCuerpoAguaBD != null && request.Actualizar)
                {
                    tipoCuerpoAguaBD.Descripcion = item.Descripcion;
                    tipoCuerpoAguaBD.TipoHomologadoDescripcion = thomologado.TipoHomologadoDescripcion;

                    _repository.Actualizar(tipoCuerpoAguaBD);
                }
                else
                {
                    var nuevoRegistro = new Domain.Entities.TipoCuerpoAgua()
                    {
                        Descripcion = item.Descripcion,
                        TipoHomologadoDescripcion = thomologado.Descripcion

                    };

                    _repository.Insertar(nuevoRegistro);
                }
            }

            return new Response<bool>(true);
        }
    }
}
