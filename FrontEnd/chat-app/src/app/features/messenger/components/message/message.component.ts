import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from 'src/app/features/auth/services/auth-service/auth.service';
import { MessageGet } from '../../models/messages/messageGet';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {

  @Input() message?: MessageGet;

  get formatedMessageTime() {
    let currentDate = new Date();
    let dateTime = currentDate
    if (this.message?.sentAt){
      dateTime = new Date(this.message.sentAt)
    }

    return dateTime.toLocaleTimeString('en-US', {
      hour: '2-digit',
      minute: '2-digit',
    });
  }

  constructor(
    public authService: AuthService
  ) { }

  ngOnInit(): void {
  }

  getProfileLetters(username: string | undefined) : string
  {
    return username?.split(' ').map(s => s[0].toUpperCase()).join('') ?? '';
  }

}
