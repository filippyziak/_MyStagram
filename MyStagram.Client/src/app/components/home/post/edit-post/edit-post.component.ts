import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/models/domain/main/post';
import { PutEmitter } from 'src/app/models/helpers/emitters/put-emitter';
import { AuthService } from 'src/app/services/auth.service';
import { FormHelper } from 'src/app/services/form-helper.service';
import { MainService } from 'src/app/services/main.service';
import { Notifier } from 'src/app/services/notifier.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-edit-post',
  templateUrl: './edit-post.component.html',
  styleUrls: ['./edit-post.component.scss']
})
export class EditPostComponent implements OnInit {
  post: Post;

  editMode: boolean;
  photoUrl: string;
  tempPhotoUrl: any;
  photo: File;
  postForm: FormGroup;
  changePhoto = false;

  constants = constants;

  constructor(private mainService: MainService, private route: ActivatedRoute, private formBuilder: FormBuilder,
    private notifier: Notifier, private router: Router, public formHelper: FormHelper, public authService: AuthService) { }

  ngOnInit() {
    this.subscribeData();
    if (this.editMode && this.post?.userId !== this.authService.currentUser.id) {
      this.router.navigate(['']);
    }
    this.createPostForm();
  }

  public createOrUpdatePost() {
    if (this.postForm.valid) {
      const postFormObjects = this.postForm.value;
      if (!this.editMode) {
        this.mainService.createPost(postFormObjects.description, this.photo).subscribe(() => {
          this.notifier.push('Post created', 'success');
          this.router.navigate(['profile/', this.authService.currentUser.id]);
        }, error => {
          this.notifier.push(error, 'error');
        });
      } else {
        const request = Object.assign({}, this.postForm.value);
        this.mainService.updatePost(postFormObjects.postId, postFormObjects.description).subscribe(() => {
          this.notifier.push('Post updated', 'success');
          this.router.navigate(['post/', this.post?.id]);
        }, error => {
          this.notifier.push(error, 'error');
        });
      }
    }
  }

  public resetPhoto() {
    this.photoUrl = '';
    this.photo = null;
    this.changePhoto = true;
  }

  public loadPhoto(photo: File) {
    var reader = new FileReader();
    this.photoUrl = photo.name;
    this.photo = photo;
    this.changePhoto = true;
    reader.readAsDataURL(photo);
    reader.onload = (_event) => {
      this.tempPhotoUrl = reader.result;
    }
  }

  private createPostForm() {
    this.postForm = !this.editMode ? this.formBuilder.group({
      description: ['', [Validators.required, Validators.maxLength(constants.maximumDescriptionLength)]]
    }) :
      this.formBuilder.group({
        postId: [this.post?.id],
        description: [this.post?.description, [Validators.required, Validators.maxLength(constants.maximumDescriptionLength)]]
      });
  }

  private subscribeData() {
    this.route.data.subscribe(data => {
      this.post = data.postResponse?.post;
      this.editMode = this.post ? true : false;
    });

    this.photoUrl = this.post?.photoUrl ? this.post.photoUrl : '';
  }

}
