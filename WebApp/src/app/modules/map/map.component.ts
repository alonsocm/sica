import { Component, OnInit } from '@angular/core';
import * as L from 'leaflet';
import { MapService } from './map.service';
import { url } from 'inspector';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
})
export class MapComponent implements OnInit {
  private centroId: L.LatLngExpression = [23.634501, -102.552784];
  constructor(private mapService: MapService) {}

  ngOnInit(): void {
    //Agrega el mapa y lo centra en las geocoordenadas de la República Mexicana con un acercamiento de 5
    var map = L.map('map').setView([23.634501, -102.552784], 5);

    //Solicita la carga de las capas vectoriales del espacio de trabajo de Sina
    var owsrootUrl =
      'https://geosinav30.conagua.gob.mx:8080/geoserver/Sina/ows';

    var defaultParameters = {
      service: 'WFS',
      version: '1.1.0',
      request: 'GetFeature',
      typeName: 'm00_acuiferos',
      srsName: 'EPSG:4326',
      outputFormat: 'application/json',
    };

    var parameters = L.Util.extend(defaultParameters);

    let URL = owsrootUrl + L.Util.getParamString(parameters);

    this.mapService.getCapas(URL).subscribe({
      next: (response: any) => {
        L.geoJson(response, {
          style: { color: '#2ECCFA', weight: 2 },
          onEachFeature: this.onEachFeature.bind(this),
        }).addTo(map);
      },
      error: (error) => {},
    });
  }

  onEachFeature(feature: any, layer: any) {
    layer.bindPopup('Nombre del acuífero ' + feature.properties.acuifero);
  }
}
