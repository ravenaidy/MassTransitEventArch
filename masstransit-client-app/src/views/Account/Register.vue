<template>
    <MassTransitNav />
    <header class="header">
        <div class="container" ref="container" v-if="showForm" >
            <div class="register-container sign-up-container">
                <RegisterAccount @registered-account="displayRegisteredAccount" />
            </div>
            <div class="register-container sign-in-container">
                <LogIn />
            </div>
            <div class="overlay-container">
                <div class="overlay">
                    <div class="overlay-panel overlay-left">
                        <h1>Welcome back!</h1>
                        <p>Please login with you username and password</p>
                        <button class="ghost" @click="openSignIn">Sign In</button>
                    </div>
                    <div class="overlay-panel overlay-right">
                        <h1>Hello!</h1>
                        <p>Please enter personal details to register account</p>
                        <button class="ghost" @click="openSignup">Sign Up</button>
                    </div>
                </div>
            </div>
        </div>
        <AccountRegistered :show-registered="showRegistered" :isRegistered="isRegistered" />
    </header>
</template>

<script>

import MassTransitNav from '@/components/MassTransitNav.vue'
import RegisterAccount from '@/components/Account/RegisterAccount'
import AccountRegistered from '@/components/Account/AccountRegistered'
import masstransitHub from "@/hubs/masstransitHub";
import LogIn from "../../components/Account/LogIn.vue";

export default {
    name: "Register",
    components: {
        RegisterAccount,
        AccountRegistered,
        MassTransitNav,
        LogIn
    },
    data() {
        return {
            showForm: false,
            showRegistered: true,
            isRegistered: false
        }
    },
    methods: {
        displayRegisteredAccount(account) {
            this.showForm = false;
            this.showRegistered = true;
            this.isRegistered = account.isRegistered;
        },
        openSignIn() {
            this.$refs.container.classList.value = 'container';
        },
        openSignup() {
            this.$refs.container.classList.value = 'container right-panel-active';
        }
    },
    mounted() {
        masstransitHub.start();
        
        masstransitHub.client.onclose(async () => {
            await masstransitHub.client.start();
        });
    }
}

</script>

<style lang="scss" scoped >

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
}

button:active {
    transform: scale(0.95);
}

button:focus {
    outline: none;
}

button.ghost {
    background-color: transparent;
    border-color: #FFFFFF;
}

.container {
    background-color: #fff;
    border-radius: 10px;
    box-shadow: 0 14px 28px rgba(0, 0, 0, 0.25),
        0 10px 10px rgba(0, 0, 0, 0.22);
    position: absolute;
    overflow: hidden;
    width: 910px;
    max-width: 100%;
    height: 550px;
    top: 15%;
    right: 26%;
}

.register-container {
    position: absolute;
    top: 0;
    height: 100%;
    transition: all 0.6s ease-in-out;
}

.sign-in-container {
    left: 0;
    width: 50%;
    z-index: 2;
}

.container.right-panel-active .sign-in-container {
    transform: translateX(100%);
}

.sign-up-container {
    left: 0;
    width: 50%;
    opacity: 0;
    z-index: 1;
}

.container.right-panel-active .sign-up-container {
    transform: translateX(100%);
    opacity: 1;
    z-index: 5;
    width: 50%;
    animation: show 0.6s;    
}

@keyframes show {

    0%,
    49.99% {
        opacity: 0;
        z-index: 1;
    }

    50%,
    100% {
        opacity: 1;
        z-index: 5;
    }
}

.overlay-container {
    position: absolute;
    top: 0;
    left: 50%;
    width: 50%;
    height: 100%;
    overflow: hidden;
    transition: transform 0.6s ease-in-out;
    z-index: 100;
}

.container.right-panel-active .overlay-container {
    transform: translateX(-100%);
}

.overlay {
    background: #0151cc;
    background: -webkit-linear-gradient(to right, #0151cc, #0151cc);
    background: linear-gradient(to right, #0151cc, #0151cc);
    background-repeat: no-repeat;
    background-size: cover;
    background-position: 0 0;
    color: #FFFFFF;
    position: relative;
    left: -100%;
    height: 100%;
    width: 200%;
    transform: translateX(0);
    transition: transform 0.6s ease-in-out;
}

.container.right-panel-active .overlay {
    transform: translateX(50%);
}


.overlay-panel {
    position: absolute;
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    padding: 0 40px;
    text-align: center;
    top: 0;
    height: 100%;
    width: 50%;
    transform: translateX(0);
    transition: transform 0.6s ease-in-out;
}

.overlay-left {
    transform: translateX(-20%);
}

.container.right-panel-active .overlay-left {
    transform: translateX(0);    
}

.overlay-right {
    right: 0;
    transform: translateX(0);        
}

.container.right-panel-active .overlay-right {
    transform: translateX(20%);    
}
</style>