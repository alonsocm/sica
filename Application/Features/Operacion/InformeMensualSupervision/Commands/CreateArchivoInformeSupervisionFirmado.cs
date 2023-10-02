using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Controllers.v1.Operacion
{
    public class CreateArchivoInformeSupervisionFirmado : IRequest<Response<bool>>
    {
        public IFormFile Archivo { get; set; }
        public long InformeId { get; set; }
    }

    public class CreateArchivoInformeSupervisionFirmadoHandler : IRequestHandler<CreateArchivoInformeSupervisionFirmado, Response<bool>>
    {
        private readonly IInformeMensualSupervisionRepository _informeMensualSupervisionRepository;
        private readonly IArchivoService _archivoService;
        public CreateArchivoInformeSupervisionFirmadoHandler(IInformeMensualSupervisionRepository informeMensualSupervisionRepository, IArchivoService archivo)
        {
            _informeMensualSupervisionRepository = informeMensualSupervisionRepository;
            _archivoService = archivo;
        }

        async Task<Response<bool>> IRequestHandler<CreateArchivoInformeSupervisionFirmado, Response<bool>>.Handle(CreateArchivoInformeSupervisionFirmado request, CancellationToken cancellationToken)
        {
            byte[] archivo = await _archivoService.ConvertIFormFileToByteArray(request.Archivo);
            _informeMensualSupervisionRepository.UpdateInformeMensualArchivoFirmado(request.InformeId, request.Archivo.FileName, archivo, 43);

            return new Response<bool>(true);
        }
    }
}