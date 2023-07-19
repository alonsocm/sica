import { Component, OnInit } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';

@Component({
  selector: 'app-laboratorio',
  templateUrl: './laboratorio.component.html',
  styleUrls: ['./laboratorio.component.css']
})
export class LaboratorioComponent extends BaseService implements OnInit {

  constructor() { 
    super();
  }

  ngOnInit(): void {
  }

}
