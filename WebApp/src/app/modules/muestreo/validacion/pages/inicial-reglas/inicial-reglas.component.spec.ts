import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InicialReglasComponent } from './inicial-reglas.component';

describe('InicialReglasComponent', () => {
  let component: InicialReglasComponent;
  let fixture: ComponentFixture<InicialReglasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InicialReglasComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InicialReglasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
