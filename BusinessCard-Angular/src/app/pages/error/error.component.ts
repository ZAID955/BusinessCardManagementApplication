import { Component } from '@angular/core';
import { MainServiceService } from 'src/app/backend/main-service.service';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css']
})
export class ErrorComponent {
  constructor(public backend:MainServiceService){}
}
