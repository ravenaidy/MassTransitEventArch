<template>
    <h2>Login</h2>
    <form @submit.prevent="login">
        <div>
            <span>
                <i>
                    <font-awesome-icon icon="user" />
                </i>
            </span>
            <input type="text" v-model="username" placeholder="Username" required />
        </div>
        <div>
            <span>
                <i>
                    <font-awesome-icon icon="lock" />
                </i>
            </span>
            <input type="text" v-model="username" placeholder="Password" required />
        </div>
        <button>Sign In</button>
    </form>
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
<style lang="scss" scoped>
h2 {
    text-align: center;
    color: #333;
    position: absolute;
    top: 15%;
    right: 37%;
    font-size: 45px;
}

form {
    background-color: #FFFFFF;
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;

    height: 100%;
    text-align: center;
    margin-top: 35px;

    div {
        position: relative;
        padding: 0;
        width: 100%;

        >span {
            position: absolute;
            color: #333;
            top: 16px;
            left: 58px
        }        
    }
}

button {
    border-radius: 20px;
    border: 1px solid #0151cc;
    background-color: #0151cc;
    color: #FFFFFF;
    font-size: 12px;
    font-weight: bold;
    padding: 12px 45px;
    letter-spacing: 1px;
    text-transform: uppercase;
    transition: transform 80ms ease-in;
    margin-top: 45px;
}

button:active {
    transform: scale(0.95);
}

button:focus {
    outline: none;
}

input {
    background-color: #eee;
    border: none;
    padding: 12px 15px 9px 35px;
    margin: 8px 0;
    width: 80%;
}
</style>