using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.TiposCuerpoAgua.Commands
{
    public class UpdateTipoCuerpoAguaCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public long? TipoHomologadoId { get; set; }
        public bool Activo { get; set; }
        public string Frecuencia { get; set; }
        public int EvidenciasEsperadas { get; set; }
        public int TiempoMinimoMuestreo { get; set; }
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
            var tipoCuerpoAgua = await _repository.ObtenerElementoPorIdAsync(request.Id);
            if (tipoCuerpoAgua == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            tipoCuerpoAgua.Descripcion = request.Descripcion;
            tipoCuerpoAgua.TipoHomologadoId = request.TipoHomologadoId;
            tipoCuerpoAgua.Activo = request.Activo;
            tipoCuerpoAgua.Frecuencia = request.Frecuencia;
            tipoCuerpoAgua.EvidenciasEsperadas = request.EvidenciasEsperadas;
            tipoCuerpoAgua.TiempoMinimoMuestreo = request.TiempoMinimoMuestreo;
            _repository.Actualizar(tipoCuerpoAgua);
            return new Response<bool>(true, "TipoCuerpoAgua actualizado exitosamente.");
        }
    }
}