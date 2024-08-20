using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;


namespace Application.Features.RevisionResultados.Queries
{
    public class UpdateResultadosExcelCommand : IRequest<Response<bool>>
    {
        public List<UpdateMuestreoExcelDto> Parametros { get; set; } = new List<UpdateMuestreoExcelDto>();
    }

    public class UpdateResultadosExcelHandler : IRequestHandler<UpdateResultadosExcelCommand, Response<bool>>
    {
        private readonly IResumenResRepository _repository;
        
        public UpdateResultadosExcelHandler(IResumenResRepository repositoryAsync)
        {
            _repository = repositoryAsync;
        }

        public async Task<Response<bool>> Handle(UpdateResultadosExcelCommand request, CancellationToken cancellationToken)
        {
            var resultados = _repository.ConvertMuestreosParamsList(request.Parametros);/// faltan aqui el repositorio

            foreach (var resultado in resultados)
            {
                var resultadoBd = await _repository.ObtenerElementoPorIdAsync(resultado.Id);
                resultadoBd.ObservacionesOcdl = resultado.ObservacionesOcdl;
                resultadoBd.ObservacionesOcdlid = resultado.ObservacionesOcdlid;
                resultadoBd.EsCorrectoOcdl = resultado.EsCorrectoOcdl;
                resultadoBd.Muestreo.EstatusOcdl = (int)Enums.EstatusOcdlSEcaia.Validado;
                //test.Muestreo.EstatusId = (int)Enums.EstatusMuestreo.Validado;
                _repository.Actualizar(resultadoBd);
            }

            return new Response<bool>(true);
        }
    }
}
