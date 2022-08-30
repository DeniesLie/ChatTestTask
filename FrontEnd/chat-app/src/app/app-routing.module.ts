import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './features/auth/components/login/login.component';
import { MessengerPageComponent } from './features/messenger/components/messenger-page/messenger-page.component';
import { AuthGuard } from './guards/auth-guard/auth.guard';
import { UnauthGuard } from './guards/unauth-guard/unauth.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'messenger',
    pathMatch: 'full'
  },
  {
    path: "login",
    component: LoginComponent,
    canActivate: [UnauthGuard]
  },
  {
    path: "messenger",
    component: MessengerPageComponent,
    canActivate: [AuthGuard]
  }
]

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})

export class AppRoutingModule { }
