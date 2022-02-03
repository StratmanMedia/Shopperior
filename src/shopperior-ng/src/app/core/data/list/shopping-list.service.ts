import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { constants } from 'src/app/shared/classes/constants';
import { environment } from 'src/environments/environment';
import { LoggingService } from '../../logging/logging.service';
import { LocalDataService } from '../local/local-data.service';
import { ShopperiorApiService } from '../shopperior-api/shopperior-api.service';
import { ListItemModel } from './models/list-tem-model';
import { ShoppingListModel } from './models/shopping-list-model';

@Injectable({
  providedIn: 'root'
})
export class ShoppingListService {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: 'ShoppingListService'
  });
  private _listSubject = new ReplaySubject<ShoppingListModel[]>(1);

  constructor(
    private _local: LocalDataService,
    private _api: ShopperiorApiService) {
      this.loadLists();
      this._listSubject.subscribe(
        (lists: ShoppingListModel[]) => {
          this._local.set(constants.storageKeys.shoppingLists, lists);
        }
      );
  }

  public getAll(): Observable<ShoppingListModel[]> {
    return this._listSubject.asObservable();
  }

  public getOne(guid: string): Observable<ShoppingListModel> {
    return this._listSubject.pipe(
      take(1),
      map(lists => {
        const list = lists.find(l => l.guid === guid);
        return list;
      })
    );
  }

  public add(shoppingList: ShoppingListModel): Observable<void> {
    return this._listSubject.pipe(
      take(1),
      map(lists => {
        lists.push(shoppingList);
        this._listSubject.next(lists);
        this._api.ShoppingLists.add(shoppingList).subscribe();
      })
    );
  }

  public delete(guid: string): Observable<void> {
    return this._listSubject.pipe(
      take(1),
      map(lists => {
        const index = lists.findIndex(a => a.guid === guid);
        lists.splice(index, 1);
        this._listSubject.next(lists);
        this._api.ShoppingLists.delete(guid).pipe(take(1)).subscribe();
      })
    );
  }

  public update(shoppingList: ShoppingListModel): Observable<void> {
    return this._listSubject.pipe(
      take(1),
      map(lists => {
        let foundList = lists.find(l => l.guid === shoppingList.guid);
        foundList.name = shoppingList.name;
        foundList.permissions = shoppingList.permissions;
        foundList.items = shoppingList.items;
        this._listSubject.next(lists);
        this._api.ShoppingLists.update(shoppingList).subscribe();
      })
    );
  }

  public addItem(item: ListItemModel): Observable<void> {
    this._logger.debug(`Adding item. ${JSON.stringify(item)}`);
    return this._listSubject.pipe(
      take(1),
      map(lists => {
        let foundList = lists.find(l => l.guid === item.shoppingListGuid);
        this._logger.debug(`Found list ${foundList.guid}`);
        foundList.items.push(item);
        this._listSubject.next(lists);
        this._api.ShoppingLists.addItem(item).subscribe();
      })
    );
  }

  private loadLists(): void {
    this._logger.debug('Loading shopping lists from API.');
    this._api.ShoppingLists.getAll().subscribe(
      (lists: ShoppingListModel[]) => {
        this._listSubject.next(lists);
      },
      (error: any) => {
        this._logger.debug('Could not load shopping lists from API. Loading from local storage.');
        this._logger.error(error);
        const lists = this._local.get<ShoppingListModel[]>(constants.storageKeys.shoppingLists);
        this._listSubject.next(lists);
      }
    );
  }
}
