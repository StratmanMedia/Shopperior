import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
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

  constructor(
    private route: ActivatedRoute,
    private shoppingListService: ShoppingListService,
    private navService: NavigationService,
    public dialog: MatDialog,
    private _shoppingListService: ShoppingListService) {

    this.guid = this.route.snapshot.params.list;
    this.shoppingList = this.shoppingListService.getOne(this.guid);
  }

  ngOnInit(): void {
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
        this._logger.debug(`Saving item. ${JSON.stringify(data)}`);
        this._shoppingListService.addItem(data).pipe(
          tap(() => this._logger.debug(`Item saved.`))
        )
        .subscribe();
      }
    });
  }
}
