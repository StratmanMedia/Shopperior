import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ListItemModel } from 'src/app/core/data/list/models/list-tem-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';
import { LoggingService } from 'src/app/core/logging/logging.service';
import { environment } from 'src/environments/environment';
import { CategoryModel } from 'src/app/core/data/category/models/category-model';
import { CategoryService } from 'src/app/core/data/category/category.service';

@Component({
  selector: 'app-list-item-form',
  templateUrl: './list-item-form.component.html',
  styleUrls: ['./list-item-form.component.scss']
})
export class ListItemFormComponent implements OnInit {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: 'ListItemFormComponent'
  });
  itemForm: UntypedFormGroup;
  @Input() listGuid: string;
  categories: Observable<CategoryModel[]>;
  
  constructor(
    private _shoppingListService: ShoppingListService,
    private _categoryService: CategoryService,
    private _router: Router) { }

  ngOnInit(): void {
    this.categories = this._categoryService.getMine();
    this.itemForm = this.buildItemForm();
  }

  onSubmit(): void {
    const item = <ListItemModel>{
      guid: '',
      shoppingListGuid: this.listGuid,
      name: this.itemForm.controls.name.value,
      brand: this.itemForm.controls.brand.value,
      comment: this.itemForm.controls.details.value,
      categoryGuid: this.itemForm.controls.category.value,
      quantity: this.itemForm.controls.quantity.value,
      measurement: this.itemForm.controls.measurement.value,
      isInCart: this.itemForm.controls.isInCart.value
    }
    this._logger.debug(`Saving item. ${JSON.stringify(item)}`);
    this._shoppingListService.addItem(item).pipe()
    .subscribe(() => {
      this._logger.debug(`Item saved.`);
      this._router.navigateByUrl(`/app/lists/${this.listGuid}`);
    });
  }

  calculateSubtotal(): void {
    const newSubtotal = Math.round(this.itemForm.controls.quantity.value * this.itemForm.controls.unitPrice.value * 100) / 100;
    this.itemForm.controls.subtotal.setValue(newSubtotal);
  }

  calculateUnitPrice(): void {
    const newUnitPrice = Math.round((this.itemForm.controls.subtotal.value / this.itemForm.controls.quantity.value) * 100) /100;
    this.itemForm.controls.unitPrice.setValue(newUnitPrice);
  }

  private buildItemForm(): UntypedFormGroup {
    const form = new UntypedFormGroup({
      name: new UntypedFormControl('', Validators.required),
      brand: new UntypedFormControl(''),
      details: new UntypedFormControl(''),
      category: new UntypedFormControl('', Validators.required),
      quantity: new UntypedFormControl(1, Validators.min(0)),
      measurement: new UntypedFormControl('ea', Validators.required),
      unitPrice: new UntypedFormControl(0),
      subtotal: new UntypedFormControl(0),
      isInCart: new UntypedFormControl(false)
    });
    return form;
  }
}
