using Application.DTOs;
using Application.DTOs.Users;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReplicaDiferente.Commands
{
    public class CargaReplicaDiferenteCommand : IRequest<Response<bool>>
    {
        public List<GeneralDescargaDiferente> Resultados { get; set; } = new List<GeneralDescargaDiferente>();
    }

    public class CargaReplicaDiferenteCommandHandler : IRequestHandler<CargaReplicaDiferenteCommand, Response<bool>>
    {
        private readonly IVwReplicaRevisionResultadoRepository _repository;
        private readonly IResultado _repositoryExcel;
        public CargaReplicaDiferenteCommandHandler(IVwReplicaRevisionResultadoRepository repositoryAsync, IResultado repositoryExcel)
        {
            _repository = repositoryAsync;
            _repositoryExcel = repositoryExcel;
        }

        
        public async Task<Response<bool>> Handle(CargaReplicaDiferenteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                foreach (var item in request.Resultados)
                {
                    var resultadovista =  _repository.ObtenerElementosPorCriterio(f => f.ClaveUnica == item.ClaveUnica ).FirstOrDefault();
                    ResultadoMuestreo resultadoexcel = await _repositoryExcel.ObtenerElementoPorIdAsync(resultadovista.ResultadoMuestreoId);
                    resultadoexcel.ObservacionSrenameca = item.ObservacionSRENAMECA;
                    resultadoexcel.FechaObservacionSrenameca = DateTime.Now;
                    resultadoexcel.Comentarios = item.ComentariosAprobacionResultados;
                    _repositoryExcel.Actualizar(resultadoexcel);
                }


                return new Response<bool>();
            }
            catch (Exception e)
            {
                _ = e.Message;
                throw;
                return new Response<bool>();
            }

        }
    }
}
