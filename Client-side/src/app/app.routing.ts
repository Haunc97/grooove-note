import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { DefaultLayoutComponent } from './containers';

import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
import { AuthGuard } from './auth.guard';
import { RegisterComponent } from './views/register/register.component';
import { ConfirmEmailComponent } from './views/confirm-email/confirm-email.component';
import { ChatComponent } from './views/chat/chat.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  {
    path: '404',
    component: P404Component,
    data: {
      title: 'Page 404'
    }
  },
  {
    path: '500',
    component: P500Component,
    data: {
      title: 'Page 500'
    }
  },
  {
    path: 'account/login',
    component: LoginComponent,
    data: {
      title: 'Login Page'
    }
  },
  {
    path:'account/register',
    component: RegisterComponent,
    data:{
      title : 'Register page'
    }
  },
  {
    path:'email-confirmation',
    component: ConfirmEmailComponent
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Home'
    },
    canActivate:[AuthGuard],
    children: [
      {
        path:'notes',
        loadChildren: () => import('./views/note/note.module').then(m=>m.NoteModule)
      },
      {
        path:'chat',
        component: ChatComponent
      },
      {
        path: 'dashboard',
        loadChildren: () => import('./views/dashboard/dashboard.module').then(m => m.DashboardModule)
      }
    ]
  },
  { path: '**', component: P404Component }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
