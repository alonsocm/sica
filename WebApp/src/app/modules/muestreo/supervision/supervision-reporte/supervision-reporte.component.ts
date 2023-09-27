import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import * as pdfMake from 'pdfmake/build/pdfmake';
import * as pdfFonts from 'pdfmake/build/vfs_fonts';
import {
  InformeMensualSupervision,
  Resultado,
} from '../models/informe-mensual-supervision';
import { Margins } from 'pdfmake/interfaces';
import { ReporteMensualSupervisionDefinition } from './reporte-mensual-supervision-definition';
import { of } from 'rxjs';
import { SupervisionService } from '../supervision.service';
import { InformeMensualSupervisionGeneral } from '../models/informe-mensual-supervision-general';
import { AuthService } from 'src/app/modules/login/services/auth.service';

(<any>pdfMake).vfs = pdfFonts.pdfMake.vfs;

@Component({
  selector: 'app-supervision-reporte',
  templateUrl: './supervision-reporte.component.html',
  styleUrls: ['./supervision-reporte.component.css'],
})
export class SupervisionReporteComponent implements OnInit {
  submitted = false;
  registroForm: FormGroup;
  directoresResponsables: Array<{
    id: number;
    nombre: string;
    puesto: string;
  }> = [];
  copias: Array<{ nombre: string; puesto: string }> = [];
  oficio = {};
  reporteInformeMensualSupervisionDefinition =
    new ReporteMensualSupervisionDefinition();

  constructor(
    private formBuilder: FormBuilder,
    private supervisionService: SupervisionService,
    private authService: AuthService
  ) {
    this.registroForm = this.formBuilder.group({
      memorando: ['No. BOO_B1208.3-08/2012', Validators.required],
      lugar: ['Guadalajara Jalisco', Validators.required],
      fecha: ['2023-09-26', Validators.required],
      destinatario: ['', Validators.required],
      responsable: [0, [Validators.required, Validators.min(1)]],
      puesto: ['', Validators.required],
      nombreCopia: ['', Validators.required],
      puestoCopia: ['', Validators.required],
      inicialesPersonas: ['', Validators.required],
      mes: [1, [Validators.required, Validators.min(1)]],
    });
  }

  ngOnInit(): void {
    this.getDirectoresResponsables();
  }

  onSubmit() {
    console.log(this.registroForm.value);
  }

  getDirectoresResponsables() {
    this.supervisionService.getDirectoresResponsables().subscribe({
      next: (response: any) => {
        this.directoresResponsables = response.data;
      },
      error: (error) => {},
    });
  }

  getNombreResponsable() {
    let nombre = '';
    let directorResponsableId = this.registroForm.value.responsable;

    if (directorResponsableId !== '0') {
      let selectedValueIndex = this.directoresResponsables.findIndex(
        (x) => x.id == directorResponsableId
      );

      nombre = this.directoresResponsables[selectedValueIndex].nombre;
    }

    return nombre;
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

  onCancelarClick() {}

  onCreatePdfClick() {
    this.submitted = true;
    let oficio = this.getDatosInforme();

    let reporteInformeMensualSupervisionDefinition =
      new ReporteMensualSupervisionDefinition();

    const pdfDocGenerator = pdfMake.createPdf(
      reporteInformeMensualSupervisionDefinition.getDocumentDefinition(oficio)
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
        usuario: this.authService.getUser().usuarioId,
      };

      this.supervisionService.postArchivoReporte(datosOficio).subscribe({
        next: (response: any) => {
          alert(response);
        },
        error: (error) => {},
      });
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
      direccionTecnica: 'Organismo de Cuenca Lerma Santiago Pacifico',
      gerenteCalidadAgua: '',
      mesReporte:
        new Date(this.registroForm.value.mes + '-01-' + '2023').toLocaleString(
          'es',
          { month: 'long' }
        ) +
        ' ' +
        '2023',
      atencion: [
        'M. en B. Claudia Nava Ramirez',
        'Ing. Martha Bustamente Herrera',
      ],
      contrato: 'CNA-CRM-029-2021',
      denominacionContrato:
        'Servicio para caracterizar la calidad del agua en zonas con problemática ambiental a nivel nacional',
      numeroSitios: '1329 a 3081',
      indicaciones:
        'La supervisión en campo deberá realizarse, de acuerdo con los recursos disponibles y al programa de trabajo en cada Organismo de Cuenca, se realizará conforme al formato elaborado para tal fin, en el que se contemplan los puntos a evaluar y se señalan los puntos críticos del muestreo en los que, en caso de incumplimiento, los trabajos de campo serán cancelados.',
      resultados: [
        {
          ocdl: 'Dirección local de colima',
          totalSitios: '3',
          intervalos: [
            {
              calificacion: '<50',
              numeroSitios: '1',
              porcentaje: '100%',
            },
            {
              calificacion: '51-60',
              numeroSitios: '1',
              porcentaje: '0%',
            },
            {
              calificacion: '61-70',
              numeroSitios: '1',
              porcentaje: '100%',
            },
            {
              calificacion: '71-80',
              numeroSitios: '1',
              porcentaje: '0%',
            },
            {
              calificacion: '81-90',
              numeroSitios: '1',
              porcentaje: '100%',
            },
            {
              calificacion: '91-100',
              numeroSitios: '1',
              porcentaje: '0%',
            },
          ],
        },
        {
          ocdl: 'Dirección local de Estado de México',
          totalSitios: '3',
          intervalos: [
            {
              calificacion: '<50',
              numeroSitios: '1',
              porcentaje: '100%',
            },
            {
              calificacion: '51-60',
              numeroSitios: '1',
              porcentaje: '0%',
            },
            {
              calificacion: '61-70',
              numeroSitios: '1',
              porcentaje: '100%',
            },
            {
              calificacion: '71-80',
              numeroSitios: '1',
              porcentaje: '0%',
            },
            {
              calificacion: '81-90',
              numeroSitios: '1',
              porcentaje: '100%',
            },
            {
              calificacion: '91-100',
              numeroSitios: '1',
              porcentaje: '0%',
            },
          ],
        },
        {
          ocdl: 'Dirección local de Estado de México',
          totalSitios: '3',
          intervalos: [
            {
              calificacion: '<50',
              numeroSitios: '1',
              porcentaje: '100%',
            },
            {
              calificacion: '51-60',
              numeroSitios: '1',
              porcentaje: '0%',
            },
            {
              calificacion: '61-70',
              numeroSitios: '1',
              porcentaje: '100%',
            },
            {
              calificacion: '71-80',
              numeroSitios: '1',
              porcentaje: '0%',
            },
            {
              calificacion: '81-90',
              numeroSitios: '1',
              porcentaje: '100%',
            },
            {
              calificacion: '91-100',
              numeroSitios: '1',
              porcentaje: '0%',
            },
          ],
        },
        {
          ocdl: 'Dirección local de Estado de México',
          totalSitios: '3',
          intervalos: [
            {
              calificacion: '<50',
              numeroSitios: '1',
              porcentaje: '100%',
            },
            {
              calificacion: '51-60',
              numeroSitios: '1',
              porcentaje: '0%',
            },
            {
              calificacion: '61-70',
              numeroSitios: '1',
              porcentaje: '100%',
            },
            {
              calificacion: '71-80',
              numeroSitios: '1',
              porcentaje: '0%',
            },
            {
              calificacion: '81-90',
              numeroSitios: '1',
              porcentaje: '100%',
            },
            {
              calificacion: '91-100',
              numeroSitios: '1',
              porcentaje: '0%',
            },
          ],
        },
        {
          ocdl: 'Dirección local de Estado de México',
          totalSitios: '3',
          intervalos: [
            {
              calificacion: '<50',
              numeroSitios: '1',
              porcentaje: '100%',
            },
            {
              calificacion: '51-60',
              numeroSitios: '1',
              porcentaje: '0%',
            },
            {
              calificacion: '61-70',
              numeroSitios: '1',
              porcentaje: '100%',
            },
            {
              calificacion: '71-80',
              numeroSitios: '1',
              porcentaje: '0%',
            },
            {
              calificacion: '81-90',
              numeroSitios: '1',
              porcentaje: '100%',
            },
            {
              calificacion: '91-100',
              numeroSitios: '1',
              porcentaje: '0%',
            },
          ],
        },
      ],
      nombreFirma: this.getNombreResponsable(),
      puestoFirma: this.registroForm.value.puesto,
      copias: this.copias,
      personasInvolucradas: this.registroForm.value.inicialesPersonas,
    };
  }
}
