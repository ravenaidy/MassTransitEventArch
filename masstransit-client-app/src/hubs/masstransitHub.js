import { HubConnectionBuilder } from "@aspnet/signalr"

class MasstransitHub {
    constructor() {
        this.client = new HubConnectionBuilder()
            .withUrl("http://localhost:5002/masstransitHub")            
            .build();        
    }
    
    start() {
        this.client.start();
    }
}
export default new MasstransitHub()