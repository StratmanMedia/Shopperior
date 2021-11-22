import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { ShoppingListModel } from 'src/app/core/data/list/models/shopping-list-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';
import { NavigationService } from 'src/app/core/navigation/navigation.service';

@Component({
  selector: 'app-shopping-list-item-add',
  templateUrl: './shopping-list-item-add.component.html',
  styleUrls: ['./shopping-list-item-add.component.scss']
})
export class ShoppingListItemAddComponent implements OnInit {

  private shoppingListSubject = new BehaviorSubject<ShoppingListModel>(null);
  
  constructor(
    private route: ActivatedRoute,
    private shoppingListService: ShoppingListService,
    private navService: NavigationService) {

    this.shoppingListService.getOne(this.route.snapshot.params.list).subscribe(list => {
      this.shoppingListSubject.next(list);
    });
  }

  ngOnInit(): void {
  }

  get shoppingList(): ShoppingListModel {
    return this.shoppingListSubject.value;
  }

  goBack(): void {
    this.navService.goBack();
  }

}
