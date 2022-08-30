import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpResponse, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Observable, pipe, Subject, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { AuthService } from 'src/app/features/auth/services/auth-service/auth.service';
import { environment } from 'src/environments/environment';
import { ChatType } from '../../models/enums/chat-type';
import { MessegeDelete } from '../../models/messages/messageDelete';
import { MessageEdit } from '../../models/messages/messageEdit';
import { MessageGet } from '../../models/messages/messageGet';
import { MessageSend } from '../../models/messages/messageSend';
import { ChatsService } from '../chats-service/chats.service';


@Injectable({
  providedIn: 'root'
})
export class MessagesService {

  private hubConnection: HubConnection | undefined;

  private hubUrl: string = environment.hubUrl
  private messagesApiUrl: string = `${environment.apiUrl}/messages`

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  
  public getMessage$ : Subject<MessageGet> = new Subject<MessageGet>()
  public editMessage$ : Subject<MessageEdit> = new Subject<MessageEdit>() 
  public deleteMessage$ : Subject<MessegeDelete> = new Subject<MessegeDelete>()

  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private chatsService: ChatsService)
  { }

  public connectToSignalRServer(): void 
  {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(
        this.hubUrl, 
        { 
          accessTokenFactory: () => this.authService.getAccessToken() 
        } as signalR.IHttpConnectionOptions)
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection has been established'))
      .catch(err => this.handleError('Connect to SignalR server', err));

    this.listenEvents();
  }

  public disconnectFromSignalRServer(): void {
    this.hubConnection?.stop()
    this.chatsService.selectedChat = undefined;
  }

  public getMessagesInChatRoom(chatroomId: number, page: number) : Observable<MessageGet[]>{
    const url: string = `${this.messagesApiUrl}/${chatroomId}/${page}`;
    return this.http.get<MessageGet[]>(url).pipe(
      tap((messages: MessageGet[]) => this.handleGetMessages(chatroomId, messages)),
      catchError(err => this.handleError("Get messages", err))
    )
  }

  public sendMessage(message: MessageSend, messageToReply: MessageGet | undefined) : Observable<MessageGet> {
    var url = `${this.messagesApiUrl}/sendToChatroom`;
    return this.http.post<MessageGet>(url, message, this.httpOptions).pipe(
      tap((message) => this.handleSendMessage(message, messageToReply)),
      catchError(err => this.handleError('Send message', err)));
  }

  public editMessage(message: Partial<MessageEdit>): Observable<MessageGet> {
    return this.http.put<MessageGet>(this.messagesApiUrl, message, this.httpOptions).pipe(
      tap((message) => this.handleEditMessage(message)),
      catchError(err => this.handleError('Edit message', err)))
  }

  public deleteMessage(message: MessegeDelete, isForEveryone: boolean): Observable<HttpResponse> {
    var deleteOption = isForEveryone ? "deleteForEveryone": "deleteForSelf"
    var url = `${this.messagesApiUrl}/${deleteOption}/${message.id}`
    return this.http.delete<HttpResponse>(url).pipe(
      tap(() => this.handleDeleteMessage(message)),
      catchError(err => this.handleError('Delete message', err)))
  }

  public joinGroups(): void {
    this.chatsService.chats
      .filter(c => c.chatType === ChatType.Group) 
      .forEach(chatroom => this.hubConnection?.invoke("joinGroup", chatroom.id))
  }

  private listenEvents(): void {
    this.hubConnection?.on('getMessage', (message : MessageGet) => {
      this.handleGetMessage(message);
      this.getMessage$.next(message);
    });

    this.hubConnection?.on('editMessage', (message: MessageEdit) => {
      this.handleEditMessage(message);
      this.editMessage$.next(message);
    });

    this.hubConnection?.on('deleteMessage', (messageToDelete: MessegeDelete) => {
      this.handleDeleteMessage(messageToDelete);
      this.deleteMessage$.next(messageToDelete);
    });
  }

  private handleError(action: string, err: any){
    console.log(`Error while trying to ${action}. ${err}`)
    return throwError(err);
  }

  private handleGetMessages(chatroomId: number, messages: MessageGet[]) : void {
    var messagesInChat = this.chatsService.findById(chatroomId)?.messages
    messagesInChat?.unshift(...messages.reverse());
  }

  private handleGetMessage(message: MessageGet): void {
    var chat = this.chatsService.findById(message.chatroomId)
    if (chat){
      chat.messages.push(message)
      chat.lastMessage = message
    }
  }

  private handleSendMessage(message: MessageGet, messageToReply: MessageGet | undefined): void {
    var chat = this.chatsService.findById(message.chatroomId)
    if (chat){
      var messagesInChat = chat.messages
      message.repliedMessage = messageToReply
      messagesInChat?.push(message)
      chat.lastMessage = message
    }
  }

  private handleEditMessage(message: Partial<MessageEdit>) : void {
    var chat = this.chatsService.findById(message?.chatroomId ?? 0)
    var messageToUpdate = chat?.messages.find(m => m.id === message.id);

    if (messageToUpdate && chat) {
      messageToUpdate.text = message.text!;
      messageToUpdate.isEdited = true;
      if (chat.lastMessage?.id == messageToUpdate.id)
        chat.lastMessage.text = messageToUpdate.text
    }
  }

  private handleDeleteMessage(messageToDelete: MessegeDelete) : void {
    var chat = this.chatsService.findById(messageToDelete.chatroomId)
    var messages = chat?.messages

    if (chat && messages) {
        chat.messages = messages.filter(m => m.id != messageToDelete.id)
        if (chat.lastMessage?.id == messageToDelete.id)
          chat.lastMessage = messages[1]
    }
  }

}