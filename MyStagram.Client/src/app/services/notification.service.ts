import { Injectable } from '@angular/core';
import { FollowersService } from './followers.service';
import { ListenerService } from './listener.service';
import { Messenger } from './messenger.service';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private listener: ListenerService, private followersService: FollowersService, private messenger: Messenger) { }


  public countUnwatchedFollows() {
    this.followersService.countUnwatchedFollowers().subscribe(res => {
      this.listener.changeCurrentUnwatchedFollowsCount(res?.body.unwatchedFollowsCount);
    }, () => { })
  }

  public countUnreadConversations() {
    this.messenger.countUnreadConversations().subscribe(res => {
      this.listener.changeCurrentUnreadConversationsCount(res?.body.unreadConversationsCount);
    }, () => { })
  }
}
