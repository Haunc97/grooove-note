import { Injectable } from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import { from, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotebookService {

  constructor(private apiService:ApiService,private http:HttpClient) { }

  getNotebookList() : Observable<any>{
      return this.http.get(`${this.apiService.auth.notebook}`);
  }
}
