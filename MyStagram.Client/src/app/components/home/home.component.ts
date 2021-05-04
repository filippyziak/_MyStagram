import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/models/domain/main/post';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { Pagination } from 'src/app/models/helpers/pagination';
import { StoryWrapper } from 'src/app/models/helpers/story/story-wrapper';
import { FetchPostsRequest } from 'src/app/resolvers/requests/fetch-posts-request';
import { AuthService } from 'src/app/services/auth.service';
import { MainService } from 'src/app/services/main.service';
import { Notifier } from 'src/app/services/notifier.service';
import { blurToggleAnimation } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  posts: Post[];
  pagination: Pagination;
  storyWrappers: StoryWrapper[];

  constructor(public authService: AuthService, private route: ActivatedRoute, private notifier: Notifier,
    private mainService: MainService) { }

  ngOnInit() {
    this.subscribeData();
  }


  public loggedIn() {
    return this.authService.isLoggedIn();
  }

  public onScroll() {
    if (this.posts.length < this.pagination.totalItems) {
      this.pagination.currentPage++;
      this.loadPosts();
    }
  }

  private loadPosts() {
    if (this.authService.isLoggedIn()) {
      const fetchPostsRequest = new FetchPostsRequest();
      fetchPostsRequest.pageNumber = this.pagination.currentPage;
      this.mainService.fetchPosts(fetchPostsRequest).subscribe(res => {
        this.pagination = res.pagination;
        this.posts = this.posts.concat(res?.result.posts);
      }, error => {
        this.notifier.push(error, 'error');
      });

    }
  }

  subscribeData() {
    this.route.data.subscribe(data => {
      if (this.authService.isLoggedIn()) {
        this.pagination = data.postsResponse?.pagination;
        this.posts = data.postsResponse?.result?.posts;
        this.storyWrappers = data.storiesResponse?.storyWrappers;
      }
    })
  }
}
