import {Component, OnInit} from '@angular/core';
import {FormBuilder} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {JwtHelperService} from '@auth0/angular-jwt';
import {UtilsService} from '../shared/utils.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm;

  constructor(public fb: FormBuilder, public http: HttpClient, public router: Router, public jwtHelper: JwtHelperService, private utils: UtilsService) {
    this.registerForm = this.fb.group({
      username: [''],
      email: [''],
      firstName: [''],
      lastName: [''],
      password: [''],
      recaptcha: ['']
    });
  }

  ngOnInit(): void {
  }

  async onSubmit() {
    const formData = new FormData();
    let dataFields = [
      this.registerForm.get('username').value,
      this.registerForm.get('email').value,
      this.registerForm.get('firstName').value,
      this.registerForm.get('lastName').value,
      this.registerForm.get('password').value
    ];

    // Bellow is kinda overkill validation and easily bypassed, this kind of validation should be left to the server, included for example purposes

    dataFields = this.utils.noWhiteSpace(dataFields);
    if (!this.utils.notNullOrWhitespace(dataFields).includes(false)) {
      formData.append('username', dataFields[0]);
      formData.append('email', dataFields[1]);
      formData.append('firstName', dataFields[2]);
      formData.append('lastName', dataFields[3]);
      formData.append('password', dataFields[4]);
      formData.append('recaptcha', this.registerForm.get('recaptcha').value);

      console.log(dataFields);
      const response = await this.utils.sendPostRequest(formData, this.utils.SERVER_URL + 'register');

      if (response.success) {
        alert('You may now login, ' + dataFields[0] + '.');
        await this.router.navigateByUrl('login');
      } else {
        alert(JSON.stringify(response.errors));
      }
    }
  }
}


