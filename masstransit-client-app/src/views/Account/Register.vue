<template>
    <MassTransitNav />
   
            <RegisterAccount @registered-account="displayRegisteredAccount" />
        
        <div v-if="showRegister === false" class="form">
            <AccountRegistered isRegistered="isRegistered" />
        </div>   
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
        }
    },
    methods: {
        displayRegisteredAccount(isRegistered) {
            this.showRegister = false;
            this.isRegistered = isRegistered;
        }
    }
}
</script>