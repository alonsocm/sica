﻿using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Operacion.SupervisionMuestreo.Commands
{
    public class SupervisionMuestreoCommand : IRequest<Response<RespuestaSupervisionDto>>
    {
        public SupervisionMuestreoDto supervision { get; set; }
    }

    public class SupervisionMuestreoCommandHandler : IRequestHandler<SupervisionMuestreoCommand, Response<RespuestaSupervisionDto>>
    {
        private readonly ISupervisionMuestreoRepository _repository;
        private readonly IValoresSupervisionMuestreoRepository _valoresrepository;
        private readonly IMapper _mapper;
        private readonly IEvidenciaSupervisionMuestreoRepository _evidenciasupervisionrepository;
        public SupervisionMuestreoCommandHandler(ISupervisionMuestreoRepository repository, IValoresSupervisionMuestreoRepository valoresrepository,
            IMapper mapper, IEvidenciaSupervisionMuestreoRepository evidenciasupervisionrepository)
        {
            _repository = repository;
            _valoresrepository = valoresrepository;
            _mapper = mapper;
            _evidenciasupervisionrepository = evidenciasupervisionrepository;
        }

        public async Task<Response<RespuestaSupervisionDto>> Handle(SupervisionMuestreoCommand request, CancellationToken cancellationToken)
        {
            var existeClaveMuestreo = new Domain.Entities.SupervisionMuestreo();
            if (request.supervision.Id == 0) { existeClaveMuestreo = _repository.ObtenerElementosPorCriterioAsync(x => x.ClaveMuestreo == request.supervision.ClaveMuestreo).Result.FirstOrDefault(); }
            else { existeClaveMuestreo = null; }
            Domain.Entities.SupervisionMuestreo supervison = null;
            var respuesta = new RespuestaSupervisionDto();

            try
            {
                if (existeClaveMuestreo==null)
                {
                    supervison = _repository.ConvertirSupervisionMuestreo(request.supervision);

                    if (request.supervision.Id != 0)
                        _repository.Actualizar(supervison);
                    else
                        _ = _repository.Insertar(supervison);

                    if (request.supervision.Clasificaciones.Count > 0)
                    {
                        List<ValoresSupervisionMuestreo> lstValores = _valoresrepository.ConvertiraValoresSupervisionMuestreo(request.supervision.Clasificaciones, supervison.Id);
                        List<ValoresSupervisionMuestreo> lstValoresActualizar = lstValores.Where(x => x.Id != 0).ToList();
                        List<ValoresSupervisionMuestreo> lstValoresNuevos = lstValores.Where(x => x.Id == 0 && x.Resultado != null).ToList();

                        if (lstValoresNuevos.Count > 0)
                            _valoresrepository.InsertarRango(lstValoresNuevos);

                        if (lstValoresActualizar.Count > 0)
                        {
                            foreach (var criterio in lstValoresActualizar)
                            {
                                _valoresrepository.Actualizar(criterio);
                            }
                        }
                    }

                  
                    respuesta.SupervisionMuestreoId = supervison.Id;
                    respuesta.Completo = (request.supervision.Id != 0 && request.supervision.Archivos.Count > 0 && request.supervision.Clasificaciones.Count > 0) ? true : false;
                    

                }
                else
                { throw new ApplicationException("Ya se encuentra la clave de muestreo registrada"); }

               
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ya se encuentra la clave de muestreo registrada"); ;
            }
            return new Response<RespuestaSupervisionDto>(respuesta);
        }
    }
}
