import { defineStore } from "pinia";

export const useChatStore = defineStore({
	id: "chat",
	state: () => ({
		messages: [],
	}),
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
