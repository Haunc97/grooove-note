import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { Observable } from "rxjs";
import { CookieService } from "ngx-cookie-service";
import {Router} from '@angular/router'
import {tap} from 'rxjs/operators/tap'
import { AuthService } from "./services/auth.service";
@Injectable()

export class RequestInterceptor implements HttpInterceptor{
    constructor(private router:Router, private cookieSerivce:CookieService,private authService:AuthService){}
    intercept(request:HttpRequest<any>,next:HttpHandler):Observable<HttpEvent<any>>{

        const strCookie = this.cookieSerivce.get(btoa('userInfo'));
        if(strCookie){
            const LoginResult = JSON.parse(atob(strCookie));
            if(LoginResult){
                const token = LoginResult.token;
                const req = request.clone({
                    headers:new HttpHeaders().append('Authorization',"Bearer " + token)
                })
                return next.handle(req).pipe(
                    tap(event=>{},err =>{
                        if(err instanceof HttpErrorResponse){
                            if(this.router.url!=='account/login'&&(err.status===401)){
                                window.alert("Phiên làm việc hết hạn. Vui lòng đăng nhập lại.")
                                this.authService.setLoggedIn(false);
                                this.router.navigate(['account/login']);
                                window.location.reload();
                            }
                        }
                    })
                );
            }
        }
        return next.handle(request);
    }
}