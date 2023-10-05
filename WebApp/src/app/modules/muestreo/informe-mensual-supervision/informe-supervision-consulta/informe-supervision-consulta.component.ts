import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseService } from 'src/app/shared/services/base.service';
import { InformeMensualSupervisionRegistro } from '../models/informe-mensual-supervision-registro';
import { Router } from '@angular/router';
import { InformeSupervisionService } from '../informe-supervision.service';
import { TipoMensaje } from 'src/app/shared/enums/tipoMensaje';
import { DirectorResponsable } from '../../supervision/models/director-responsable';
import { NotificationService } from 'src/app/shared/services/notification.service';

@Component({
  selector: 'app-informe-supervision-consulta',
  templateUrl: './informe-supervision-consulta.component.html',
  styleUrls: ['./informe-supervision-consulta.component.css'],
})
export class InformeSupervisionConsultaComponent
  extends BaseService
  implements OnInit
{
  formBusqueda: FormGroup;
  registrosInformeMensual: Array<InformeMensualSupervisionRegistro> = [];
  lugares: Array<string> = [];
  memorandos: Array<string> = [];
  informeId: number = 0;
  archivoInforme: any;
  valoresInicialesForm: any;
  directoresResponsables: Array<DirectorResponsable> = [];

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private informeSupervisionService: InformeSupervisionService,
    private notificacionService: NotificationService
  ) {
    super();
    this.informeSupervisionService.mensaje.subscribe((data) => {
      if (data.mostrar) {
        this.mostrarMensaje(data.mensaje, data.tipoMensaje);
      }
    });

    this.formBusqueda = this.formBuilder.group({
      memorando: [''],
      lugar: [''],
      fechaRegistro: [''],
      fechaRegistroFin: [''],
      contrato: [''],
      responsable: [''],
      puesto: [''],
      copia: [''],
      inicialesPersonas: [''],
      mes: [0],
    });

    this.valoresInicialesForm = this.formBusqueda.value;
  }

  ngOnInit(): void {
    this.getMemorandosInformeSupervision();
    this.getLugaresInformeSupervision();
    this.getDirectoresResponsables();
    this.getInformes();
  }

  getInformes() {
    let criteriosBusqueda = this.getFormValues();
    this.informeSupervisionService.getInformes(criteriosBusqueda).subscribe({
      next: (response: any) => {
        this.registrosInformeMensual = response.data;
      },
      error: (error) => {},
    });
  }

  onSubmit() {
    this.getInformes();
  }

  onRegistrarInformeSupervisionClick() {
    this.router.navigate(['/informe-mensual-supervision']);
  }

  onLimpiarClick() {
    this.formBusqueda.reset(this.valoresInicialesForm);
  }

  onEditClick(registro: number) {
    this.informeSupervisionService.updateInformeId(registro);
    this.router.navigate(['/informe-mensual-supervision']);
  }

  onViewClick(informe: number) {
    this.informeId = informe;
    document.getElementById('btnUploadInforme')?.click();
  }

  getFormValues() {
    let criteriosBusqueda: InformeMensualSupervisionRegistro = {
      id: 1,
      oficio: this.formBusqueda.value.memorando,
      lugar: this.formBusqueda.value.lugar,
      fechaRegistro: this.formBusqueda.value.fechaRegistro,
      fechaRegistroFin: this.formBusqueda.value.fechaRegistroFin,
      contrato: this.formBusqueda.value.contrato,
      direccionTecnica: this.formBusqueda.value.responsable,
      gerenteCalidadAgua: '',
      mesReporte:
        this.formBusqueda.value.mes == 0 ? '' : this.formBusqueda.value.mes,
      denominacionContrato: '',
      numeroSitios: '',
      indicaciones: '',
      nombreFirma: '',
      puestoFirma: '',
    };

    return criteriosBusqueda;
  }

  onArchivoInformeChange(event: any) {
    this.archivoInforme = event.target.files[0];
  }

  onGuardarArchivoInformeClick() {
    this.informeSupervisionService
      .postArchivoInforme(String(this.informeId), this.archivoInforme)
      .subscribe({
        next: (response: any) => {
          this.informeSupervisionService.updateMensaje({
            tipoMensaje: TipoMensaje.Correcto,
            mensaje: 'Archivo cargado correctamente',
            mostrar: true,
          });
        },
        error: (error) => {},
      });
  }

  getDirectoresResponsables() {
    this.informeSupervisionService.getDirectoresResponsables().subscribe({
      next: (response: any) => {
        this.directoresResponsables = response.data;
      },
      error: (error) => {},
    });
  }

  getLugaresInformeSupervision() {
    this.informeSupervisionService.getLugaresInformeSupervision().subscribe({
      next: (response: any) => {
        this.lugares = response.data;
      },
      error: (error) => {},
    });
  }

  getMemorandosInformeSupervision() {
    this.informeSupervisionService.getMemorandosInformeSupervision().subscribe({
      next: (response: any) => {
        this.memorandos = response.data;
      },
      error: (error) => {},
    });
  }

  onVerInformeClick(informe: number) {
    this.informeSupervisionService
      .getArchivoInformeSupervision(informe, 1)
      .subscribe({
        next: (response: any) => {
          const dataUrl = URL.createObjectURL(response);
          const targetElement = document.querySelector('#iframeContainer');
          let preview = document.getElementById('iframe-preview');

          if (preview) {
            targetElement?.removeChild(preview);
          }

          const iframe = document.createElement('iframe');
          iframe.id = 'iframe-preview';
          iframe.src = dataUrl;
          iframe.style.height = '400px';
          iframe.style.width = '100%';
          targetElement?.appendChild(iframe);
          document.getElementById('btn-preview-report')?.click();
        },
        error: (error) => {},
      });
  }

  onDeleteClick(informe: number) {
    this.informeId = informe;
    document.getElementById('btn-confirm-modal')?.click();
  }

  onConfirmDeleteClick() {
    if (this.informeId != 0 && this.informeId != null) {
      this.informeSupervisionService.deleteInforme(this.informeId).subscribe({
        next: (response: any) => {
          this.notificacionService.updateNotification({
            type: TipoMensaje.Correcto,
            text: 'Registro eliminado correctamente',
            show: true,
          });
          this.getInformes();
        },
        error: (error) => {},
      });
    }
  }
}
