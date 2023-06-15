import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcumulacionResultadosComponent } from './acumulacion-resultados.component';

describe('AcumulacionResultadosComponent', () => {
  let component: AcumulacionResultadosComponent;
  let fixture: ComponentFixture<AcumulacionResultadosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AcumulacionResultadosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AcumulacionResultadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
