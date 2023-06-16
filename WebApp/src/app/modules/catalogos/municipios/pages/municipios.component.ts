import { Component, Input, NgModule, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Respuesta } from '../../../../interfaces/respuesta.interface';
import { Municipios } from '../../../../interfaces/catalogos/municipio.interface';
import { Router, Data } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MunicipiosService } from '../services/municipios.service';
import { Estados } from '../../../../interfaces/catalogos/estado.interface';

@Component({
  selector: 'app-municipios',
  templateUrl: './municipios.component.html',
  styleUrls: ['./municipios.component.css'],
})
export class MunicipiosComponent implements OnInit {
  public page: number | undefined;
  registroMunicipio: FormGroup;
  public keyword = 'nombre';
  errorMessage: any;

  @Input() respuesta: Respuesta = {
    succeded: false,
    message: '',
    errors: '',
    data: [],
  };
  public Municipios!: Municipios;
  public municipio: any[] = [];
  public _municipio: any[] = [];
  public _Filtrado: any[] = [];
  public municipioId: number = 0;

  constructor(
    private http: HttpClient,
    private router: Router,
    private fb: FormBuilder,
    private municipiosService: MunicipiosService
  ) {
    this.registroMunicipio = this.fb.group({
      estadoId: ['', Validators.required],
      nombre: ['', Validators.required],
    });
  }
  ngOnInit(): void {
    this.municipiosService.getAllMunicipio().subscribe(
      (result) => {
        this.municipio = result.data;
        this._municipio = result.data;
      },
      (error) => console.error(error)
    );
  }
  // filtro estado
  onChangeEstSearch(val: string = '') {
    this._municipio = this.municipio.filter(
      (x) => x.estado.nombre.toLowerCase().indexOf(val.toLowerCase()) !== -1
    );
    this._Filtrado = this._municipio;
    console.log( document.getElementById('autoEstado')?.textContent)
    // this._municipio = this.municipio.filter(
    //     (x) => x.estado.nombre.toLowerCase().indexOf(val.toLowerCase()) !== -1
    //   );
  }

  selectEstEvent(item: any) {
    this._municipio = this.municipio.filter(
      (x) => x.estado.nombre == item.nombre
    );
  }

  // filtro municipio
  onChangeSearch(val: string = '') {
    this._municipio = this._Filtrado.filter(
      (x) => x.nombre.toLowerCase().indexOf(val.toLowerCase()) !== -1
    );
  }

  selectEvent(item: any) {
    this._municipio = this.municipio.filter((x) => x.nombre == item.nombre);
  }
}
