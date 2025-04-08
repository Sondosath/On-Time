import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditVaocherComponent } from './edit-vaocher.component';

describe('EditVaocherComponent', () => {
  let component: EditVaocherComponent;
  let fixture: ComponentFixture<EditVaocherComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditVaocherComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditVaocherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
