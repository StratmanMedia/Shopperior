import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { constants } from 'src/app/shared/classes/constants';
import { environment } from 'src/environments/environment';
import { LoggingService } from '../../logging/logging.service';
import { LocalDataService } from '../local/local-data.service';
import { ShopperiorApiService } from '../shopperior-api/shopperior-api.service';
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
      map(lists => {
        const list = lists.find(l => l.guid === guid);
        return list;
      })
    );
  }

  public add(shoppingList: ShoppingListModel): Observable<void> {
    return this._listSubject.pipe(
      map(lists => {
        lists.push(shoppingList);
        this._listSubject.next(lists);
      })
    );
  }

  public update(shoppingList: ShoppingListModel): Observable<void> {
    return new Observable(observer => {
      this.getAll().subscribe(savedLists => {
        let foundList = savedLists.find(l => l.guid === shoppingList.guid);
        foundList.name = shoppingList.name;
        foundList.description = shoppingList.description;
        foundList.items = shoppingList.items;
        this._local.set(constants.storageKeys.shoppingLists, savedLists);
        observer.next();
        observer.complete();
      });
    });
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
