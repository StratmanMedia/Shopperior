import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoppingListItemsViewComponent } from './shopping-list-items-view.component';

describe('ShoppingListItemsViewComponent', () => {
  let component: ShoppingListItemsViewComponent;
  let fixture: ComponentFixture<ShoppingListItemsViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShoppingListItemsViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShoppingListItemsViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
