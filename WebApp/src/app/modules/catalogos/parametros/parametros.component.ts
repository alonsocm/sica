import { Component, OnInit } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';

@Component({
  selector: 'app-parametros',
  templateUrl: './parametros.component.html',
  styleUrls: ['./parametros.component.css'],
})
export class ParametrosComponent extends BaseService implements OnInit {
  constructor() {
    super();
  }

  ngOnInit(): void {}
}
