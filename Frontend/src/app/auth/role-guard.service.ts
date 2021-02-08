import {Injectable} from '@angular/core';
import {Router, CanActivate, ActivatedRouteSnapshot} from '@angular/router';
import decode from 'jwt-decode';
import {UtilsService} from '../shared/utils.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuardService implements CanActivate {

  constructor(public router: Router, private utils: UtilsService) {
  }

  canActivate(route: ActivatedRouteSnapshot): boolean {

    const expectedRole = route.data.expectedRole;
    const token = localStorage.getItem('token');
    const tokenPayload = decode(token);

    if (this.utils.isTokenExpired() || tokenPayload.role !== expectedRole) {
      this.router.navigateByUrl('login');
      return false;
    }
    return true;
  }
}
