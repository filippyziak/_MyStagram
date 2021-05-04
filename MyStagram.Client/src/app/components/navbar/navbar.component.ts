import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserAuth } from 'src/app/models/domain/auth/user-auth';
import { AuthService } from 'src/app/services/auth.service';
import { ListenerService } from 'src/app/services/listener.service';
import { Messenger } from 'src/app/services/messenger.service';
import { NotificationService } from 'src/app/services/notification.service';
import { SignalRService } from 'src/app/services/signalR.service';
import { SIGNALR_ACTIONS } from 'src/environments/environment';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean;
  currentUser: UserAuth;
  unwatchedFollowsCount: number;
  unreadConvrsationsCount: number;

  constructor(public authService: AuthService, private listener: ListenerService, private signalR: SignalRService,
    private notificationService: NotificationService, private router: Router, private messenger: Messenger) { }

  ngOnInit() {
    this.subscribeData();
    this.subscribeSignalR();
  }

  private subscribeData() {
    this.listener.currentUser.subscribe(user => this.currentUser = user);
    this.authService.currentLoggedIn.subscribe(loggedIn => this.isLoggedIn = loggedIn);
    if (this.isLoggedIn) {
      this.notificationService.countUnwatchedFollows();
      this.notificationService.countUnreadConversations();
    }
    this.listener.currentUnwatchedFollowsCount.subscribe(unwatchedFollowsCount => {
      this.unwatchedFollowsCount = unwatchedFollowsCount;
    });
    this.listener.currentUnreadConversationsCount.subscribe(unreadConvrsationsCount => {
      this.unreadConvrsationsCount = unreadConvrsationsCount;
    });
  }

  private subscribeSignalR() {
    this.signalR.subscribeAction(SIGNALR_ACTIONS.ON_MESSAGE_SEND, (value) => {
      if (this.authService.isLoggedIn()) {
        if (this.router.url === `/messenger/${value[0].senderId}`) {
          this.messenger.ReadMessage(value[0].id).subscribe(() => {
            this.notificationService.countUnreadConversations();
          }, () => { });
        }
        else {
          this.notificationService.countUnreadConversations();
        }
      }
    });
  }
}
