using Application.DTOs;
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

namespace Application.Features.Operacion.Muestreos.Commands.Actualizar
{
    public class ActualizarEstatusListMuestreos : IRequest<Response<bool>>
    {
        public int estatusId { get; set; }
        public List<long>? muestreos { get; set; }



    }

    public class ActualizarEstatusListMuestreosHandler : IRequestHandler<ActualizarEstatusListMuestreos, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly IMapper _mapper;


        public ActualizarEstatusListMuestreosHandler(IMuestreoRepository muestreoRepository, IMapper mapper)
        {
            _muestreoRepository = muestreoRepository;
            _mapper = mapper;
        }
        public async Task<Response<bool>> Handle(ActualizarEstatusListMuestreos request, CancellationToken cancellationToken)
        {
           
            try
            {   
            if (request.muestreos == null)
            { return new Response<bool> { Succeded = false }; }
            else
            {
                var muestreo = await _muestreoRepository.ObtenerElementosPorCriterioAsync(x => request.muestreos.Contains(x.Id));
                foreach (var dato in muestreo)
                {
                    dato.EstatusId = request.estatusId;
                        // Si se envia al estatus 29 "Acumulados de resultados" se actualiza tambien la bandera de ValidacionEvidencias a true
                        dato.ValidacionEvidencias = (request.estatusId == (int)Application.Enums.EstatusMuestreo.AcumulacionResultados) ? true : false;
                        //Estatusid 2 en "Enviado", pasa de Liberacion a revision OCDL SECAIA 
                        if (request.estatusId == (int)Application.Enums.EstatusMuestreo.Enviado)
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
                return new Response<bool> { Succeded = true };
                throw new ApplicationException(ex.Message);
            }
        }

    }
}


