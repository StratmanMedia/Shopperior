import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CategoryService } from 'src/app/core/data/category/category.service';
import { CategoryModel } from 'src/app/core/data/category/models/category-model';
import { LoggingService } from 'src/app/core/logging/logging.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.scss']
})
export class CreateCategoryComponent implements OnInit {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: 'CreateCategoryComponent'
  });
  categoryForm: FormGroup = this.buildListForm();

  constructor(
    private router: Router,
    private _categoryService: CategoryService) { }

  ngOnInit(): void {
  }

  submitForm(): void {
    const categoryModel = <CategoryModel>{
      name: this.categoryForm.controls.name.value
    };
    this._categoryService.add(categoryModel).subscribe(() => {
      this.router.navigateByUrl('/app/categories');
    });
  }

  private buildListForm(): FormGroup {
    const form = new FormGroup({
      name: new FormControl('', Validators.required)
    });
    return form;
  }

}
