import { Like } from './like';
import { Comment } from './comment';


export interface Post {
    id: string;
    description: string;
    created: Date;
    photoUrl: string;
    userId: string;
    userName: string;
    commentsCount: number;
    likesCount: number;
    userPhotoUrl: string;

    comments: Comment[];
    likes: Like[];

}