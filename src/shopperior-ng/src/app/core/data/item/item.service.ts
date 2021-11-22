import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { constants } from 'src/app/shared/classes/constants';
import { Guid } from 'src/app/shared/classes/guid';
import { LocalDataService } from '../local/local-data.service';
import { ItemModel } from './models/item-model';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(private local: LocalDataService) { }

  getAll(): Observable<ItemModel[]> {
    return new Observable<ItemModel[]>(observer => {
      let savedItems = this.local.get<ItemModel[]>(constants.storageKeys.items);
      if (savedItems === null) {
        savedItems = [];
      }
      observer.next(savedItems);
      observer.complete();
    });
  }

  getOne(guid: string): Observable<ItemModel> {
    return new Observable(observer => {
      this.getAll().subscribe(savedItems => {
        const item = savedItems.find(i => i.guid === guid);
        observer.next(item);
        observer.complete();
      });
    });
  }

  add(item: ItemModel): Observable<ItemModel> {
    return new Observable(observer => {
      try {
        this.getAll().subscribe(savedItems => {
          item.guid = Guid.newGuid().toString();
          savedItems.push(item);
          this.local.set(constants.storageKeys.items, savedItems);
          observer.next(item);
          observer.complete();
        });
      } catch(ex) {
        observer.error(ex);
        observer.complete();
      }      
    });
  }

  getOrAdd(item: ItemModel): Observable<ItemModel> {
    return new Observable(observer => {
      this.getAll().subscribe(savedItems => {
        const foundItem = savedItems.find(i => 
          i.name.toLowerCase() === item.name.toLowerCase() &&
          i.details.toLowerCase() === item.details.toLowerCase() &&
          i.brand.toLowerCase() === item.brand.toLowerCase()
        );
        if (foundItem === undefined) {
          this.add(item).subscribe(newItem => {
            observer.next(newItem);
            observer.complete();
          });
        } else {
          observer.next(foundItem);
          observer.complete();
        }
      });
    });
  }

}
