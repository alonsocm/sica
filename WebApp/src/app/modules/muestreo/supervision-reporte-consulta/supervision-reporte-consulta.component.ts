import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseService } from 'src/app/shared/services/base.service';
import { InformeMensualSupervisionRegistro } from '../supervision/models/informe-mensual-supervision-registro';

@Component({
  selector: 'app-supervision-reporte-consulta',
  templateUrl: './supervision-reporte-consulta.component.html',
  styleUrls: ['./supervision-reporte-consulta.component.css'],
})
export class SupervisionReporteConsultaComponent
  extends BaseService
  implements OnInit
{
  formBusqueda: FormGroup;
  registrosInformeMensual: Array<InformeMensualSupervisionRegistro> = [];

  constructor(private formBuilder: FormBuilder) {
    super();
    this.formBusqueda = this.formBuilder.group({
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

  onSubmit() {}

  onRegistrarInformeSupervisionClick() {}

  onLimpiarClick() {}

  onEditClick(registro: number) {}

  onViewClick(registro: number) {}

  onDeleteClick(registro: number) {}
}
