import { Component, OnInit } from '@angular/core';
import { Column } from 'src/app/interfaces/filter/column';
import { ICommonMethods } from 'src/app/shared/interfaces/ICommonMethods';
import { BaseService } from 'src/app/shared/services/base.service';

@Component({
  selector: 'app-liberacion-resultados',
  templateUrl: './liberacion-resultados.component.html',
  styleUrls: ['./liberacion-resultados.component.css'],
})
export class LiberacionResultadosComponent
  extends BaseService
  implements OnInit, ICommonMethods
{
  onExportarResultadosClick() {
    throw new Error('Method not implemented.');
  }
  onEnviarResultadosRevisionClick() {
    throw new Error('Method not implemented.');
  }
  onAsignarFechaLimiteClick() {
    throw new Error('Method not implemented.');
  }
  onConfirmarEliminacionClick() {
    throw new Error('Method not implemented.');
  }
  onCargarEvidenciasClick($event: Event) {
    throw new Error('Method not implemented.');
  }
  onCargarArchivoClick($event: Event) {
    throw new Error('Method not implemented.');
  }
  registros: any[] = [];
  registrosSeleccionados: any[] = [];
  fechaActual: string = '';
  fechaLimiteRevision: string = '';

  constructor() {
    super();
  }
  setColumns(): void {
    throw new Error('Method not implemented.');
  }
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
  getPreviousSelected(
    registros: Array<any>,
    registrosSeleccionados: Array<any>
  ): void {
    throw new Error('Method not implemented.');
  }

  ngOnInit(): void {}
}
