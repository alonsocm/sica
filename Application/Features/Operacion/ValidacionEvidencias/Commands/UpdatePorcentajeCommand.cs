using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.ValidacionEvidencias.Commands
{
    public class UpdatePorcentajeCommand: IRequest<Response<bool>>
    {
        public List<VwValidacionEvidenciaRealizada> Muestreos { get; set; } = new List<VwValidacionEvidenciaRealizada>();
    }

    public class UpdatePorcentajeCommandHandler : IRequestHandler<UpdatePorcentajeCommand, Response<bool>>
    {
        private readonly IValidacionEvidenciaRepository _repository;
      

        public UpdatePorcentajeCommandHandler(IValidacionEvidenciaRepository repositoryAsync)
        {
            _repository = repositoryAsync;
           
        }

        public async Task<Response<bool>> Handle(UpdatePorcentajeCommand request, CancellationToken cancellationToken)
        {
            List<long> validacionesId = request.Muestreos.Select(x => x.ValidacionEvidenciaId).ToList();
            List<ValidacionEvidencia> lstValidaciones =  _repository.ObtenerElementosPorCriterioAsync(x => validacionesId.Contains(x.Id)).Result.ToList();

            foreach (var validacion in lstValidaciones)
            {
                validacion.PorcentajePago = request.Muestreos.Where(x => x.ValidacionEvidenciaId == validacion.Id).Select(x => x.PorcentajePago).FirstOrDefault();
                _repository.Actualizar(validacion);
            }
            return new Response<bool>(true);
        }


    }
}
