import { HubConnectionBuilder, LogLevel } from "@aspnet/signalr"

class MasstransitHub {
    constructor() {
        this.client = new HubConnectionBuilder()
            .withUrl("http://localhost:5002/masstransitHub")
            .configureLogging(LogLevel.Information)
            .build();        
    }
    
    start() {
        this.client.start();
    }
}
export default new MasstransitHub()