import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddVaocherComponent } from './add-vaocher.component';

describe('AddVaocherComponent', () => {
  let component: AddVaocherComponent;
  let fixture: ComponentFixture<AddVaocherComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddVaocherComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddVaocherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
