import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormatoResultadoComponent } from './formato-resultado.component';

describe('FormatoResultadoComponent', () => {
  let component: FormatoResultadoComponent;
  let fixture: ComponentFixture<FormatoResultadoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormatoResultadoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormatoResultadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
