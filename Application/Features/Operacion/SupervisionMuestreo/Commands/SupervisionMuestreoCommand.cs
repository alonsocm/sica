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
        private readonly IRepositoryAsync<Domain.Entities.SupervisionMuestreo> _repositoryAsync;
        
        private IMapper _mapper;
     

        public SupervisionMuestreoCommandHandler(IRepositoryAsync<Domain.Entities.SupervisionMuestreo> repositoryAsync, IMapper mapper )
        {
            _repositoryAsync = repositoryAsync;           
            _mapper = mapper;
      
        }

        public async Task<Response<bool>> Handle(SupervisionMuestreoCommand request, CancellationToken cancellationToken)
        {
         
            if (request.supervision.Id != 0) { }
            else
            {                
               var newSupervision = _mapper.Map<Domain.Entities.SupervisionMuestreo>(request);
               var data =  await _repositoryAsync.AddAsync(newSupervision);
            }
            return new Response<bool>(true);

        }



    }


}
