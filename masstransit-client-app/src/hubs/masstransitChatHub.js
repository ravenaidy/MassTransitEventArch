import { HubConnectionBuilder } from "@microsoft/signalr";

class MasstransitChatHub {
	start(token) {
		this.client = new HubConnectionBuilder()
			.withUrl("http://localhost:5002/masstransitChatHub", {
				accessTokenFactory: () => token,
			})
			.build();
		return new Promise((resolve) => {
			this.client.start().then(() => {
				resolve(this.client.state);
			});
		});
	}
}
export default new MasstransitChatHub();
