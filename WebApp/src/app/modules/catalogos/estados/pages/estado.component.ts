import { Component, Input, NgModule, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Respuesta } from '../../../../interfaces/respuesta.interface';
import { Estados } from '../../../../interfaces/catalogos/estado.interface';
import { Router, Data } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EstadosService } from '../services/estados.service';

@Component({
  selector: 'app-estado',
  templateUrl: './estado.component.html',
  styleUrls: ['./estado.component.css'],
})
export class EstadoComponent implements OnInit {
  public page: number | undefined;
  registroEstado: FormGroup;
  errorMessage: any;
  public keyword = 'nombre';
  modalTitle: string = '';
  editar: boolean = false;

  @Input() respuesta: Respuesta = {
    succeded: false,
    message: '',
    errors: '',
    data: [],
  };

  public Estado!: Estados;
  public Estados: any[] = [];
  public _estados: any[] = [];
  public EstadoId: number = 0;

  constructor(
    private http: HttpClient,
    private router: Router,
    private fb: FormBuilder,
    private EstadoService: EstadosService
  ) {
    this.registroEstado = this.fb.group({
      nombre: ['', Validators.required],
      abreviatura: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.EstadoService.getAllEstado().subscribe(
      (result) => {      
        this.Estados = result.data;
        this._estados = result.data;
      },
      (error) => console.error(error)
    );
  }

  // AddEstado() {}

  // EditEstado(estado: any) {}

  // DeleteEstado(estado: any) {}

  onChangeSearch(val: string = '') {
    this._estados = this.Estados.filter(
      (x) => x.nombre.toLowerCase().indexOf(val.toLowerCase()) !== -1
    );
  }
  selectEvent(item: any) {
    this._estados = this.Estados.filter((x) => x.nombre == item.nombre);
  }

  onChangeSearchAbre(val: string = '') {
    this._estados = this.Estados.filter(
      (x) => x.abreviatura.toLowerCase().indexOf(val.toLowerCase()) !== -1
    );
  }
  selectEventAbre(item: any) {
    this._estados = this.Estados.filter((x) => x.abreviatura == item.abreviatura);
  }
}
