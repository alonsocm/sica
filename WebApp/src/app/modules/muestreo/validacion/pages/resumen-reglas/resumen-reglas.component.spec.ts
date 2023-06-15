import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResumenReglasComponent } from './resumen-reglas.component';

describe('ResumenReglasComponent', () => {
  let component: ResumenReglasComponent;
  let fixture: ComponentFixture<ResumenReglasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResumenReglasComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ResumenReglasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
