<template>
  <link rel="stylesheet" href="all.css">

  <div v-show="showRegisteredAccount">
    <AccountRegistered isRegistered=isRegistered />
  </div>
  <div v-show="showRegisterAccount">
    <RegisterAccount @registered-account="displayRegisteredAccount" />
  </div>  
</template>

<script>
import RegisterAccount from "@/components/Account/RegisterAccount";
import AccountRegistered from "@/components/Account/AccountRegistered";

export default {
  name: 'App',
  components: {    
    RegisterAccount,
    AccountRegistered
  },
  data() {
    return {
      showRegisteredAccount: false,
      showRegisterAccount: true,
      isRegistered: false
    }
  },
  methods: {
    displayRegisteredAccount(isRegistered) {
      this.showRegisteredAccount = true;
      this.showRegisterAccount = false;
      this.isRegistered = isRegistered;
    }
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;  
}
</style>
