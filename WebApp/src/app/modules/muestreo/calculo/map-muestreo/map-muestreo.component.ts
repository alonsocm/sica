import { Component, OnInit } from '@angular/core';
import * as L from 'leaflet';
import { getJSON } from 'jquery';
import { EvidenciasService } from '../../evidencias/services/evidencias.service';
import { MapService } from '../../../map/map.service';


let _staticMessage = 'Cargando información de cuencas <br/>Espere por favor...';
let _urlCuencasJson = "https://geosinav30.conagua.gob.mx:8080/geoserver/Sina/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=Sina%3Am00_cuencas&outputFormat=application%2Fjson";
let _htmlinfo;

@Component({
  selector: 'app-map-muestreo',
  templateUrl: './map-muestreo.component.html',
  styleUrls: ['./map-muestreo.component.css']
})
export class MapMuestreoComponent implements OnInit {

  iconRojo: string = 'assets/images/map/iconRojo.png';
  iconVerde: string = 'assets/images/map/iconVerde.png';
  iconAmarillo: string = 'assets/images/map/iconAmarillo.png';
  iconMorado: string = 'assets/images/map/iconMorado.png';
  iconAzul: string = 'assets/images/map/iconAzul.png';
  iconNaranja: string = 'assets/images/map/iconNaranja.png';

  mapas: Array<string> = ['Sina:m00_estados', 'Sina:m00_cuencas', 'Sina:m00_acuiferos', 'Sina:m00_cuerposagua', 'Sina:m00_consejocuencas','Sina:m00_riosprincipales'];

  owsrootUrl = 'https://geosinav30.conagua.gob.mx:8080/geoserver/Sina/ows';

  nombreMapa: string = '';

  constructor(
    private evidenciasService: EvidenciasService,
    private mapService: MapService
  ) {
    
  }

  ngOnInit(): void {

    var FA = L.marker([19.414022989, -98.988707758], { icon: this.iconControl(this.iconVerde) }).bindPopup('Foto de aforo'),
      FM = L.marker([19.414025766, -98.988697555], { icon: this.iconControl(this.iconNaranja) }).bindPopup('Foto de Muestreo'),
      TR = L.marker([19.414030000, -98.988660000], { icon: this.iconControl(this.iconMorado) }).bindPopup('Punto más cercano al Track'),
      FS = L.marker([19.413751843, -98.988978327], { icon: this.iconControl(this.iconRojo) }).bindPopup('Foto de la Muestra');

    var puntos = L.layerGroup([FA, FM, TR, FS]);

    var osm = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 20,
      attribution: '© OpenStreetMap'
    });

    var map = L.map('map', {
      center: [19.414030000, -98.988660000],
      zoom: 15,
      layers: [osm, puntos]
    });


    var osmHOT = L.tileLayer('https://{s}.tile.openstreetmap.fr/hot/{z}/{x}/{y}.png', {
      maxZoom: 20,
      attribution: '© OpenStreetMap contributors, US Census Bureau'
    });

    var baseMaps = {
      "OpenStreetMap": osm, 
      "OpenStreetMap.HOT": osmHOT
  
    };

    var mostrarPuntos = {
      "Todos los puntos  ": puntos,
      "FA (Foto de aforo)": FA,
      "FM (Foto de muestreo)": FM,
      "FS (Foto de la muestra)": FS,
      "TR (Punto más cecano al track)": TR
    };

    var layerControl = L.control.layers(baseMaps, mostrarPuntos).addTo(map);

    var openTopoMap = L.tileLayer('https://{s}.tile.opentopomap.org/{z}/{x}/{y}.png', {
      maxZoom: 19,
      attribution: 'Map data: © OpenStreetMap contributors, SRTM | Map style: © OpenTopoMap (CC-BY-SA)'
    });

    layerControl.addBaseLayer(openTopoMap, "OpenTopoMap");    

    this.cargarCapas(map, layerControl);
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

