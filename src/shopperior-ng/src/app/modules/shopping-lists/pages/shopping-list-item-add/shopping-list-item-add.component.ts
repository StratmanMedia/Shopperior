import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { ShoppingListModel } from 'src/app/core/data/list/models/shopping-list-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';
import { NavigationService } from 'src/app/core/navigation/navigation.service';

@Component({
  selector: 'app-shopping-list-item-add',
  templateUrl: './shopping-list-item-add.component.html',
  styleUrls: ['./shopping-list-item-add.component.scss']
})
export class ShoppingListItemAddComponent implements OnInit {

  listGuid: string;
  shoppingList = new Observable<ShoppingListModel>();
  
  constructor(
    private route: ActivatedRoute,
    private shoppingListService: ShoppingListService,
    private navService: NavigationService) {

      this.listGuid = this.route.snapshot.params.list;
      this.shoppingList = this.shoppingListService.getOne(this.listGuid);
  }

  ngOnInit(): void {
  }

  goBack(): void {
    this.navService.goBack();
  }

}
