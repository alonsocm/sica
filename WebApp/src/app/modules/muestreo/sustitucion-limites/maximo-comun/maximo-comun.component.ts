import { Component, OnInit } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';

@Component({
  selector: 'app-maximo-comun',
  templateUrl: './maximo-comun.component.html',
  styleUrls: ['./maximo-comun.component.css'],
})
export class MaximoComunComponent extends BaseService implements OnInit {
  constructor() {
    super();
  }

  ngOnInit(): void {}
}