using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.TiposCuerpoAgua.Commands
{
    public class AddTipoCuerpoAguaCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public long? TipoHomologadoId { get; set; }
        public bool Activo { get; set; }
        public string Frecuencia { get; set; }
        public int EvidenciasEsperadas { get; set; }
        public int TiempoMinimoMuestreo { get; set; }
    }
    public class AddTipoCuerpoAguaCommandHandler : IRequestHandler<AddTipoCuerpoAguaCommand, Response<bool>>
    {
        private readonly ITipoCuerpoAguaRepository _repository;
        public AddTipoCuerpoAguaCommandHandler(ITipoCuerpoAguaRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<bool>> Handle(AddTipoCuerpoAguaCommand request, CancellationToken cancellationToken)
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
            var tipocuerpoaguaDB = await _repository.ObtenerElementosPorCriterioAsync(x => x.Descripcion == request.Descripcion.Trim());

            if (tipocuerpoaguaDB.Any())
            {
                return new Response<bool>(false)
                {
                    Succeded = false,
                    Message = "Se encontraron tipo cuerpo de agua registrados previamente."

                };
            }
            var tipoCuerpoAgua = new Domain.Entities.TipoCuerpoAgua()
            {
                Id = request.Id,
                Descripcion = request.Descripcion,
                TipoHomologadoId = request.TipoHomologadoId,
                Activo = request.Activo,
                Frecuencia = request.Frecuencia,
                EvidenciasEsperadas = request.EvidenciasEsperadas,
                TiempoMinimoMuestreo = request.TiempoMinimoMuestreo
            };
            _repository.Insertar(tipoCuerpoAgua);
            return new Response<bool>(true, "TipoCuerpoAgua agregado exitosamente.");
        }
    }
}
