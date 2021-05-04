import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { FormHelper } from 'src/app/services/form-helper.service';
import { Notifier } from 'src/app/services/notifier.service';
import { constants } from 'src/environments/environment';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {

  constructor(private authService: AuthService, private notifier: Notifier, private formBuilder: FormBuilder,
    private route: ActivatedRoute, private router: Router, public formHelper: FormHelper) { }

  resetPasswordForm: FormGroup;

  private userId: string;
  private token: string;

  constants = constants;
  ngOnInit() {
    this.validateResetPasswordLink();

    this.createResetPasswordForm();
  }

  public resetPassword() {
    if (this.resetPasswordForm.valid) {
      const request = Object.assign({}, this.resetPasswordForm.value);

      request.userId = this.userId;
      request.token = this.token;

      this.authService.resetPassword(request).subscribe(() => {
        this.notifier.push('Password has been changed', 'success');
      }, error => {
        this.notifier.push(error, 'error');

      });

      this.router.navigate(['/login']);
    }
  }

  private validateResetPasswordLink() {
    this.route.queryParams.subscribe(params => {
      this.userId = params.userId;
      this.token = params.token;
    });

    if (this.userId && this.token) {
      this.confirmResetPassword();
    }
    else {
      this.notifier.push('Invalid reset password link', 'error');
      this.router.navigate(['']);
    }
  }

  private confirmResetPassword() {
    this.authService.confirmResetPassword(this.userId, this.token).subscribe(() => {
    }, error => {
      this.notifier.push(error, 'error');
    });
  }

  private passwordMatchValidator(formGroup: FormGroup) {
    const newPassword: string = formGroup.get('newPassword').value;
    const confirmPassword: string = formGroup.get('confirmPassword').value;

    if (newPassword !== confirmPassword) {
      formGroup.get('confirmPassword').setErrors({ passwordMismatch: true });
    }
  }

  private createResetPasswordForm() {
    this.resetPasswordForm = this.formBuilder.group({
      newPassword: ['', [Validators.required, Validators.minLength(constants.minUserPasswordLength),
      Validators.maxLength(constants.maxUserPasswordLength)]],
      confirmPassword: ['', [Validators.required]]
    }, {
      validators: [
        this.passwordMatchValidator
      ]
    });
  }

}
