import { defineStore } from "pinia";

export const useAuthStore = defineStore({
	id: "user",
	state: () => {
		return {
			user: {
				username: "",
				token: "",
				id: "",
			},
		};
	},
	actions: {
		setUserAuth(username, token, id) {
			this.user.username = username;
			this.user.token = token;
			this.user.id = id;
		},
	},
	getters: {
		getToken: (state) => {
			return state.user.token;
		},
		getLoginId: (state) => {
			return state.user.id;
		},
		getUserName: (state) => {
			return state.user.username;
		},
	},
});
