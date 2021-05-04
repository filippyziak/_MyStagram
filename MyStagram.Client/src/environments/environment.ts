import { animate, style, transition, trigger } from '@angular/animations';

export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api/',
  signalRUrl: 'http://localhost:5000/api/hub/',
  pageSize: 10,
  messagesThreadPageSize: 13,
  postsPageSize: 9
};

export const SIGNALR_ACTIONS = {
  GET_CONNECTION_ID: 'GetConnectionId',
  ON_FOLLOWER_ACCEPT: 'OnFollowerAccept',
  ON_FOLLOW_USER: "OnFollowUser",
  ON_UNFOLLOW_USER:"OnUnFollowUser",
  ON_MESSAGE_SEND: "OnMessageSend"
};

export const constants = {
  maxUserNameLength: 32,
  minUserNameLength: 4,
  minUserPasswordLength: 6,
  maxUserPasswordLength: 32,
  maximumDescriptionLength: 500,
  maximumCommentLength: 255,
  fakeEmailAddress: 'example@example.example',
  fakeUserId: '0A749474094548D2'
};

export const blurToggleAnimation = [
  trigger('blurToggle', [
    transition(':enter', [
      style({ opacity: 0 }),
      animate('.3s ease-out', style({ opacity: 1 }))
    ]),
    transition(':leave', [
      animate('.3s ease-out', style({ opacity: 0 }))
    ])
  ])
];