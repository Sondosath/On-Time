import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetVaocherComponent } from './get-vaocher.component';

describe('GetVaocherComponent', () => {
  let component: GetVaocherComponent;
  let fixture: ComponentFixture<GetVaocherComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetVaocherComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetVaocherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
