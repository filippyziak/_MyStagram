export interface Follower {
    senderId: string;   
    recipientId: string;
    senderName: string;
    senderPhotoUrl: string;
    recipientName: string;
    recipientPhotoUrl: string;
    recipientAccepted: boolean;
    isWatched: boolean;
}