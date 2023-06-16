using Application.DTOs;
using Application.DTOs.Users;
using Application.Features.Operacion.Replicas.Commandas;
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

namespace Application.Features.ReplicaDiferente.Commands
{
    public class CambiarEstatusReplicaDiferenteCommand : IRequest<Response<bool>>
    {
        public long? IdUsuario { get; set; }
        
        public long ResultadosMuestreo { get; set; } 
    }

    public class CambiarEstatusReplicaDiferenteCommandHandler : IRequestHandler<CambiarEstatusReplicaDiferenteCommand, Response<bool>>
    {
        private readonly IReplicas _resultadoRepository;
        private readonly  IResultado _resultadoDiferenteRepository;

        public CambiarEstatusReplicaDiferenteCommandHandler(IReplicas resultadoRepository, IResultado resultadoDiferenteRepository)
        {
            _resultadoRepository = resultadoRepository;
            _resultadoDiferenteRepository = resultadoDiferenteRepository;
        }

        public async Task<Response<bool>> Handle(CambiarEstatusReplicaDiferenteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var resultadoBd = await _resultadoRepository.ObtenerElementosPorCriterioAsync(f => f.ResultadoMuestreoId == request.ResultadosMuestreo);
                
                if (resultadoBd != null)
                {
                    foreach(var item in resultadoBd.ToList())
                    {
                        item.UsuarioRevisionId = (long)request.IdUsuario;
                        _resultadoRepository.Actualizar(item);
                    }
                }

                var resultadoDiferente = await _resultadoDiferenteRepository.ObtenerElementoPorIdAsync(request.ResultadosMuestreo);

                if (resultadoDiferente != null)
                {
                    resultadoDiferente.FechaEstatusFinal = DateTime.Now;
                    _resultadoDiferenteRepository.Actualizar(resultadoDiferente);
                }

                return new Response<bool>(true);
            }
            catch (Exception e)
            {
                _ = e.Message;
                throw;
                return new Response<bool>(false);
            }
            
        }
    }




}
