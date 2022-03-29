import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShoppingListsRoutingModule } from './shopping-lists-routing.module';
import { ShoppingListsViewComponent } from './pages/shopping-lists-view/shopping-lists-view.component';
import { MaterialModule } from 'src/app/core/material/material.module';
import { ShoppingListCreateComponent } from './pages/shopping-list-create/shopping-list-create.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ShoppingListItemsViewComponent } from './pages/shopping-list-items-view/shopping-list-items-view.component';
import { ListItemFormComponent } from './components/list-item-form/list-item-form.component';
import { ShoppingListSettingsComponent } from './pages/shopping-list-settings/shopping-list-settings.component';
import { UserListPermissionDialogComponent } from './components/user-list-permission-dialog/user-list-permission-dialog.component';
import { ListItemDialogComponent } from './components/list-item-dialog/list-item-dialog.component';
import { ShoppingListAddItemComponent } from './pages/shopping-list-add-item/shopping-list-add-item.component';
import { CategoryDialogComponent } from './components/category-dialog/category-dialog.component';
import { CategoriesModule } from '../categories/categories.module';

@NgModule({
  declarations: [
    ShoppingListsViewComponent,
    ShoppingListCreateComponent,
    ShoppingListItemsViewComponent,
    ListItemFormComponent,
    ShoppingListSettingsComponent,
    UserListPermissionDialogComponent,
    ListItemDialogComponent,
    ShoppingListAddItemComponent,
    CategoryDialogComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    CategoriesModule,
    ShoppingListsRoutingModule
  ]
})
export class ShoppingListsModule { }
