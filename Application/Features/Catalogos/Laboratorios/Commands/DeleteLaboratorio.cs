using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.Laboratorios.Commands
{
    public class DeleteLaboratorio : IRequest<Response<bool>>
    {
        public int LaboratorioId { get; set; }
    }

    public class DeleteLaboratorioHandler : IRequestHandler<DeleteLaboratorio, Response<bool>>
    {
        private readonly ILaboratorioRepository _laboratorioRepository;

        public DeleteLaboratorioHandler(ILaboratorioRepository laboratorioRepository)
        {
            _laboratorioRepository=laboratorioRepository;
        }

        public async Task<Response<bool>> Handle(DeleteLaboratorio request, CancellationToken cancellationToken)
        {
            var laboratorioBD = await _laboratorioRepository.ObtenerElementoPorIdAsync(request.LaboratorioId);

            if (laboratorioBD == null)
                throw new KeyNotFoundException();

            _laboratorioRepository.Eliminar(laboratorioBD);
            return new Response<bool>(true);
        }
    }
}
