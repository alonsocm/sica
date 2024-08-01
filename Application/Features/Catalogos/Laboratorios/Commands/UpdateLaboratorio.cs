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
            var parametroBD = await _laboratorioRepository.ObtenerElementoPorIdAsync(request.Id);

            if (parametroBD == null)
                throw new KeyNotFoundException();

            parametroBD.Descripcion = request.Descripcion;
            parametroBD.Nomenclatura = request.Nomenclatura;

            _laboratorioRepository.Actualizar(parametroBD);
            return new Response<bool>(true);
        }
    }
}
