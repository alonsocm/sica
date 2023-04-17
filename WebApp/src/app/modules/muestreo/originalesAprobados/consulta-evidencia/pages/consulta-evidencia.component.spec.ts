import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultaEvidenciaComponent } from './consulta-evidencia.component';

describe('ConsultaEvidenciaComponent', () => {
  let component: ConsultaEvidenciaComponent;
  let fixture: ComponentFixture<ConsultaEvidenciaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConsultaEvidenciaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConsultaEvidenciaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
