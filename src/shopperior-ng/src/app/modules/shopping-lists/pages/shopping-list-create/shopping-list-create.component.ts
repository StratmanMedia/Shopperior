import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ShoppingListModel } from 'src/app/core/data/list/models/shopping-list-model';
import { ShoppingListService } from 'src/app/core/data/list/shopping-list.service';
import { LoggingService } from 'src/app/core/logging/logging.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-shopping-list-create',
  templateUrl: './shopping-list-create.component.html',
  styleUrls: ['./shopping-list-create.component.scss']
})
export class ShoppingListCreateComponent implements OnInit {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: typeof ShoppingListCreateComponent
  });
  listForm: FormGroup = this.buildListForm();

  constructor(
    private shoppingListService: ShoppingListService,
    private router: Router) { }

  ngOnInit(): void {
  }

  submitForm(): void {
    const shoppingListModel = <ShoppingListModel>{
      name: this.listForm.controls.name.value
    };
    this.shoppingListService.add(shoppingListModel).subscribe(() => {
      this.router.navigateByUrl('/app/lists');
    });
  }

  private buildListForm(): FormGroup {
    const form = new FormGroup({
      name: new FormControl('', Validators.required)
    });
    return form;
  }
}
