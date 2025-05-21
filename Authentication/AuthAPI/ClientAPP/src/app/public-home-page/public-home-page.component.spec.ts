import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicHomePageComponent } from './public-home-page.component';

describe('PublicHomePageComponent', () => {
  let component: PublicHomePageComponent;
  let fixture: ComponentFixture<PublicHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PublicHomePageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PublicHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
