import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router'
import { AuthService, LoginInfo, FacebookAuthInfo } from '../../services/auth.service';
declare var FB: any;
@Component({
  selector: 'app-dashboard',
  templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit {
  constructor(private router: Router, private http: HttpClient, private authservice: AuthService, private fb: FormBuilder, private cookieService: CookieService) {

  }
  LoginForm: FormGroup;
  data: LoginInfo = {} as LoginInfo;
  fbAuthData : FacebookAuthInfo = {} as FacebookAuthInfo;
  Isloginfail = false;
  loading = false;
  ngOnInit() {
    (window as any).fbAsyncInit = function () {
      FB.init({
        appId: '345985072746368',
        cookie: true,
        xfbml: true,
        version: 'v3.1'
      });
      FB.AppEvents.logPageView();
    };

    (function (d, s, id) {
      var js, fjs = d.getElementsByTagName(s)[0];
      if (d.getElementById(id)) { return; }
      js = d.createElement(s); js.id = id;
      js.src = "https://connect.facebook.net/en_US/sdk.js";
      fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));

    this.LoginForm = this.fb.group({
      username: [this.data.Username, Validators.compose([Validators.required])],
      password: [this.data.Password, Validators.compose([Validators.required])]
    })
  }
  onLoginFormSubmit() {
    this.loading = true;
    this.authservice.login(this.data).subscribe(res => {
      this.loading = false;
      this.Isloginfail = false;
      // Save to cookies
      this.cookieService.set(btoa('userInfo'), btoa(JSON.stringify(res)));
      // Set loggedIn status
      this.authservice.setLoggedIn(true);
      // Ridirect to home page
      this.router.navigate(['/dashboard']);
    }, err => { console.log(err), this.Isloginfail = true, this.loading = false })
  }
  private authWindow: Window;
  failed: boolean;
  error: string;
  errorDescription: string;
  isRequesting: boolean;

  // launchFbLogin() {
  //     // launch facebook login dialog
  //     this.authWindow = window.open('https://www.facebook.com/v2.11/dialog/oauth?&response_type=token&display=popup&client_id=345985072746368&display=popup&redirect_uri=http://localhost:4200/abc&scope=email', null, 'width=600,height=400');
  // }
  launchFbLogin() {
    console.log("submit login to facebook");
    // FB.login();
    FB.login((response) => {
      console.log('submitLogin', response);
      if (response.authResponse) {
        this.fbAuthData.AccessToken = response.authResponse.accessToken;
        this.authservice.loginByFacebook(this.fbAuthData).subscribe(async res=>{
          this.loading = false;
          this.Isloginfail = false;
          // Save to cookies
          this.cookieService.set(btoa('userInfo'), btoa(JSON.stringify(res)));
          // Set loggedIn status
          this.authservice.setLoggedIn(true);
          // Ridirect to home page
          await this.router.navigate(['/dashboard']);
          window.location.reload();
        }, err => { console.log(err), this.Isloginfail = true, this.loading = false });
      }
      else {
        console.log('User login failed');
      }
    });
  }
}
