import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { ShoppingListModel } from 'src/app/core/data/list/models/shopping-list-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';
import { LoggingService } from 'src/app/core/logging/logging.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-shopping-lists-view',
  templateUrl: './shopping-lists-view.component.html',
  styleUrls: ['./shopping-lists-view.component.scss']
})
export class ShoppingListsViewComponent implements OnInit {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: 'ShoppingListsViewComponent'
  });
  shoppingLists: Observable<ShoppingListModel[]>;

  constructor(private _shoppingListService: ShoppingListService) {
    this.shoppingLists = this._shoppingListService.getAll();
  }

  ngOnInit(): void {
  }

  deleteList(guid: string) {
    this._shoppingListService.delete(guid).pipe(take(1)).subscribe();
  }
}
