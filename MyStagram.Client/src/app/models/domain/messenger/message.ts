export interface Message {
    id: string;
    senderId: string;
    recipientId: string;
    content: string;
    dateCreated: Date;
    senderName: string;
    senderPhotoUrl: string;
    recipientName: string;
    recipientPhotoUrl: string;
    isRead: boolean;
}