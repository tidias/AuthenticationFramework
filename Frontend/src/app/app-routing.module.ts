import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {LoginComponent} from './login/login.component';
import {AuthGuardService} from './auth/auth-guard.service';
import {RoleGuardService} from './auth/role-guard.service';
import {RegisterComponent} from './register/register.component';
import {WelcomeComponent} from './welcome/welcome.component';


const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'welcome', component: WelcomeComponent, canActivate: [RoleGuardService], data: {expectedRole: 'user'}}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
