<template>

  <div class="showcase-content" v-if="showRegistered">
    <p>{{ displayMessage }}</p>
    <img :src="require(`../../assets/img/${displayImageSrc}`)" />
    
  </div>

</template>

<script>

export default {
  name: "AccountRegistered",
  props: {
    showRegistered: Boolean,
    isRegistered: Boolean
  },
  data() {
    return {
      displayMessage: "",
      displayImageSrc: ""
    }
  },
  beforeMount() {
    if (this.isRegistered) {
      this.displayMessage = "Account registration was successful. You are lucky to have been granted an account. Let us not regret our decision";
      this.displayImageSrc = "oops.jpg";
      return;
    }
    this.displayMessage = "Unfortunately your account registration was unsuccessful. From your input we have determined you are not a fit. Sorry not sorry";
    this.displayImageSrc = "oops.jpg";
  }
}

</script>

<style scoped lang="scss">
.showcase-content {
  display: flex;
  align-items: center;
  margin: 10% 20%;
  line-height: 1.2;

  p {
    justify-content:space-between;
    text-align: left;
    font-size: large;
    overflow: auto;
  }

  img {
    width: 40%;    
  }
}
</style>