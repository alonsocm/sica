using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Liberacion
{
    public class LiberarRevisionSECAIAOCDLCommand : IRequest<Response<bool>>
    {
        public int EstatusId { get; set; }
        public IEnumerable<long> Muestreos { get; set; }
    }

    public class LiberarRevisionSECAIAOCDLCommandHandler : IRequestHandler<LiberarRevisionSECAIAOCDLCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;

        public LiberarRevisionSECAIAOCDLCommandHandler(IMuestreoRepository muestreoRepository)
        {
            _muestreoRepository = muestreoRepository;
        }

        public async Task<Response<bool>> Handle(LiberarRevisionSECAIAOCDLCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Muestreos == null)
                {
                    return new Response<bool> { Succeded = false };
                }
                else
                {
                    var muestreos = await _muestreoRepository.ObtenerElementosPorCriterioAsync(x => request.Muestreos.Contains(x.Id) && x.FechaLimiteRevision != null);

                    if (muestreos.Count() != request.Muestreos.Count())
                    {
                        throw new ArgumentException("No todos los registros seleccionados contienen fecha límite de revisión");
                    }

                    foreach (var muestreo in muestreos)
                    {
                        muestreo.EstatusId = request.EstatusId;
                        // Si se envia al estatus 29 "Acumulados de resultados" se actualiza tambien la bandera de ValidacionEvidencias a true
                        muestreo.ValidacionEvidencias = request.EstatusId == (int)Enums.EstatusMuestreo.AcumulacionResultados;
                        //Estatusid 2 en "Enviado", pasa de Liberacion a revision OCDL SECAIA 
                        if (request.EstatusId == (int)Enums.EstatusMuestreo.RevisiónOCDLSECAIA)
                        {
                            var lstNumeroEntrega = _muestreoRepository.GetListNumeroEntrega().Result.ToList();
                            muestreo.NumeroEntrega = (lstNumeroEntrega.ToList()[lstNumeroEntrega.ToList().Count - 1] == null) ? 1 : lstNumeroEntrega.ToList()[lstNumeroEntrega.ToList().Count - 1] +1;
                        }

                        _muestreoRepository.Actualizar(muestreo);
                    }

                    return new Response<bool> { Succeded = true };
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
