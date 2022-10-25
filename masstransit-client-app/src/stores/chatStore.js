import { defineStore } from "pinia";

export const useChatStore = defineStore({
	id: "chat",
	state: () => {
		return {
			messages: [],
		};
	},
	actions: {
		addMessage: (message) => {
			this.messages.push(message);
		},
	},
	getters: {
		getAllMessages: (state) => {
			return state.messages;
		},
	},
});
