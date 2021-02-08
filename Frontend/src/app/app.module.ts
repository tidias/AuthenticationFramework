import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {LoginComponent} from './login/login.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatCardModule} from '@angular/material/card';
import {RecaptchaModule, RecaptchaFormsModule} from 'ng-recaptcha';
import {HttpClientModule} from '@angular/common/http';
import {JwtHelperService, JWT_OPTIONS} from '@auth0/angular-jwt';
import {RegisterComponent} from './register/register.component';
import {WelcomeComponent} from './welcome/welcome.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    WelcomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    FormsModule,
    MatSidenavModule,
    MatCardModule,
    RecaptchaModule,
    RecaptchaFormsModule,
    HttpClientModule
  ],
  providers: [{provide: JWT_OPTIONS, useValue: JWT_OPTIONS}, JwtHelperService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
