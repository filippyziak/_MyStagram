import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { FormHelper } from 'src/app/services/form-helper.service';
import { Notifier } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-send-reset-password',
  templateUrl: './send-reset-password.component.html',
  styleUrls: ['./send-reset-password.component.scss']
})
export class SendResetPasswordComponent implements OnInit {

  resetPasswordForm: FormGroup;
  karol: string;

  constructor(private authService: AuthService, private notifier: Notifier, private formBuilder: FormBuilder,
    public formHelper: FormHelper, private router: Router) { }

  ngOnInit(): void {
    this.createResetPasswordForm();
  }

  public sendResetPassword() {
    if (this.resetPasswordForm.valid) {
      const request = Object.assign({}, this.resetPasswordForm.value);

      this.authService.sendResetPassword(request).subscribe(() => {
        this.notifier.push('Reset password email has been sent', 'info');
      }, error => {
        this.notifier.push(error, 'error');
      });
    }
  }

  private createResetPasswordForm() {
    this.resetPasswordForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

}
