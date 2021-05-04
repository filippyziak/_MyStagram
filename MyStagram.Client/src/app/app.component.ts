import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './services/auth.service';
import { UserAuth } from './models/domain/auth/user-auth';
import { Notifier } from './services/notifier.service';
import { ListenerService } from './services/listener.service';
import { FollowersRequest } from './resolvers/requests/followers-request';
import { FollowersService } from './services/followers.service';
import { SignalRService } from './services/signalR.service';
import { SIGNALR_ACTIONS } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  jwtHelper = new JwtHelperService();

  constructor(private authService: AuthService, private notifier: Notifier, private listener: ListenerService,
    private followersService: FollowersService, private signalR: SignalRService) {

  }

  ngOnInit() {
    this.validateAuthorization();
    this.subscribeData();
  }

  private validateAuthorization() {
    const token = localStorage.getItem('token');
    const user: UserAuth = JSON.parse(localStorage.getItem('user'));

    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
    if (user) {
      this.authService.currentUser = user;
      this.listener.changeCurrentUser(user);
    }
    if (token && user && this.validateTokenExpiration(token)) {
      this.authService.isLoggedInEmit();
    }
  }

  private validateTokenExpiration(token: any): boolean {
    if (this.jwtHelper.isTokenExpired(token)) {
      this.authService.logout();
      this.notifier.push('Authorization token expired. Please sign in again', 'warning');

      return false;
    }

    return true;
  }

  private subscribeData() {
    // const followersRequest = new FollowersRequest();
    if (this.authService.isLoggedIn()) {
      this.signalR.startConnection();

    }
  }
}


