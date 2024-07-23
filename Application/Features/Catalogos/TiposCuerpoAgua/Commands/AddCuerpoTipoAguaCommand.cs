﻿using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.TiposCuerpoAgua.Commands.AddTipoCuerpoAguaCommand
{
    public class AddTipoCuerpoAguaCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public long? TipoHomologadoId { get; set; }
        public bool Activo { get; set; }
        public string Frecuencia { get; set; }
        public int EvidenciaEsperada { get; set; }
        public int TiempoMinimoMuestreo { get; set; }
    }
    public class AddTipoCuerpoAguaCommandHandler : IRequestHandler<AddTipoCuerpoAguaCommand, Response<bool>>
    {
        private readonly ITipoCuerpoAguaRepository _repository;
        public AddTipoCuerpoAguaCommandHandler(ITipoCuerpoAguaRepository repository)
        {
            _repository = repository;
        }
        public Task<Response<bool>> Handle(AddTipoCuerpoAguaCommand request, CancellationToken cancellationToken)
        {
            var duplicadodescripcion = _repository.Equals(request.Descripcion);
            if (duplicadodescripcion != null)
            {
                return Task.FromResult(new Response<bool>(true, "Ya existe un TipoCuerpoAgua con la misma descripción."));
            }
            var tipoCuerpoAgua = new Domain.Entities.TipoCuerpoAgua()
            {
                Id = request.Id,
                Descripcion = request.Descripcion,
                TipoHomologadoId = request.TipoHomologadoId,
                Activo = request.Activo,
                Frecuencia = request.Frecuencia,
                EvidenciasEsperadas = request.EvidenciaEsperada,
                TiempoMinimoMuestreo = request.TiempoMinimoMuestreo
            };
            _repository.Insertar(tipoCuerpoAgua);
            return Task.FromResult(new Response<bool>(true, "TipoCuerpoAgua agregado exitosamente."));
        }
    }
}
