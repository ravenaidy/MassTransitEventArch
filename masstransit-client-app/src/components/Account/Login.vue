<template>
    <div class="registercontainer" showlogin="showlogin">
        <form @submit.prevent="login">
            <div class="loginform">
                <div class="col">
                    <span>
                        <i>
                            <font-awesome-icon icon="envelope" />
                        </i>
                    </span>
                    <input type="email" v-model="username" placeholder="Username" required />
                </div>
                <div class="col">
                    <span>
                        <i>
                            <font-awesome-icon icon="envelope" />
                        </i>
                    </span>
                    <input type="email" v-model="username" placeholder="Password" required />
                </div>
                <input class="button" type="submit" value="Register" />
            </div>
        </form>
    </div>
</template>
<script>
export default {
    name: "Login",
    props: {
        showlogin: Boolean
    },
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
        masstransitHub.start();
        masstransitHub.client.on("PublishLogin", async (login) => {            
            this.$emit("loggedin", login);
        });
    }
}
</script>
<style lang="scss" scoped>
.registercontainer {
    margin: 0 auto;
    min-height: 400px;
    background-color: $background-color;

    .loginform {
        width: 600px;
        position: absolute;
        background-color: #fff;
        color: #333;
        margin: 0 auto;
        border: 1px solid;
        padding: 10px;
        top: 15%;
        left: 34.5%;
        display: flex;
        flex-direction: column;
        border-radius: 10px;
        grid-template-columns: 1fr 1fr;
        box-shadow: 0 0 3px rgba(0, 0, 0, 0.1);
    }
}
</style>