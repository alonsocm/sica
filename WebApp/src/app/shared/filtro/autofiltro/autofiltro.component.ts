import { Component, Input, OnInit } from '@angular/core';
import { Column } from '../../../interfaces/filter/column';
import { BaseService } from '../../services/base.service';
import { FiltroHistorialService } from '../../services/filtro-historial.service';

@Component({
  selector: 'app-autofiltro',
  templateUrl: './autofiltro.component.html',
  styleUrls: ['./autofiltro.component.css']
})
export class AutofiltroComponent extends BaseService implements OnInit {
  @Input() columnaEspecial: Column = this.columnaFiltroEspecial;
  @Input() opcionesFiltrosmodal: Array<string> =[];

  constructor(private filtroHistorialService: FiltroHistorialService) {
    super();
    this.filtroHistorialService.columnaFiltroEspecial.subscribe((columna) => { this.columnaFiltroEspecial = columna });
  }

  ngOnInit(): void { }

  onFiltrar() {   
    this.filtroHistorialService.columnaFiltroEspecialSeleccionados = this.columnaEspecial;
  }

}
