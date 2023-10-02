import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseService } from 'src/app/shared/services/base.service';
import { InformeMensualSupervisionRegistro } from '../models/informe-mensual-supervision-registro';
import { Router } from '@angular/router';
import { InformeSupervisionService } from '../informe-supervision.service';

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

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private informeSupervisionService: InformeSupervisionService
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
  }

  ngOnInit(): void {
    this.getInformes();
  }

  getInformes() {
    let criteriosBusqueda = this.getFormValues();
    this.informeSupervisionService.getInformes(criteriosBusqueda).subscribe({
      next: (response: any) => {
        console.table(response.data);
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

  onLimpiarClick() {}

  onEditClick(registro: number) {
    this.informeSupervisionService.updateInformeId(registro);
    this.router.navigate(['/informe-mensual-supervision']);
  }

  onViewClick(registro: number) {}

  onDeleteClick(registro: number) {}

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
      // atencion: Array<string>;
      denominacionContrato: '',
      numeroSitios: '',
      indicaciones: '',
      nombreFirma: '',
      puestoFirma: '',
    };

    return criteriosBusqueda;
  }
}
