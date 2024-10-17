import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { MainServiceService } from 'src/app/backend/main-service.service';
import { createCardDTO } from 'src/app/dtos/backendDTO/createCardDTO';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.css']
})
export class ConfirmDialogComponent {
  input:createCardDTO = new createCardDTO()
  constructor(public router:Router,
    public spinner:NgxSpinnerService,
    public toastr:ToastrService,
    public backend:MainServiceService,
    public dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: createCardDTO
  ) {

  }

  createNewCard(){
    this.spinner.show()
    this.backend.createCard(this.data).subscribe(res=>{
      this.spinner.hide()
      this.router.navigate([''])
      this.toastr.success('Business Card Has Been Created')
    },err=>{
    })
  }
}
