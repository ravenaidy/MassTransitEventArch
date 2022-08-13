const state = {
	auth: undefined,
};

const getters = {
	getAuth(state) {
		return state.auth;
	},
};

const mutations = {
	addLogin(state, login) {
		state.auth = login;
	},
};

const actions = {
	addLogin({ commit }, login) {
		commit("addLogin", login);
	},
};

export default {
	state,
	getters,
	actions,
	mutations,
};
