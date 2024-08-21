﻿using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.TiposCuerpoAgua.Commands
{
    public class UpdateTipoCuerpoAguaCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public long? TipoHomologadoId { get; set; }

    }
    public class UpdateTipoCuerpoAguaCommandHandler : IRequestHandler<UpdateTipoCuerpoAguaCommand, Response<bool>>
    {
        private readonly ITipoCuerpoAguaRepository _repository;

        public UpdateTipoCuerpoAguaCommandHandler(ITipoCuerpoAguaRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<bool>> Handle(UpdateTipoCuerpoAguaCommand request, CancellationToken cancellationToken)
        {
            // revisa una cadena de texto y te dice si está vacía o con espacios en blanco
            if (string.IsNullOrWhiteSpace(request.Descripcion))
            {
                return new Response<bool>(false)
                {
                    Succeded = false,
                    Message = "La descripción es un campo obligatorio."
                };
            }
            var tipoCuerpoAgua = await _repository.ObtenerElementoPorIdAsync(request.Id);

            if (tipoCuerpoAgua == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }

            tipoCuerpoAgua.Descripcion = request.Descripcion;
            tipoCuerpoAgua.TipoHomologadoId = request.TipoHomologadoId;
            _repository.Actualizar(tipoCuerpoAgua);

            return new Response<bool>(true, "TipoCuerpoAgua actualizado exitosamente.");
        }
    }
}