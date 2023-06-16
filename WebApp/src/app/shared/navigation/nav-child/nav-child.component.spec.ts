import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavChildComponent } from './nav-child.component';

describe('ChildMenuComponent', () => {
  let component: NavChildComponent;
  let fixture: ComponentFixture<NavChildComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NavChildComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NavChildComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
