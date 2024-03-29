import { createApp } from "vue";
import { createPinia } from "pinia";
import App from "./App.vue";
import router from "@/router";
import { library } from "@fortawesome/fontawesome-svg-core";
import {
	faEnvelope,
	faUser,
	faLock,
	faAddressBook,
	faCity,
	faPhone,
	faCircleCheck,
	faExclamationCircle,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import Toast, { POSITION } from "vue-toastification";
import "vue-toastification/dist/index.css";

library.add(
	faEnvelope,
	faUser,
	faLock,
	faAddressBook,
	faCity,
	faPhone,
	faCircleCheck,
	faExclamationCircle
);

const toastOptions = {
	position: POSITION.BOTTOM_LEFT,
	timeout: 3000,
	showCloseButtonOnHover: true,
	hideProgressBar: true,
	transition: "Vue-Toastification__fade",
};

createApp(App)
	.use(router)
	.use(Toast, toastOptions)
	.component("font-awesome-icon", FontAwesomeIcon)
	.use(createPinia())
	.mount("#app");
