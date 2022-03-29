import { Injectable } from '@angular/core';
import { concat, Observable } from 'rxjs';
import { concatMap, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LoggingService } from '../../logging/logging.service';
import { ShoppingListService } from '../list/shopping-list.service';
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

  constructor(
    private _shoppingListService: ShoppingListService,
    private _api: ShopperiorApiService) { }

    add(category: CategoryModel): Observable<void> {
      this._logger.debug('Adding category: ' + JSON.stringify(category));
      var addCategory$ = this._api.Categories.add(category);
      var updateList$ = this._shoppingListService.getOne(category.shoppingListGuid).pipe(
        take(1),
        concatMap(list => {
          list.categories.push(category);
          return this._shoppingListService.update(list);
        })
      );
      return concat(addCategory$, updateList$);
    }
}
