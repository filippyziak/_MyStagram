import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Notifier } from 'src/app/services/notifier.service';
import { ProfileService } from 'src/app/services/profile.service';
import { ListenerService } from 'src/app/services/listener.service';
import { FollowersService } from 'src/app/services/followers.service';
import { FollowersResponse } from 'src/app/resolvers/responses/followers-response';
import { FollowersRequest } from 'src/app/resolvers/requests/followers-request';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  private returnToUrl: string;

  constructor(private authService: AuthService, private router: Router, private notifier: Notifier,
    private route: ActivatedRoute, private formBuilder: FormBuilder, private notification: NotificationService) { }

  ngOnInit() {
    this.createLoginForm();

    this.returnToUrl = this.route.snapshot.queryParams.returnTo || '/';
  }

  public login() {
    const userLogin = Object.assign({}, this.loginForm.value);

    this.authService.login(userLogin).subscribe(() => {
      this.notifier.push('Logged in', 'success');

      this.router.navigate([this.returnToUrl]);
    }, error => {
      this.notifier.push(error, 'error');

      this.router.navigate(['/login']);
    }, () => {
      this.notification.countUnreadConversations();
      this.notification.countUnwatchedFollows();
    });
  }

  private createLoginForm() {
    this.loginForm = this.formBuilder.group({
      email: [''],
      password: ['']
    });
  }
}
