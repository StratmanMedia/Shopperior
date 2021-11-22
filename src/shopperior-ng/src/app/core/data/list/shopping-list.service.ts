import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { constants } from 'src/app/shared/classes/constants';
import { LocalDataService } from '../local/local-data.service';
import { ShoppingListModel } from './models/shopping-list-model';

@Injectable({
  providedIn: 'root'
})
export class ShoppingListService {

  constructor(private local: LocalDataService) { }

  public getAll(): Observable<ShoppingListModel[]> {
    return new Observable(observer => {
      let savedLists = this.local.get<ShoppingListModel[]>(constants.storageKeys.shoppingLists);
      if (savedLists === null) {
        savedLists = [];
      }
      observer.next(savedLists);
      observer.complete();
    });
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
          this.local.set(constants.storageKeys.shoppingLists, savedLists);
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
        this.local.set(constants.storageKeys.shoppingLists, savedLists);
        observer.next();
        observer.complete();
      });
    })
  }
}
