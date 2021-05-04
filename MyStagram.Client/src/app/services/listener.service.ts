import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { UserAuth } from '../models/domain/auth/user-auth';
import { UserProfile } from '../models/domain/Profile/user-profile';
import { Follower } from '../models/domain/social/follower';

@Injectable({
  providedIn: 'root'
})

export class ListenerService {

  private user = new BehaviorSubject<UserAuth>(null);
  currentUser = this.user.asObservable();

  private unwatchedFollowsCount = new BehaviorSubject<number>(0);
  currentUnwatchedFollowsCount = this.unwatchedFollowsCount.asObservable();

  private unreadConversationsCount = new BehaviorSubject<number>(0);
  currentUnreadConversationsCount = this.unreadConversationsCount.asObservable();

  private unreadMessagesCount = new BehaviorSubject<number>(0);
  currentUnreadMessagesCount = this.unreadMessagesCount.asObservable();
  // private followers = new BehaviorSubject<Follower[]>([]);
  // currentFollowers = this.followers.asObservable();

  public changeCurrentUser(user: UserAuth) {
    localStorage.setItem('user', JSON.stringify(user));
    this.user.next(user);
  }

  public changeCurrentUnwatchedFollowsCount(unwatchedFollowsCount: number) {
    this.unwatchedFollowsCount.next(unwatchedFollowsCount);
  }

  public changeCurrentUnreadConversationsCount(unreadConversationsCount: number) {
    this.unreadConversationsCount.next(unreadConversationsCount);
  }

  public changeCurrentUnreadMessagesCount(unreadMessagesCount: number) {
    this.unreadMessagesCount.next(unreadMessagesCount);
  }

  public decrementCurrentUnreadMessagesCount() {
    if (this.unreadMessagesCount.value > 0) {
      this.unreadMessagesCount.next(this.unreadMessagesCount.value - 1);
    }
  }
  // public changeCurrentFollowersRequest(followers: Follower[]) {
  //   this.followers.next(followers);
  // }

}