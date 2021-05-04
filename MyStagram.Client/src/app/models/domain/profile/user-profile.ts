import { Post } from '../main/post';
import { Follower } from '../social/follower';

export interface UserProfile {
    id: string;
    email: string;
    userName: string;
    name: string;
    surname: string;
    created: string;
    photoUrl: string;
    description: string;
    isFollowed: boolean;
    isPrivate: boolean;
    followersCount: number;
    followingCount: number;
    posts: Post[];
    followers: Follower[];
    following: Follower[];
    followersRequests: Follower[];
    followingRequests: Follower[];
}