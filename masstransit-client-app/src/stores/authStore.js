import { defineStore } from "pinia";

export const useAuthStore = defineStore({
	id: "user",
	state: () => ({
		user: {
			username: "",
			token: "",
			id: "",
		},
	}),
	actions: {
		setUserAuth(username, token, id) {
			this.user.username = username;
			this.user.token = token;
			this.user.id = id;
		},
	},
	getters: {
		getToken: (state) => {
			return state.token;
		},
		getLoginId: (state) => {
			return state.user.id;
		},
		getUserName: (state) => {
			return state.user.username;
		},
	},
});
