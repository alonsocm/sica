import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { CorreoModel } from "../models/correo-model";

@Injectable({
  providedIn: 'root',
})
export class correoService {
  private correoPrivate: BehaviorSubject<CorreoModel> =
    new BehaviorSubject<CorreoModel>({
      destinatarios: '',
      copias: '',
      asunto: '',
      cuerpo: '',
      archivos: []
    });
  get correo() {
    return this.correoPrivate.asObservable();
  }
  set correoSeleccionados(columnas: CorreoModel) {
    this.correoPrivate.next(columnas);
  }

}
