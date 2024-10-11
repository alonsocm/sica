import { Component, OnInit } from '@angular/core';
import { Column } from 'src/app/interfaces/filter/column';
import { ICommonMethods } from 'src/app/shared/interfaces/ICommonMethods';
import { BaseService } from 'src/app/shared/services/base.service';


@Component({
  selector: 'app-validado',
  templateUrl: './validado.component.html',
  styleUrls: ['./validado.component.css'],
})

export class ValidadoComponent
  extends BaseService
  implements OnInit, ICommonMethods
{
  sort(column: string, order: 'asc' | 'desc'): void {
    throw new Error('Method not implemented.');
  }
  onSelectClick(registro: any): void {
    throw new Error('Method not implemented.');
  }
  onFilterClick(columna: Column, isFiltroEspecial: boolean): void {
    throw new Error('Method not implemented.');
  }
  onFilterIconClick(column: Column): void {
    throw new Error('Method not implemented.');
  }
  onDeleteFilterClick(columnName: string): void {
    throw new Error('Method not implemented.');
  }
  onPageClick(page: any): void {
    throw new Error('Method not implemented.');
  }
  getPreviousSelected(registros: Array<any>, registrosSeleccionados: Array<any>): void {
    throw new Error('Method not implemented.');
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
}
