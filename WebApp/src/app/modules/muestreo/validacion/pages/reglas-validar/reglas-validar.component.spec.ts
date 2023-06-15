import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReglasValidarComponent } from './reglas-validar.component';

describe('ReglasValidarComponent', () => {
  let component: ReglasValidarComponent;
  let fixture: ComponentFixture<ReglasValidarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReglasValidarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReglasValidarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
