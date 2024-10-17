import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MainServiceService } from 'src/app/backend/main-service.service';
import { createCardDTO } from 'src/app/dtos/backendDTO/createCardDTO';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/sheardcomponent/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-create-card',
  templateUrl: './create-card.component.html',
  styleUrls: ['./create-card.component.css']
})
export class CreateCardComponent {
  input:createCardDTO = new createCardDTO()
  attachment:File|undefined;
  constructor(public router:Router,
    public backend:MainServiceService,
    public dialog: MatDialog) {}

  ConfirmDialogToCreate(){
  this.dialog.open(ConfirmDialogComponent, {
    width: '500px',
    height: '420px',
    data:this.input
  });
}
onFileSelected(event: any) {
  if (event.target.files && event.target.files[0]) {
    this.attachment = event.target.files[0];
    if (this.attachment) {
      const reader = new FileReader();
      reader.onloadend = () => {
        const baseTypew = "data:image/webp;base64,";
        const base64Image = reader.result?.toString().split(',')[1] || '';
        this.input.photo = baseTypew + base64Image;
            };
      reader.readAsDataURL(this.attachment);
    }
  }
}
}
