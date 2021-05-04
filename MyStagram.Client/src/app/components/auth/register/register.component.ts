import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Notifier } from 'src/app/services/notifier.service';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { FormHelper } from 'src/app/services/form-helper.service';
import { constants } from 'src/environments/environment';
import { debounceTime, distinctUntilChanged, switchMap, map } from 'rxjs/operators';
import { RegisterValidation } from 'src/app/enum/register-validation.enum';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  constants = constants;

  constructor(private authService: AuthService, private notifier: Notifier, private formBuilder: FormBuilder,
    private router: Router, public formHelper: FormHelper) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  public register() {
    if (this.registerForm.valid) {
      const userRegister = Object.assign({}, this.registerForm.value);
      this.authService.register(userRegister).subscribe(() => {
        this.notifier.push('Account has been created. Activation link has been sent to your email', 'success');
      }, error => {
        this.notifier.push(error, 'error');

        this.router.navigate(['/auth']);
      }, () => {
        this.formHelper.resetForm(this.registerForm);
      });
    }
  }

  private createRegisterForm(): void {
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email],
      ],
      userName: ['', [Validators.required, Validators.minLength(constants.minUserNameLength),
      Validators.maxLength(constants.maxUserNameLength)],
      ],
      password: ['', [Validators.required, Validators.minLength(constants.minUserPasswordLength),
      Validators.maxLength(constants.maxUserPasswordLength)]],
      confirmPassword: ['', [Validators.required]]
    }, {
      validators: [
        this.passwordMatchValidator
      ]
    });
  }

  private passwordMatchValidator(formGroup: FormGroup) {
    const password: string = formGroup.get('password').value;
    const confirmPassword: string = formGroup.get('confirmPassword').value;
    if (password !== confirmPassword) {
      formGroup.get('confirmPassword').setErrors({ passwordMismatch: true });
    }

  }

  private getRegisterValidation(registerValidation: RegisterValidation, content: string, error: object) {
    return this.authService.getRegisterValidation(registerValidation, content).pipe(
      map((response: any) => {
        return response.isAvailable ? null : error;
      })
    );
  }

  private checkEmailAvailability(control: AbstractControl) {
    return control.valueChanges.pipe(
      debounceTime(1000),
      distinctUntilChanged(),
      switchMap(value => {
        return this.getRegisterValidation(RegisterValidation.Email, value, { emailTaken: true }).pipe(
          map(response => {
            ;
            control.setErrors(response);
          })
        );
      })
    );
  }

  private checkUserNameAvailability(control: AbstractControl) {
    return control.valueChanges.pipe(
      debounceTime(1000),
      distinctUntilChanged(),
      switchMap(value => {
        return this.getRegisterValidation(RegisterValidation.UserName, value, { userNameTaken: true }).pipe(
          map(response => {
            control.setErrors(response);
          })
        );
      })
    );
  }
}
