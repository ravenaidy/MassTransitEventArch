import { createApp } from 'vue'
import App from './App.vue'
import router from "@/router";
import { library } from '@fortawesome/fontawesome-svg-core'
import { faEnvelope, faUser, faLock, faAddressBook, faCity, faPhone } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

library.add(faEnvelope, faUser, faLock, faAddressBook, faCity, faPhone)

createApp(App)
    .use(router)
    .component("font-awesome-icon", FontAwesomeIcon)
    .mount("#app");