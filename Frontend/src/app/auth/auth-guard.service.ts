import {Injectable} from '@angular/core';
import {Router, CanActivate, CanLoad} from '@angular/router';
import {UtilsService} from '../shared/utils.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor( public router: Router, private utils: UtilsService) {

  }

  canActivate(): boolean {
    if (this.utils.isTokenExpired()) {
      return false;
    }
    return true;
  }


}
