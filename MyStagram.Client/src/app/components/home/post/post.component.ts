import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/models/domain/main/post';
import { DeleteEmitter } from 'src/app/models/helpers/emitters/delete-emitter';
import { PutEmitter } from 'src/app/models/helpers/emitters/put-emitter';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {
  post: Post;

  commentForm: FormGroup;

  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.subscribeData();
  }

  public onPostDeleted(emitter: DeleteEmitter) {
    if (emitter.deleted) {
      this.router.navigate(['']);
    }
  }

  private subscribeData() {
    this.route.data.subscribe(data => this.post = data.postResponse.post);
  }
}
