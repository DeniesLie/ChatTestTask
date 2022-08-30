export interface MessageGet {
    id: number
    text: string
    sentAt: Date
    senderId: number
    senderName: string
    chatroomId: number
    receiverId: number
    isEdited: boolean;
    repliedMessage? : Partial<MessageGet>;
}