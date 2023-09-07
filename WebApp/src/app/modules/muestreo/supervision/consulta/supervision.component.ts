import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Columna } from 'src/app/interfaces/columna-inferface';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { BaseService } from 'src/app/shared/services/base.service';
import { SupervisionService } from '../supervision.service';
import { OrganismoDireccion } from '../models/organismo-direccion';
import { Laboratorio } from '../models/laboratorio';
import { TipoCuerpoAgua } from '../models/tipo-cuerpo-agua';
import { SupervisionBase } from '../models/supervision-base';
import { FileService } from 'src/app/shared/services/file.service';

@Component({
  selector: 'app-supervision',
  templateUrl: './supervision.component.html',
  styleUrls: ['./supervision.component.css'],
})
export class SupervisionComponent extends BaseService implements OnInit {
  organismosDirecciones: Array<OrganismoDireccion> = [];
  laboratorios: Array<Laboratorio> = [];
  tiposCuerpoAgua: Array<TipoCuerpoAgua> = [];
  supervisiones: Array<SupervisionBase> = [];

  constructor(
    private router: Router,
    private supervisionService: SupervisionService
  ) {
    super();
  }

  supervisionBusquedaForm = new FormGroup({
    ocdlRealiza: new FormControl(0),
    sitio: new FormControl(''),
    fechaMuestreo: new FormControl(''),
    puntaje: new FormControl(''),
    laboratorio: new FormControl(0),
    claveMuestreo: new FormControl(''),
    tipoCuerpoAgua: new FormControl(0),
  });

  ngOnInit(): void {
    this.definirColumnas();
    this.getOrganismosDirecciones();
    this.getTiposCuerpoAgua();
    this.getSupervisiones();
    // this.supervisiones.push({
    //   ocdlRealiza: 'Golfo Centro/Hidalgo',
    //   nombreSitio: '0',
    //   fechaMuestreo: new Date('08/28/2023').toLocaleDateString('es-MX'),
    //   puntajeObtenido: 0,
    //   laboratorio: 'ABC MATRIZ',
    //   claveMuestreo: 'OCLSP3827-210822',
    //   tipoCuerpoAgua: 'Costero (humedal)',
    //   id: 35,
    // });
  }

  getSupervisiones() {
    this.supervisionService.getSupervisiones().subscribe({
      next: (response: any) => {
        this.supervisiones = response;
      },
      error: (error) => {},
    });
  }

  definirColumnas() {
    this.columnas = [
      {
        nombre: 'ocdl',
        etiqueta: 'OC/DL realiza la supervisión',
        orden: 2,
        filtro: new Filter(),
      },
      {
        nombre: 'laboratorio',
        etiqueta: 'Laboratorio que realizó muestreo',
        orden: 3,
        filtro: new Filter(),
      },
      {
        nombre: 'sitio',
        etiqueta: 'Sitio',
        orden: 4,
        filtro: new Filter(),
      },
      {
        nombre: 'claveMuestreo',
        etiqueta: 'Clave del muestreo',
        orden: 5,
        filtro: new Filter(),
      },
      {
        nombre: 'fechaMuestreo',
        etiqueta: 'Fecha de muestreo',
        orden: 6,
        filtro: new Filter(),
      },
      {
        nombre: 'tipoCuerpoAgua',
        etiqueta: 'Tipo de cuerpo de agua',
        orden: 7,
        filtro: new Filter(),
      },
      {
        nombre: 'puntuaje',
        etiqueta: 'Puntaje obtenido',
        orden: 8,
        filtro: new Filter(),
      },
    ];
  }

  getOrganismosDirecciones() {
    this.supervisionService.getOCDL().subscribe({
      next: (response: any) => {
        this.organismosDirecciones = response;
      },
      error: (error) => {},
    });
  }

  getLaboratorios() {
    this.supervisionService.getLaboratorios().subscribe({
      next: (response: any) => {
        this.laboratorios = response.data;
        console.log(this.laboratorios);
      },
      error: (error) => {},
    });
  }

  getTiposCuerpoAgua() {
    this.supervisionService.getTiposCuerpoAgua().subscribe({
      next: (response: any) => {
        this.tiposCuerpoAgua = response.data;
        console.log(this.laboratorios);
      },
      error: (error) => {},
    });
  }

  search() {
    console.log(this.supervisionBusquedaForm.value);
  }

  downloadFormatoSupervision() {
    this.supervisionService.getFormatoSupervision().subscribe({
      next: (response: any) => {
        FileService.download(response, 'supervisionMuestreo.pdf');
      },
      error: (error) => {},
    });
  }

  registrarSupervision() {
    this.router.navigate(['/supervision-registro']);
  }

  onEditClick(supervision: number) {
    this.supervisionService.updateSupervisionId(supervision);
    this.router.navigate(['/supervision-registro']);
  }

  onViewClick(supervision: number) {
    this.supervisionService.updateSupervisionId(supervision);
    this.supervisionService.updateEsConsulta(true);
    this.router.navigate(['/supervision-registro']);
  }
}
