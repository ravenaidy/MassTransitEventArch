<template>
  <router-view></router-view>
</template>

<script>
export default {
  name: 'App',
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

<style lang="scss">

@import "./assets/scss/config";
@import "./assets/scss/theme.scss";

</style>
