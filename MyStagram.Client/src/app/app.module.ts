import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import { NotifierModule } from 'angular-notifier';
import { JwtModule } from '@auth0/angular-jwt';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { appRoutes } from './routes';

import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTabsModule } from '@angular/material/tabs';
import { MatBadgeModule } from '@angular/material/badge';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MatMenuModule } from '@angular/material/menu';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule } from '@angular/material/radio';

import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './components/home/home.component';
import { AuthComponent } from './components/auth/auth/auth.component';
import { LoginComponent } from './components/auth/login/login.component';
import { InterceptorProvider } from './services/interceptor.service';
import { RegisterComponent } from './components/auth/register/register.component';
import { ConfirmEmailComponent } from './components/auth/register/confirm-email/confirm-email.component';
import { MatDialogModule } from '@angular/material/dialog';
import { SendResetPasswordComponent } from './components/auth/auth/send-reset-password/send-reset-password.component';
import { ResetPasswordComponent } from './components/auth/auth/reset-password/reset-password.component';
import { ProfileComponent } from './components/user/profile/profile.component';
import { EditProfileComponent } from './components/user/profile/edit-profile/edit-profile.component';
import { ProfileResolver } from './resolvers/profile.resolver';
import { EditProfileResolver } from './resolvers/edit-profile.resolver';
import { PostResolver } from './resolvers/post.resolver';
import { EditPostComponent } from './components/home/post/edit-post/edit-post.component';
import { PostComponent } from './components/home/post/post.component';
import { PostCardComponent } from './components/home/post-card/post-card.component';
import { CommentsComponent } from './components/home/post/comments/comments.component';
import { PostsResolver } from './resolvers/posts.resolver';
import { FollowersResolver } from './resolvers/followers.resolver';
import { MessengerComponent } from './components/messenger/messenger.component';
import { MessagesThreadComponent } from './components/messenger/messages-thread/messages-thread.component';
import { MessageCardComponent } from './components/messenger/messages-thread/message-card/message-card.component';
import { ConversationCardComponent } from './components/messenger/conversation-card/conversation-card.component';
import { MessagesResolver } from './resolvers/messages.resolver';
import { ConversationsResolver } from './resolvers/conversations.resolver';
import { TimeAgoPipe } from './pipes/time-ago.pipe';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { NotificationPageComponent } from './components/notification-page/notification-page.component';
import { NotificationPageResolver } from './resolvers/notification-page.resolver';
import { UserSearchComponent } from './components/user/user-search/user-search.component';
import { SearchResolver } from './resolvers/search.resolver';
import { FetchPostsResolver } from './resolvers/fetch-posts.resolver';
import { StoriesResponse } from './resolvers/responses/stories-response';
import { StoriesResolver } from './resolvers/stories.resolver';
import { UserStoriesResponse } from './resolvers/responses/user-stories-response';
import { UserStoriesResolver } from './resolvers/user-stories.resolver';
import { StoriesComponent } from './components/stories/stories.component';
import { StoryPageComponent } from './components/stories/story-page/story-page.component';



export const tokenGetter = () => localStorage.getItem('token');

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    AuthComponent,
    LoginComponent,
    RegisterComponent,
    ConfirmEmailComponent,
    SendResetPasswordComponent,
    ResetPasswordComponent,
    ProfileComponent,
    EditProfileComponent,
    EditPostComponent,
    PostCardComponent,
    PostComponent,
    CommentsComponent,
    MessengerComponent,
    MessagesThreadComponent,
    MessageCardComponent,
    ConversationCardComponent,
    NotificationPageComponent,
    UserSearchComponent,
    StoriesComponent,
    StoryPageComponent,

    TimeAgoPipe
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    NotifierModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes),

    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatOptionModule,
    MatPaginatorModule,
    MatTabsModule,
    MatBadgeModule,
    FontAwesomeModule,
    MatDialogModule,
    MatMenuModule,
    MatCheckboxModule,
    MatRadioModule,
    ScrollingModule,

    InfiniteScrollModule,

    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: ['localhost:5000'],
        disallowedRoutes: ['localhost:5000/api/auth/']
      }
    })
  ],
  providers: [
    ProfileResolver,
    EditProfileResolver,
    PostResolver,
    PostsResolver,
    FollowersResolver,
    ConversationsResolver,
    MessagesResolver,
    NotificationPageResolver,
    SearchResolver,
    FetchPostsResolver,
    StoriesResolver,
    UserStoriesResolver,

    InterceptorProvider,
    { provide: LOCALE_ID, useValue: 'en-EN' },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
