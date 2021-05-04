import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PutEmitter } from 'src/app/models/helpers/emitters/put-emitter';
import { FormHelper } from 'src/app/services/form-helper.service';
import { MainService } from 'src/app/services/main.service';
import { Notifier } from 'src/app/services/notifier.service';
import { Comment } from 'src/app/models/domain/main/comment';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss'],
})
export class CommentsComponent implements OnInit {
  @Input() postId: string;
  @Input() comments: Comment[];

  commentForm: FormGroup;

  constructor(private mainService: MainService, private formBuilder: FormBuilder, private notifier: Notifier,
    public formHelper: FormHelper, public authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  public goToUserProfile(userId: string) {
    this.router.navigate(['profile/', userId]);
  }

  public deleteComment(commentId: string) {
    this.mainService.deleteComment(commentId).subscribe(() => {
      this.notifier.push('Comment deleted', 'info');
      this.comments = this.comments.filter(c => c.id !== commentId);
    }, error => {
      this.notifier.push(error, 'error');
    });

  }
}
