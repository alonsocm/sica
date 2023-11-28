import { Component, OnInit } from '@angular/core';
import * as L from 'leaflet';
import { EvidenciasService } from '../../evidencias/services/evidencias.service';
import { MapService } from '../../../map/map.service';

@Component({
  selector: 'app-map-muestreo',
  templateUrl: './map-muestreo.component.html',
  styleUrls: ['./map-muestreo.component.css'],
})
export class MapMuestreoComponent implements OnInit {
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
    let FA = L.marker([19.414022989, -98.988707758], {
        icon: this.iconControl(this.iconVerde),
      }).bindPopup('Foto de aforo'),
      FM = L.marker([19.414025766, -98.988697555], {
        icon: this.iconControl(this.iconNaranja),
      }).bindPopup('Foto de Muestreo'),
      TR = L.marker([19.41403, -98.98866], {
        icon: this.iconControl(this.iconMorado),
      }).bindPopup('Punto más cercano al Track'),
      FS = L.marker([19.413751843, -98.988978327], {
        icon: this.iconControl(this.iconRojo),
      }).bindPopup('Foto de la Muestra');

    let puntos = L.layerGroup([FA, FM, TR, FS]);

    let osm = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png');

    let map = L.map('map', {
      center: [19.41403, -98.98866],
      zoom: 15,
      layers: [osm, puntos],
    });

    let baseMaps = {
      OpenStreetMap: osm,
    };

    let layerControl = L.control.layers().addTo(map);

    this.cargarCapas(map, layerControl);

    let circle = L.circle([19.414022989, -98.988707758], {
      color: 'red',
      fillColor: '#f03',
      fillOpacity: 0.5,
      radius: 150,
    }).addTo(map);
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
