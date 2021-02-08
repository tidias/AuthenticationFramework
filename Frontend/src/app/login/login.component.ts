import {Component, OnInit} from '@angular/core';
import {FormBuilder} from '@angular/forms';
import {Router} from '@angular/router';
import {UtilsService} from '../shared/utils.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm;
  apiResponse;

  constructor(public fb: FormBuilder, public router: Router, private utils: UtilsService, ) {
    this.loginForm = this.fb.group({
      username: [''],
      password: [''],
      recaptcha: ['']
    });
  }

  ngOnInit(): void {
  }



  async onSubmit() {
    const formData = new FormData();
    let dataFields = [
      this.loginForm.get('username').value,
      this.loginForm.get('password').value];

    // Bellow is kinda overkill validation and easily bypassed, this kind of validation should be left to the server, included for example purposes

    dataFields = this.utils.noWhiteSpace(dataFields);
    if (!this.utils.notNullOrWhitespace(dataFields).includes(false)) {
      formData.append('username', dataFields[0]);
      formData.append('password', dataFields[1]);
      formData.append('ReCaptchaToken', this.loginForm.get('recaptcha').value);

      const response = await this.utils.sendPostRequest(formData, this.utils.SERVER_URL + 'login');

      if (response.success) {
        localStorage.setItem('token', response.token);
        await this.router.navigateByUrl('welcome');
      } else {
        alert(JSON.stringify(response.errors));
      }
    }
  }


}

