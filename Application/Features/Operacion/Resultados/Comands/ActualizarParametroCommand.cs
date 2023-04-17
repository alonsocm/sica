using Application.DTOs;
using Application.Features.Resultados.Comands;
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

    public class ActualizarParametroCommand : IRequest<Response<bool>>
    {
        public List<ResultadoMuestreoDto> Parametros { get; set; } = new List<ResultadoMuestreoDto>();
    }

    public class ActualizarParametroHandler : IRequestHandler<ActualizarParametroCommand, Response<bool>>
    {
        private readonly IResumenResRepository _repository;

        public ActualizarParametroHandler(IResumenResRepository repositoryAsync)
        {
            _repository = repositoryAsync;

        }

        public async Task<Response<bool>> Handle(ActualizarParametroCommand request, CancellationToken cancellationToken)
        {
            
            foreach (var muestreo in request.Parametros)
            {
                var test = await _repository.ObtenerElementoPorIdAsync(muestreo.Id);
                test.ObservacionesSecaia = muestreo.ObservacionSECAIA;
                test.ObservacionesSecaiaid = muestreo.ObservacionSECAIAId;
                test.EsCorrectoSecaia = (muestreo.ObservacionSECAIAId != null) ? false : true;
                //test.EsCorrectoSecaia = muestreo.EsCorrectoResultado;
                //test.Muestreo.EstatusId = (int)Enums.EstatusMuestreo.Validado;
                _repository.Actualizar(test);
            }

            return new Response<bool>(true);
        }

    }


}
