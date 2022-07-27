<template>

  <div class="registercontainer" v-if="showRegistered">
    <div class="showcase">      
      <i v-if="isRegistered === true"><font-awesome-icon icon="circle-check" size="6x" color="#fff" /></i>
      <i v-else><font-awesome-icon icon="exclamation-circle" size="6x" color="red" /></i>
      <p v-if="isRegistered === true">Account registration was successful. You are lucky to have been granted an account. Let us not regret our decision.</p>
      <p v-else>Unfortunately your account registration was unsuccessful. From your input we have determined you are not a fit. Sorry not sorry.</p>
    </div>
  </div>

</template>

<script>

export default {
  name: "AccountRegistered",
  props: {
    showRegistered: Boolean,
    isRegistered: Boolean
  } 
}

</script>

<style scoped lang="scss">
@import "../../assets/scss/config";

.registercontainer {  
  min-height: 400px;  
  background-color: $background-color;

  .showcase {
    width: 600px;
    display: flex;
    flex-direction: column;
    padding: 40px;
    line-height: 1.2;
    justify-content: space-between;
    align-items: center;
    margin: 0px auto;    

    p {
      font-size: large;
      padding: 40px 30px;
      color: #fff;
    }

    i {      
      padding-top: 50px;
      padding-right: 35px ;
    }
  }
}
</style>