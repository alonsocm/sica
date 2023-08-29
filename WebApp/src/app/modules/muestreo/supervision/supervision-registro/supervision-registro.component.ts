import { Component, OnInit } from '@angular/core';
import { Supervision } from '../models/supervision';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-supervision-registro',
  templateUrl: './supervision-registro.component.html',
  styleUrls: ['./supervision-registro.component.css'],
})
export class SupervisionRegistroComponent implements OnInit {
  public supervisionForm: FormGroup;
  supervision: Supervision = {
    clasificaciones: [],
  };

  get f(): { [key: string]: AbstractControl } {
    return this.supervisionForm.controls;
  }
  submitted = false;
  constructor() {
    this.supervisionForm = new FormGroup({
      fechaMuestreo: new FormControl('', Validators.required),
      horaInicio: new FormControl('', Validators.required),
      horaFin: new FormControl('', Validators.required),
      horaTomaMuestra: new FormControl('', Validators.required),
      puntajeObtenido: new FormControl('', Validators.required),
      ocdlRealiza: new FormControl('', Validators.required),
      nombreSupervisor: new FormControl('', Validators.required),
      ocdlReporta: new FormControl('', Validators.required),
      claveSitio: new FormControl('', Validators.required),
      claveMuestreo: new FormControl('', Validators.required),
      nombreSitio: new FormControl('', Validators.required),
      tipoCuerpoAgua: new FormControl('', Validators.required),
      latitudSitio: new FormControl('', Validators.required),
      longitudSitio: new FormControl('', Validators.required),
      latitudToma: new FormControl('', Validators.required),
      longitudToma: new FormControl('', Validators.required),
      laboratorio: new FormControl('', Validators.required),
      nombreResponsableMuestra: new FormControl('', Validators.required),
      nombreResponsableMediciones: new FormControl('', Validators.required),
      observacionesMuestreo: new FormControl(''),
    });
  }

  ngOnInit(): void {
    this.supervision.clasificaciones = [
      {
        numero: 1,
        descripcion: 'PREVIO A LA TOMA DE MUESTRAS',
        criterios: [
          {
            critico: false,
            numero: 1,
            descripcion:
              'El sitio de muestreo es el correcto (verifican coordenadas y cumple con la base actualizada).*',
            puntaje: 2.5,
            cumplimiento: '',
            observacion: 'observación uno',
          },
        ],
      },
      {
        numero: 2,
        descripcion:
          'VERIFICACIÓN DE EQUIPO Y MATERIAL PARA MEDICIONES DE CAMPO Y MUESTREO',
        criterios: [
          {
            critico: true,
            numero: 1,
            descripcion:
              'Cuenta con termómetro o termopar para la medición de temperatura del agua con resolución de  0.1°C . *',
            puntaje: 2.5,
            cumplimiento: '',
            observacion: '',
          },
        ],
      },
    ];
  }

  onFileChange(event: any) {
    this.supervision.archivoPdfSupervision = event.target.files[0];
    console.log(this.supervision.archivoPdfSupervision);
  }

  onFileChangeEvidencias(event: any) {
    this.supervision.archivosEvidencias = event.target.files;
  }

  guardar() {
    this.submitted = true;
    if (this.supervisionForm.invalid) {
      return;
    }
  }
}
