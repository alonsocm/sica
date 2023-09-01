using Application.DTOs;
using Application.Enums;
using Application.Features.Operacion.SustitucionLimites.Commands;
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

namespace Application.Features.Operacion.SupervisionMuestreo.Commands
{
    public class SupervisionMuestreoCommand: IRequest<Response<bool>>
    {
        public SupervisionMuestreoDto supervision { get; set; }
    }

    public class SupervisionMuestreoCommandHandler :  IRequestHandler<SupervisionMuestreoCommand, Response<bool>>
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

        public async Task<Response<bool>> Handle(SupervisionMuestreoCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.SupervisionMuestreo supervison = _repository.ConvertirSupervisionMuestreo(request.supervision);
            if (request.supervision.Id != 0) { _repository.Actualizar(supervison); }
            else
            { var dato = _repository.Insertar(supervison); }

            if (request.supervision.Clasificaciones.Count > 0)
            {                
                List<ValoresSupervisionMuestreo> lstValores = _valoresrepository.ConvertiraValoresSupervisionMuestreo(request.supervision.Clasificaciones, supervison.Id);
                List<ValoresSupervisionMuestreo> lstValoresActualizar = lstValores.Where(x => x.Id != 0).ToList();
                List<ValoresSupervisionMuestreo> lstValoresNuevos = lstValores.Where(x => x.Id == 0).ToList();
                if (lstValoresNuevos.Count > 0) { _valoresrepository.InsertarRango(lstValoresNuevos); }
                if (lstValoresActualizar.Count > 0) { await _valoresrepository.ActualizarBulkAsync(lstValoresActualizar); }
                
            }
            if (request.supervision.LstEvidencia.Count > 0)
            {
                List<EvidenciaSupervisionMuestreo> lstevidencias = _mapper.Map<List<EvidenciaSupervisionMuestreo>>(request.supervision.LstEvidencia);
                List<EvidenciaSupervisionMuestreo> lstevidenciasActualizar = lstevidencias.Where(x => x.Id != 0).ToList();
                   List<EvidenciaSupervisionMuestreo> lstevidenciasNuevas = lstevidencias.Where(x => x.Id == 0).ToList();
                if (lstevidenciasNuevas.Count > 0) { _evidenciasupervisionrepository.InsertarRango(lstevidencias); }
                if (lstevidenciasActualizar.Count > 0) { await _evidenciasupervisionrepository.ActualizarBulkAsync(lstevidenciasActualizar); }
            }

            return new Response<bool>(true);
        }}
}
