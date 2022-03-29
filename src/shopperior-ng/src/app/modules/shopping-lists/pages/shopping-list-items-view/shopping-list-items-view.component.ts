import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, ReplaySubject } from 'rxjs';
import { take, tap } from 'rxjs/operators';
import { ListItemModel } from 'src/app/core/data/list/models/list-tem-model';
import { ShoppingListModel } from 'src/app/core/data/list/models/shopping-list-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';
import { LoggingService } from 'src/app/core/logging/logging.service';
import { NavigationService } from 'src/app/core/navigation/navigation.service';
import { environment } from 'src/environments/environment';
import { ShoppingListItemsViewModel } from '../../models/shopping-list-items-view-model';

@Component({
  selector: 'app-shopping-list-items-view',
  templateUrl: './shopping-list-items-view.component.html',
  styleUrls: ['./shopping-list-items-view.component.scss']
})
export class ShoppingListItemsViewComponent implements OnInit {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: 'ShoppingListItemsViewComponent'
  });
  shoppingList = new Observable<ShoppingListModel>();
  guid: string;
  listVM = new ReplaySubject<ShoppingListItemsViewModel[]>(1);
  cartItems = new ReplaySubject<ListItemModel[]>(1);

  constructor(
    private route: ActivatedRoute,
    private shoppingListService: ShoppingListService,
    private navService: NavigationService,
    private _shoppingListService: ShoppingListService) { }

  ngOnInit(): void {
    this.guid = this.route.snapshot.params.list;
    this.shoppingList = this.shoppingListService.getOne(this.guid).pipe(
      tap(list => {
        let listItems = list.items.filter(i => !i.isInCart);
        listItems.sort((a, b) => a.name.localeCompare(b.name));
        let cartItems = list.items.filter(i => i.isInCart).sort((a, b) => a.name.localeCompare(b.name));
        const listVM = listItems.reduce((viewModels, item) => {
          const category = list.categories.find(c => c.guid === item.categoryGuid);
          if(!!category?.name) {
            let vm = viewModels.find(v => v.categoryName === category.name);
            if (!!vm) {
              vm.items.push(item);
              return viewModels;
            }
            vm = <ShoppingListItemsViewModel>{ categoryName: category.name, items: [] };
            vm.items.push(item);
            viewModels.push(vm);
            return viewModels;
          }
        }, new Array<ShoppingListItemsViewModel>());
        this.listVM.next(listVM.sort((a, b) => a.categoryName.localeCompare(b.categoryName)));
        this.cartItems.next(cartItems);
      })
    );
  }

  public goBack(): void {
    this.navService.goBack();
  }

  public updateListItem(item: ListItemModel): void {
    this._logger.debug(`Updating item. ${JSON.stringify(item)}`);
    this._shoppingListService.updateItem(item).pipe(
      tap(() => this._logger.debug(`Item updated.`))
    )
    .subscribe();
  }

  public checkout(): void {
    this.cartItems.pipe(
      take(1),
      tap(items => {
        for (let i=0; i < items.length; i++) {
          let item = items[i];
          item.hasPurchased = true;
          this._shoppingListService.updateItem(item).subscribe();
        };
      })
    )
    .subscribe();
  }

  private addListItem(item: ListItemModel): void {
    this._logger.debug(`Saving item. ${JSON.stringify(item)}`);
    this._shoppingListService.addItem(item).pipe(
      tap(() => this._logger.debug(`Item saved.`))
    )
    .subscribe();
  }
}
