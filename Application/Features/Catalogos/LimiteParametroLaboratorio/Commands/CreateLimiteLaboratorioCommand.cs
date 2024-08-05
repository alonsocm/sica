using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;


namespace Application.Features.Catalogos.LimiteParametroLaboratorio.Commands
{
    public class CreateLimiteLaboratorioCommand: IRequest<Response<long>>
    { 
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

    public class CreateLimiteLaboratorioHandler : IRequestHandler<CreateLimiteLaboratorioCommand, Response<long>>
    {
        private readonly IRepositoryAsync<Domain.Entities.LimiteParametroLaboratorio> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateLimiteLaboratorioHandler(IRepositoryAsync<Domain.Entities.LimiteParametroLaboratorio> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<long>> Handle(CreateLimiteLaboratorioCommand request, CancellationToken cancellationToken)
        {
            var nuevoRegistro = _mapper.Map<Domain.Entities.LimiteParametroLaboratorio>(request);
            var data = await _repositoryAsync.AddAsync(nuevoRegistro);
            return new Response<long>(data.Id);
        }
    }
}
