using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;


namespace Application.Features.Catalogos.LimiteParametroLaboratorio.Commands
{
    public class UpdateLimiteLaboratorioCommand: IRequest<Response<long>>
    {
        public long Id { get; set; }
        public long ParametroId { get; set; }
        public long LaboratorioId { get; set; }
        public int? RealizaLaboratorioMuestreoId { get; set; }
        public long? LaboratorioMuestreoId { get; set; }
        public int? PeriodoId { get; set; }
        public bool Activo { get; set; }
        public string? LDMaCumplir { get; set; }
        public string? LPCaCumplir { get; set; }
        public bool? LoMuestra { get; set; }
        public int? LoSubrogaId { get; set; }
        public long? LaboratorioSubrogadoId { get; set; }
        public string? MetodoAnalitico { get; set; }
        public string? LDM { get; set; }
        public string? LPC { get; set; }
        public long AnioId { get; set; }
    }

    public class UpdateLimiteLaboratorioCommandHandler : IRequestHandler<UpdateLimiteLaboratorioCommand, Response<long>>
    {
        private readonly IRepositoryAsync<Domain.Entities.LimiteParametroLaboratorio> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateLimiteLaboratorioCommandHandler(IRepositoryAsync<Domain.Entities.LimiteParametroLaboratorio> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<long>> Handle(UpdateLimiteLaboratorioCommand request, CancellationToken cancellationToken)
        {
            var limitesLaboratorio = _mapper.Map<Domain.Entities.LimiteParametroLaboratorio>(request);
            await _repositoryAsync.UpdateAsync(limitesLaboratorio);
            return new Response<long>(limitesLaboratorio.Id);
        }
    }
}
