using Application.DTOs.Catalogos;
using Application.Features.Catalogos.ParametrosGrupo.Commands;
using Application.Interfaces.IRepositories;
using Application.Models;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Catalogos.Sitios.Commands
{
    public class CargaSitiosCommand : IRequest<Response<bool>>
    {
        public List<SitiosExcel> Sitios { get; set; }
        public bool Actualizar { get; set; }
    }

    public class CargaSitiosHandler : IRequestHandler<CargaSitiosCommand, Response<bool>>
    {
        private readonly IRepositoryAsync<Estado> _repositoryEstadoAsync;
        private readonly IRepositoryAsync<Municipio> _repositoryMunicipioAsync;
        private readonly IRepositoryAsync<Domain.Entities.CuerpoTipoSubtipoAgua> _repositoryCuerpoTipoSubtipoAsync;
        private readonly ISitioRepository _sitioRepository;
        private readonly ICuencaDireccionesLocalesRepository _cuencasDireccionesRepository;

        public CargaSitiosHandler(ISitioRepository sitioRepository,
            ICuencaDireccionesLocalesRepository cuencasDireccionesRepository,
            IRepositoryAsync<Estado> repositoryEstadoAsync, IRepositoryAsync<Municipio> repositoryMunicipioAsync,
            IRepositoryAsync<Domain.Entities.CuerpoTipoSubtipoAgua> repositoryCuerpoTipoSubtipoAsync)
        {

            _sitioRepository = sitioRepository;
            _cuencasDireccionesRepository = cuencasDireccionesRepository;
            _repositoryEstadoAsync = repositoryEstadoAsync;
            _repositoryMunicipioAsync = repositoryMunicipioAsync;
            _repositoryCuerpoTipoSubtipoAsync = repositoryCuerpoTipoSubtipoAsync;
        }

        public async Task<Response<bool>> Handle(CargaSitiosCommand request, CancellationToken cancellationToken)
        {
            var cuencasDirecciones = _cuencasDireccionesRepository.ObtenerTodosElementosAsync();
            var estados = await _repositoryEstadoAsync.ListAsync();
            var municipios = await _repositoryMunicipioAsync.ListAsync();
            var cuerpoTiposSubtiposAgua = await _repositoryCuerpoTipoSubtipoAsync.ListAsync();

            foreach (var sitio in request.Sitios)
            {
                var cuencaDireccion = cuencasDirecciones.Result.Where(x => x.Ocuenca.Descripcion.ToUpper() == sitio.Cuenca.ToUpper() && x.Dlocal.Descripcion.ToUpper() == sitio.DireccionLocal.ToUpper()); ;
                var estado = estados.Where(x => x.Nombre.ToUpper() == sitio.Estado.ToUpper());
                var municipio = municipios.Where(x => x.EstadoId == estado.First().Id && x.Nombre == sitio.Municipio.ToUpper());
                var cuerpotiposubtipoagua = cuerpoTiposSubtiposAgua.Where(x => x.CuerpoAgua.Descripcion == sitio.CuerpoAgua.ToUpper() &&
                x.TipoCuerpoAgua.Descripcion == sitio.TipoCuerpoAgua.ToUpper() && x.SubtipoCuerpoAgua.Descripcion.ToUpper() == sitio.SubtipoCuerpoAgua.ToUpper());

                var existeSitio = _sitioRepository.ObtenerElementosPorCriterioAsync(x => x.ClaveSitio == sitio.ClaveSitio).Result.FirstOrDefault();

                if (existeSitio != null && !request.Actualizar)
                {
                    return new Response<bool> { Succeded = false, Message = "Se encontraron parámetros registrados previamente" };
                }
                else if (existeSitio != null && request.Actualizar)
                {
                    existeSitio.ClaveSitio = sitio.ClaveSitio;
                    existeSitio.NombreSitio = sitio.NombreSitio;
                    existeSitio.CuencaDireccionesLocalesId = cuencaDireccion.FirstOrDefault().Id;
                    existeSitio.EstadoId = estado.FirstOrDefault().Id;
                    existeSitio.MunicipioId = municipio.FirstOrDefault().Id;
                    existeSitio.CuerpoTipoSubtipoAguaId = cuerpotiposubtipoagua.FirstOrDefault().Id;
                    existeSitio.Latitud = Convert.ToDouble(sitio.Latitud);
                    existeSitio.Longitud = Convert.ToDouble(sitio.Longitud);
                    existeSitio.Observaciones = sitio.Observaciones;
                    _sitioRepository.Actualizar(existeSitio);
                }
                else
                {
                    var nuevoRegistro = new Sitio()
                    {
                        ClaveSitio = sitio.ClaveSitio,
                        NombreSitio = sitio.NombreSitio,
                        CuencaDireccionesLocalesId = cuencaDireccion.FirstOrDefault().Id,
                        EstadoId = estado.FirstOrDefault().Id,
                        MunicipioId = municipio.FirstOrDefault().Id,
                        CuerpoTipoSubtipoAguaId = cuerpotiposubtipoagua.FirstOrDefault().Id,
                        Latitud = Convert.ToDouble(sitio.Latitud),
                        Longitud = Convert.ToDouble(sitio.Longitud),
                        Observaciones = sitio.Observaciones
                    };
                    _sitioRepository.Insertar(nuevoRegistro);

                }
            }

            return new Response<bool>(true);
        }
    }
}
