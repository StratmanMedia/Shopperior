import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShoppingListsRoutingModule } from './shopping-lists-routing.module';
import { ShoppingListsViewComponent } from './pages/shopping-lists-view/shopping-lists-view.component';
import { MaterialModule } from 'src/app/core/material/material.module';
import { ShoppingListCreateComponent } from './pages/shopping-list-create/shopping-list-create.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ShoppingListItemsViewComponent } from './pages/shopping-list-items-view/shopping-list-items-view.component';
import { ShoppingListItemAddComponent } from './pages/shopping-list-item-add/shopping-list-item-add.component';
import { ListItemFormComponent } from './components/list-item-form/list-item-form.component';

@NgModule({
  declarations: [
    ShoppingListsViewComponent,
    ShoppingListCreateComponent,
    ShoppingListItemsViewComponent,
    ShoppingListItemAddComponent,
    ListItemFormComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    ShoppingListsRoutingModule
  ]
})
export class ShoppingListsModule { }
