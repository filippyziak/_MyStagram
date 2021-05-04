import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { UserAuth } from '../models/domain/auth/user-auth';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';
import { RegisterValidation } from '../enum/register-validation.enum';
import { ListenerService } from './listener.service';
import { ProfileService } from './profile.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
 private readonly baseUrl = environment.apiUrl + 'auth/';

  jwtHelper = new JwtHelperService();

  currentUser: UserAuth;
  decodedToken: any;

  private loggedIn = new BehaviorSubject(this.isLoggedIn());
  currentLoggedIn = this.loggedIn.asObservable();

  constructor(private httpClient: HttpClient, private router: Router, private listener: ListenerService,
    private profileService: ProfileService) { }

  public login(user: any) {
    return this.httpClient.post(this.baseUrl + 'login', user, { observe: 'response' })
      .pipe(
        map((res: any) => {
          const response = res.body;
          if (response && response.isSucceeded) {
            localStorage.setItem('token', response.token);
            this.decodedToken = this.jwtHelper.decodeToken(response.token);
            this.currentUser = response.user;

            this.listener.changeCurrentUser(this.currentUser);
            this.isLoggedInEmit();
          }
        })
      );
  }

  public register(user: any) {
    return this.httpClient.post(this.baseUrl + 'register', user);
  }

  public logout() {
    localStorage.clear();

    this.decodedToken = null;
    this.currentUser = null;
    this.isLoggedInEmit();
    this.router.navigate([' ']);
  }

  public confirmAccount(userId: string, token: string) {
    return this.httpClient.get(this.baseUrl + 'account/confirm', { params: { userId: userId.toString(), token }, responseType: 'text' });
  }

  public resetPassword(request: any) {
    return this.httpClient.patch(this.baseUrl + 'account/resetPassword', request);
  }

  public sendResetPassword(request: any) {
    return this.httpClient.post(this.baseUrl + 'account/resetPassword/send', request);
  }

  public confirmResetPassword(userId: string, token: string) {
    return this.httpClient.get(this.baseUrl + 'account/resetPassword/confirm', { params: { userId, token } });
  }

  public getRegisterValidation(registerValidation: RegisterValidation, content: string) {
    return this.httpClient.get(this.baseUrl + 'register/validate',
      { params: { registerValidation: registerValidation.toString(), content } });
  }


  public isLoggedIn() {
    return !!this.decodedToken;
  }

  public isLoggedInEmit() {
    this.loggedIn.next(this.isLoggedIn());
  }

  public permissionsCheck(roles: string[]) {
    let isPermitted = false;
    const userRoles = this.decodedToken.role as string[];

    if (!userRoles) {
      return false;
    }

    for (const role of roles) {
      if (userRoles.includes(role)) {
        isPermitted = true;
        break;
      }
    }

    return isPermitted;
  }
}
