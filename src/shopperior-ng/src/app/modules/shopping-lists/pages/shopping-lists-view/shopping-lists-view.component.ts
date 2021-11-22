import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ShoppingListModel } from 'src/app/core/data/list/models/shopping-list-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';

@Component({
  selector: 'app-shopping-lists-view',
  templateUrl: './shopping-lists-view.component.html',
  styleUrls: ['./shopping-lists-view.component.scss']
})
export class ShoppingListsViewComponent implements OnInit {

  private shoppingListsSubject = new BehaviorSubject<ShoppingListModel[]>([]);

  constructor(private shoppingListService: ShoppingListService) {
    this.shoppingListService.getAll().subscribe(lists => {
      this.shoppingListsSubject.next(lists);
    });
  }

  ngOnInit(): void {
  }

  public get shoppingLists(): ShoppingListModel[] {
    return this.shoppingListsSubject.value;
  }

  onDeleteListClick() {
    console.warn('delete list clicked');
  }
}
