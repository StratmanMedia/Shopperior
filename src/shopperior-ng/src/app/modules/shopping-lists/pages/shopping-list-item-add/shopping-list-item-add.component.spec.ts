import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoppingListItemAddComponent } from './shopping-list-item-add.component';

describe('ShoppingListItemAddComponent', () => {
  let component: ShoppingListItemAddComponent;
  let fixture: ComponentFixture<ShoppingListItemAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShoppingListItemAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShoppingListItemAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
