import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Post } from 'src/app/models/domain/main/post';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { AuthService } from 'src/app/services/auth.service';
import { FormHelper } from 'src/app/services/form-helper.service';
import { MainService } from 'src/app/services/main.service';
import { Notifier } from 'src/app/services/notifier.service';
import { blurToggleAnimation, constants } from 'src/environments/environment';

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.scss'],
  animations: blurToggleAnimation
})
export class PostCardComponent implements OnInit {
  @Input() post: Post;
  
  commentForm: FormGroup;
  constants = constants;
  areCommentsDisplayed: boolean;
  isLiked: boolean;
  @Output() postDeleted = new EventEmitter<DeleteEmitter>();

  constructor(public authService: AuthService, private mainService: MainService, private notifier: Notifier,
    public formHelper: FormHelper, private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit() {
    this.createCommentForm();
    this.likeStatus();
  }

  public navigateToStory(userId: string) {
    this.router.navigate(['story/', userId]);
  }
  
  public createComment() {
    if (this.commentForm.valid) {
      this.mainService.createComment(this.commentForm.value.content, this.post.id).subscribe(result => {
        const response: any = result?.body;

        this.notifier.push('Comment added', 'success');
        this.post.comments.push(response.comment);
        this.post.commentsCount++;
        this.formHelper.resetForm(this.commentForm);
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  public deletePost() {
    if (confirm('Do you want to delete this post permanently')) {
      this.mainService.deletePost(this.post.id).subscribe(() => {
        this.notifier.push('Post deleted', 'info');
        this.postDeleted.emit({ objectId: this.post.id, deleted: true });
        this.router.navigate(['profile/', this.post.userId])
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  public likePost() {
    if (this.authService.isLoggedIn()) {
      this.mainService.likePost(this.post.id).subscribe(result => {
        const response: any = result.body;

        if (response?.result) {
          this.post?.likes?.push(response?.like);
          this.post.likesCount++;
          this.isLiked = true;
        } else {
          this.post.likes = this.post?.likes?.filter(l => l.userId !== this.authService.currentUser.id);
          this.post.likesCount--;
          this.isLiked = false;
        }
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  public displayComments() {
    this.areCommentsDisplayed = true;
  }

  public closeComments() {
    this.areCommentsDisplayed = false;
  }

  public goToUserProfile() {
    this.router.navigate(['profile/', this.post.userId]);
  }
  public goToPostEdit() {
    this.router.navigate(['post/update/', this.post.id]);
  }
  public goToMessenger() {
    if (this.authService.currentUser.id === this.post.userId) {
      this.router.navigate(['messenger']);
    }
    else {
      this.router.navigate(['messenger/', this.post.userId]);
    }
  }

  private likeStatus() {
    if (this.post?.likes?.some(l => l.userId === this.authService.currentUser.id)) {
      this.isLiked = true;
    }
  }

  private createCommentForm() {
    this.commentForm = this.formBuilder.group({
      content: ['', [Validators.required, Validators.maxLength(constants.maximumCommentLength)]]
    });
  }
}
