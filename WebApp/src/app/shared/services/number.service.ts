import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NumberService {
  fechaActual = new Date();

  constructor() { }

  getOrdinal(number: string): string{
    if(number === '' || number === '0') return '';
    if(number == '1' || number == '3' ){
      return number + 'ra'
    }
    return number+'a'
  }

  getOrdinalYear(number: string): string {
    
    if (number === '' || number === '0') return '';
    if (number == '1' || number == '3') {
      return number + 'RA-' + this.fechaActual.getFullYear();
    }
    if (number == '2') {
      return number + 'DA-' + this.fechaActual.getFullYear();
    }
    return number + 'A-' + this.fechaActual.getFullYear();
  }
}
