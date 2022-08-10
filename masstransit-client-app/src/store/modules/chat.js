const state = {
	messages: [],
};

const getters = {
	allMessages(state) {
		return state.messages;
	},
};

const mutations = {
	addMessage(state, message) {
		state.messages.push(message);
	},
};

const actions = {
	addMessage({ commit }, message) {
		commit("addMessage", message);
	},
};

export default {
	state,
	getters,
	actions,
	mutations,
};
