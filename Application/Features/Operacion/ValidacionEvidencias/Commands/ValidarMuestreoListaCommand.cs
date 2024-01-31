using Application.DTOs.EvidenciasMuestreo;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.ValidacionEvidencias.Commands
{
    public class ValidarMuestreoListaCommand: IRequest<Response<bool>>
    {
        public List<vwValidacionEvienciasDto> Muestreos { get; set; } = new List<vwValidacionEvienciasDto>();
        public long usuarioId { get; set; }
    }

    public class ValidarMuestreoListaCommandHandler : IRequestHandler<ValidarMuestreoListaCommand, Response<bool>>
    {
        private readonly IValidacionEvidenciaRepository _repository;
        private IMapper _mapper;


        public ValidarMuestreoListaCommandHandler(IValidacionEvidenciaRepository repositoryAsync, IMapper mapper)
        {
            _repository = repositoryAsync;
            _mapper = mapper;

        }

        public async Task<Response<bool>> Handle(ValidarMuestreoListaCommand request, CancellationToken cancellationToken)
        {
            var muestreos = _repository.ConvertirValidacionEvidenciaLista(request.Muestreos, request.usuarioId);
            long Id = 0;
            Id = _repository.InsertarRango(muestreos);
            return new Response<bool>((Id != 0) ? true : false);
        }


    }
}
