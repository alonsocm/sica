using Application.DTOs;
using Application.DTOs.EvidenciasMuestreo;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
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
    }

    public class ValidarMuestreoCommandHandler : IRequestHandler<ValidarMuestreoCommand, Response<bool>>
    {
        private readonly IValidacionEvidenciaRepository _repository;
   

        public ValidarMuestreoCommandHandler(IValidacionEvidenciaRepository repositoryAsync)
        {
            _repository = repositoryAsync;
        
        }

        public async Task<Response<bool>> Handle(ValidarMuestreoCommand request, CancellationToken cancellationToken)
        {
            var muestreos = _repository.ConvertirValidacionEvidencia(request.Muestreos);
            _repository.Insertar(muestreos);
            return new Response<bool>(true);
        }


    }


}
