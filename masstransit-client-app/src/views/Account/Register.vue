<template>
    <MassTransitNav />
    <RegisterAccount @registered-account="displayRegisteredAccount" :show-registration="showRegister" />    
    <AccountRegistered :show-registered="showRegistered" isRegistered="isRegistered" />    
</template>

<script>

import MassTransitNav from '@/components/MassTransitNav.vue'
import RegisterAccount from '@/components/Account/RegisterAccount'
import AccountRegistered from '@/components/Account/AccountRegistered'

export default {
    name: "Register",
    components: {
        RegisterAccount,
        AccountRegistered,
        MassTransitNav
    },
    data() {
        return {
            showRegister: true,
            isRegistered: false,
            showRegistered: false
        }
    },
    methods: {
        displayRegisteredAccount(isRegistered) {
            this.showRegister = false;
            this.showRegistered = true;
            this.isRegistered = isRegistered;
        }
    }
}
</script>