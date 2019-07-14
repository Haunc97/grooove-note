import { Injectable } from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Register} from '../models/register.model';
import { ConfirmEmail } from '../models/confirm.email.model';
@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private apiService:ApiService,private http:HttpClient) { }

  register(model:Register): Observable<any>{
     return this.http.post(`${this.apiService.auth.register}`,model);
  }
  confirmEmail(model:ConfirmEmail):Observable<any>{
    return this.http.post(`${this.apiService.auth.confirmEmail}`,model);
  }
}
