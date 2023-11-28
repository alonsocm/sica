import { Component, OnInit } from '@angular/core';
import * as L from 'leaflet';
import { EvidenciasService } from '../../evidencias/services/evidencias.service';
import { MapService } from '../../../map/map.service';
import { puntosMuestreo, puntosMuestreoNombre } from 'src/app/shared/enums/puntosMuestreo';
import { PuntosEvidenciaMuestreo } from 'src/app/interfaces/puntosEvidenciaMuestreo.interface';

let puntos;

@Component({
  selector: 'app-map-muestreo',
  templateUrl: './map-muestreo.component.html',
  styleUrls: ['./map-muestreo.component.css']
})
export class MapMuestreoComponent implements OnInit {

  puntosmuestreos: Array<PuntosEvidenciaMuestreo> = [];
  iconRojo: string = 'assets/images/map/iconRojo.png';
  iconVerde: string = 'assets/images/map/iconVerde.png';
  iconAmarillo: string = 'assets/images/map/iconAmarillo.png';
  iconMorado: string = 'assets/images/map/iconMorado.png';
  iconAzul: string = 'assets/images/map/iconAzul.png';
  iconNaranja: string = 'assets/images/map/iconNaranja.png';

  mapas: Array<string> = ['Sina:m00_estados', 'Sina:m00_cuencas', 'Sina:m00_acuiferos', 'Sina:m00_cuerposagua', 'Sina:m00_consejocuencas', 'Sina:m00_riosprincipales'];
  owsrootUrl = 'https://geosinav30.conagua.gob.mx:8080/geoserver/Sina/ows';
  nombreMapa: string = ''; 

  constructor(
    private evidenciasService: EvidenciasService,
    private mapService: MapService) { }
  ngOnInit(): void {
    this.cargarPuntosMuestreo();
 
  }
  cargarPuntosMuestreo() {
    this.obtenerCoordenadas();
    let puntoFA = this.puntosmuestreos.filter((x) => x.punto == puntosMuestreo.FotodeAforo_FA)[0];
    let puntoFM = this.puntosmuestreos.filter((x) => x.punto == puntosMuestreo.FotodeMuestreo_FM)[0];
    let puntoTR = this.puntosmuestreos.filter((x) => x.punto == puntosMuestreo.PuntoCercanoalTrack_TR)[0];
    let puntoFS = this.puntosmuestreos.filter((x) => x.punto == puntosMuestreo.FotodeMuestras_FS)[0];
    let puntoPR = this.puntosmuestreos.filter((x) => x.punto == puntosMuestreo.PuntodeReferencia_PR)[0];
    let puntoPM = this.puntosmuestreos.filter((x) => x.punto == puntosMuestreo.PuntodeMuestreo_PM)[0];

    let FA = L.marker([puntoFA.latitud, puntoFA.longitud], { icon: this.iconControl(this.iconVerde) }).bindPopup('Foto de aforo'),
      FM = L.marker([puntoFM.latitud, puntoFM.longitud], { icon: this.iconControl(this.iconNaranja) }).bindPopup('Foto de Muestreo'),
      TR = L.marker([puntoTR.latitud, puntoTR.longitud], { icon: this.iconControl(this.iconMorado) }).bindPopup('Punto más cercano al Track'),
      FS = L.marker([puntoFS.latitud, puntoFS.longitud], { icon: this.iconControl(this.iconRojo) }).bindPopup('Foto de la Muestra'),
      PR = L.marker([puntoPR.latitud, puntoPR.longitud], { icon: this.iconControl(this.iconAzul) }).bindPopup('Punto de referencia'),
      PM = L.marker([puntoPM.latitud, puntoPM.longitud], { icon: this.iconControl(this.iconAmarillo) }).bindPopup('Punto de muestreo');

    puntos = L.layerGroup([FA, FM, TR, FS, PR, PM]);

    let osm = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 30,
      attribution: '© OpenStreetMap'
    });
    let map = L.map('map', {
      center: [puntoPR.latitud, puntoPR.longitud],
      zoom: 20,
      layers: [osm, puntos]
    });
    let osmHOT = L.tileLayer('https://{s}.tile.openstreetmap.fr/hot/{z}/{x}/{y}.png', {
      maxZoom: 30,
      attribution: '© OpenStreetMap contributors, US Census Bureau'
    });
    let baseMaps = {
      "OpenStreetMap": osm,
      "OpenStreetMap.HOT": osmHOT
    };

    let mostrarPuntos = {
      "Todos los puntos  ": puntos,
      "FA (Foto de aforo)": FA,
      "FM (Foto de muestreo)": FM,
      "FS (Foto de la muestra)": FS,
      "TR (Punto más cecano al track)": TR,
      "PR (Punto de referencia)": PR,
      "PM (Punto de muestreo)": PM };
    let layerControl = L.control.layers(baseMaps, mostrarPuntos).addTo(map);
    let openTopoMap = L.tileLayer('https://{s}.tile.opentopomap.org/{z}/{x}/{y}.png', {
      maxZoom: 30,
      attribution: 'Map data: © OpenStreetMap contributors, SRTM | Map style: © OpenTopoMap (CC-BY-SA)'
    });

    layerControl.addBaseLayer(openTopoMap, "OpenTopoMap");
    this.cargarCapas(map, layerControl);

    let radioPR_PM = L.polygon([
      [puntoPR.latitud, puntoPR.longitud],
      [puntoPM.latitud, puntoPM.longitud]
    ], {color:'yellow'}).addTo(map);

    var distance = map.distance([puntoPR.latitud, puntoPR.longitud], [puntoPM.latitud, puntoPM.longitud]);

    let circle = L.circle([puntoPR.latitud, puntoPR.longitud], {
      color: 'red',
      fillColor: '#f03',
      fillOpacity: 0.5,
      radius: distance
    }).addTo(map);

  }
  obtenerCoordenadas() {
    this.evidenciasService.coordenadas.subscribe((data) => {
      this.puntosmuestreos = data;
      console.log('map');
      console.log(this.puntosmuestreos);
    });
  }
  onEachFeature(feature: any, layer: any) {
    layer.bindPopup('Nombre del acuífero ' + feature.properties.acuifero);
  }
  iconControl(ruta: string) {
    var greenIcon = L.icon({
      iconUrl: ruta,
      iconSize: [45, 45],
    });
    return greenIcon;
  }
  obtenerCapa(nombrecapa: string) {
    var defaultParameters = {
      service: 'WFS',
      version: '1.0.0',
      request: 'GetFeature',
      typeName: nombrecapa,
      outputFormat: 'application/json',
      srsName: "EPSG:4326"
    };
    return defaultParameters;
  }
  cargarCapas(map: any, layerControl: any) {

    for (var i = 0; i < this.mapas.length; i++) {
      const urlToJSonMap: string = this.owsrootUrl + L.Util.getParamString(L.Util.extend(this.obtenerCapa(this.mapas[i])));
      this.mapService.getCapas(urlToJSonMap).subscribe({
        next: (response: any) => {
          var cuencas = L.geoJson(response, {
            style: { color: '#2ECCFA', weight: 2 },
            onEachFeature: this.onEachFeature.bind(this),
          }).addTo(map);

          switch (this.mapas[i]) {
            case 'Sina: m00_cuencas': this.nombreMapa = 'Cuencas';
              break;
            case 'Sina: m00_acuiferos': this.nombreMapa = 'Acuiferos';
              break;
            default: 'xxxx'; break;
          }
          layerControl.addBaseLayer(cuencas, this.nombreMapa);
        },
        error: (error) => { },
      });
    }
  }
}

