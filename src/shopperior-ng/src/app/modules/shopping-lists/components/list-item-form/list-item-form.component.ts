import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { ItemModel } from 'src/app/core/data/item/models/item-model';
import { ItemService } from 'src/app/core/data/item/item.service';
import { ListItemModel } from 'src/app/core/data/list/models/list-tem-model';
import { ShoppingListModel } from 'src/app/core/data/list/models/shopping-list-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';
import { LoggingService } from 'src/app/core/logging/logging.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-list-item-form',
  templateUrl: './list-item-form.component.html',
  styleUrls: ['./list-item-form.component.scss']
})
export class ListItemFormComponent implements OnInit {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: typeof ListItemFormComponent
  });
  itemForm: FormGroup = this.buildItemForm();
  private shoppingListSubject = new BehaviorSubject<ShoppingListModel>(null);
  
  constructor(
    private route: ActivatedRoute,
    private shoppingListService: ShoppingListService,
    private itemService: ItemService,
    private router: Router) {

    this.shoppingListService.getOne(this.route.snapshot.params.list).subscribe(list => {
      this.shoppingListSubject.next(list);
    });
  }

  ngOnInit(): void {
  }

  get shoppingList(): ShoppingListModel {
    return this.shoppingListSubject.value;
  }

  onSubmit(): void {
    this._logger.warn(this.itemForm.value);
    const formItem = <ItemModel>{
      guid: '',
      name: this.itemForm.controls.name.value,
      details: this.itemForm.controls.details.value,
      brand: this.itemForm.controls.brand.value
    }
    this.itemService.getOrAdd(formItem).subscribe(item => {
      let items = (this.shoppingList.items !== null) ? this.shoppingList.items : [];
      const listItem = <ListItemModel>{
        item: item,
        location: this.itemForm.controls.location.value,
        quantity: this.itemForm.controls.quantity.value,
        units: this.itemForm.controls.units.value,
        unitPrice: this.itemForm.controls.unitPrice.value,
        subtotal: this.itemForm.controls.subtotal.value
      };
      items.push(listItem);
      const shoppingListModel: ShoppingListModel = {
        guid: this.shoppingList.guid,
        name: this.shoppingList.name,
        description: this.shoppingList.description,
        items: items
      };
      this.shoppingListService.update(shoppingListModel).subscribe(() => {
        this.router.navigateByUrl(`/lists/${this.shoppingList.guid}`);
      });
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

  private buildItemForm(): FormGroup {
    const form = new FormGroup({
      name: new FormControl('', Validators.required),
      details: new FormControl(''),
      brand: new FormControl(''),
      location: new FormControl(''),
      quantity: new FormControl(0),
      units: new FormControl(''),
      unitPrice: new FormControl(0),
      subtotal: new FormControl(0)
    });
    return form;
  }
}
