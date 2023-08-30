import { Component, OnInit } from '@angular/core';
import { Supervision } from '../models/supervision';
import {
  AbstractControl,
  CheckboxControlValueAccessor,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { SupervisionService } from '../supervision.service';

@Component({
  selector: 'app-supervision-registro',
  templateUrl: './supervision-registro.component.html',
  styleUrls: ['./supervision-registro.component.css'],
})
export class SupervisionRegistroComponent implements OnInit {
  submitted = false;
  public supervisionForm: FormGroup;
  supervision: Supervision = {
    fechaMuestreo: new Date('2023/08/28'),
    horaInicio: '09:24',
    horaTermino: '10:14',
    horaTomaMuestra: '11:14',
    puntajeObtenido: '0',
    ocdlRealiza: 'Golfo Centro/Hidalgo',
    nombreSupervisor: 'Luis Eduardo Alfaro Sánchez',
    ocdlReporta: 'Golfo Centro/Hidalgo',
    claveSitio: 'OCLSP3823M1',
    claveMuestreo: 'OCLSP3827-210822',
    nombreSitio: 'PRESA SAN ONOFRE CENTRO',
    tipoCuerpoAgua: 'Costero (humedal)',
    laboratorio: 'ABC MATRIZ',
    latitudSitio: 232614,
    longitudSitio: 232614,
    latitudToma: 232614,
    longitudToma: 232614,
    nombreResponsableMuestra: 'VICTOR RAMÍREZ ÁNGULO',
    nombreResponsableMediciones: 'FELIX ALVARADO CRUZ',
    observacionesMuestreo: '',
    clasificaciones: [],
  };

  get f(): { [key: string]: AbstractControl } {
    return this.supervisionForm.controls;
  }

  constructor(private supervisionService: SupervisionService) {
    this.supervisionForm = new FormGroup({
      fechaMuestreo: new FormControl(
        this.supervision.fechaMuestreo?.toISOString().split('T')[0],
        Validators.required
      ),
      horaInicio: new FormControl(
        this.supervision.horaInicio,
        Validators.required
      ),
      horaFin: new FormControl(
        this.supervision.horaTermino,
        Validators.required
      ),
      horaTomaMuestra: new FormControl(
        this.supervision.horaTomaMuestra,
        Validators.required
      ),
      puntajeObtenido: new FormControl(
        this.supervision.puntajeObtenido,
        Validators.required
      ),
      ocdlRealiza: new FormControl(
        this.supervision.ocdlRealiza,
        Validators.required
      ),
      nombreSupervisor: new FormControl(
        this.supervision.nombreSupervisor,
        Validators.required
      ),
      ocdlReporta: new FormControl(
        this.supervision.ocdlReporta,
        Validators.required
      ),
      claveSitio: new FormControl(
        this.supervision.claveSitio,
        Validators.required
      ),
      claveMuestreo: new FormControl(
        this.supervision.claveMuestreo,
        Validators.required
      ),
      nombreSitio: new FormControl(
        this.supervision.nombreSitio,
        Validators.required
      ),
      tipoCuerpoAgua: new FormControl(
        this.supervision.tipoCuerpoAgua,
        Validators.required
      ),
      latitudSitio: new FormControl(
        this.supervision.latitudSitio,
        Validators.required
      ),
      longitudSitio: new FormControl(
        this.supervision.longitudSitio,
        Validators.required
      ),
      latitudToma: new FormControl(
        this.supervision.latitudToma,
        Validators.required
      ),
      longitudToma: new FormControl(
        this.supervision.longitudToma,
        Validators.required
      ),
      laboratorio: new FormControl(
        this.supervision.laboratorio,
        Validators.required
      ),
      nombreResponsableMuestra: new FormControl(
        this.supervision.nombreResponsableMuestra,
        Validators.required
      ),
      nombreResponsableMediciones: new FormControl(
        this.supervision.nombreResponsableMediciones,
        Validators.required
      ),
      observacionesMuestreo: new FormControl(
        this.supervision.observacionesMuestreo
      ),
    });

    this.supervisionService.data.subscribe((data) => {
      console.log(data);
    });

    this.supervisionService.updateData(2);
  }

  ngOnInit(): void {
    this.supervision.clasificaciones = [
      {
        id: 1,
        descripcion: 'PREVIO A LA TOMA DE MUESTRAS',
        criterios: [
          {
            obligatorio: false,
            id: 1,
            descripcion:
              'El sitio de muestreo es el correcto (verifican coordenadas y cumple con la base actualizada).*',
            puntaje: 2.5,
            cumplimiento: '',
            observacion: 'observación uno',
          },
        ],
      },
      {
        id: 2,
        descripcion:
          'VERIFICACIÓN DE EQUIPO Y MATERIAL PARA MEDICIONES DE CAMPO Y MUESTREO',
        criterios: [
          {
            obligatorio: true,
            id: 1,
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

    this.supervision.fechaMuestreo = this.supervisionForm.value.fechaMuestreo;
    this.supervision.horaInicio = this.supervisionForm.value.horaInicio;
    this.supervision.horaTermino = this.supervisionForm.value.horaFin;
    this.supervision.horaTomaMuestra =
      this.supervisionForm.value.horaTomaMuestra;
    this.supervision.puntajeObtenido =
      this.supervisionForm.value.puntajeObtenido;
    this.supervision.ocdlRealiza = this.supervisionForm.value.ocdlRealiza;
    this.supervision.nombreSupervisor =
      this.supervisionForm.value.nombreSupervisor;
    this.supervision.ocdlReporta = this.supervisionForm.value.ocdlReporta;
    this.supervision.claveSitio = this.supervisionForm.value.claveSitio;
    this.supervision.claveMuestreo = this.supervisionForm.value.claveMuestreo;
    this.supervision.nombreSitio = this.supervisionForm.value.nombreSitio;
    this.supervision.tipoCuerpoAgua = this.supervisionForm.value.tipoCuerpoAgua;
    this.supervision.laboratorio = this.supervisionForm.value.laboratorio;
    this.supervision.latitudSitio = this.supervisionForm.value.latitudSitio;
    this.supervision.longitudSitio = this.supervisionForm.value.longitudSitio;
    this.supervision.latitudToma = this.supervisionForm.value.latitudToma;
    this.supervision.longitudSitio = this.supervisionForm.value.longitudSitio;
    this.supervision.nombreResponsableMuestra =
      this.supervisionForm.value.nombreResponsableMuestra;
    this.supervision.nombreResponsableMediciones =
      this.supervisionForm.value.nombreResponsableMediciones;
    this.supervision.observacionesMuestreo =
      this.supervisionForm.value.observacionesMuestreo;
    console.log(this.supervision);

    this.supervisionService.postSupervision(this.supervision).subscribe({
      next: (response: any) => {
        console.log(response);
      },
      error: (error) => {},
    });
  }
}
