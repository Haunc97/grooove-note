import { Component, OnDestroy, Inject, OnInit } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { navItems } from '../../_nav';
import {Router, RouterModule} from '@angular/router'
import {CookieService} from 'ngx-cookie-service'
import {AuthService} from '../../services/auth.service';
@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent implements OnDestroy, OnInit {

  public navItems = navItems;
  public sidebarMinimized = true;
  private changes: MutationObserver;
  public element: HTMLElement;
  public user_email : string;
  constructor(private cookieService : CookieService,private authService:AuthService, private router:Router, @Inject(DOCUMENT) _document?: any) {

    this.changes = new MutationObserver((mutations) => {
      this.sidebarMinimized = _document.body.classList.contains('sidebar-minimized');
    });
    this.element = _document.body;
    this.changes.observe(<Element>this.element, {
      attributes: true,
      attributeFilter: ['class']
    });
  }
  ngOnInit(): void {
    const strCookie = this.cookieService.get(btoa('userInfo'));
    const LoginResult = JSON.parse(atob(strCookie));
    this.user_email = LoginResult.email;
  }
  onLogout(){
    console.log("ok")
    this.cookieService.deleteAll();
    this.authService.setLoggedIn(false);
    this.router.navigate(['/account/login']);
  }
  ngOnDestroy(): void {
    this.changes.disconnect();
  }
}
