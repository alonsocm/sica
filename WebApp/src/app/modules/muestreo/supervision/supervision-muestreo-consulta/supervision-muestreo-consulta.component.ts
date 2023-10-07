import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Filter } from 'src/app/interfaces/filtro.interface';
import { BaseService } from 'src/app/shared/services/base.service';
import { SupervisionService } from '../supervision.service';
import { OrganismoDireccion } from '../models/organismo-direccion';
import { Laboratorio } from '../models/laboratorio';
import { TipoCuerpoAgua } from '../models/tipo-cuerpo-agua';
import { SupervisionBase } from '../models/supervision-base';
import { FileService } from 'src/app/shared/services/file.service';
import { TipoMensaje } from 'src/app/shared/enums/tipoMensaje';
import { Sitio } from '../models/sitio';
import { SupervisionBusqueda } from '../models/supervision-busqueda';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { NotificationType } from 'src/app/shared/enums/notification-type';

@Component({
  selector: 'app-supervision-muestreo-consulta',
  templateUrl: './supervision-muestreo-consulta.component.html',
  styleUrls: ['./supervision-muestreo-consulta.component.css'],
})
export class SupervisionMuestreoConsultaComponent
  extends BaseService
  implements OnInit
{
  organismosDirecciones: Array<OrganismoDireccion> = [];
  laboratorios: Array<Laboratorio> = [];
  tiposCuerpoAgua: Array<TipoCuerpoAgua> = [];
  supervisiones: Array<SupervisionBase> = [];
  sitios: Array<Sitio> = [];
  sitioId: number = 0;
  supervision: number = 0;
  supervisionBusqueda: SupervisionBusqueda = {};
  puntajes: Array<{ id: number; descripcion: string }> = [];
  clavesMuestreo: Array<string> = [];

  constructor(
    private router: Router,
    private supervisionService: SupervisionService,
    private notificationService: NotificationService
  ) {
    super();
  }

  supervisionBusquedaForm = new FormGroup({
    ocdlRealiza: new FormControl(0),
    sitio: new FormControl(0),
    fechaMuestreo: new FormControl(''),
    fechaMuestreoFin: new FormControl(''),
    puntaje: new FormControl(0),
    laboratorio: new FormControl(0),
    claveMuestreo: new FormControl(''),
    tipoCuerpoAgua: new FormControl(0),
  });

  valoresInicialesForm = this.supervisionBusquedaForm.value;

  ngOnInit(): void {
    this.definirColumnas();
    this.getOrganismosDirecciones();
    this.getTiposCuerpoAgua();
    this.getSupervisiones();
    this.getLaboratorios();
    this.getPuntajes();
  }

  getFormValues() {
    this.supervisionBusqueda = {
      organismosDireccionesRealizaId:
        this.supervisionBusquedaForm.value.ocdlRealiza ?? 0,
      sitioId: this.sitioId ?? 0,
      fechaMuestreo: this.supervisionBusquedaForm.value.fechaMuestreo ?? '',
      fechaMuestreoFin:
        this.supervisionBusquedaForm.value.fechaMuestreoFin ?? '',
      puntajeObtenido: this.supervisionBusquedaForm.value.puntaje ?? 0,
      laboratorioRealizaId: this.supervisionBusquedaForm.value.laboratorio ?? 0,
      claveMuestreo: this.supervisionBusquedaForm.value.claveMuestreo ?? '',
      tipoCuerpoAguaId: this.supervisionBusquedaForm.value.tipoCuerpoAgua ?? 0,
    };
  }

  getSupervisiones() {
    this.getFormValues();
    this.supervisionService
      .getSupervisiones(this.supervisionBusqueda)
      .subscribe({
        next: (response: any) => {
          this.supervisiones = response.data;
        },
        error: (error) => {},
      });
  }

  getPuntajes() {
    this.supervisionService.getIntervalosPuntaje().subscribe({
      next: (response: any) => {
        this.puntajes = response.data;
      },
      error: (error) => {},
    });
  }

  onLimpiarClick() {
    this.sitios = [];
    this.clavesMuestreo = [];
    this.sitioId = 0;
    this.supervisionBusquedaForm.reset(this.valoresInicialesForm);
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

  onOrganismosDireccionesChange() {
    let organismoDireccionId = this.supervisionBusquedaForm.value.ocdlRealiza;
    this.sitios = [];
    this.supervisionBusquedaForm.patchValue({ sitio: 0 });
    this.sitioId = 0;
    this.clavesMuestreo = [];
    this.supervisionBusquedaForm.patchValue({ claveMuestreo: '' });

    if (
      organismoDireccionId !== 0 ||
      organismoDireccionId !== null ||
      organismoDireccionId !== undefined
    ) {
      this.getSitios(organismoDireccionId ?? 0);
    }
  }

  onClaveSitioChange() {
    let claveSitio = this.supervisionBusquedaForm.value.sitio;
    this.sitioId = 0;
    this.clavesMuestreo = [];
    this.supervisionBusquedaForm.patchValue({ claveMuestreo: '' });

    if (claveSitio != 0) {
      this.getSitio(String(claveSitio));
    }
  }

  getSitios(cuencaDireccion: number) {
    this.supervisionService.getClavesSitios(cuencaDireccion).subscribe({
      next: (response: any) => {
        if (response.data.length > 0) {
          this.sitios = response.data.map(
            (val: any) =>
              <Sitio>{
                claveSitio: val,
                // nombre: val.nombreSitio,
                // sitioId: val.id,
              }
          );
        }
      },
      error: (error) => {},
    });
  }

  getSitio(claveSitio: string) {
    this.supervisionService.getSitio(claveSitio).subscribe({
      next: (response: any) => {
        this.clavesMuestreo = response.data.clavesMuestreo;
        this.sitioId = response.data.sitioId;
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

  getTiposCuerpoAgua() {
    this.supervisionService.getTiposCuerpoAgua().subscribe({
      next: (response: any) => {
        this.tiposCuerpoAgua = response.data;
      },
      error: (error) => {},
    });
  }

  onBuscarClick() {
    this.getSupervisiones();
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
    this.router.navigate(['/supervision-muestreo']);
  }

  onEditClick(supervision: number) {
    this.supervisionService.updateSupervisionId(supervision);
    this.router.navigate(['/supervision-muestreo']);
  }

  onViewClick(supervision: number) {
    this.supervisionService.updateSupervisionId(supervision);
    this.supervisionService.updateEsConsulta(true);
    this.router.navigate(['/supervision-muestreo']);
  }

  onDeleteClick(supervision: number) {
    this.supervision = supervision;
    document.getElementById('btn-confirm-modal')?.click();
  }

  onConfirmDeleteFileClick() {
    if (this.supervision != 0 && this.supervision != null) {
      this.supervisionService.deleteSupervision(this.supervision).subscribe({
        next: (response: any) => {
          if (response.succeded) {
            let index =
              this.supervisiones?.findIndex((x) => x.id === this.supervision) ??
              -1;
            if (index > -1) {
              this.supervisiones?.splice(index, 1);
            }
            this.notificationService.updateNotification({
              show: true,
              text: 'Registro eliminado correctamente',
              type: NotificationType.success,
            });
          }
        },
        error: (error) => {
          this.mostrarMensaje(
            'Error al eliminar el registro.',
            TipoMensaje.Error
          );
        },
      });
    }
  }
}
