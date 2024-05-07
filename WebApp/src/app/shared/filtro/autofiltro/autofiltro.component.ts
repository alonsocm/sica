import { Component, OnInit } from '@angular/core';
import { BaseService } from '../../services/base.service';

@Component({
  selector: 'app-autofiltro',
  templateUrl: './autofiltro.component.html',
  styleUrls: ['./autofiltro.component.css']
})
export class AutofiltroComponent extends BaseService implements OnInit {

  constructor() { super(); }

  ngOnInit(): void {
  }

}
