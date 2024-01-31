using Application.DTOs;
using Application.DTOs.EvidenciasMuestreo;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.ValidacionEvidencias.Commands
{
    public class ValidarMuestreoCommand: IRequest<Response<bool>>
    {
        public vwValidacionEvienciasDto Muestreos { get; set; } = new vwValidacionEvienciasDto();
        public long usuarioId { get; set; }
    }

    public class ValidarMuestreoCommandHandler : IRequestHandler<ValidarMuestreoCommand, Response<bool>>
    {
        private readonly IValidacionEvidenciaRepository _repository;
        private IMapper _mapper;


        public ValidarMuestreoCommandHandler(IValidacionEvidenciaRepository repositoryAsync, IMapper mapper)
        {
            _repository = repositoryAsync;
            _mapper = mapper;
        
        }

        public async Task<Response<bool>> Handle(ValidarMuestreoCommand request, CancellationToken cancellationToken)
        {   
            var muestreos = _repository.ConvertirValidacionEvidencia(request.Muestreos, request.usuarioId);
            long Id = 0;
            Id = _repository.Insertar(muestreos);
            return new Response<bool>((Id != 0) ?true:false);
        }


    }


}
