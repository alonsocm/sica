using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.Laboratorios.Commands
{
    public class UpdateLaboratorio : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Nomenclatura { get; set; }
    }

    public class UpdateLaboratorioHandler : IRequestHandler<UpdateLaboratorio, Response<bool>>
    {
        private readonly ILaboratorioRepository _laboratorioRepository;

        public UpdateLaboratorioHandler(ILaboratorioRepository laboratorioRepository)
        {
            _laboratorioRepository=laboratorioRepository;
        }

        public async Task<Response<bool>> Handle(UpdateLaboratorio request, CancellationToken cancellationToken)
        {
            var laboratorioBD = await _laboratorioRepository.ObtenerElementoPorIdAsync(request.Id);

            if (laboratorioBD == null)
                throw new KeyNotFoundException();

            if (laboratorioBD.Nomenclatura == request.Nomenclatura || laboratorioBD.Descripcion == request.Descripcion)
            {
                return new Response<bool>
                {
                    Succeded = false,
                    Message = $"No se pudo actualizar el laboratorio. El nombre o nomenclatura ya se encuentran registrados."
                };
            }

            laboratorioBD.Descripcion = request.Descripcion;
            laboratorioBD.Nomenclatura = request.Nomenclatura;

            _laboratorioRepository.Actualizar(laboratorioBD);
            return new Response<bool>(true);
        }
    }
}
