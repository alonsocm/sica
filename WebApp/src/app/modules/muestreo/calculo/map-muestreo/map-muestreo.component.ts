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

    var cities = L.layerGroup([FA, FM, TR, FS]);

    var osm = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 20,
      attribution: '© OpenStreetMap'
    });

    var map = L.map('map', {
      center: [19.414030000, -98.988660000],
      zoom: 15,
      layers: [osm, cities]
    });



    var osmHOT = L.tileLayer('https://{s}.tile.openstreetmap.fr/hot/{z}/{x}/{y}.png', {
      maxZoom: 20,
      attribution: '© OpenStreetMap contributors, US Census Bureau'
    });

    var baseMaps = {
      "OpenStreetMap": osm, 
      "OpenStreetMap.HOT": osmHOT
  
    };

    var overlayMaps = {
      "Todos los puntos  ": cities,
      "FA (Foto de aforo)": FA,
      "FM (Foto de muestreo)": FM,
      "FS (Foto de la muestra)": FS,
      "TR (Punto más cecano al track)": TR
    };

    


    var layerControl = L.control.layers(baseMaps, overlayMaps).addTo(map);


    var openTopoMap = L.tileLayer('https://{s}.tile.opentopomap.org/{z}/{x}/{y}.png', {
      maxZoom: 19,
      attribution: 'Map data: © OpenStreetMap contributors, SRTM | Map style: © OpenTopoMap (CC-BY-SA)'
    });

    layerControl.addBaseLayer(openTopoMap, "OpenTopoMap");

    var owsrootUrl = 'https://geosinav30.conagua.gob.mx:8080/geoserver/Sina/ows';
    var defaultParameters = {
      service: 'WFS',
      version: '1.0.0',
      request: 'GetFeature',
      typeName: 'Sina:m00_cuencas',
      outputFormat: 'application/json',
      srsName: "EPSG:4326"
    };

    var defaultParametersAcuiferos = {
      service: 'WFS',
      version: '1.0.0',
      request: 'GetFeature',
      typeName: 'Sina:m00_acuiferos',
      outputFormat: 'application/json',
      srsName: "EPSG:4326"
    };



    var parameters = L.Util.extend(defaultParameters);
    var parametersAcuifero = L.Util.extend(defaultParametersAcuiferos);


    const urlToJSonCuencas: string = owsrootUrl + L.Util.getParamString(parameters);
    const urlToJSonAcuiferos: string = owsrootUrl + L.Util.getParamString(parametersAcuifero);

    var cuencasData = this.mapService.getCapas(urlToJSonCuencas).subscribe({
      next: (response: any) => {
        
        var cuencas =L.geoJson(response, {
          style: { color: '#2ECCFA', weight: 2 },
          onEachFeature: this.onEachFeature.bind(this),
        
        }).addTo(map);
        layerControl.addBaseLayer(cuencas, "Cuencas");
      },
      error: (error) => { },
    });


    var AcuiferosData = this.mapService.getCapas(urlToJSonAcuiferos).subscribe({
      next: (response: any) => {

        var acuiferos = L.geoJson(response, {
          style: { color: '#808080 ', weight: 2 },
          onEachFeature: this.onEachFeature.bind(this),

        }).addTo(map);
        layerControl.addBaseLayer(acuiferos, "Límite de Acuífero");
      },
      error: (error) => { },
    });



  
    const infoDiv = (props: {
      cuenca: string;
      clvcuenca: string;
      rh: string;
      clvrh: string;
    }): string => (`
<b>Cuenca: ${props.cuenca}</b><br /> 
<b>Clave: ${props.clvcuenca}</b><br />
<b>Región Hidrológica: ${props.rh}</b><br /> 
<b>Clave Región: ${props.clvrh}</b><br />
<b>Latitud: <span id="s_lat"/></b><br />
<b>Longitud: <span id="s_lng"/></b><br />`);



    //var marker = L.marker([51.5, -0.09]).addTo(map);

    //var circle = L.circle([51.508, -0.11], {
    //  color: 'red',
    //  fillColor: '#f03',
    //  fillOpacity: 0.5,
    //  radius: 500
    //}).addTo(map);

    //var polygon = L.polygon([
    //  [51.509, -0.08],
    //  [51.503, -0.06],
    //  [51.51, -0.047]
    //]).addTo(map);

    //marker.bindPopup("<b>Hello world!</b><br>I am a popup.").openPopup();
    //circle.bindPopup("I am a circle.");
    //polygon.bindPopup("I am a polygon.");




    //var popup = L.popup()
    //  .setLatLng([51.513, -0.09])
    //  .setContent("I ams.")
    //  .openOn(map);

    //function onMapClick(e: any): void {
    //  alert("You clicked the map at " + e.latlng);
    //}

    //map.on('click', onMapClick);

    //map.attributionControl.addAttribution('Population data &copy; <a href="http://census.gov/">US Census Bureau</a>');
    //map.on("click", clickHandler);




    //const popup: L.Popup = L.popup();

    //function onMapClick(e: L.LeafletMouseEvent): void {
    //  popup
    //    .setLatLng(e.latlng)
    //    .setContent("You clicked the map at " + e.latlng.toString())
    //    .openOn(map);
    //}

    //map.on('click', onMapClick);

   
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
}

