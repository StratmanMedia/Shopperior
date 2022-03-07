import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ShoppingListAddItemComponent } from './pages/shopping-list-add-item/shopping-list-add-item.component';
import { ShoppingListCreateComponent } from './pages/shopping-list-create/shopping-list-create.component';
import { ShoppingListItemsViewComponent } from './pages/shopping-list-items-view/shopping-list-items-view.component';
import { ShoppingListSettingsComponent } from './pages/shopping-list-settings/shopping-list-settings.component';
import { ShoppingListsViewComponent } from './pages/shopping-lists-view/shopping-lists-view.component';

const routes: Routes = [
  { path: '', component: ShoppingListsViewComponent },
  { path: 'create', component: ShoppingListCreateComponent },
  { path: ':list', component: ShoppingListItemsViewComponent },
  { path: ':list/settings', component: ShoppingListSettingsComponent },
  { path: ':list/items/add', component: ShoppingListAddItemComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ShoppingListsRoutingModule { }
