using Application.DTOs.Catalogos;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
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
        private readonly IRepositoryAsync<TipoHomologado> _repositoryTipoHomologado;

        public AddTipoCuerpoAguaExcelCommandHandler(
            ITipoCuerpoAguaRepository repository,
            IRepositoryAsync<TipoHomologado> repositoryTipoHomologado)
        {
            _repository = repository;
            _repositoryTipoHomologado = repositoryTipoHomologado;
        }

        public async Task<Response<bool>> Handle(AddTipoCuerpoAguaExcelCommand request, CancellationToken cancellationToken)
        {
            var tiposHomologados = await _repositoryTipoHomologado.ListAsync(cancellationToken);

            foreach (var item in request.TipoCuerpoAgua)
            {
                var tipoHomologado = tiposHomologados
                    .FirstOrDefault(w => string.Equals(w.Descripcion, item.TipoHomologadoDescripcion, StringComparison.OrdinalIgnoreCase));

                if (tipoHomologado == null && !string.IsNullOrWhiteSpace(item.TipoHomologadoDescripcion))
                {
                    return new Response<bool> { Succeded = false, Message = $"No se encontró el Tipo Homologado con la descripción: {item.TipoHomologadoDescripcion}" };
                }

                if (string.IsNullOrWhiteSpace(item.Descripcion))
                {
                    return new Response<bool> { Succeded = false, Message = "La descripción del tipo de cuerpo de agua no puede estar vacía o contener solo espacios en blanco." };
                }

                var tipoCuerpoAguaBD = await _repository.ObtenerElementosPorCriterioAsync(x => x.Descripcion == item.Descripcion);

                if (tipoCuerpoAguaBD.Any() && !request.Actualizar)
                {
                    return new Response<bool> { Succeded = false, Message = "Se encontraron tipo cuerpo de agua registrados previamente" };
                }
                else if (tipoCuerpoAguaBD.Any() && request.Actualizar)
                {
                    var existingTipoCuerpoAgua = tipoCuerpoAguaBD.First();
                    existingTipoCuerpoAgua.Descripcion = item.Descripcion;
                    existingTipoCuerpoAgua.TipoHomologadoId = tipoHomologado?.Id;

                    _repository.Actualizar(existingTipoCuerpoAgua);
                }
                else
                {
                    var nuevoRegistro = new TipoCuerpoAgua
                    {
                        Descripcion = item.Descripcion,
                        TipoHomologadoId = tipoHomologado?.Id,
                        Activo = true
                    };

                    _repository.Insertar(nuevoRegistro);
                }
            }

            return new Response<bool>(true);
        }
    }
}
