import { Component, OnInit } from '@angular/core';
import * as L from 'leaflet';
import { EvidenciasService } from '../../evidencias/services/evidencias.service';
import { MapService } from '../../../map/map.service';
import { PuntosMuestreo } from 'src/app/shared/enums/puntosMuestreo';
import { PuntosEvidenciaMuestreo } from 'src/app/interfaces/puntosEvidenciaMuestreo.interface';

@Component({
  selector: 'app-map-muestreo',
  templateUrl: './map-muestreo.component.html',
  styleUrls: ['./map-muestreo.component.css'],
})
export class MapMuestreoComponent implements OnInit {
  puntosMuestreo: Array<PuntosEvidenciaMuestreo> = [];
  iconRojo: string = 'assets/images/map/iconRojo.png';
  iconVerde: string = 'assets/images/map/iconVerde.png';
  iconAmarillo: string = 'assets/images/map/iconAmarillo.png';
  iconMorado: string = 'assets/images/map/iconMorado.png';
  iconAzul: string = 'assets/images/map/iconAzul.png';
  iconNaranja: string = 'assets/images/map/iconNaranja.png';

  nombresCapas: Array<string> = [
    'Sina:m00_estados',
    'Sina:m00_cuencas',
    'Sina:m00_acuiferos',
    'Sina:m00_cuerposagua',
    'Sina:m00_consejocuencas',
    'Sina:m00_riosprincipales',
  ];

  owsrootUrl = 'https://geosinav30.conagua.gob.mx:8080/geoserver/Sina/ows';

  constructor(
    private evidenciasService: EvidenciasService,
    private mapService: MapService
  ) {}

  ngOnInit(): void {
    this.cargarPuntosMuestreo();
  }

  cargarPuntosMuestreo() {
    this.obtenerCoordenadas();
    let puntoFA = this.puntosMuestreo.filter(
      (x) => x.punto == PuntosMuestreo.FotodeAforo_FA
    )[0];
    let puntoFM = this.puntosMuestreo.filter(
      (x) => x.punto == PuntosMuestreo.FotodeMuestreo_FM
    )[0];
    let puntoTR = this.puntosMuestreo.filter(
      (x) => x.punto == PuntosMuestreo.PuntoCercanoalTrack_TR
    )[0];
    let puntoFS = this.puntosMuestreo.filter(
      (x) => x.punto == PuntosMuestreo.FotodeMuestras_FS
    )[0];
    let puntoPR = this.puntosMuestreo.filter(
      (x) => x.punto == PuntosMuestreo.PuntodeReferencia_PR
    )[0];
    let puntoPM = this.puntosMuestreo.filter(
      (x) => x.punto == PuntosMuestreo.PuntodeMuestreo_PM
    )[0];

    let FA = L.marker([puntoFA.latitud, puntoFA.longitud], {
        icon: this.iconControl(this.iconVerde),
      }).bindPopup('Foto de aforo'),
      FM = L.marker([puntoFM.latitud, puntoFM.longitud], {
        icon: this.iconControl(this.iconNaranja),
      }).bindPopup('Foto de Muestreo'),
      TR = L.marker([puntoTR.latitud, puntoTR.longitud], {
        icon: this.iconControl(this.iconMorado),
      }).bindPopup('Punto más cercano al Track'),
      FS = L.marker([puntoFS.latitud, puntoFS.longitud], {
        icon: this.iconControl(this.iconRojo),
      }).bindPopup('Foto de la Muestra'),
      PR = L.marker([puntoPR.latitud, puntoPR.longitud], {
        icon: this.iconControl(this.iconAzul),
      }).bindPopup('Punto de referencia'),
      PM = L.marker([puntoPM.latitud, puntoPM.longitud], {
        icon: this.iconControl(this.iconAmarillo),
      }).bindPopup('Punto de muestreo');

    let puntos = L.layerGroup([FA, FM, TR, FS, PR, PM]);

    let osm = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png');

    let map = L.map('map', {
      center: [puntoPR.latitud, puntoPR.longitud],
      zoom: 20,
      layers: [osm, puntos],
    });

    let layerControl = L.control.layers().addTo(map);

    this.cargarCapas(map, layerControl);

    let radioPR_PM = L.polygon(
      [
        [puntoPR.latitud, puntoPR.longitud],
        [puntoPM.latitud, puntoPM.longitud],
      ],
      { color: 'yellow' }
    ).addTo(map);

    let distance = map.distance(
      [puntoPR.latitud, puntoPR.longitud],
      [puntoPM.latitud, puntoPM.longitud]
    );

    let circle = L.circle([puntoPR.latitud, puntoPR.longitud], {
      color: 'red',
      fillColor: '#f03',
      fillOpacity: 0.5,
      radius: distance,
    }).addTo(map);
  }

  obtenerCoordenadas() {
    this.evidenciasService.coordenadas.subscribe((data) => {
      this.puntosMuestreo = data;
    });
  }

  onEachFeature(feature: any, layer: any) {
    layer.bindPopup('Nombre del acuífero ' + feature.properties.acuifero);
  }

  iconControl(ruta: string) {
    let greenIcon = L.icon({
      iconUrl: ruta,
      iconSize: [45, 45],
    });
    return greenIcon;
  }

  obtenerCapa(nombrecapa: string) {
    let defaultParameters = {
      service: 'WFS',
      version: '1.0.0',
      request: 'GetFeature',
      typeName: nombrecapa,
      outputFormat: 'application/json',
      srsName: 'EPSG:4326',
    };
    return defaultParameters;
  }

  cargarCapas(map: any, layerControl: any) {
    for (let i = 0; i < this.nombresCapas.length; i++) {
      const urlToJSonMap: string =
        this.owsrootUrl +
        L.Util.getParamString(
          L.Util.extend(this.obtenerCapa(this.nombresCapas[i]))
        );

      this.mapService.getCapas(urlToJSonMap).subscribe({
        next: (response: any) => {
          let capa = L.geoJson(response, {
            style: { color: '#2ECCFA', weight: 2 },
            onEachFeature: this.onEachFeature.bind(this),
          });

          let nombreCapa = '';

          switch (this.nombresCapas[i]) {
            case 'Sina:m00_cuencas':
              nombreCapa = 'Cuencas';
              break;
            case 'Sina:m00_acuiferos':
              nombreCapa = 'Acuiferos';
              break;
            case 'Sina:m00_estados':
              nombreCapa = 'Estados';
              break;
            case 'Sina:m00_cuerposagua':
              nombreCapa = 'Cuerpos de agua';
              break;
            case 'Sina:m00_consejocuencas':
              nombreCapa = 'Consejo cuencas';
              break;
            case 'Sina:m00_riosprincipales':
              nombreCapa = 'Principales rios';
              break;
            default:
              'xxxx';
              break;
          }
          layerControl.addOverlay(capa, nombreCapa);
        },
        error: (error) => {},
      });
    }
  }
}
