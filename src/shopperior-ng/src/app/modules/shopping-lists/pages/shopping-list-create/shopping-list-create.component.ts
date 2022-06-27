import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
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
  listForm: UntypedFormGroup = this.buildListForm();

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

  private buildListForm(): UntypedFormGroup {
    const form = new UntypedFormGroup({
      name: new UntypedFormControl('', Validators.required)
    });
    return form;
  }
}
