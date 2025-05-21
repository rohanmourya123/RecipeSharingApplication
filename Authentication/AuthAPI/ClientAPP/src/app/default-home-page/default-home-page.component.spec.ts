import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefaultHomePageComponent } from './default-home-page.component';

describe('DefaultHomePageComponent', () => {
  let component: DefaultHomePageComponent;
  let fixture: ComponentFixture<DefaultHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DefaultHomePageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DefaultHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
