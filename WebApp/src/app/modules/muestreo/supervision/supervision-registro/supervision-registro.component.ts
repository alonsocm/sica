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
import { BaseService } from 'src/app/shared/services/base.service';
import { TipoMensaje } from 'src/app/shared/enums/tipoMensaje';
import { Router } from '@angular/router';
import { FileService } from 'src/app/shared/services/file.service';

@Component({
  selector: 'app-supervision-registro',
  templateUrl: './supervision-registro.component.html',
  styleUrls: ['./supervision-registro.component.css'],
})
export class SupervisionRegistroComponent
  extends BaseService
  implements OnInit
{
  supervisionId: number = 0;
  organismosDirecciones: Array<OrganismoDireccion> = [];
  clavesSitios: Array<string> = [];
  sitio: Sitio = {};
  cuencas: Array<Cuenca> = [];
  laboratorios: Array<Laboratorio> = [];
  muestreadoresLaboratorio: Array<Muestreador> = [];
  submitted = false;
  public supervisionForm: FormGroup;
  supervision: Supervision = {};
  imgSrc: string = '';
  nombreArchivo: string = '';
  esConsulta: boolean = false;
  excepcionesCriterios: Array<number> = [11, 17, 60, 43];

  get f(): { [key: string]: AbstractControl } {
    return this.supervisionForm.controls;
  }

  constructor(
    private router: Router,
    private supervisionService: SupervisionService
  ) {
    super();

    this.supervisionService.supervisionId.subscribe((data) => {
      this.supervisionId = data;
    });

    this.supervisionService.esConsulta.subscribe((data) => {
      this.esConsulta = data;
    });

    if (this.supervisionId !== 0) {
      this.supervisionService.getSupervision(this.supervisionId).subscribe({
        next: (response: any) => {
          this.supervision = response.data;
          this.setSupervisionFormValues(this.supervision);
        },
        error: (error) => {},
      });
    }

    this.supervisionForm = new FormGroup(
      {
        fechaMuestreo: new FormControl(
          this.supervision.fechaMuestreo?.toISOString().split('T')[0] ?? '',
          Validators.required
        ),
        horaInicio: new FormControl(
          this.supervision.horaInicio ?? '',
          Validators.required
        ),
        horaFin: new FormControl(
          this.supervision.horaTermino ?? '',
          Validators.required
        ),
        horaTomaMuestra: new FormControl(
          this.supervision.horaTomaMuestra ?? '',
          Validators.required
        ),
        puntajeObtenido: new FormControl(
          { value: this.supervision.puntajeObtenido ?? '', disabled: true },
          Validators.required
        ),
        ocdlRealiza: new FormControl(
          this.supervision.organismosDireccionesRealizaId ?? 0,
          [Validators.required, Validators.min(1)]
        ),
        nombreSupervisor: new FormControl(
          this.supervision.supervisorConagua ?? '',
          Validators.required
        ),
        ocdlReporta: new FormControl(
          {
            value: this.supervision.organismoCuencaReporta ?? '',
            disabled: true,
          },
          [Validators.required, Validators.min(1)]
        ),
        claveSitio: new FormControl(this.supervision.claveSitio ?? 0, [
          Validators.required,
          Validators.min(1),
        ]),
        claveMuestreo: new FormControl(
          this.supervision.claveMuestreo ?? '',
          Validators.required
        ),
        nombreSitio: new FormControl(
          { value: this.supervision.nombreSitio ?? '', disabled: true },
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
          this.supervision.latitudToma ?? '',
          Validators.required
        ),
        longitudToma: new FormControl(
          this.supervision.longitudToma ?? '',
          Validators.required
        ),
        laboratorio: new FormControl(
          this.supervision.laboratorioRealizaId ?? 0,
          [Validators.required, Validators.min(1)]
        ),
        nombreResponsableMuestra: new FormControl(
          this.supervision.responsableTomaId ?? 0,
          [Validators.required, Validators.min(1)]
        ),
        nombreResponsableMediciones: new FormControl(
          this.supervision.responsableMedicionesId ?? 0,
          [Validators.required, Validators.min(1)]
        ),
        observacionesMuestreo: new FormControl(
          this.supervision.observacionesMuestreo ?? ''
        ),
      },
      {
        validators: [this.validarHoraTomaMuestra, this.validarHoraFin],
      }
    );
  }

  validarHoraTomaMuestra(control: AbstractControl) {
    const { horaInicio, horaFin, horaTomaMuestra } = control.value; // Extraemos valores de ambos campos necesarios

    if (horaTomaMuestra >= horaInicio && horaTomaMuestra <= horaFin) {
      return null; // Validación correcta, devolvemos null
    }

    return { horaTomaMuestra: true };
  }

  validarHoraFin(control: AbstractControl) {
    const { horaInicio, horaFin } = control.value; // Extraemos valores de ambos campos necesarios

    if (horaInicio < horaFin) {
      return null; // Validación correcta, devolvemos null
    }

    return { horaFin: true };
  }

  ngOnInit(): void {
    if (this.supervisionId === 0) {
      this.getClasificacionesCriterios();
    }
    this.getOrganismosDirecciones();
    this.getCuencas();
    this.getLaboratorios();
  }

  ngOnDestroy() {
    this.supervisionService.updateSupervisionId(0);
    this.supervisionService.updateEsConsulta(false);
  }

  getSupervision(id: number) {
    this.supervisionService.getSupervision(id).subscribe({
      next: (response: any) => {
        this.supervision = response.data;
      },
      error: (error) => {},
    });
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
      this.getSitio(claveSitio, true);
    } else {
      this.sitio.nombre = '';
      this.sitio.tipoCuerpoAgua = '';
      this.sitio.latitud = '';
      this.sitio.longitud = '';
      this.supervisionForm.patchValue({ claveMuestreo: '' });
    }
  }

  getSitio(claveSitio: string, reemplazarClaveMuestreo: boolean = false) {
    this.supervisionService.getSitio(claveSitio).subscribe({
      next: (response: any) => {
        this.sitio = response.data;

        if (reemplazarClaveMuestreo) {
          this.supervisionForm.patchValue({
            claveMuestreo: this.sitio.claveSitio + '-',
          });
        }
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

  getClasificacionesCriterios() {
    this.supervisionService.getClasificacionesCriterios().subscribe({
      next: (response: any) => {
        this.supervision.clasificaciones = response.data;
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
    let organismoDireccionId = this.supervisionForm.value.ocdlRealiza;
    let nombreOrganismoDireccion = '';
    this.supervisionForm.patchValue({ claveSitio: '0', claveMuestreo: '' });
    this.getClavesSitios(organismoDireccionId);

    if (organismoDireccionId !== '0') {
      let oc = this.getOrganismoCuencaId(organismoDireccionId);
      nombreOrganismoDireccion = oc.nombreOrganismoCuenca;
      this.supervision.organismoCuencaReportaId = oc.organismoCuencaId;
    }

    this.supervisionForm.patchValue({
      ocdlReporta: nombreOrganismoDireccion,
    });
  }

  getOrganismoCuencaId(organismoCuencaDireccionLocal: number) {
    let oc = this.organismosDirecciones.filter(
      (x) => x.id == organismoCuencaDireccionLocal
    );

    return oc[0];
  }

  onCumplimientoChange() {
    this.supervision.puntajeObtenido = this.supervision.clasificaciones?.reduce(
      (accumulator, currentValue) => {
        return (
          accumulator +
          currentValue.criterios
            .filter(
              (x) =>
                x.cumplimiento === 'CUMPLE' ||
                (x.cumplimiento === 'NOAPLICA' && !x.obligatorio) ||
                (x.cumplimiento === 'NOAPLICA' &&
                  this.excepcionesCriterios.includes(x.id))
            )
            .reduce((acc, val) => acc + val.puntaje, 0)
        );
      },
      0
    );
  }

  validateCriteriosObligatorios() {
    const sum = this.supervision.clasificaciones?.reduce(
      (accumulator, currentValue) => {
        return (
          accumulator +
          currentValue.criterios
            .filter(
              (x) =>
                x.obligatorio &&
                x.cumplimiento === 'NOCUMPLE' &&
                (x.observacion === null || x.observacion === '')
            )
            .reduce((acc, val) => acc + 1, 0)
        );
      },
      0
    );

    return sum ?? 0 > 0;
  }

  onFileChange(event: any) {
    this.supervision.archivoPdfSupervision = event.target.files[0];
  }

  onFileChangeEvidencias(event: any) {
    this.supervision.archivosEvidencias = event.target.files;
  }

  uploadArchivosSupervision() {
    if (
      this.supervision.archivoPdfSupervision != null ||
      (this.supervision.archivosEvidencias != null &&
        this.supervision.id != 0 &&
        this.supervision.claveMuestreo !== null)
    ) {
      this.supervisionService
        .postArchivosSupervision(
          this.supervision.id ?? 0,
          this.supervision.archivoPdfSupervision,
          this.supervision.archivosEvidencias ?? [],
          this.supervision.claveMuestreo ?? ''
        )
        .subscribe({
          next: (response: any) => {
            this.supervisionService.updateSupervisionId(0);
            this.router.navigate(['/supervision-muestreo']);
            this.mostrarMensaje(
              'Supervisión de muestreo registrada correctamente',
              TipoMensaje.Correcto
            );
          },
          error: (error) => {
            console.log(error);
            this.hacerScroll();
            this.mostrarMensaje(
              'Error al guardar los archivos de supervisión de muestreo',
              TipoMensaje.Error
            );
          },
        });
    }
  }

  onDeleteArchivoClick(nombreArchivo: string) {
    this.nombreArchivo = nombreArchivo;
    document.getElementById('btn-confirm-modal')?.click();
  }

  onConfirmDeleteFileClick() {
    if (this.supervisionId != 0 && this.supervisionId != null) {
      this.supervisionService
        .deleteArchivo(this.supervisionId, this.nombreArchivo)
        .subscribe({
          next: (response: any) => {
            let index =
              this.supervision.archivos?.findIndex(
                (x) => x.nombreArchivo === this.nombreArchivo
              ) ?? -1;
            if (index > -1) {
              this.supervision.archivos?.splice(index, 1);
            }
          },
          error: (error) => {},
        });
    }
  }

  onPreviewDownloadArchivoClick(
    nombreArchivo: string,
    tipoArchivo: number,
    descargar: boolean = false
  ) {
    if (this.supervisionId != 0 && this.supervisionId != null) {
      this.supervisionService
        .getArchivo(this.supervisionId, nombreArchivo)
        .subscribe((data: Blob) => {
          if (descargar) {
            FileService.download(data, nombreArchivo);
          } else if (!(tipoArchivo === 10)) {
            const reader = new FileReader();
            reader.onloadend = () => {
              this.imgSrc = reader.result as string;
            };
            reader.readAsDataURL(data);
            document.getElementById('btn-img-modal')?.click();
          } else {
            const url = URL.createObjectURL(data);
            window.open(url);
          }
        });
    }
  }

  onSubmit() {
    this.submitted = true;

    if (
      this.supervisionForm.invalid ||
      !this.existeClaveMuestreo(this.supervisionForm.value.claveMuestreo)
    ) {
      this.hacerScroll();
      return;
    } else if (this.validateCriteriosObligatorios()) {
      this.mostrarMensaje(
        'Se encontraron criterios obligatorios marcados como "NO CUMPLE". Es necesario capturar las observaciones.',
        TipoMensaje.Alerta
      );
      this.hacerScroll();
      return;
    } else if (this.validateArchivoSupervision()) {
      return;
    }

    this.setSupervisionMuestreoValues();

    this.supervisionService.postSupervision(this.supervision).subscribe({
      next: (response: any) => {
        if (response.succeded) {
          this.supervision.id = response.data.supervisionMuestreoId;
          this.hacerScroll();
          this.mostrarMensaje(
            'Supervisión de muestreo guardada correctamente.',
            TipoMensaje.Correcto
          );
          if (
            this.supervision.archivoPdfSupervision != null ||
            (this.supervision.archivosEvidencias != null &&
              this.supervision.id != 0 &&
              this.supervision.claveMuestreo !== null)
          ) {
            this.uploadArchivosSupervision();
          } else {
            this.supervisionService.updateSupervisionId(0);
            this.router.navigate(['/supervision-muestreo']);
          }
        }
      },
      error: (error) => {
        this.hacerScroll();
        this.mostrarMensaje(
          'Error al guardar supervisión de muestreo',
          TipoMensaje.Error
        );
      },
    });
  }

  validateArchivoSupervision() {
    return (
      this.supervision.archivoPdfSupervision === undefined &&
      this.supervision.archivos === undefined
    );
  }

  setSupervisionFormValues(supervision: Supervision) {
    this.getClavesSitios(Number(supervision.organismosDireccionesRealizaId));
    this.getMuestreadoresLaboratorio(Number(supervision.laboratorioRealizaId));
    this.getSitio(supervision.claveSitio ?? '');

    this.supervisionForm.patchValue({
      fechaMuestreo: supervision.fechaMuestreo,
      horaInicio: supervision.horaInicio,
      horaFin: supervision.horaTermino,
      horaTomaMuestra: supervision.horaTomaMuestra,
      puntajeObtenido: supervision.puntajeObtenido,
      ocdlRealiza: supervision.organismosDireccionesRealizaId,
      nombreSupervisor: supervision.supervisorConagua,
      ocdlReporta: supervision.organismoCuencaReportaId,
      claveSitio: supervision.claveSitio,
      claveMuestreo: supervision.claveMuestreo,
      nombreSitio: supervision.nombreSitio,
      tipoCuerpoAgua: supervision.tipoCuerpoAgua,
      latitudSitio: supervision.latitudSitio,
      longitudSitio: supervision.longitudSitio,
      latitudToma: supervision.latitudToma,
      longitudToma: supervision.longitudToma,
      laboratorio: supervision.laboratorioRealizaId,
      nombreResponsableMuestra: supervision.responsableTomaId,
      nombreResponsableMediciones: supervision.responsableMedicionesId,
      observacionesMuestreo: supervision.observacionesMuestreo,
    });
  }

  setSupervisionMuestreoValues() {
    this.supervision.id = this.supervisionId;
    this.supervision.fechaMuestreo = this.supervisionForm.value.fechaMuestreo;
    this.supervision.horaInicio = this.supervisionForm.value.horaInicio;
    this.supervision.horaTermino = this.supervisionForm.value.horaFin;
    this.supervision.horaTomaMuestra =
      this.supervisionForm.value.horaTomaMuestra;
    this.supervision.organismosDireccionesRealizaId =
      this.supervisionForm.value.ocdlRealiza;
    this.supervision.supervisorConagua =
      this.supervisionForm.value.nombreSupervisor;
    // this.supervision.organismoCuencaReportaId =
    //   this.supervisionForm.value.ocdlReporta;
    this.supervision.claveSitio = this.supervisionForm.value.claveSitio;
    this.supervision.sitioId = this.sitio.sitioId;
    this.supervision.claveMuestreo = String(
      this.supervisionForm.value.claveMuestreo
    );
    this.supervision.laboratorioRealizaId =
      this.supervisionForm.value.laboratorio;
    this.supervision.latitudToma = this.supervisionForm.value.latitudToma;
    this.supervision.longitudToma = this.supervisionForm.value.longitudToma;
    this.supervision.responsableTomaId =
      this.supervisionForm.value.nombreResponsableMuestra;
    this.supervision.responsableMedicionesId =
      this.supervisionForm.value.nombreResponsableMediciones;
    this.supervision.observacionesMuestreo =
      this.supervisionForm.value.observacionesMuestreo;
  }

  onCancelarClick() {
    this.supervisionService.updateSupervisionId(0);
    this.router.navigate(['/supervision-muestreo']);
  }

  existeClaveMuestreo(claveMuestreo: string) {
    return this.sitio.clavesMuestreo?.includes(String(claveMuestreo));
  }
}
