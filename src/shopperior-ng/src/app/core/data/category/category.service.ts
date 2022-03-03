import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { constants } from 'src/app/shared/classes/constants';
import { environment } from 'src/environments/environment';
import { LoggingService } from '../../logging/logging.service';
import { LocalDataService } from '../local/local-data.service';
import { ShopperiorApiService } from '../shopperior-api/shopperior-api.service';
import { CategoryModel } from './models/category-model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: 'CategoryService'
  });
  private _categorySubject = new ReplaySubject<CategoryModel[]>(1);

  constructor(
    private _local: LocalDataService,
    private _api: ShopperiorApiService) {
      this.loadCategories();
      this._categorySubject.subscribe(
        (categories: CategoryModel[]) => {
          this._logger.debug(`Storing updated categories. # of categories: ${categories.length}`);
          this._local.set(constants.storageKeys.categories, categories);
        }
      );
    }

    getMine(): Observable<CategoryModel[]> {
      return this._api.Categories.getAllForUser();
    }

    add(category: CategoryModel): Observable<void> {
      return this._api.Categories.add(category);
    }

    private loadCategories(): void {
      this._logger.debug('Loading categories from API.');
      this._api.Categories.getAllForUser().subscribe(
        (categories: CategoryModel[]) => {
          this._categorySubject.next(categories);
        },
        (error: any) => {
          this._logger.debug('Could not load categories from API. Loading from local storage.');
          this._logger.error(error);
          const categories = this._local.get<CategoryModel[]>(constants.storageKeys.categories);
          this._categorySubject.next((!!categories) ? categories : []);
        }
      );
    }
}
