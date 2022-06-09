import { Injectable } from '@angular/core';
import { webSocket } from 'rxjs/webSocket';
import { Subject } from 'rxjs';
import { studentDataConfig } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WebSocketConnectionService {

  private readonly WS_ENDPOINT:string = studentDataConfig.studentDataWebSocket;

  public subject: Subject<any>;
 
  public connect(): Subject<any>{
    if(!this.subject){
      this.subject = this.create();
      console.log("hurray! socket connection successful");
    }
    return this.subject;
  }

  private create():Subject<any>{

    const subject = webSocket(this.WS_ENDPOINT);

    return subject;

  }
}
