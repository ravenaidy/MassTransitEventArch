<template>
  <div class="form_wrapper">
    <div class="form_container">
      <div class="title_container">
        <h2>{{registrationHeader}}</h2>
      </div>
      <div class="row clearfix">
        <p>
          {{registrationMessage}}
        </p>
      </div>      
    </div>    
  </div>
</template>

<script>
export default {
  name: "AccountRegistered",
  data() {
    return {
      registrationHeader: "header",
      registrationMessage: "message"
    }
  }
}
</script>

<style scoped>

</style>