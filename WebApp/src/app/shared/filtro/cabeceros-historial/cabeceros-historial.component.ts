import { Component, OnInit } from '@angular/core';
import { Muestreo } from '../../../interfaces/Muestreo.interface';
import { BaseService } from '../../services/base.service';
import { MuestreoService } from '../../../modules/muestreo/liberacion/services/muestreo.service';

@Component({
  selector: 'app-cabeceros-historial',
  templateUrl: './cabeceros-historial.component.html',
  styleUrls: ['./cabeceros-historial.component.css']
})
export class CabecerosHistorialComponent extends BaseService implements OnInit {

  //filtrosCabeceroFoco: Array<any> = []; //Listado de cabeceros utilizado en el drop para redirigir al usuario al cabecero seleccionado


  muestreos: Array<Muestreo> = []; //Contiene los registros consultados a la API*/
  muestreosSeleccionados: Array<Muestreo> = []; //Contiene los registros que se van seleccionando*/
  

  constructor(private muestreoService: MuestreoService,) { super();  }

  ngOnInit(): void {
  }

  public consultarMonitoreos(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {
    this.muestreoService
      .obtenerMuestreosPaginados(false, page, pageSize, filter, this.orderBy)
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.muestreos = response.data;
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          this.getPreviousSelected(this.muestreos, this.muestreosSeleccionados);
          this.selectedPage = this.anyUnselected() ? false : true;
        },
        error: (error) => { },
      });
  }

  anyUnselected() {
    return this.muestreos.some((f) => !f.isChecked);
  }





}
