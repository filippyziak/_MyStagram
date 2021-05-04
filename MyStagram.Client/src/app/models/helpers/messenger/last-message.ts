export interface LastMessage {
    content: string;
    senderId: string;
    senderUserName: string;
    senderPhotoUrl: string;
    dateCreated: Date;
    isRead: boolean;
}