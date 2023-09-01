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
import { OrganismoDireccion } from '../models/organismo-direccion';
import { Cuenca } from '../models/cuenca';
import { Muestreador } from '../models/muestreador';
import { Laboratorio } from '../models/laboratorio';
import { Sitio } from '../models/sitio';

@Component({
  selector: 'app-supervision-registro',
  templateUrl: './supervision-registro.component.html',
  styleUrls: ['./supervision-registro.component.css'],
})
export class SupervisionRegistroComponent implements OnInit {
  organismosDirecciones: Array<OrganismoDireccion> = [];
  clavesSitios: Array<string> = [];
  sitio: Sitio = {};
  cuencas: Array<Cuenca> = [];
  laboratorios: Array<Laboratorio> = [];
  muestreadoresLaboratorio: Array<Muestreador> = [];
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
    claveSitio: '0',
    claveMuestreo: 'OCLSP3827-210822',
    nombreSitio: 'PRESA SAN ONOFRE CENTRO',
    // tipoCuerpoAgua: '0',
    laboratorio: '0',
    // latitudSitio: 0,
    // longitudSitio: 0,
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
        { value: this.supervision.puntajeObtenido, disabled: true },
        Validators.required
      ),
      ocdlRealiza: new FormControl(0, [Validators.required, Validators.min(1)]),
      nombreSupervisor: new FormControl(
        this.supervision.nombreSupervisor,
        Validators.required
      ),
      ocdlReporta: new FormControl(0, [Validators.required, Validators.min(1)]),
      claveSitio: new FormControl(this.supervision.claveSitio, [
        Validators.required,
        Validators.min(1),
      ]),
      claveMuestreo: new FormControl(
        this.supervision.claveMuestreo,
        Validators.required
      ),
      nombreSitio: new FormControl(
        { value: this.sitio.nombre, disabled: true },
        Validators.required
      ),
      tipoCuerpoAgua: new FormControl(
        { value: this.supervision.tipoCuerpoAgua ?? '', disabled: true },
        Validators.required
      ),
      latitudSitio: new FormControl(
        { value: this.supervision.latitudSitio ?? '', disabled: true },
        Validators.required
      ),
      longitudSitio: new FormControl(
        { value: this.supervision.longitudSitio ?? '', disabled: true },
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
      laboratorio: new FormControl(this.supervision.laboratorio, [
        Validators.required,
        Validators.min(1),
      ]),
      nombreResponsableMuestra: new FormControl(0, [
        Validators.required,
        Validators.min(1),
      ]),
      nombreResponsableMediciones: new FormControl(0, [
        Validators.required,
        Validators.min(1),
      ]),
      observacionesMuestreo: new FormControl(
        this.supervision.observacionesMuestreo
      ),
    });

    this.supervisionService.data.subscribe((data) => {});

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
    this.getOrganismosDirecciones();
    this.getCuencas();
    this.getLaboratorios();
  }

  getOrganismosDirecciones() {
    this.supervisionService.getOCDL().subscribe({
      next: (response: any) => {
        this.organismosDirecciones = response;
      },
      error: (error) => {},
    });
  }

  getCuencas() {
    this.supervisionService.getCuencas().subscribe({
      next: (response: any) => {
        this.cuencas = response.data;
      },
      error: (error) => {},
    });
  }

  getClavesSitios(organismoDireccion: number) {
    this.supervisionService.getClavesSitios(organismoDireccion).subscribe({
      next: (response: any) => {
        this.clavesSitios = response.data;
      },
      error: (error) => {},
    });
  }

  onClaveSitioChange() {
    let claveSitio = this.supervisionForm.value.claveSitio;
    if (claveSitio != '0') {
      this.getSitio(claveSitio);
    } else {
      this.sitio.nombre = '';
      this.sitio.tipoCuerpoAgua = '';
      this.sitio.latitud = '';
      this.sitio.longitud = '';
    }
  }

  getSitio(claveSitio: string) {
    this.supervisionService.getSitio(claveSitio).subscribe({
      next: (response: any) => {
        this.sitio = response.data;
      },
      error: (error) => {},
    });
  }

  getLaboratorios() {
    this.supervisionService.getLaboratorios().subscribe({
      next: (response: any) => {
        this.laboratorios = response.data;
      },
      error: (error) => {},
    });
  }

  getMuestreadoresLaboratorio(laboratorio: number) {
    this.supervisionService.getMuestreadoresLaboratorio(laboratorio).subscribe({
      next: (response: any) => {
        this.muestreadoresLaboratorio = response;
      },
      error: (error) => {},
    });
  }

  onLaboratorioChange() {
    this.supervisionForm.patchValue({
      nombreResponsableMuestra: 0,
      nombreResponsableMediciones: 0,
    });

    this.getMuestreadoresLaboratorio(this.supervisionForm.value.laboratorio);
  }

  onOrganismosDireccionesChange() {
    this.supervisionForm.patchValue({ claveSitio: '0' });
    let organismoDireccionId = this.supervisionForm.value.ocdlRealiza;
    this.getClavesSitios(organismoDireccionId);
  }

  onFileChange(event: any) {
    this.supervision.archivoPdfSupervision = event.target.files[0];
  }

  onFileChangeEvidencias(event: any) {
    this.supervision.archivosEvidencias = event.target.files;

    this.uploadArcchivosSupervision();
  }

  uploadArcchivosSupervision() {
    this.supervisionService
      .postArchivosSupervision(
        this.supervision.archivoPdfSupervision,
        this.supervision.archivosEvidencias ?? []
      )
      .subscribe({
        next: (response: any) => {
          this.laboratorios = response.data;
        },
        error: (error) => {},
      });
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
      next: (response: any) => {},
      error: (error) => {},
    });
  }
}
