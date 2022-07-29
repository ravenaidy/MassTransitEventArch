import { HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr'

class MasstransitHub {
    constructor() {
        this.client = new HubConnectionBuilder()
            .withUrl("http://localhost:5002/masstransitHub")
            .build();
    }

    start() {
        try {
            if (this.client.state === HubConnectionState.Disconnected) {
                this.client.start();
            }
        } catch (err) {
            setTimeout(this.start(), 5000);
        }
    }
}
export default new MasstransitHub()