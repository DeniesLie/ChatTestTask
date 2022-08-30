import { ChatType } from "./enums/chat-type";
import { MessageGet } from "./messages/messageGet";

export interface Chat {
    id : number
    name? : string
    chatType? : ChatType
    messages: MessageGet[]
    lastMessage?: MessageGet
}

export function initChat(options?: Partial<Chat>): Chat {
    const defaults = {
        id: 0,
        name: '',
        messages: [] as MessageGet[],
    }

    return {
        ...defaults,
        ...options,
    }
}
