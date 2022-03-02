import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoriesRoutingModule } from './categories-routing.module';
import { CategoryListComponent } from './pages/category-list/category-list.component';
import { MaterialModule } from 'src/app/core/material/material.module';


@NgModule({
  declarations: [
    CategoryListComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    CategoriesRoutingModule
  ]
})
export class CategoriesModule { }
