import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, map, switchMap } from 'rxjs/operators';
import { RegisterValidation } from 'src/app/enum/register-validation.enum';
import { UserProfile } from 'src/app/models/domain/Profile/user-profile';
import { ProfileResponse } from 'src/app/resolvers/responses/profile-response';
import { AuthService } from 'src/app/services/auth.service';
import { FormHelper } from 'src/app/services/form-helper.service';
import { ListenerService } from 'src/app/services/listener.service';
import { Notifier } from 'src/app/services/notifier.service';
import { ProfileService } from 'src/app/services/profile.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef;

  currentUser: UserProfile;
  profileResponse: ProfileResponse;
  isPrivate: boolean;
  test:boolean;
  userDataForm: FormGroup;
  changePasswordForm: FormGroup;
  constants = constants;

  constructor(private profileService: ProfileService, private notifier: Notifier, private formBuilder: FormBuilder,
    public formHelper: FormHelper, private authService: AuthService, private route: ActivatedRoute,
    private router: Router, private listener: ListenerService) { }

  ngOnInit() {
    this.subscribeData();
    
    this.createUserDataForm();
    this.createChangePasswordForm();
  }

  public updateProfile() {
    if (this.userDataForm.valid) {
      const request = Object.assign({}, this.userDataForm.value);
      request.isPrivate = this.isPrivate;
      this.profileService.updateProfile(request).subscribe(() => {

        this.notifier.push('User data saved', 'success');


        this.authService.currentUser.email = request.Email;
        this.authService.currentUser.userName = request.UserName;
        this.authService.currentUser.isPrivate = request.isPrivate;
        // this.authService.currentUser.name = request.newName;
        // this.authService.currentUser.surname = request.newSurname;
        // this.authService.currentUser.description = request.newDescrption;

        this.currentUser.email = request.Email;
        this.currentUser.userName = request.UserName;
        this.currentUser.isPrivate = request.isPrivate;
        // this.currentUser.name = request.newName;
        // this.currentUser.surname = request.newSurname;
        // this.currentUser.description = request.newDescrption;
        this.listener.changeCurrentUser(this.authService.currentUser);
        this.createUserDataForm();
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['account/edit']);
      });
    }
  }

  public changePassword() {
    if (this.changePasswordForm.valid) {
      const request = Object.assign({}, this.changePasswordForm.value);
      this.profileService.changePassword(request).subscribe(() => {
        this.notifier.push('Password changed', 'success');
      }, error => {
        this.notifier.push(error, 'error');
        this.router.navigate(['account/edit']);
      });
      this.formHelper.resetForm(this.changePasswordForm);
    }
  }

  public loadAvatar(file: File) {
    if (file) {
      const reader = new FileReader();
      reader.readAsDataURL(file);

      reader.onload = () => {
        const url = reader.result.toString();
        this.currentUser.photoUrl = url;
        this.profileService.changeAvatar(file).subscribe(() => {
          this.notifier.push('Avatar set', 'info');

          this.authService.currentUser.photoUrl = url;
          this.listener.changeCurrentUser(this.authService.currentUser);
        }, error => {
          this.notifier.push(error, 'error');
        });
      };

      this.fileInput.nativeElement.value = '';
    } else {
      this.notifier.push('Error occurred during selecting avatar', 'error');
    }
  }

  public deleteAvatar() {
    if (this.currentUser.photoUrl) {
      this.profileService.deleteAvatar().subscribe(() => {
        this.notifier.push('Avatar deleted', 'info');
        this.currentUser.photoUrl = null;
        this.authService.currentUser.photoUrl = null;
        this.listener.changeCurrentUser(this.authService.currentUser);
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  public checkboxChanged(event) {
    this.isPrivate = !this.isPrivate;
  }
  

  private subscribeData() {
    this.route.data.subscribe(data => this.profileResponse = data.editProfileResponse);
    if (!this.profileResponse.isSucceeded) {
      this.notifier.push('User profile loading failed', 'error');
      this.router.navigate(['']);
    } else {
      this.currentUser = this.profileResponse.userProfile;
      this.isPrivate = this.profileResponse.userProfile.isPrivate;
    }
  }

  private createUserDataForm() {
    this.userDataForm = this.formBuilder.group({
      Email: [this.currentUser?.email, [Validators.required, Validators.email],
      ],
      UserName: [this.currentUser?.userName,
      [Validators.required, Validators.minLength(constants.minUserNameLength),
      Validators.maxLength(constants.maxUserNameLength)],
      ],
      Name: [this.currentUser?.name],
      Description: [this.currentUser?.description],
      Surname: [this.currentUser?.surname]
    });
  }

  private createChangePasswordForm() {
    this.changePasswordForm = this.formBuilder.group({
      oldPassword: ['', [Validators.required]],
      newPassword: ['', [Validators.required, Validators.minLength(constants.minUserPasswordLength),
      Validators.maxLength(constants.maxUserPasswordLength)]]
    });
  }

  // private getRegisterValidation(registerValidation: RegisterValidation, content: string, error: object) {
  //   return this.authService.getRegisterValidation(registerValidation, content).pipe(
  //     map((response: any) => {
  //       return response.isAvailable ? null : error;
  //     })
  //   );
  // }

  // private checkEmailAvailability(control: AbstractControl) {
  //   return control.valueChanges.pipe(
  //     debounceTime(1000),
  //     distinctUntilChanged(),
  //     switchMap(value => {
  //       return this.getRegisterValidation(RegisterValidation.Email, value, { emailTaken: true }).pipe(
  //         map(response => {
  //           control.setErrors(response);
  //         })
  //       );
  //     })
  //   );
  // }

  // private checkUsernameAvailability(control: AbstractControl) {
  //   return control.valueChanges.pipe(
  //     debounceTime(1000),
  //     distinctUntilChanged(),
  //     switchMap(value => {
  //       return this.getRegisterValidation(RegisterValidation.UserName, value, { usernameTaken: true }).pipe(
  //         map(response => {
  //           control.setErrors(response);
  //         })
  //       );
  //     })
  //   );
  // }
}
