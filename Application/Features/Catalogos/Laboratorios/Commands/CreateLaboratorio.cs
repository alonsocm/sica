using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.Laboratorios.Commands
{
    public class CreateLaboratorio : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Nomenclatura { get; set; }
    }

    public class CreateLaboratorioHandler : IRequestHandler<CreateLaboratorio, Response<bool>>
    {
        private readonly ILaboratorioRepository _laboratorioRepository;

        public CreateLaboratorioHandler(ILaboratorioRepository laboratorioRepository)
        {
            _laboratorioRepository=laboratorioRepository;
        }

        public async Task<Response<bool>> Handle(CreateLaboratorio request, CancellationToken cancellationToken)
        {
            var laboratorioBD = await _laboratorioRepository.ObtenerElementosPorCriterioAsync(x => x.Nomenclatura == request.Nomenclatura);

            if (laboratorioBD.Any())
            {
                return new Response<bool>
                {
                    Succeded = false,
                    Message = $"No se pudo registrar el laboratorio. La nomenclatura {request.Nomenclatura}, ya se encuentra registrada."
                };
            }

            var laboratorio = new Domain.Entities.Laboratorios()
            {
                Descripcion = request.Descripcion,
                Nomenclatura = request.Nomenclatura,
            };

            _laboratorioRepository.Insertar(laboratorio);
            return new Response<bool>(true);
        }
    }
}
