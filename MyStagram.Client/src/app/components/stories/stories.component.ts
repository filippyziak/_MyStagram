import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StoryWrapper } from 'src/app/models/helpers/story/story-wrapper';
import { AuthService } from 'src/app/services/auth.service';
import { blurToggleAnimation } from 'src/environments/environment';

@Component({
  selector: 'app-stories',
  templateUrl: './stories.component.html',
  styleUrls: ['./stories.component.scss'],
  animations: blurToggleAnimation
})
export class StoriesComponent implements OnInit {

  @Input() storyWrappers: StoryWrapper[];
  areStoriesDisplayed: boolean;

  constructor(private router: Router, public authService: AuthService) { }

  ngOnInit() {
  }

  public navigateToStory(userId: string) {
    this.router.navigate(['story/', userId]);
  }
}
