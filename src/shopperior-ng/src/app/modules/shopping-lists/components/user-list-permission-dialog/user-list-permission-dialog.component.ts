import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserListPermissionModel } from 'src/app/core/data/list/models/user-list-permission-model';
import { UserService } from 'src/app/core/data/user/user.service';

@Component({
  selector: 'app-user-list-permission-dialog',
  templateUrl: './user-list-permission-dialog.component.html',
  styleUrls: ['./user-list-permission-dialog.component.scss']
})
export class UserListPermissionDialogComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<UserListPermissionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserListPermissionModel,
    private userService: UserService) { }

  ngOnInit(): void {
  }

  cancel(): void {
    this.dialogRef.close();
  }

  add(): void {
    this.userService.getOneByEmail(this.data.userEmail).subscribe(
      user => {
        this.data.userGuid = user.guid;
        this.dialogRef.close(this.data);
      },
      err => {
        this.dialogRef.close();
      }
    );
  }
}
