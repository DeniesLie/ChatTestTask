import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators'
import { environment } from 'src/environments/environment';
import { Chat, initChat } from '../../models/chat';

@Injectable({
  providedIn: 'root'
})
export class ChatsService {

  private chatsUrl: string = `${environment.apiUrl}/chatrooms`;
  private _chats : Chat[] = [];
  private _selectedChat : Chat | undefined;

  public get chats() : Chat[] {
    return this._chats;
  }

  public get selectedChat() : Chat | undefined {
    return this._selectedChat;
  }
  public set selectedChat(chat : Chat | undefined) {
    this._selectedChat = chat;
  }

  constructor(private httpClient: HttpClient) { }

  public getChats() : Observable<Chat[]> 
  {
    return this.httpClient.get<Chat[]>(this.chatsUrl).pipe(
      tap((chats : Chat[]) => this._chats = chats.map(c => initChat(c))),
      catchError(err => this.handleError('get messages', err))
    );
  }

  public findById(id: number) : Chat | undefined 
  {
    var chat = this.chats.find(c => c.id == id);
    return chat;
  }

  private handleError(method: string, err: any) {
    console.log(`Error while trying to {action}. ${err}`);
    return throwError(err);
  }  
}
