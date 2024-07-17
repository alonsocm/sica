using Application.Features.Sitios.Commands.UpdateSitioCommand;
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

namespace Application.Features.Catalogos.TiposCuerpoAgua.Commands.UpdateTipoCuerpoAguaCommand
{
    public class UpdateTipoCuerpoAguaCommand : IRequest<Response<long>>
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public long TipoHomologadoid { get; set; }
        public bool Activo { get; set; }
        public string Frecuencia { get; set; }
        public int EvidenciaEsperada { get; set; }
        public int TiempoMinimoMuestreo { get; set; }
    }
    public class UpdateTipoCuerpoAguaCommandHandler : IRequest<UpdateTipoCuerpoAguaCommand>
        {
        private readonly IRepository<TipoCuerpoAgua> _repository;
        private readonly IMapper _mapper;

        public UpdateTipoCuerpoAguaCommandHandler(IRepository<TipoCuerpoAgua> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
                    }
        public async Task<Response<long>> Handle(UpdateTipoCuerpoAguaCommand request, CancellationToken cancellationToken)
        {
            var tipoCuerpoAgua = _repository(Id);

            if (tipoCuerpoAgua == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }

            tipoCuerpoAgua.Descripcion = request.Descripcion;
            tipoCuerpoAgua.Activo = request.Activo;
            tipoCuerpoAgua.Frecuencia = request.Frecuencia;
            tipoCuerpoAgua.TiempoMinimoMuestreo = request.TiempoMinimoMuestreo;
           
                      
            _repository.Actualizar(tipoCuerpoAgua);

            return new Response<long>(tipoCuerpoAgua.Id);
        }

    }
}
