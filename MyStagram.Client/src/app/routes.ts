import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AuthComponent } from './components/auth/auth/auth.component';
import { ConfirmEmailComponent } from './components/auth/register/confirm-email/confirm-email.component';
import { AnonymousGuard } from './guards/anonymous.guard';
import { ResetPasswordComponent } from './components/auth/auth/reset-password/reset-password.component';
import { SendResetPasswordComponent } from './components/auth/auth/send-reset-password/send-reset-password.component';
import { EditProfileComponent } from './components/user/profile/edit-profile/edit-profile.component';
import { AuthGuard } from './guards/auth.guard';
import { ProfileComponent } from './components/user/profile/profile.component';
import { ProfileResolver } from './resolvers/profile.resolver';
import { EditProfileResolver } from './resolvers/edit-profile.resolver';
import { EditPostComponent } from './components/home/post/edit-post/edit-post.component';
import { PostResolver } from './resolvers/post.resolver';
import { PostComponent } from './components/home/post/post.component';
import { PostsResolver } from './resolvers/posts.resolver';
import { FollowersResolver } from './resolvers/followers.resolver';
import { MessengerComponent } from './components/messenger/messenger.component';
import { ConversationsResolver } from './resolvers/conversations.resolver';
import { MessagesThreadComponent } from './components/messenger/messages-thread/messages-thread.component';
import { MessagesResolver } from './resolvers/messages.resolver';
import { NotificationPageComponent } from './components/notification-page/notification-page.component';
import { NotificationPageResolver } from './resolvers/notification-page.resolver';
import { UserSearchComponent } from './components/user/user-search/user-search.component';
import { SearchResolver } from './resolvers/search.resolver';
import { FetchPostsResolver } from './resolvers/fetch-posts.resolver';
import { StoriesResolver } from './resolvers/stories.resolver';
import { UserStoriesResolver } from './resolvers/user-stories.resolver';
import { StoryPageComponent } from './components/stories/story-page/story-page.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent, resolve: { postsResponse: FetchPostsResolver, storiesResponse: StoriesResolver } },
    { path: 'account/confirm', component: ConfirmEmailComponent },

    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AnonymousGuard],
        children: [
            { path: 'login', component: AuthComponent },
            { path: 'account/reset/password', component: SendResetPasswordComponent },
            { path: 'account/confirm/password', component: ResetPasswordComponent }
        ]
    },

    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'account/edit', component: EditProfileComponent, resolve: { editProfileResponse: EditProfileResolver } },
            { path: 'profile/:userId', component: ProfileComponent, resolve: { profileResponse: ProfileResolver, postsResponse: PostsResolver, followersResponse: FollowersResolver, userStoriesResponse: UserStoriesResolver } },
            { path: 'post/create', component: EditPostComponent },
            { path: 'post/update/:postId', component: EditPostComponent, resolve: { postResponse: PostResolver } },
            { path: 'post/:postId', component: PostComponent, resolve: { postResponse: PostResolver } },
            { path: 'messenger', component: MessengerComponent, resolve: { conversationsResponse: ConversationsResolver } },
            { path: 'messenger/:recipientId', component: MessagesThreadComponent, resolve: { messagesResponse: MessagesResolver } },
            { path: 'notifications', component: NotificationPageComponent, resolve: { notificationsResponse: NotificationPageResolver } },
            { path: 'search', component: UserSearchComponent, resolve: { searchResponse: SearchResolver } },
            { path: 'story/:userId', component: StoryPageComponent, resolve: { userStoryResponse: UserStoriesResolver } }
        ]
    },

    { path: '**', redirectTo: '', pathMatch: 'full' }
];
