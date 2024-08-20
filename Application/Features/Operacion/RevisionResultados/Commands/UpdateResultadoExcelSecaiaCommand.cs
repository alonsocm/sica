using Application.DTOs;
using Application.Features.RevisionResultados.Queries;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.RevisionResultados.Commands
{
    public class UpdateResultadoExcelSecaiaCommand : IRequest<Response<bool>>
    {
        public List<UpdateMuestreoSECAIAExcelDto> Parametros { get; set; } = new List<UpdateMuestreoSECAIAExcelDto>();
        public int UserId { get; set; }
    }

    public class UpdateResultadoExcelSecaiaHandler : IRequestHandler<UpdateResultadoExcelSecaiaCommand, Response<bool>>
    {
        private readonly IResumenResRepository _repository;
        private readonly IMuestreoRepository _repositorMuestreoyAsync;

        public UpdateResultadoExcelSecaiaHandler(IResumenResRepository repositoryAsync, IMuestreoRepository muestreorepositorio)
        {
            _repository = repositoryAsync;
            _repositorMuestreoyAsync = muestreorepositorio;
        }

        public async Task<Response<bool>> Handle(UpdateResultadoExcelSecaiaCommand request, CancellationToken cancellationToken) 
        {
            var muestreosRM = _repository.ConvertMuestreosParamsListSECAIA(request.Parametros);/// faltan aqui el repositorio

            foreach (var muestreo in muestreosRM)
            {
                var test = await _repository.ObtenerElementoPorIdAsync(muestreo.Id);
                test.ObservacionesSecaia = muestreo.ObservacionesSecaia;
                test.ObservacionesSecaiaid = muestreo.ObservacionesSecaiaid;               
                test.EsCorrectoSecaia = muestreo.ObservacionesOcdl != "" ? false : true;
                _repository.Actualizar(test);

                var muestreoRM = await _repositorMuestreoyAsync.ObtenerElementoPorIdAsync(test.MuestreoId);
                muestreoRM.UsuarioRevisionSecaiaid = request.UserId;                 
                muestreoRM.FechaRevisionSecaia = DateTime.Now;
                muestreoRM.EstatusSecaia = (int)Enums.EstatusOcdlSEcaia.Validado;
                _repositorMuestreoyAsync.Actualizar(muestreoRM);
            }

            


            return new Response<bool>(true);
        }
    }
}
