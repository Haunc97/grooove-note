import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { MessagePackHubProtocol } from '@aspnet/signalr-protocol-msgpack'; 
import { CookieService } from "ngx-cookie-service";
@Injectable({
    providedIn: 'root'
})
export class SignalRService {
    constructor(private cookieService:CookieService){

    }
    public hubConnection: signalR.HubConnection
    public startConnection = () => {
        const strCookie = this.cookieService.get(btoa('userInfo'));
        const LoginResult = JSON.parse(atob(strCookie));
        const token = LoginResult.token;
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:44330/hubs/chat',{accessTokenFactory:()=>token})
            //.withHubProtocol(new)
            .build();

        this.hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))
    }
    public addSendPrivateMessage(toUser:string,text:string){
        this.hubConnection.invoke("sendChatMessage", toUser,text ).catch(function (err) {
            return console.error(err.toString());
        });
    }
}