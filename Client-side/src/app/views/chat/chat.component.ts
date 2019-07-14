import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../../services/signalR.service';
import { HttpClient } from '@angular/common/http';
import { ContactService } from '../../services/contact.service';
import { Contact } from '../../models/contact.model';
import { Message } from '../../models/message.model';
@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  message: string;
  contactList: Array<Contact> = new Array<Contact>();
  currentUser: string;
  messageList: Array<Message> = new Array<Message>(
    
  );
  constructor(public signalRService: SignalRService, private http: HttpClient, private contactService: ContactService) { }

  ngOnInit() {
    this.signalRService.startConnection();
    this.getContact();
    this.signalRService.hubConnection.on('ReceiveMessage', (username,message) => {
      this.messageList.push({text:message,isIncoming:true,sender:username})
      this.currentUser = username;
    });
  }
  onSendMessage() {
    this.messageList.push({ text: this.message, isIncoming: false, sender: "" })
    this.signalRService.addSendPrivateMessage(this.currentUser, this.message);
    this.message='';
  }
  getContact() {
    this.contactService.getContactList().subscribe((data) => {
      this.contactList = data;
    }, err => console.log(err));
  }
  onChatWith(contact: Contact) {
    this.currentUser = contact.userName;
  }
}
