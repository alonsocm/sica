using Application.DTOs;
using Application.Features.Catalogos.Emergencias.Commands;
using Application.Features.Operacion.MuestreosEmergencias.Queries;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Carga
{
    public class CargaMuestreosEmergenciaCommand : IRequest<Response<List<string>>>
    {
        public int Anio { get; set; }
        public bool? Reemplazar { get; set; }
        public List<CargaMuestreoEmergenciaDto> Muestreos { get; set; } = new List<CargaMuestreoEmergenciaDto>();
    }

    public class CargaMuestreosEmergenciaCommandHandler : IRequestHandler<CargaMuestreosEmergenciaCommand, Response<List<string>>>
    {
        private readonly IMuestreoEmergenciasRepository _muestreoEmergenciasRepository;
        private readonly IMediator _mediator;

        public CargaMuestreosEmergenciaCommandHandler(IMuestreoEmergenciasRepository repositoryAsync, IMediator mediator)
        {
            _muestreoEmergenciasRepository=repositoryAsync;
            _mediator=mediator;
        }

        public async Task<Response<List<string>>> Handle(CargaMuestreosEmergenciaCommand request, CancellationToken cancellationToken)
        {
            var emergencias = request.Muestreos.Select(x => new { x.NombreEmergencia, x.Sitio }).Distinct().ToList();

            List<Emergencia> emergenciasNuevas = new();
            List<string> emergenciasPreviamenteCargadas = new();

            foreach (var emergencia in emergencias)
            {
                var existeEmergenciaCatalogo = await _mediator.Send(new ExisteEmergenciaQuery { NombreEmergencia = emergencia.NombreEmergencia }, cancellationToken);

                if (!existeEmergenciaCatalogo.Data)
                {
                    Emergencia nuevaEmergencia = new()
                    {
                        Anio = Convert.ToInt32(request.Anio),
                        NombreEmergencia = emergencia.NombreEmergencia,
                        NombreSitio = emergencia.Sitio,
                        Latitud = string.Empty,
                        Longitud = string.Empty
                    };

                    emergenciasNuevas.Add(nuevaEmergencia);
                    await _mediator.Send(new AgregarEmergenciaCommand { Emergencia = nuevaEmergencia }, cancellationToken);
                }
                else
                {
                    var existeCargaPrevia = await _mediator.Send(new ExisteMuestreoEmergenciaQuery { NombreEmergencia = emergencia.NombreEmergencia, NombreSitio = emergencia.Sitio }, cancellationToken);

                    if (existeCargaPrevia.Data)
                    {
                        emergenciasPreviamenteCargadas.Add(emergencia.NombreEmergencia);
                    }
                }
            }

            if (emergenciasPreviamenteCargadas.Any() && (request.Reemplazar == null || (bool)!request.Reemplazar))
            {
                return new Response<List<string>>() { Succeded=false, Data=emergenciasPreviamenteCargadas, Message="Se encontraron emergencias previamente cargadas" };
            }
            else
            {
                var resultadosEliminar = await _muestreoEmergenciasRepository.ObtenerElementosPorCriterioAsync(x => emergenciasPreviamenteCargadas.Contains(x.NombreEmergencia));

                foreach (var resultado in resultadosEliminar)
                {
                    _muestreoEmergenciasRepository.Eliminar(resultado);
                }
            }

            var muestreos = _muestreoEmergenciasRepository.ConvertToMuestreosList(request.Muestreos);
            _muestreoEmergenciasRepository.InsertarRango(muestreos);

            return new Response<List<string>>() { Succeded=true };
        }
    }
}
