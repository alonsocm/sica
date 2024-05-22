import { Injectable } from '@angular/core';
import { Column } from 'src/app/interfaces/filter/column';
import { BehaviorSubject } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class FiltroHistorialService {
  private columnNamePrivate: BehaviorSubject<string> = new BehaviorSubject<string>('');

  get columnName() {
    return this.columnNamePrivate.asObservable();
  }
  set columnDeleted(filtros: string) {
    this.columnNamePrivate.next(filtros);
  }

  private columnaFiltroEspecialPrivate: BehaviorSubject<Column> = new BehaviorSubject<Column>({ name: '', label: '', order: 0, data: [], filteredData: [], selectAll: false, filtered: true, dataType: '', selectedData: '' });
  get columnaFiltroEspecial() {
    return this.columnaFiltroEspecialPrivate.asObservable();
  }
  set columnaFiltroEspecialSeleccionados(columnas: Column) { this.columnaFiltroEspecialPrivate.next(columnas); }


  constructor() { }
}