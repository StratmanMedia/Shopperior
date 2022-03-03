import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryService } from 'src/app/core/data/category/category.service';
import { CategoryModel } from 'src/app/core/data/category/models/category-model';
import { LoggingService } from 'src/app/core/logging/logging.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: 'CategoryListComponent'
  });
  categories: Observable<CategoryModel[]>;
  
  constructor(private _categoryService: CategoryService) {
    this.categories = this._categoryService.getMine();
  }

  ngOnInit(): void {
  }

}
