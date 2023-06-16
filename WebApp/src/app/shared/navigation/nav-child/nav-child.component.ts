import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-child-menu',
  templateUrl: './nav-child.component.html',
  styleUrls: ['./nav-child.component.css'],
})
export class NavChildComponent implements OnInit {
  @Input() nivel = 1;
  @Input() idPagina = 0;
  @Input() paginas: any[] = [];
  paginasHijo: any[] = [];
  constructor() {}

  ngOnInit(): void {
    this.paginasHijo = this.paginas.filter(
      (f) => f.idPaginaPadre == this.idPagina
    );
  }

  mouseOver(idPagina: number) {
    let hijo = document.getElementById('elemento-hijo' + idPagina);
    if (hijo != null) {
      hijo.classList.remove('hide');
      hijo.classList.add('show');
      hijo.classList.add('segundo-nivel');
    }
  }

  mouseLeave(idPagina: number) {
    let hijo = document.getElementById('elemento-hijo' + idPagina);
    if (hijo != null) {
      hijo.classList.remove('show');
      hijo.classList.add('hide');
    }
  }
}
