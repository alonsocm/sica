using Application.DTOs.Catalogos;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
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

            foreach (var tipoCuaerpoAgua in request.TipoCuerpoAgua)
            {
                var tipoCuerpoAguaE = _repository.ObtenerElementosPorCriterioAsync(x => x.Descripcion == tipoCuaerpoAgua.Descripcion).Result.FirstOrDefault();
                if (tipoCuerpoAguaE != null && !request.Actualizar)
                {
                    return new Response<bool> { Succeded = false, Message = "Se econtraron Tipos cuerpo de agua con la misma descripcion" };
                }
                else if (tipoCuerpoAguaE != null && !request.Actualizar)
                {
                    tipoCuerpoAguaE.Descripcion = tipoCuaerpoAgua.Descripcion;
                    tipoCuerpoAguaE.TipoHomologadoId = tipoCuaerpoAgua.TipoHomologadoId;
                    _repository.Actualizar(tipoCuerpoAguaE);
                }
                else
                {
                    var nuevoRegistro = new Domain.Entities.TipoCuerpoAgua()
                    {

                        Descripcion = tipoCuaerpoAgua.Descripcion,
                        TipoHomologadoId = tipoCuaerpoAgua.TipoHomologadoId,

                    };
                    _repository.Insertar(nuevoRegistro);
                }
            }

            return new Response<bool>(true);
        }
    }
}
