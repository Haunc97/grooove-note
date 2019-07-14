import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor() { }
  baseUrl = "https://localhost:44330/api/";
  auth = {
    login : this.baseUrl + 'auth/login',
    register: this.baseUrl + 'auth/register',
    confirmEmail:this.baseUrl+ 'auth/confirmemail',
    fblogin: this.baseUrl + 'auth/external/facebook/login',
    user:this.baseUrl + 'users',
    note: this.baseUrl + 'note',
    notebook:this.baseUrl+'notebook',
    contact:this.baseUrl+'contact'
  };
  getApiUrl(name:string){
    return this.baseUrl + name;
  }
}
