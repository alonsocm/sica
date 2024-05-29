import { Component, OnInit } from '@angular/core';
import { FiltroHistorialService } from '../../services/filtro-historial.service';
import { MuestreoService } from 'src/app/modules/muestreo/liberacion/services/muestreo.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-historial',
  templateUrl: './historial.component.html',
  styleUrls: ['./historial.component.css'],
})
export class HistorialComponent implements OnInit {
  muestreosServiceSub: Subscription;
  filtros: any = [];

  constructor(
    private muestreosService: MuestreoService,
    private filtroHistorialService: FiltroHistorialService
  ) {
    this.muestreosServiceSub = muestreosService.filtros.subscribe((filtros) => {
      this.filtros = filtros;
    });
  }

  ngOnInit(): void {}

  onDeleteClick(name: string) {
    let index = this.filtros.findIndex((f: any) => f.name === name);

    if (index !== -1) {
      this.filtros.splice(index, 1);
      this.filtroHistorialService.columnDeleted = name;
    }
  }

  ngOnDestroy() {
    this.muestreosServiceSub.unsubscribe();
  }
}
