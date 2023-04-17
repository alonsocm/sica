import { Injectable } from '@angular/core';
import { ErrorHandler } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GlobalerrorHandlerServiceService implements ErrorHandler {

  constructor() { }
  handleError(error: any): void {
    console.error('Ocurrió un error:', error.message)
  }
}
 