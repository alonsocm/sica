using Application.DTOs;
using Application.Features.Operacion.Muestreos.Commands.Actualizar;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Resultados.Comands
{
    public class EnviarAcumuladosReplica : IRequest<Response<bool>>
    {        
        public IEnumerable<long> lstResultados { get; set; }
    }

    public class EnviarAcumuladosReplicaHandler : IRequestHandler<EnviarAcumuladosReplica, Response<bool>>
    {
        private readonly IResultado _resultadoRepository;

        public EnviarAcumuladosReplicaHandler(IResultado resultadoRepository)
        {
            _resultadoRepository = resultadoRepository;
        }

        public async Task<Response<bool>> Handle(EnviarAcumuladosReplica request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.lstResultados == null)
                {
                    return new Response<bool> { Succeded = false };
                }
                else
                {
                    var resultados = await _resultadoRepository.ObtenerElementosPorCriterioAsync(x => request.lstResultados.Contains(x.MuestreoId)
                    && x.EstatusResultadoId == (int)Application.Enums.EstatusResultado.CargaRéplicasLaboratorioExterno);

                    bool actualizados = await _resultadoRepository.CambiarEstatusAsync(Enums.EstatusResultado.AcumulaciónResultadosReplica, resultados.Select(x => x.Id));
                    return new Response<bool> { Succeded = actualizados };

                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
