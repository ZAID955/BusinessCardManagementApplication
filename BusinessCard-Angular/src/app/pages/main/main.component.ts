import { ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { MainServiceService } from 'src/app/backend/main-service.service';
import { cardDTO } from 'src/app/dtos/backendDTO/cardDTO';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, MatPaginatorIntl } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { ConfirmDialogDeleteComponent } from 'src/app/sheardcomponent/confirm-dialog-delete/confirm-dialog-delete.component';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {
displayedColumns: string[] = ['email', 'name', 'dateOfBirth', 'gendear', 'address'];
dataSource: MatTableDataSource<cardDTO>;
filteredCards: cardDTO[] = [];
myMainCard: cardDTO[] = [];
searchTerm: string = '';

@ViewChild(MatPaginator) paginator: MatPaginator = new MatPaginator(new MatPaginatorIntl(), ChangeDetectorRef.prototype);
@ViewChild(MatSort) sort: MatSort;

pageSize = 10;
currentPage = 0;
totalItems = 0;

constructor(public router:Router,
  public backend:MainServiceService,
  public dialog: MatDialog,
  public spinner:NgxSpinnerService,
  public toastr:ToastrService)
{
  this.dataSource = new MatTableDataSource()
  this.sort = new MatSort()
}

ngOnInit() {
  this.spinner.show();
  this.backend.getCards().subscribe(res => {
    this.myMainCard = res;
    this.totalItems = res.length;
    this.applyPagination();
    this.spinner.hide();
  }, err => {
    console.log(err);
  });
}
ngAfterViewInit(){
  this.dataSource.paginator = this.paginator;
  this.dataSource.sort = this.sort;
}


applyFilterWithBackend (searchTerm: string) {
  this.spinner.show();
  this.backend.filter(searchTerm).subscribe(res =>{
  this.myMainCard = res;
  this.totalItems = res.length;
  this.applyPagination();
  this.spinner.hide();
},err =>{
  this.spinner.hide();
})
}
ConfirmDialogToDelete(cardid:number){
  const dialogres = this.dialog.open(ConfirmDialogDeleteComponent, {
    width: '300px',
    height: '180px',
  });
  dialogres.afterClosed().subscribe(result =>{
    this.spinner.show();
    if (result) {
      this.backend.deleteCard(cardid).subscribe(res =>{
      },err =>{
        this.toastr.success('Business Card Has Been Deleted')
        this.backend.getCards().subscribe(res => {
          this.myMainCard = res;
          this.totalItems = res.length;
          this.applyPagination();
          this.spinner.hide();
        }, err => {
          console.log(err);
        });
      })
    } else {
      this.spinner.hide();
    }
  },err =>{

  })
}

applyPagination() {
  const start = this.currentPage * this.pageSize;
  const end = start + this.pageSize;
  this.filteredCards = this.myMainCard.slice(start, end);
}

applyFilter(event: Event) {
  const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();

  const filtered = this.myMainCard.filter(card =>
    (card.name?.toLowerCase() || '').includes(filterValue) ||
    (card.email?.toLowerCase() || '').includes(filterValue) ||
    (card.gendear?.toLowerCase() || '').includes(filterValue) ||
    (card.dateOfBirth?.toLowerCase() || '').includes(filterValue) ||
    (card.phone?.toLowerCase() || '').includes(filterValue)
  );

  this.totalItems = filtered.length;
  this.filteredCards = filtered.slice(0, this.pageSize);
}

handlePage(event: any) {
  this.currentPage = event.pageIndex;
  this.pageSize = event.pageSize;
  this.applyPagination();
}

}
