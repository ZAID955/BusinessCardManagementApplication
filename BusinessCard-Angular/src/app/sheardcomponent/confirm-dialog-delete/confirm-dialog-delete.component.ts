import { Component, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Route } from '@angular/router';
import { MainServiceService } from 'src/app/backend/main-service.service';

@Component({
  selector: 'app-confirm-dialog-delete',
  templateUrl: './confirm-dialog-delete.component.html',
  styleUrl: './confirm-dialog-delete.component.css'
})
export class ConfirmDialogDeleteComponent {

constructor(public dialogRef: MatDialogRef<ConfirmDialogDeleteComponent>,
  ) {}

  clickNo() {
    this.dialogRef.close(false)
  }

  clickYes(){
    this.dialogRef.close(true)
  }
}
