import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryListComponent } from './pages/category-list/category-list.component';
import { MaterialModule } from 'src/app/core/material/material.module';
import { CreateCategoryComponent } from './components/create-category/create-category.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    CategoryListComponent,
    CreateCategoryComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule
  ],
  exports: [
    CreateCategoryComponent
  ]
})
export class CategoriesModule { }
