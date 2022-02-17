import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Observable, ReplaySubject } from 'rxjs';
import { take, tap } from 'rxjs/operators';
import { ListItemModel } from 'src/app/core/data/list/models/list-tem-model';
import { ShoppingListModel } from 'src/app/core/data/list/models/shopping-list-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';
import { LoggingService } from 'src/app/core/logging/logging.service';
import { NavigationService } from 'src/app/core/navigation/navigation.service';
import { environment } from 'src/environments/environment';
import { ListItemDialogComponent } from '../../components/list-item-dialog/list-item-dialog.component';

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
  listItems = new ReplaySubject<ListItemModel[]>(1);
  cartItems = new ReplaySubject<ListItemModel[]>(1);

  constructor(
    private route: ActivatedRoute,
    private shoppingListService: ShoppingListService,
    private navService: NavigationService,
    public dialog: MatDialog,
    private _shoppingListService: ShoppingListService) { }

  ngOnInit(): void {
    this.guid = this.route.snapshot.params.list;
    this.shoppingList = this.shoppingListService.getOne(this.guid).pipe(
      tap(list => {
        let listItems = list.items.filter(i => !i.isInCart).sort((a, b) => a.name.localeCompare(b.name));
        let cartItems = list.items.filter(i => i.isInCart).sort((a, b) => a.name.localeCompare(b.name));
        this.listItems.next(listItems);
        this.cartItems.next(cartItems);
      })
    );
  }

  public goBack(): void {
    this.navService.goBack();
  }

  public openItemDialog(): void {
    const dialogRef = this.dialog.open(ListItemDialogComponent, {
      width: '100%',
      data: <ListItemModel>{
        shoppingListGuid: this.guid
      }
    });
    dialogRef.afterClosed().subscribe(data => {
      if (!!data) {
        this.addListItem(data);
      }
    });
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
