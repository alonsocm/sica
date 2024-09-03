import { Column } from 'src/app/interfaces/filter/column';

export interface ICommonMethods {
  sort(column: string, order: 'asc' | 'desc'): void;
  onSelectClick(registro: any): void;
  onSelectPageClick(registros: any[], registrosSeleccionados: any[]): void;
  onFilterClick(columna: Column, isFiltroEspecial: boolean): void;
  onFilterIconClick(column: Column): void;
  onDeleteFilterClick(columnName: string): void;
  onPageClick(page: any): void;
  getPreviousSelected(
    registros: Array<any>,
    registrosSeleccionados: Array<any>
  ): void;
}
