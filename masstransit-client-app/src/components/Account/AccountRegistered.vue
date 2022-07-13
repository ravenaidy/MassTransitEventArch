<template>
  <div class="details">
    <div class="form_wrapper">
      <div class="form_container">
        <div v-show="isRegistered" class="title_container" >
          <h2>Success</h2>
        </div>
        <div v-show="!isRegistered" class="title_container">
          <h2>Failed</h2>
        </div>
        <div v-show="isRegistered" class="row clearfix">
          <p>
            Account registration was successful.
          </p>
        </div>
        <div v-show="!isRegistered" class="row clearfix">
          <p>
            Account registration was unsuccessful.
          </p>
        </div>
      </div>    
    </div>
  </div>
</template>

<script>
export default {
  name: "AccountRegistered",
  props: {
    isRegistered: Boolean
  }
}
</script>

<style scoped>

</style>