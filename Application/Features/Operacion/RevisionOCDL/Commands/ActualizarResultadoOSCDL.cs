using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.RevisionOCDL.Commands
{
    public class ActualizarResultadoOSCDL : IRequest<Response<bool>>
    {
        public ResultadoDto Resultados { get; set; }
    }
    public class ActualizarResultadoOSCDLHandler : IRequestHandler<ActualizarResultadoOSCDL, Response<bool>>
    {
        private readonly IResultado _repository;
        private readonly IMuestreoRepository _repositoryMuestreo;
        public ActualizarResultadoOSCDLHandler(IResultado repository, IMuestreoRepository muestreoRepository)
        {
            _repository = repository;
            _repositoryMuestreo = muestreoRepository;
        }
        public async Task<Response<bool>> Handle(ActualizarResultadoOSCDL request, CancellationToken cancellationToken)
        {
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
                    muestreo.EstatusOcdl = (isEstatusEnviado) ? null : ((request.Resultados.EstatusOCDLId != null) ? request.Resultados.EstatusOCDLId : muestreo.EstatusOcdl);
                    muestreo.FechaRevisionOcdl = (isEstatusEnviado) ? null : ((request.Resultados.EstatusOCDLId != null) ? DateTime.Now : muestreo.FechaRevisionOcdl);
                    muestreo.UsuarioRevisionOcdlid = (isEstatusEnviado) ? null : ((request.Resultados.EstatusOCDLId != null) ? request.Resultados.IdUsuario : muestreo.UsuarioRevisionOcdlid);
                    _repositoryMuestreo.Actualizar(muestreo);
                });
            }

            return new Response<bool>(true);
        }
    }
}
