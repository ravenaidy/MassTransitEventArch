<template>
    <div class="register-container sign-in-container">
        <h2>Login</h2>
        <form @submit.prevent="login">
            <span>
                <i>
                    <font-awesome-icon icon="user" />
                </i>
            </span>
            <input type="text" v-model="username" placeholder="Username" required class="input" />
            <span>
                <i>
                    <font-awesome-icon icon="user" />
                </i>
            </span>
            <input type="text" v-model="username" placeholder="Password" required />
            <button>Sign In</button>
        </form>
    </div>
</template>
<script>

import masstransitHub from "@/hubs/masstransitHub";

export default {
    name: "LogIn",
    data() {
        return {
            username: "",
            password: ""
        }
    },
    methods: {
        login() {
            const request = {
                username: this.username,
                password: this.password
            };
            masstransitHub.client.invoke("SendLoginRequest", request);
        }
    },
    mounted() {
        masstransitHub.client.on("PublishLogin", async (login) => {
            this.$emit("loggedin", login);
        });
    }
}
</script>