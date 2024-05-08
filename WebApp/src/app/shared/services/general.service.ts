import { Injectable } from '@angular/core';
import { Muestreo } from '../../interfaces/Muestreo.interface';
import { MuestreoService } from '../../modules/muestreo/liberacion/services/muestreo.service';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class GeneralService extends BaseService {
  muestreos: Array<Muestreo> = []; //Contiene los registros consultados a la API*/
  muestreosSeleccionados: Array<Muestreo> = []; //Contiene los registros que se van seleccionando*/

  constructor(private muestreoService: MuestreoService,) { super(); }

  public consultarMonitoreos(
    page: number = this.page,
    pageSize: number = this.NoPage,
    filter: string = this.cadena
  ): void {
    console.log("cadena");
    console.log(this.cadena);
    console.log(this.page);
    console.log(this.NoPage);
 
    this.muestreoService
      .obtenerMuestreosPaginados(false, page, pageSize, filter, this.orderBy)
      .subscribe({
        next: (response: any) => {
          this.selectedPage = false;
          this.muestreos = response.data;
          this.page = response.totalRecords !== this.totalItems ? 1 : this.page;
          this.totalItems = response.totalRecords;
          //this.getPreviousSelected(this.muestreos, this.muestreosSeleccionados);
          this.selectedPage = this.anyUnselected() ? false : true;
        },
        error: (error) => { },
      });
  }

  anyUnselected() {
    return this.muestreos.some((f) => !f.isChecked);
  }


}
