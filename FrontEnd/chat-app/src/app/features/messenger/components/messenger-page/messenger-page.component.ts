import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from 'src/app/features/auth/services/auth-service/auth.service';
import { Chat } from '../../models/chat';
import { ChatsService } from '../../services/chats-service/chats.service';
import { MessagesService } from '../../services/messages-service/messages.service';

@Component({
  selector: 'app-messenger-page',
  templateUrl: './messenger-page.component.html',
  styleUrls: ['./messenger-page.component.css']
})
export class MessengerPageComponent implements OnInit, OnDestroy{

  constructor(
    public chatsService: ChatsService,
    public messagesService: MessagesService,
  ) { }

  ngOnInit(): void {
    // load chats into service
    this.chatsService.getChats()
      .subscribe(() => this.messagesService.joinGroups());
      
    this.messagesService.connectToSignalRServer();
  }

  ngOnDestroy(): void {
    this.messagesService.disconnectFromSignalRServer();
  }

  onChatSelect(chat: Chat): void {
    this.chatsService.selectedChat = chat;
  }
}
