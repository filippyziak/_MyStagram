import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Direction } from 'src/app/enum/direction';
import { Story } from 'src/app/models/domain/story/story';
import { AuthService } from 'src/app/services/auth.service';
import { Notifier } from 'src/app/services/notifier.service';
import { ProfileService } from 'src/app/services/profile.service';
import { StoryService } from 'src/app/services/story.service';
import { blurToggleAnimation } from 'src/environments/environment';

@Component({
  selector: 'app-story-page',
  templateUrl: './story-page.component.html',
  styleUrls: ['./story-page.component.scss'],
  animations: blurToggleAnimation
})
export class StoryPageComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef;

  index = -3;
  currentStory: Story;
  currentStories: Story[] = [];
  direction = Direction;
  userId: string;
  myStory: boolean;


  constructor(private route: ActivatedRoute, private router: Router, private storyService: StoryService,
    private notifier: Notifier, private authService: AuthService, private profileService: ProfileService) { }

  ngOnInit() {
    this.subscirbeData();
  }

  public goToMain() {
    this.router.navigate(['']);
  }

  public changePhoto(direction: Direction) {
    this.index += direction;
    if (this.index < 0 || this.index > this.currentStories.length - 1) {
      this.goToMain();
    }
    else {
      this.currentStory = this.currentStories[this.index];
      this.watchStory(this.currentStory.id);
    }
  }

  public watchStory(storyId: string) {
    this.storyService.watchStory(storyId).subscribe(() => { },
      error => {
        this.notifier.push(error, 'error');
      });
  }

  public createStory(file: File) {
    this.storyService.createStory(file).subscribe(res => {
      const response: any = res?.body;
      this.currentStory = response?.story;
      this.currentStories.push(this.currentStory);
      this.index = this.currentStories.length - 1;
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  public deleteStory(storyId: string) {
    this.storyService.deleteStory(storyId).subscribe(() => {
      if (this.index > 0) {
        this.index -= 1;
        this.currentStories = this.currentStories.filter(s => s.id !== storyId);
        this.currentStory = this.currentStories[this.index];
      }
      else if (this.currentStories.length > 1 && this.index === 0) {
        this.index += 1;
        this.currentStories = this.currentStories.filter(s => s.id !== storyId);
        this.currentStory = this.currentStories[this.index];
      }
      else {
        this.goToMain();
      }
    },
      error => {
        this.notifier.push(error, 'error');
      });
  }

  private subscirbeData() {
    this.route.data.subscribe(data => {
      this.route.params.subscribe(params => {
        this.userId = params?.userId;
      });

      this.profileService.getProfile(this.userId).subscribe(res => {
        const user = res?.userProfile;
        if (user.isPrivate && !user.followers.some(f => f.senderId === this.authService.currentUser.id) && user.id !== this.authService.currentUser.id) {
          this.goToMain();
        }
      }, error => {
        this.notifier.push(error, 'error');
      });

      if (data?.userStoryResponse?.storyToWatch !== null) {
        this.currentStory = data?.userStoryResponse?.storyToWatch;
        this.watchStory(this.currentStory.id);
        this.currentStories = data?.userStoryResponse?.stories;
        this.index = this.currentStories.findIndex(story => story.id === this.currentStory.id);
      }
      if (this.userId === this.authService.currentUser.id) {
        this.myStory = true;
      }

      if (data?.userStoryResponse === null && this.userId !== this.authService.currentUser.id) {
        this.goToMain();
      }

    });
  }
}
