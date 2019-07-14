import { Injectable } from '@angular/core';
import {ApiService} from './api.service';
import {HttpClient} from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
export interface LoginInfo{
  Username:string;
  Password:string;
}
export interface FacebookAuthInfo{
  AccessToken:string;
}
@Injectable({
  providedIn: 'root'
})
export class AuthService {
 private loggedInStatus = JSON.parse(atob(atob(atob(this.cookieService.get(btoa(btoa('IsLogin'))))))||'false');
  constructor(private api:ApiService,private cookieService:CookieService,private http:HttpClient ) { }
  setLoggedIn(value:boolean){
    this.loggedInStatus = value;
    this.cookieService.set(btoa(btoa('IsLogin')),btoa(btoa(btoa(value.toString()))));
  }
  get isLoggedIn(){
    return this.loggedInStatus;
  }
  login(loginInfo:LoginInfo){
    return this.http.post(this.api.auth.login,loginInfo,{
      observe:'body'
    });
  }
  loginByFacebook(fbAuthInfo:FacebookAuthInfo){
    return this.http.post(this.api.auth.fblogin,fbAuthInfo,{
      observe:'body'
    })
  }
}
