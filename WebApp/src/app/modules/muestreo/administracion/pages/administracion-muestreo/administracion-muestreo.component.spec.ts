import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministracionMuestreoComponent } from './administracion-muestreo.component';

describe('AdministracionMuestreoComponent', () => {
  let component: AdministracionMuestreoComponent;
  let fixture: ComponentFixture<AdministracionMuestreoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdministracionMuestreoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdministracionMuestreoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
