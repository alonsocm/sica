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

  coordinates: Array<any> = [];
  xmlDocument!: XMLDocument;


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


    //this.coordinates = [
    //  { lat: puntoFA.latitud, lng: puntoFA.longitud },
    //  { lat: puntoFM.latitud, lng: puntoFM.longitud },
    //  { lat: puntoTR.latitud, lng: puntoTR.longitud },
    //  { lat: puntoFS.latitud, lng: puntoFS.longitud },
    //  { lat: puntoPR.latitud, lng: puntoPR.longitud },
    //  { lat: puntoPM.latitud, lng: puntoPM.longitud }
    //];

    //this.coordinates = [
    //  { lat: 14.05891566, lng: -19.9981566 },
    //  { lat: 14.05668566, lng: -19.9566123 },
    //  { lat: 14.05567413, lng: -19.9467456 },
    //  { lat: 14.05455655, lng: -19.9367125 }
    //]

    this.coordinates = [
      { lat: 20.28375, lng: -101.02229 },
      { lat: 20.28364, lng: -101.02232 },
      { lat: 20.283622222, lng: -101.022638889 },
      { lat: 20.283538889, lng: -101.022580556 },
      { lat: 20.283750534, lng: -101.022293091 },
      { lat: 20.283663889, lng: -101.022544444 }
    ]


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
  createAndDownloadKML(): void {
    const textXML = this.createKMLFileFromCoordinates(this.coordinates);
    this.download('points.kml', textXML);
  }

  createKMLFileFromCoordinates(coordinates: { lat: number, lng: number }[]): string {
    this.xmlDocument = document.implementation.createDocument("", "", null);
    const kmlNode = this.xmlDocument.createElement('kml');
    kmlNode.setAttribute('xmlns', 'http://www.opengis.net/kml/2.2');
    const documentNode = this.xmlDocument.createElement('Document');
    kmlNode.appendChild(documentNode);
    this.xmlDocument.appendChild(kmlNode);
  

    coordinates.forEach((coord, i) => {
      documentNode.appendChild(this.createPointNode(i.toString(), coord.lat, coord.lng));
    });
    return this.xmlDocumentToString(this.xmlDocument);
  }

  download(filename: string, xmlDocument: any): void {
    var element = document.createElement('a');
    element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(xmlDocument));
    element.setAttribute('download', filename);

    element.style.display = 'none';
    document.body.appendChild(element);

    element.click();

    document.body.removeChild(element);
  }

  xmlDocumentToString(xmlDocument: XMLDocument): string {
    let textXML = new XMLSerializer().serializeToString(this.xmlDocument);
    textXML = '<?xml version="1.0" encoding="UTF-8"?>' + textXML;
    return textXML;
  }

  createPointNode(name: string, lat: any, lng: any): HTMLElement {
    const placemarkNode = this.xmlDocument.createElement('Placemark');
    const nameNode = this.xmlDocument.createElement('name');
    nameNode.innerHTML = name;
    const descriptionNode = this.xmlDocument.createElement('description');
    const pointNode = this.xmlDocument.createElement('Point');
    const coordinatesNode = this.xmlDocument.createElement('coordinates');
    coordinatesNode.innerHTML = `${lat},${lng}`;
    placemarkNode.appendChild(nameNode);
    placemarkNode.appendChild(descriptionNode);
    placemarkNode.appendChild(pointNode);
    pointNode.appendChild(coordinatesNode);
    return placemarkNode;
  }
}

