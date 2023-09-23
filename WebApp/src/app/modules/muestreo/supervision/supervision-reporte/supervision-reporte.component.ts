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

(<any>pdfMake).vfs = pdfFonts.pdfMake.vfs;

@Component({
  selector: 'app-supervision-reporte',
  templateUrl: './supervision-reporte.component.html',
  styleUrls: ['./supervision-reporte.component.css'],
})
export class SupervisionReporteComponent implements OnInit {
  registroForm: FormGroup;
  copias: Array<string> = [];

  constructor(
    private formBuilder: FormBuilder,
    private supervisionService: SupervisionService
  ) {
    this.registroForm = this.formBuilder.group({
      memorando: ['No. BOO_B1208.3-08/2012', Validators.required],
      lugar: ['Guadalajara Jalisco', Validators.required],
      fecha: ['', Validators.required],
      destinatario: ['', Validators.required],
      responsable: ['', Validators.required],
      puesto: ['', Validators.required],
      copia: ['', Validators.required],
      inicialesPersonas: ['', Validators.required],
      mes: [0, [Validators.required, Validators.min(1)]],
    });
  }

  ngOnInit(): void {}

  onSubmit() {
    console.log(this.registroForm.value);
  }

  onAgregarCopiaClick() {
    this.copias.push(this.registroForm.value.copia);
    this.registroForm.patchValue({ copia: '' });
  }

  onDeleteCopiaClick(nombre: string) {
    let index = this.copias.findIndex((x) => nombre);
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

  onCreatePdfClick() {
    let oficio: InformeMensualSupervision = {
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
      nombreFirma: '',
      puestoFirma: '',
    };

    let reporteInformeMensualSupervisionDefinition =
      new ReporteMensualSupervisionDefinition();

    const pdfDocGenerator = pdfMake.createPdf(
      reporteInformeMensualSupervisionDefinition.getDocumentDefinition(oficio)
    );

    pdfDocGenerator.getDataUrl((dataUrl) => {
      const targetElement = document.querySelector('#iframeContainer');
      const iframe = document.createElement('iframe');
      iframe.src = dataUrl;
      iframe.style.height = '400px';
      iframe.style.width = '800px';
      targetElement?.appendChild(iframe);
    });

    pdfDocGenerator.getBuffer((buffer) => {
      var blob = new Blob([buffer], { type: 'application/pdf' });

      this.supervisionService.postArchivoReporte(blob).subscribe({
        next: (response: any) => {
          alert(response);
        },
        error: (error) => {},
      });
    });
  }
}
