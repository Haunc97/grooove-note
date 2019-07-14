import { Injectable } from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { from, Observable } from 'rxjs';
import { Contact } from '../models/contact.model';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  constructor(private apiService:ApiService,private http:HttpClient) { }

  getContactList() : Observable<Contact[]>{
      return this.http.get<Contact[]>(`${this.apiService.auth.contact}`);
  }
}
