<template>
  <link rel="stylesheet" href="all.css">  
   <RegisterAccount />  
</template>

<script>
import RegisterAccount from "@/components/RegisterAccount";

export default {
  name: 'App',
  components: {
    RegisterAccount
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
