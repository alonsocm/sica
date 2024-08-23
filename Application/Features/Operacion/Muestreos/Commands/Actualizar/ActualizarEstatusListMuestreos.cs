using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Actualizar
{
    public class ActualizarEstatusListMuestreos : IRequest<Response<bool>>
    {
        public int EstatusId { get; set; }
        public List<long> Muestreos { get; set; }
    }

    public class ActualizarEstatusListMuestreosHandler : IRequestHandler<ActualizarEstatusListMuestreos, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;

        public ActualizarEstatusListMuestreosHandler(IMuestreoRepository muestreoRepository)
        {
            _muestreoRepository = muestreoRepository;
        }

        public async Task<Response<bool>> Handle(ActualizarEstatusListMuestreos request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Muestreos == null)
                {
                    return new Response<bool> { Succeded = false };
                }
                else
                {
                    var muestreo = await _muestreoRepository.ObtenerElementosPorCriterioAsync(x => request.Muestreos.Contains(x.Id));

                    foreach (var dato in muestreo)
                    {
                        dato.EstatusId = request.EstatusId;
                        // Si se envia al estatus 29 "Acumulados de resultados" se actualiza tambien la bandera de ValidacionEvidencias a true
                        dato.ValidacionEvidencias = (request.EstatusId == (int)Application.Enums.EstatusMuestreo.AcumulacionResultados) ? true : false;
                        //Estatusid 2 en "Enviado", pasa de Liberacion a revision OCDL SECAIA 
                        if (request.EstatusId == (int)Application.Enums.EstatusMuestreo.RevisiónOCDLSECAIA)
                        {
                            var lstnumeroentrega = _muestreoRepository.GetListNumeroEntrega().Result.ToList();
                            dato.NumeroEntrega = (lstnumeroentrega.ToList()[lstnumeroentrega.ToList().Count - 1] == null) ? 1 : lstnumeroentrega.ToList()[lstnumeroentrega.ToList().Count - 1] +1;
                        }

                        _muestreoRepository.Actualizar(dato);
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


