import {Injectable} from '@angular/core';
import {JwtHelperService} from '@auth0/angular-jwt';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Router} from '@angular/router';
import {isNotNullOrUndefined} from 'codelyzer/util/isNotNullOrUndefined';

@Injectable({
  providedIn: 'root'
})
export class UtilsService {
  public SERVER_URL = 'https://localhost:44352/api/';

  constructor(private jwtHelper: JwtHelperService, private http: HttpClient, private router: Router) {
  }

  public logout() {
    localStorage.clear();
    this.router.navigateByUrl('');
  }

  public isTokenExpired() {
    return this.jwtHelper.isTokenExpired(localStorage.getItem('token'));
  }

  public sendPostRequest(postData, endpoint: string) {
    return this.http.post(endpoint, postData).toPromise().catch(error => error.error);
  }

  public sendGetRequest(endpoint: string) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get(endpoint, {headers}).toPromise().catch(error => error.error);
  }

  public noWhiteSpace(array: string[]) {
    return array.map(x => x.trim() && x.replace(/\s/g, ''));
  }

  public noWhiteSpaceText(text: string) {
    return text.trim().replace(/\s/g, '');
  }

  public notNullOrWhitespace(array: string[]) {
    return array.map(x => isNotNullOrUndefined(x) && x !== '');
  }
}
