import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { cardDTO } from '../dtos/backendDTO/cardDTO';
import { createCardDTO } from '../dtos/backendDTO/createCardDTO';
import { cardDetailsDTO } from '../dtos/backendDTO/cardDetailsDTO';

@Injectable({
  providedIn: 'root'
})
export class MainServiceService {

  private baseURL: string = 'https://localhost:7297';
  constructor(private http:HttpClient) { }

  getCards():Observable<cardDTO[]>{
    return this.http.get<cardDTO[]>(`${this.baseURL}/api/BusinessCard/GetAllBusinessCard`)
  }

createCard(input:createCardDTO):Observable<any>{
  const headers = new HttpHeaders({
    'Accept': 'text/plain'
  });
  return this.http.post(`${this.baseURL}/api/BusinessCard/CreateBusinessCard`,input, { headers, responseType: 'text' })
}
deleteCard(id:number):Observable<any>{
  return this.http.delete(`${this.baseURL}/api/BusinessCard/DeleteBusinessCard/${id}`)
}
filter(input:string):Observable<cardDTO[]>{
  return this.http.get<cardDTO[]>(`${this.baseURL}/api/BusinessCard/FilterOnBusinessCard?input=${input}`)
}




uploadImage(file: File) : Observable<any> {
  const formData: FormData = new FormData();
  formData.append('file', file, file.name);

  const headers = new HttpHeaders({
    'Accept': 'text/plain'
  });
  return this.http.post(`${this.baseURL}/api/Files/ImportImageToBase64`, formData, { headers, responseType: 'text' })
}

}
