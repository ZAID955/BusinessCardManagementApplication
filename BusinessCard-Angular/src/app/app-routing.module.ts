import { createComponent , NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './pages/main/main.component';
import { ErrorComponent } from './pages/error/error.component';
import { CreateCardComponent } from './pages/create-card/create-card.component';
import { ConfirmDialogDeleteComponent } from './sheardcomponent/confirm-dialog-delete/confirm-dialog-delete.component';
import { ConfirmDialogComponent } from './sheardcomponent/confirm-dialog/confirm-dialog.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent
  },
  {
    path: 'main',
    component: MainComponent
  },
  {
    path:'ConfirmDialogDelete',
    component: ConfirmDialogDeleteComponent
  },
  {
    path:'createCard',
    component: CreateCardComponent
  },
  {
    path:'error',
    component: ErrorComponent
  },
  {
    path:'errConfirmDialogComponentor',
    component: ConfirmDialogComponent
  },
  {
    path: '**',
    component: ErrorComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
