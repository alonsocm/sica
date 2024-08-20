using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Resultados.Comands
{
    public class ActualizarResultadoCommand : IRequest<Response<bool>>
    {

        public ResultadoDto Resultados { get; set; }
    }

    public class ActualizarResultadoCommandHandler : IRequestHandler<ActualizarResultadoCommand, Response<bool>>
    {
        private readonly IResultado _repository;
        private readonly IMuestreoRepository _repositoryMuestreo;
        public ActualizarResultadoCommandHandler(IResultado repository, IMuestreoRepository muestreoRepository)
        {
            _repository = repository;
            _repositoryMuestreo = muestreoRepository;
        }

        public async Task<Response<bool>> Handle(ActualizarResultadoCommand request, CancellationToken cancellationToken)
        {
            //Los resultados muestreos solo se actualizan si el estatus destino es 'Enviado'(2)
            bool isEstatusEnviado = (request.Resultados.EstatusId == (int)Enums.EstatusMuestreo.RevisiónOCDLSECAIA);
            if (isEstatusEnviado)
            {
                var resultados = _repository.ObtenerElementosPorCriterio(c => request.Resultados.MuestreoId.Contains(Convert.ToInt32(c.MuestreoId))).ToList();

                resultados.ForEach(resultado =>
                {
                    resultado.ObservacionesOcdlid = null;
                    resultado.ObservacionesOcdl = null;
                    resultado.EsCorrectoOcdl = null;                   
                    _repository.Actualizar(resultado);
                });
            }

            var muestreos = _repositoryMuestreo.ObtenerElementosPorCriterio(c => request.Resultados.MuestreoId.Contains(Convert.ToInt32(c.Id))).ToList();

            if (muestreos.Any())
            {
                muestreos.ForEach(muestreo =>
                {
                    muestreo.EstatusOcdl = (isEstatusEnviado) ? null :((request.Resultados.EstatusOCDLId != null) ? request.Resultados.EstatusOCDLId : muestreo.EstatusOcdl);
                    muestreo.FechaRevisionOcdl = (isEstatusEnviado) ? null :((request.Resultados.EstatusOCDLId != null) ? DateTime.Now : muestreo.FechaRevisionOcdl);
                    muestreo.UsuarioRevisionOcdlid = (isEstatusEnviado) ? null : ((request.Resultados.EstatusOCDLId != null) ? request.Resultados.IdUsuario : muestreo.UsuarioRevisionOcdlid);

                    muestreo.EstatusSecaia = (request.Resultados.EstatusSECAIAId != null) ? request.Resultados.EstatusSECAIAId : muestreo.EstatusSecaia;
                    muestreo.FechaRevisionSecaia = (request.Resultados.EstatusSECAIAId != null) ? DateTime.Now : muestreo.FechaRevisionSecaia;
                    muestreo.UsuarioRevisionSecaiaid = (request.Resultados.EstatusSECAIAId != null) ? request.Resultados.IdUsuario : muestreo.UsuarioRevisionSecaiaid;

                    muestreo.EstatusId = (muestreo.EstatusOcdl == (int)Enums.EstatusOcdlSEcaia.AprobacionFinal && muestreo.EstatusSecaia == (int)Enums.EstatusOcdlSEcaia.AprobacionFinal) ? (int)Enums.EstatusMuestreo.Aprobaciónderesultados : muestreo.EstatusId;
                    _repositoryMuestreo.Actualizar(muestreo);
                });
            }

            return new Response<bool>(true);
        }

    }
}
