import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserAuth } from 'src/app/models/domain/auth/user-auth';
import { Follower } from 'src/app/models/domain/social/follower';
import { Pagination } from 'src/app/models/helpers/pagination';
import { FollowersRequest } from 'src/app/resolvers/requests/followers-request';
import { AuthService } from 'src/app/services/auth.service';
import { FollowersService } from 'src/app/services/followers.service';
import { ListenerService } from 'src/app/services/listener.service';
import { NotificationService } from 'src/app/services/notification.service';
import { Notifier } from 'src/app/services/notifier.service';
import { SignalRService } from 'src/app/services/signalR.service';
import { SIGNALR_ACTIONS } from 'src/environments/environment';

@Component({
  selector: 'app-notification-page',
  templateUrl: './notification-page.component.html',
  styleUrls: ['./notification-page.component.scss']
})
export class NotificationPageComponent implements OnInit {

  currentFollowers: Follower[];
  pagination: Pagination;
  currentUser: UserAuth;

  constructor(public authService: AuthService, private followersService: FollowersService, private notifier: Notifier,
    private signalR: SignalRService, private listener: ListenerService, private route: ActivatedRoute,
    private notificationService: NotificationService, private router: Router) { }

  ngOnInit() {
    this.subscribeData();
    this.subscribeSignalR();
  }
  
  public acceptFollower(accepted: boolean, senderId: string, $event: any) {
    $event.stopPropagation();
    this.followersService.acceptFollower(senderId, this.currentUser.id, accepted).subscribe(() => {
      if (!accepted) {
        this.currentFollowers = this.currentFollowers.filter(f => (f.senderId !== senderId));
      }
      else {
        const followerIndex = this.currentFollowers.findIndex(f => f.senderId === senderId);
        this.currentFollowers[followerIndex].recipientAccepted = true;
      }
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  public onScroll() {
    if (this.currentFollowers.length < this.pagination.totalItems) {
      this.pagination.currentPage++;
      this.loadFollowers();
    }
  }

  private loadFollowers() {
    if (this.authService.isLoggedIn()) {
      const followersRequest = new FollowersRequest();
      followersRequest.areAccepted = !this.authService.currentUser.isPrivate;
      followersRequest.userId = this.authService.currentUser.id;
      followersRequest.pageNumber = this.pagination.currentPage;
      this.followersService.getFollowers(followersRequest).subscribe(res => {
        const followers = res.result?.followers;
        this.pagination = res.pagination;
        this.currentFollowers = this.currentFollowers.concat(followers);
      });
    }
  }

  private subscribeData() {
    this.listener.currentUser.subscribe(user => this.currentUser = user);
    this.route.data.subscribe(data => {
      this.currentFollowers = data.notificationsResponse.result?.followers;
      this.pagination = data.notificationsResponse.pagination;
      this.notificationService.countUnwatchedFollows();
    });
  }

  private subscribeSignalR() {
    this.signalR.subscribeAction(SIGNALR_ACTIONS.ON_FOLLOW_USER, (value) => {
      if (this.authService.isLoggedIn()) {
        this.currentFollowers.unshift(value[0]);

        if (this.router.url === `/notifications`) {
          this.followersService.WatchFollower(value[0].senderId).subscribe(() => {
            this.notificationService.countUnwatchedFollows();
          }, () => { });
        }
        else {
          this.notificationService.countUnwatchedFollows();
        }
      }
    });
    
    this.signalR.subscribeAction(SIGNALR_ACTIONS.ON_UNFOLLOW_USER, (value) => {
      if (this.authService.isLoggedIn()) {
        this.currentFollowers = this.currentFollowers.filter(f => f.senderId !== value[0]);
        this.notificationService.countUnwatchedFollows();
      }
    });
  }
}
