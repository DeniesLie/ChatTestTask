import { Component, Input, OnInit } from '@angular/core';
import { Chat } from '../../models/chat';

@Component({
  selector: 'app-chat-preview',
  templateUrl: './chat-preview.component.html',
  styleUrls: ['./chat-preview.component.css']
})
export class ChatPreviewComponent implements OnInit {

  @Input() chat?: Chat;
  
  constructor() { }

  ngOnInit(): void {
  }

}
