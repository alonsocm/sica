import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultaResultadoComponent } from './consulta-resultado.component';

describe('ConsultaResultadoComponent', () => {
  let component: ConsultaResultadoComponent;
  let fixture: ComponentFixture<ConsultaResultadoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConsultaResultadoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConsultaResultadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
