import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

//Module
import {ReactiveFormsModule,FormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';

// Extensions
import { CookieService } from 'ngx-cookie-service';
import { NgxLoadingModule } from 'ngx-loading';
//import { LoadingBarHttpClientModule } from '@ngx-loading-bar/http-client';
// Service
import {AuthService} from '../../src/app/services/auth.service';
import {ApiService} from '../../src/app/services/api.service';
import { ContactService } from './services/contact.service';
const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

import { AppComponent } from './app.component';

// Import containers
import { DefaultLayoutComponent } from './containers';

import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
// Import 3rd party components
import { AuthGuard } from './auth.guard';
import {RequestInterceptor} from './app.interceptor'
const APP_CONTAINERS = [
  DefaultLayoutComponent
];

import {
  AppAsideModule,
  AppBreadcrumbModule,
  AppHeaderModule,
  AppFooterModule,
  AppSidebarModule,
} from '@coreui/angular';

// Import routing module
import { AppRoutingModule} from './app.routing';

// Import 3rd party components
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { RegisterComponent } from './views/register/register.component';
import { ConfirmEmailComponent } from './views/confirm-email/confirm-email.component';
import { ChatComponent } from './views/chat/chat.component';


@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AppAsideModule,
    AppBreadcrumbModule.forRoot(),
    AppFooterModule,
    AppHeaderModule,
    AppSidebarModule,
    PerfectScrollbarModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    NgxLoadingModule.forRoot({}),
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ToastrModule.forRoot() // ToastrModule added
  ],
  declarations: [
    AppComponent,
    ...APP_CONTAINERS,
    P404Component,
    P500Component,
    LoginComponent,
    RegisterComponent,
    ConfirmEmailComponent,
    ChatComponent
  ],
  providers: [{
    provide: LocationStrategy,
    useClass: HashLocationStrategy
  },{provide:HTTP_INTERCEPTORS,useClass: RequestInterceptor,multi:true},CookieService,AuthService,ApiService,AuthGuard,ContactService],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
