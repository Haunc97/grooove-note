import { Injectable } from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { from, Observable } from 'rxjs';
import {Note} from '../models/note.model';

@Injectable({
  providedIn: 'root'
})
export class NoteService {

  constructor(private apiService:ApiService,private http:HttpClient) { }

  getNoteList() : Observable<Note[]>{
      return this.http.get<Note[]>(`${this.apiService.auth.note}`);
  }
  getSingleNote(id : number):Observable<Note>{
    return this.http.get<Note>(`${this.apiService.auth.note}/${id}`);
  }
  saveNote(note:Note) : Observable<Note>{
    const httpOptions = {headers:new HttpHeaders({
      'Content-Type':'application/json'
    })};
    if(note.id){
      return this.http.put<Note>(`${this.apiService.auth.note}/${note.id}`,note,httpOptions)
    }else{
      // add new here.
      return this.http.post<Note>(this.apiService.auth.note,note,httpOptions);
    }
  }
  deleteNote(id:number){
    return this.http.delete(`${this.apiService.auth.note}/${id}`);
  }
}
