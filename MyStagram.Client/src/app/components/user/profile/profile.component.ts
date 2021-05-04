import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/models/domain/main/post';
import { UserProfile } from 'src/app/models/domain/Profile/user-profile';
import { Pagination } from 'src/app/models/helpers/pagination';
import { PostsRequest } from 'src/app/resolvers/requests/posts-request';
import { AuthService } from 'src/app/services/auth.service';
import { FollowersService } from 'src/app/services/followers.service';
import { MainService } from 'src/app/services/main.service';
import { Notifier } from 'src/app/services/notifier.service';
import { blurToggleAnimation, constants } from 'src/environments/environment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
  animations: blurToggleAnimation
})
export class ProfileComponent implements OnInit {

  userProfile: UserProfile;
  posts: Post[];
  currentUserId: string;
  pagination: Pagination;
  constants = constants;
  constructor(private authService: AuthService, private route: ActivatedRoute, private notifier: Notifier,
    private router: Router, private followersService: FollowersService, private mainService: MainService) { }

  ngOnInit() {
    this.subscribeData();
    if (this.userProfile.email === this.constants.fakeEmailAddress) {
      this.router.navigate(['']);
    }
  }

  public goToPost(postId: string) {
    this.router.navigate(['post/', postId]);
  }
  public goToEditProfile() {
    this.router.navigate(['account/edit']);
  }

  public checkFollower() {
    return this.userProfile.followers.some(f => f.senderId === this.currentUserId);
  }

  public checkPrivacy() {
    return this.userProfile.followersRequests.some(f => (f.senderId === this.currentUserId));
  }

  public followUser() {
    if (this.authService.isLoggedIn()) {
      this.followersService.followUser(this.userProfile.id).subscribe(result => {
        const response: any = result.body;

        if (!this.userProfile.isPrivate) {
          this.userProfile?.followers?.push(response.follower);
          this.userProfile.followersCount++;
          this.notifier.push('User followed', 'success');
        }
        else {
          this.userProfile.followersRequests?.push(response.follower);
          this.notifier.push('Request has been send', 'success');
        }

      }, error => {
        this.notifier.push(error, 'error');
      })
    }
  }

  public unFollowUser() {
    if (this.authService.isLoggedIn()) {
      this.followersService.unFollowUser(this.userProfile.id).subscribe(() => {

        if (this.userProfile.followers.some(f => f.senderId === this.currentUserId)) {

          this.userProfile.followers = this.userProfile.followers.filter(f => f.senderId !== this.currentUserId);
          this.userProfile.followersCount--;
          this.notifier.push('User unfollowed', 'success');
        }
        else {
          this.userProfile.followersRequests = this.userProfile.followersRequests.filter(f => f.senderId !== this.currentUserId);
          this.notifier.push('Request has been canceled', 'info');
        }
      }, error => {
        this.notifier.push(error, 'error');
      })
    }
  }

  public onScroll() {
    if (this.posts.length < this.pagination.totalItems) {
      this.pagination.currentPage++;
      this.loadPosts();
    }
  }

  private loadPosts() {
    const postsRequest = new PostsRequest();
    postsRequest.userId = this.userProfile.id;
    postsRequest.pageNumber = this.pagination.currentPage;

    this.mainService.getPosts(postsRequest).subscribe(res => {
      this.pagination = res?.pagination;
      this.posts = this.posts.concat(res?.result.posts);
    }, error => {
      this.notifier.push(error, 'error');
    });
  }
  private subscribeData() {
    this.route.data.subscribe(data => {
      this.userProfile = data.profileResponse?.userProfile;
      this.posts = data.postsResponse.result?.posts;
      this.pagination = data.postsResponse.pagination;
    });
    this.currentUserId = this.authService.currentUser.id;
  }

}
