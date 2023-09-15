import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-supervision-reporte',
  templateUrl: './supervision-reporte.component.html',
  styleUrls: ['./supervision-reporte.component.css'],
})
export class SupervisionReporteComponent implements OnInit {
  registroForm: FormGroup;
  copias: Array<string> = [];

  constructor(private formBuilder: FormBuilder) {
    this.registroForm = this.formBuilder.group({
      memorando: ['', Validators.required],
      lugar: ['', Validators.required],
      fecha: ['', Validators.required],
      destinatario: ['', Validators.required],
      responsable: ['', Validators.required],
      puesto: ['', Validators.required],
      copia: ['', Validators.required],
      inicialesPersonas: ['', Validators.required],
      mes: [0, Validators.required],
    });
  }

  ngOnInit(): void {}

  onSubmit() {
    console.log(this.registroForm.value);
  }

  onAgregarCopiaClick() {
    this.copias.push(this.registroForm.value.copia);
    this.registroForm.patchValue({ copia: '' });
  }

  onDeleteCopiaClick(nombre: string) {
    let index = this.copias.findIndex((x) => nombre);
    this.copias.splice(index, 1);
  }
}
