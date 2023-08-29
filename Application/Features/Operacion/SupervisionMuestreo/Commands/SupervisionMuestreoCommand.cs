using Application.DTOs;
using Application.Enums;
using Application.Features.Operacion.SustitucionLimites.Commands;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
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

    public class SupervisionMuestreoCommandHandler : IRequestHandler<SupervisionMuestreoCommand, Response<bool>>
    {
       
        private readonly ISupervisionMuestreoRepository _supervisionMuestreoRepository;
        
        public SupervisionMuestreoCommandHandler(ISupervisionMuestreoRepository supervisionRepository)
        {
            _supervisionMuestreoRepository = supervisionRepository;
      
        }

        public async Task<Response<bool>> Handle(SupervisionMuestreoCommand request, CancellationToken cancellationToken)
        {
            if (request.supervision.Id != 0) { }
            else
            { }





            return new Response<bool>(true);

        }



    }


}
