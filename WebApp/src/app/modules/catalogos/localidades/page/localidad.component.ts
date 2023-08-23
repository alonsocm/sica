import { Component, Input, NgModule, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, Data } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Respuesta } from '../../../../interfaces/respuesta.interface';
import {LocalidadesService} from '../services/localidades.service';
import { Municipios } from '../../../../interfaces/catalogos/municipio.interface';

@Component({
  selector: 'app-localidad',
  templateUrl: './localidad.component.html',
  styleUrls: ['./localidad.component.css']
})
export class LocalidadComponent implements OnInit {

  public page: number | undefined;
  registroLocalidad: FormGroup;
  errorMessage: any;

  @Input() respuesta: Respuesta = {
    succeded: false,
    message: '',
    errors: '',
    data: [],
  };

  public Localidades: any[] = [];
  public _localidades: any[] = [];
  public localidadId: number = 0;

  constructor(
    private http: HttpClient,
    private router: Router,
    private fb: FormBuilder,
    private LocalidadService: LocalidadesService
  ) {
    this.registroLocalidad = this.fb.group({

    });
  }

  ngOnInit(): void {
    this.LocalidadService.getAllLocalidad().subscribe(
      (result) => {     
        this.Localidades = result.data;
        this._localidades = result.data;
      },
      (error) => console.error(error)
    );
  }
// Filto de estado
  onChangeEstSearch(val: string = '') {   
    this._localidades = this.Localidades.filter(
      (x) => x.estado.nombre.toLowerCase().indexOf(val.toLowerCase()) !== -1
    );
  }

  selectEstEvent(item: any) {
    this._localidades = this.Localidades.filter(
      (x) => x.estado.nombre == item.nombre
    );   
  }
// Filtro de municipio
  onChangeMuniSearch(val: string = '') {   
    this._localidades = this.Localidades.filter(
      (x) => x.municipio.nombre.toLowerCase().indexOf(val.toLowerCase()) !== -1
    );
  }

  selectMuniEvent(item: any) {
    this._localidades = this.Localidades.filter(
      (x) => x.municipio.nombre == item.nombre
    );   
  }
// Filtro de Localidad
  onChangeSearch(val: string = '') {
    this._localidades = this.Localidades.filter(
      (x) => x.nombre.toLowerCase().indexOf(val.toLowerCase()) !== -1
    );
  }

  selectEvent(item: any) {
    this._localidades = this.Localidades.filter((x) => x.nombre == item.nombre);
  }

}
