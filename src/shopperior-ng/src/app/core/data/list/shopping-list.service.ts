import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
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
    return new Observable(observer => {
      this.getAll().subscribe(savedLists => {
        const list = savedLists.find(l => l.guid === guid);
        observer.next(list);
        observer.complete();
      });
    })
  }

  public add(shoppingList: ShoppingListModel): Observable<void> {
    return new Observable(observer => {
      try {
        this.getAll().subscribe(savedLists => {
          savedLists.push(shoppingList);
          this._local.set(constants.storageKeys.shoppingLists, savedLists);
          observer.next();
          observer.complete();
        });
      } catch(ex) {
        observer.error(ex);
        observer.complete();
      }      
    });
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
    this._api.ShoppingLists.getAll().subscribe(
      (lists: ShoppingListModel[]) => {
        this._listSubject.next(lists);
      },
      (error: any) => {
        this._logger.debug('Could not load shopping lists form API.');
        const lists = this._local.get<ShoppingListModel[]>(constants.storageKeys.shoppingLists);
        this._listSubject.next(lists);
      }
    );
  }
}
