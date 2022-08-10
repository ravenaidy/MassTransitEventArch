import { createStore } from "vuex";
import chat from "./modules/chat";
import auth from "./modules/auth";

export default createStore({
	modules: {
		auth,
		chat,
	},
});
