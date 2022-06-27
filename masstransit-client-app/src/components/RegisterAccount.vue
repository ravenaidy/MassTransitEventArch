<template>
  <div id="app">
    <form @submit.prevent="register">
      <div>
        <label for="username">Username:</label>
        <input name="username" v-model="username" placeholder="username">
      </div>
      <div>
        <label for="password">Password:</label>
        <input name="password" v-model="password" placeholder="password" type="password">
      </div>
      <div>
        <label for="firstname">Firstname:</label>
        <input name="firstname" v-model="firstname" placeholder="firstname">
      </div>
      <div>
        <label for="lastname">Lastname:</label>
        <input name="lastname" v-model="lastname" placeholder="lastname">
      </div>
      <div>
        <label for="gender">Gender:</label>
        <select v-model="gender">
          <option disabled value="">Please select gender</option>
          <option value="1">Male</option>
          <option value="2">Female</option>
        </select>
      </div>
      <div>
        <label for="addressline1">Address line 1:</label>
        <input name="addressline1" v-model="addressline1" placeholder="addressline1">
      </div>
      <div>
        <label for="addressline2">Address line 2:</label>
        <input name="addressline2" v-model="addressline2" placeholder="addressline2">
      </div>
      <div>
        <label for="addressline3">Address line 3:</label>
        <input name="addressline3" v-model="addressline3" placeholder="addressline3">
      </div>
      <div>
        <label for="city">City:</label>
        <input name="city" v-model="city" placeholder="city">
      </div>
      <div>
        <label for="postalcode">Postal Code:</label>
        <input name="postalcode" v-model="postalcode" placeholder="postalcode" type="number">
      </div>
      <div>
        <input type="button" value="Register">
      </div>
    </form>    
  </div>
</template>

<script>
export default {
  name: "RegisterAccount",
  data() {
    return {
      username: "",
      password: "",
      firstname: "",
      lastname: "",
      gender: "",
      addressline1: "",
      addressline2: "",
      addressline3: "",
      city: "",
      postalcode: ""      
    }
  },
  methods: {
    async register() {     
    }
  }
}
</script>

<style scoped>

</style>