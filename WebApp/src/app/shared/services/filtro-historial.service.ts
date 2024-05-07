import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FiltroHistorialService {
  private columnNamePrivate: BehaviorSubject<string> =
    new BehaviorSubject<string>('');

  get columnName() {
    return this.columnNamePrivate.asObservable();
  }

  set columnDeleted(filtros: string) {
    this.columnNamePrivate.next(filtros);
  }

  constructor() {}
}
