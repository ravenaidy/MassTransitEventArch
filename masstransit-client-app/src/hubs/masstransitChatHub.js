import { HubConnectionBuilder } from '@microsoft/signalr';

class MasstransitChatHub {
    constructor() {
        this.client = new HubConnectionBuilder()
            .withUrl("http://localhost:5002/masstransitChatHub")            
            .build();
    }

    start() {       
            return new Promise((resolve) => {
                this.client.start().then(() => {
                    resolve(this.client.state);
                }); 
            });
    }
}
export default new MasstransitChatHub()