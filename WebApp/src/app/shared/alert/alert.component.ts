import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChange, SimpleChanges } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertComponent implements OnInit, OnChanges {
  @Input() message: string = '';
  @Input() type: string = '';
  @Input() mostrar: boolean = false;
  @Output() mostrando = new EventEmitter<boolean>();

  constructor(public sanitizer: DomSanitizer) { 
  }

  ngOnChanges(changes: SimpleChanges): void {
  }
  
  ngOnInit(): void {
  }

  ocultar(){
    this.mostrar = false;
    this.mostrando.emit(this.mostrar);
  }
}
