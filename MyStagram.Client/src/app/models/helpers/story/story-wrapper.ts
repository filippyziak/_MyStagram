import { Story } from "../../domain/story/story";

export interface StoryWrapper {
    userId: string;
    userName: string;
    userPhotoUrl: string;
    isWatched: boolean;
    storyToWatch: Story;

    stories: Story[];
}