using Application.DTOs.InformeMensualSupervisionCampo;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.InformeMensualSupervision.Commands
{
    public class UpdateInformeMensualSupervision : IRequest<Response<bool>>
    {
        public InformeMensualDto Informe { get; set; }
        public long InformeId { get; set; }
    }

    public class UpdateInformeMensualSupervisionHandler : IRequestHandler<UpdateInformeMensualSupervision, Response<bool>>
    {
        private readonly IInformeMensualSupervisionRepository _informeMensualSupervisionRepository;
        private readonly IArchivoService _archivoService;
        public UpdateInformeMensualSupervisionHandler(IInformeMensualSupervisionRepository informeMensualSupervisionRepository, IArchivoService archivoService)
        {
            _informeMensualSupervisionRepository = informeMensualSupervisionRepository;
            _archivoService = archivoService;
        }

        public async Task<Response<bool>> Handle(UpdateInformeMensualSupervision request, CancellationToken cancellationToken)
        {
            var archivo = await _archivoService.ConvertIFormFileToByteArray(request.Informe.Archivo);
            bool actualizado = _informeMensualSupervisionRepository.UpdateInformeMensual(request.Informe, request.InformeId, archivo);

            return new Response<bool>(actualizado);
        }
    }
}
