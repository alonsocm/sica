import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import * as pdfMake from 'pdfmake/build/pdfmake';
import * as pdfFonts from 'pdfmake/build/vfs_fonts';
import { InformeMensualSupervision } from '../models/informe-mensual-supervision';
import { ReporteMensualSupervisionDefinition } from './reporte-mensual-supervision-definition';
import { InformeSupervisionService } from '../informe-supervision.service';
import { InformeMensualSupervisionGeneral } from '../models/informe-mensual-supervision-general';
import { Router } from '@angular/router';
import { TipoMensaje } from 'src/app/shared/enums/tipoMensaje';
import { DirectorResponsable } from '../../supervision/models/director-responsable';
(<any>pdfMake).vfs = pdfFonts.pdfMake.vfs;

@Component({
  selector: 'app-informe-supervision',
  templateUrl: './informe-supervision.component.html',
  styleUrls: ['./informe-supervision.component.css'],
})
export class InformeSupervisionComponent implements OnInit {
  informeId = 0;
  datosPlantilla: any = {};
  submitted = false;
  registroForm: FormGroup;
  directoresResponsables: Array<DirectorResponsable> = [];
  copias: Array<{ nombre: string; puesto: string }> = [];
  oficio = {};
  reporteInformeMensualSupervisionDefinition =
    new ReporteMensualSupervisionDefinition();
  mensaje = {};

  constructor(
    private formBuilder: FormBuilder,
    private informeSupervisionService: InformeSupervisionService,
    private router: Router
  ) {
    this.registroForm = this.formBuilder.group({
      memorando: ['', Validators.required],
      lugar: ['', Validators.required],
      fecha: ['', Validators.required],
      responsable: [0, [Validators.required]],
      puesto: ['', Validators.required],
      nombreCopia: [''],
      puestoCopia: [''],
      inicialesPersonas: [
        '',
        [Validators.required, Validators.pattern(/^[a-zA-Z/]*$/)],
      ],
      mes: [1, [Validators.required, Validators.min(1)]],
    });

    this.informeSupervisionService.updateMensaje({
      tipoMensaje: '',
      mensaje: '',
      mostrar: false,
    });

    this.informeSupervisionService.informeId.subscribe((data) => {
      this.informeId = data;
    });
  }

  ngOnInit(): void {
    this.getDirectoresResponsables();

    if (this.informeId !== 0) {
      this.getInformeSupervision(String(this.informeId));
    }
  }

  onSubmit() {}

  getDirectoresResponsables() {
    this.informeSupervisionService.getDirectoresResponsables().subscribe({
      next: (response: any) => {
        this.directoresResponsables = response.data;
      },
      error: (error) => {},
    });
  }

  getInformeSupervision(informe: string) {
    this.informeSupervisionService.getInformeSupervision(informe).subscribe({
      next: (response: any) => {
        let informe = response.data;
        this.copias = informe.copias;
        this.registroForm.patchValue({
          memorando: informe.oficio,
          lugar: informe.lugar,
          fecha: informe.fecha,
          responsable: informe.responsableId,
          mes: informe.mes,
          inicialesPersonas: informe.personasInvolucradas,
        });
        this.onDirectorResponsableChange();
      },
      error: (error) => {},
    });
  }

  getDatosResponsable() {
    let responsable: { nombre: string; puesto: string; ocDl: string } = {
      nombre: '',
      puesto: '',
      ocDl: '',
    };
    let directorResponsableId = this.registroForm.value.responsable;

    if (directorResponsableId !== '0') {
      let selectedValueIndex = this.directoresResponsables.findIndex(
        (x) => x.id == directorResponsableId
      );

      responsable = this.directoresResponsables[selectedValueIndex];
    }

    return responsable;
  }

  onDirectorResponsableChange() {
    let directorResponsableId = this.registroForm.value.responsable;
    let puesto = '';

    if (directorResponsableId !== '0') {
      let selectedValueIndex = this.directoresResponsables.findIndex(
        (x) => x.id == directorResponsableId
      );
      puesto = this.directoresResponsables[selectedValueIndex].puesto;
    }

    this.registroForm.patchValue({ puesto: puesto });
  }

  getOCId() {
    let directorResponsableId = this.registroForm.value.responsable;
    let ocId = 0;

    if (directorResponsableId !== 0) {
      let selectedValueIndex = this.directoresResponsables.findIndex(
        (x) => x.id == directorResponsableId
      );

      ocId = this.directoresResponsables[selectedValueIndex].ocid;
    }

    return ocId;
  }

  onAgregarCopiaClick() {
    let copia = {
      nombre: this.registroForm.value.nombreCopia,
      puesto: this.registroForm.value.puestoCopia,
    };

    if (copia.nombre != '' && copia.puesto != '') {
      this.copias.push(copia);
      this.registroForm.patchValue({ nombreCopia: '', puestoCopia: '' });
    }
  }

  onDeleteCopiaClick(nombre: string, puesto: string) {
    let index = this.copias.findIndex(
      (x) => x.nombre === nombre && x.puesto === puesto
    );
    this.copias.splice(index, 1);
  }

  getLongFormatDate(inputDate: string): string {
    // Crear un objeto Date a partir del input
    let utcDate = new Date(inputDate);

    // Ajustar al huso horario local
    let localDate = new Date(
      utcDate.getTime() + utcDate.getTimezoneOffset() * 60000
    );

    // Obtener el día con el formato "01"
    let day = String(localDate.getDate()).padStart(2, '0');

    return ` ${day} de ${localDate.toLocaleString('es-ES', {
      month: 'long',
      year: 'numeric',
    })}`;
  }

  onCancelarClick() {
    this.router.navigate(['/informe-mensual-supervision-consulta']);
  }

  getDatosGeneralesInforme() {
    this.informeSupervisionService
      .getDatosGeneralesInforme(
        String(2022),
        String(2023),
        this.registroForm.value.mes,
        String(this.getOCId())
      )
      .subscribe({
        next: (response: any) => {
          this.datosPlantilla = response.data;
          let oficio = this.getDatosInforme();

          let reporteInformeMensualSupervisionDefinition =
            new ReporteMensualSupervisionDefinition();

          const pdfDocGenerator = pdfMake.createPdf(
            reporteInformeMensualSupervisionDefinition.getDocumentDefinition(
              oficio
            )
          );

          pdfDocGenerator.getDataUrl((dataUrl) => {
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
          });

          document.getElementById('btn-preview-report')?.click();
        },
        error: (error) => {},
      });
  }

  onVistaPreviaClick() {
    this.submitted = true;

    if (this.registroForm.valid && this.copias.length !== 0) {
      this.getDatosGeneralesInforme();
    }

    // pdfDocGenerator.getBuffer((buffer) => {
    //   var blob = new Blob([buffer], { type: 'application/pdf' });

    //   this.supervisionService.postArchivoReporte(blob).subscribe({
    //     next: (response: any) => {
    //       alert(response);
    //     },
    //     error: (error) => {},
    //   });
    // });
  }

  onValidarClick() {
    let informe = this.getDatosInforme();

    let reporteInformeMensualSupervisionDefinition =
      new ReporteMensualSupervisionDefinition();

    const pdfDocGenerator = pdfMake.createPdf(
      reporteInformeMensualSupervisionDefinition.getDocumentDefinition(informe)
    );

    pdfDocGenerator.getBuffer((buffer) => {
      var archivo = new Blob([buffer], { type: 'application/pdf' });
      let datosOficio: InformeMensualSupervisionGeneral = {
        oficio: informe.oficio,
        lugar: informe.lugar,
        fecha: informe.fecha,
        responsableId: this.registroForm.value.responsable,
        mes: this.registroForm.value.mes,
        copias: this.copias,
        personasInvolucradas: informe.personasInvolucradas,
        archivo: archivo,
      };

      if (this.informeId === 0) {
        this.informeSupervisionService.postInforme(datosOficio).subscribe({
          next: (response: any) => {
            this.informeSupervisionService.updateMensaje({
              tipoMensaje: TipoMensaje.Correcto,
              mensaje: 'Informe creado correctamente',
              mostrar: true,
            });
            this.router.navigate(['/informe-mensual-supervision-consulta']);
          },
          error: (error) => {},
        });
      } else {
        this.informeSupervisionService
          .putInforme(datosOficio, String(this.informeId))
          .subscribe({
            next: (response: any) => {
              this.informeSupervisionService.updateMensaje({
                tipoMensaje: TipoMensaje.Correcto,
                mensaje: 'Informe actualizado correctamente',
                mostrar: true,
              });
              this.router.navigate(['/informe-mensual-supervision-consulta']);
            },
            error: (error) => {},
          });
      }
    });
  }

  onImprimirClick() {
    let oficio: InformeMensualSupervision = this.getDatosInforme();

    pdfMake
      .createPdf(
        this.reporteInformeMensualSupervisionDefinition.getDocumentDefinition(
          oficio
        )
      )
      .print({}, window.frames[0]);
  }

  private getDatosInforme(): InformeMensualSupervision {
    return {
      oficio: this.registroForm.value.memorando,
      lugar: this.registroForm.value.lugar,
      fecha: this.getLongFormatDate(this.registroForm.value.fecha),
      direccionTecnica: this.getDatosResponsable().ocDl,
      gerenteCalidadAgua: this.datosPlantilla.gerenteCalidadAgua,
      mesReporte:
        new Date(this.registroForm.value.mes + '-01-' + '2023').toLocaleString(
          'es',
          { month: 'long' }
        ) +
        ' ' +
        '2023',
      atencion: this.datosPlantilla.atencion,
      contrato: this.datosPlantilla.contrato,
      denominacionContrato: this.datosPlantilla.denominacionContrato,
      numeroSitios: this.datosPlantilla.numeroSitios,
      indicaciones: this.datosPlantilla.indicaciones,
      resultados: this.datosPlantilla.resultados,
      nombreFirma: this.getDatosResponsable().nombre,
      puestoFirma: this.registroForm.value.puesto,
      copias: this.copias,
      personasInvolucradas: this.registroForm.value.inicialesPersonas,
    };
  }
}
