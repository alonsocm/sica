import { Component, OnInit } from '@angular/core';
import * as L from 'leaflet';
import { EvidenciasService } from '../../evidencias/services/evidencias.service';
import { MapService } from '../../../map/map.service';
import { PuntosMuestreo } from 'src/app/shared/enums/puntosMuestreo';
import { PuntosEvidenciaMuestreo } from 'src/app/interfaces/puntosEvidenciaMuestreo.interface';
import { calculosMuestreo } from '../../../../interfaces/calculosMuestreo.interface';

const pi: number = 3.141592653589793238462643383;
@Component({
  selector: 'app-map-muestreo',
  templateUrl: './map-muestreo.component.html',
  styleUrls: ['./map-muestreo.component.css'],
})
export class MapMuestreoComponent implements OnInit {
  latitude: any;
  longitude: any;
  datosCalculo: Array<calculosMuestreo> = [];
  puntoPRGeneral!: PuntosEvidenciaMuestreo;
  circle: any;
  radio: number = 0;
  map: any;
  coordinates: Array<any> = [];
  xmlDocument!: XMLDocument;
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

    this.puntoPRGeneral = puntoPR;

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

    this.coordinates = [
      { lat: puntoFA.latitud, lng: puntoFA.longitud },
      { lat: puntoFM.latitud, lng: puntoFM.longitud },
      { lat: puntoTR.latitud, lng: puntoTR.longitud },
      { lat: puntoFS.latitud, lng: puntoFS.longitud },
      { lat: puntoPR.latitud, lng: puntoPR.longitud },
      { lat: puntoPM.latitud, lng: puntoPM.longitud }
    ];
   
    let puntos = L.layerGroup([FA, FM, TR, FS, PR, PM]);

    let osm = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 30,
      attribution: '© OpenStreetMap',
    });

    this.map = L.map('map', {
      center: [puntoPR.latitud, puntoPR.longitud],
      zoom: 25,
      maxZoom: 40,
      layers: [osm, puntos],
    });

    let layerControl = L.control.layers().addTo(this.map);
    this.cargarCapas(this.map, layerControl);
    this.latitude = puntoPM.latitud;
    this.longitude = puntoPM.longitud;
    this.crearCircunferencia(this.puntoPRGeneral, this.map);

    this.circle.bindPopup(
      "<div'>Radio:" +
        this.radio +
        '</br> Área:' +
        this.obtenerArea(this.radio) +
        '</br>Circunferencia:' +
        this.obtenerCircunferencia(this.radio) +
        '</div>'
    );
    this.map.on('click', (e: any) => {
      this.onMapClick(e);
    });
  }
  private crearCircunferencia(puntoPR: any, map: any) {
    let radioPR_PM = L.polygon(
      [
        [puntoPR.latitud, puntoPR.longitud],
        [this.latitude, this.longitude],
      ],
      { color: 'yellow' }
    ).addTo(map);

    this.radio = this.map.distance(
      [puntoPR.latitud, puntoPR.longitud],
      [this.latitude, this.longitude]
    );

    this.circle = L.circle([puntoPR.latitud, puntoPR.longitud], {
      color: 'red',
      fillColor: '#f03',
      fillOpacity: 0.5,
      radius: this.radio,
    }).addTo(map);
  }
  onMapClick(e: any) {
    this.longitude = e.latlng.lng;
    this.latitude = e.latlng.lat;
    this.crearCircunferencia(this.puntoPRGeneral, this.map);
    this.calculosCircunferencia();
  }
  calculosCircunferencia() {
    let calculos: calculosMuestreo = {
      puntoOrigen:
        this.puntoPRGeneral.latitud + ',' + this.puntoPRGeneral.longitud,
      puntoDestino: this.latitude + ',' + this.longitude,
      radio: this.radio.toString(),
      area: this.obtenerArea(this.radio).toString(),
      circunferencia: this.obtenerCircunferencia(this.radio).toString(),
    };
    this.datosCalculo.push(calculos);
  }
  obtenerArea(radio: number) {
    let area = radio * radio;
    area = area * pi;
    return area;
  }
  obtenerCircunferencia(radio: number) {
    let circunferencia = 2 * pi * radio;
    return circunferencia;
  }
  getLocation() {
    this.evidenciasService.getPosition().then((pos) => {
      this.latitude = pos.lat;
      this.longitude = pos.lng;
    });
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
          let nombreCapa = '';
          let capa: any;

          switch (this.nombresCapas[i]) {
            case 'Sina:m00_cuencas':
              nombreCapa = 'Cuencas';
              capa = L.geoJson(response, {
                style: { color: '#2ECCFA', weight: 2 },
                onEachFeature: this.onEachFeature.bind(this),
              });
              layerControl.addOverlay(capa, nombreCapa);
              break;
            case 'Sina:m00_acuiferos':
              nombreCapa = 'Acuiferos';
              capa = L.geoJson(response, {
                style: { color: '#54D9FC', weight: 2 },
                onEachFeature: this.onEachFeature.bind(this),
              });
              layerControl.addOverlay(capa, nombreCapa);
              break;
            case 'Sina:m00_estados':
              nombreCapa = 'Estados';
              capa = L.geoJson(response, {
                style: { color: '#DC7633', weight: 2 },
                onEachFeature: this.onEachFeature.bind(this),
              });
              layerControl.addOverlay(capa, nombreCapa);
              break;
            case 'Sina:m00_cuerposagua':
              nombreCapa = 'Cuerpos de agua';
              capa = L.geoJson(response, {
                style: { color: '#85C1E9', weight: 2 },
                onEachFeature: this.onEachFeature.bind(this),
              });
              layerControl.addOverlay(capa, nombreCapa);
              break;
            case 'Sina:m00_consejocuencas':
              nombreCapa = 'Consejo cuencas';
              capa = L.geoJson(response, {
                style: { color: '#1D79EE', weight: 2 },
                onEachFeature: this.onEachFeature.bind(this),
              });
              layerControl.addOverlay(capa, nombreCapa);
              break;
            case 'Sina:m00_riosprincipales':
              nombreCapa = 'Principales rios';
              capa = L.geoJson(response, {
                style: { color: '#2E86C1', weight: 2 },
                onEachFeature: this.onEachFeature.bind(this),
              });
              layerControl.addOverlay(capa, nombreCapa);
              break;
            default:
              'xxxx';
              break;
          }
        },
        error: (error) => {},
      });
    }
  }
  createAndDownloadKML(): void {
    const textXML = this.createKMLFileFromCoordinates(this.coordinates);
    this.download('PuntosMuestreo' + localStorage.getItem('claveMuestreoCalculo') + '.kml', textXML);
  }
  createKMLFileFromCoordinates(
    coordinates: { lat: number; lng: number }[]
  ): string {
    this.xmlDocument = document.implementation.createDocument('', '', null);
    const kmlNode = this.xmlDocument.createElement('kml');
    kmlNode.setAttribute('xmlns', 'http://www.opengis.net/kml/2.2');
    const documentNode = this.xmlDocument.createElement('Document');
    kmlNode.appendChild(documentNode);
    this.xmlDocument.appendChild(kmlNode);

    this.puntosMuestreo.forEach((coord, i) => { documentNode.appendChild(this.createPointNode(coord.punto, coord.longitud, coord.latitud)); });

    return this.xmlDocumentToString(this.xmlDocument);
  }
  download(filename: string, xmlDocument: any): void {
    var element = document.createElement('a');
    element.setAttribute(
      'href',
      'data:text/plain;charset=utf-8,' + encodeURIComponent(xmlDocument)
    );
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
