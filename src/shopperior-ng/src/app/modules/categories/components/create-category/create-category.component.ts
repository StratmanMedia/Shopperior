import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CategoryModel } from 'src/app/core/data/category/models/category-model';
import { LoggingService } from 'src/app/core/logging/logging.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.scss']
})
export class CreateCategoryComponent implements OnInit {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: 'CreateCategoryComponent'
  });
  categoryForm: FormGroup = this.buildListForm();
  @Input() category: CategoryModel;
  @Output() onSave = new EventEmitter<CategoryModel>();
  @Output() onCancel = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  submitForm(): void {
    this.category.name = this.categoryForm.controls.name.value;
    this._logger.debug('Emitting category: ' + JSON.stringify(this.category));
    this.onSave.emit(this.category);
  }

  cancelForm(): void {
    this.onCancel.emit();
  }

  private buildListForm(): FormGroup {
    const form = new FormGroup({
      name: new FormControl(this.category, Validators.required)
    });
    return form;
  }

}
