<template>
  <h2>Registration</h2>
  <form @submit.prevent="register">
    <div class="fullcol">
      <span>
        <i>
          <font-awesome-icon icon="envelope" />
        </i>
      </span>
      <input type="email" v-model="email" placeholder="Email" required />
    </div>
    <div class="halfcol">
      <span><i>
          <font-awesome-icon icon="user" />
        </i></span>
      <input type="text" v-model="username" placeholder="Username" required />
    </div>
    <div class="halfcol">
      <span><i>
          <font-awesome-icon icon="lock" />
        </i></span>
      <input type="password" v-model="password" placeholder="Password" required />
    </div>
    <div class="halfcol">
      <span><i>
          <font-awesome-icon icon="user" />
        </i></span>
      <input type="text" v-model="firstname" placeholder="Firstname" required />
    </div>
    <div class="halfcol">
      <span><i>
          <font-awesome-icon icon="user" />
        </i></span>
      <input type="text" v-model="lastname" placeholder="Lastname" required />
    </div>
    <div class="halfcol">
      <span><i>
          <font-awesome-icon icon="phone" />
        </i></span>
      <input type="text" v-model.number="phoneNumber" placeholder="Phone number" required />
    </div>
    <div class="halfcol">
      <select v-model.number="gender" required>
        <option value="">Select gender</option>
        <option value="1">Male</option>
        <option value="2">Female</option>
      </select>
      <div class="select_arrow"></div>
    </div>
    <div class="halfcol">
      <span><i>
          <font-awesome-icon icon="address-book" />
        </i></span>
      <input type="text" v-model="addressline1" placeholder="Address line 1" required />
    </div>
    <div class="halfcol">
      <span><i>
          <font-awesome-icon icon="address-book" />
        </i></span>
      <input type="text" v-model="addressline2" placeholder="Address line 2" required />
    </div>
    <div class="halfcol">
      <span><i>
          <font-awesome-icon icon="address-book" />
        </i></span>
      <input type="text" v-model="addressline3" placeholder="Address line 3" />
    </div>
    <div class="halfcol">
      <span><i>
          <font-awesome-icon icon="city" />
        </i></span>
      <input type="text" v-model="city" placeholder="City" required />
    </div>
    <div class="halfcol">
      <span><i>
          <font-awesome-icon icon="address-book" />
        </i></span>
      <input type="text" v-model.number="postalcode" placeholder="Postal Code" required />
    </div>
    <div class="halfcol">
      <select v-model="country" placeholder="Country" required>
        <option value="">Select country</option>
        <option value="1">South Africa</option>
        <option value="2">USA</option>
      </select>
      <div class="select_arrow"></div>
    </div>
    <div class="button_container">
      <button>Register</button>
    </div>
  </form>
</template>
<script>
import masstransitHub from "@/hubs/masstransitHub";

export default {
  name: "RegisterAccount",
  props: {
    showRegistration: Boolean
  },
  data() {
    return {
      email: "",
      username: "",
      password: "",
      firstname: "",
      lastname: "",
      gender: "",
      phoneNumber: "",
      addressline1: "",
      addressline2: "",
      addressline3: "",
      city: "",
      postalcode: "",
      country: ""
    };
  },
  methods: {
    async register() {
      const request = {
        email: this.email,
        username: this.username,
        password: this.password,
        firstname: this.firstname,
        lastname: this.lastname,
        phoneNumber: this.phoneNumber,
        gender: this.gender,
        addressline1: this.addressline1,
        addressline2: this.addressline2,
        addressline3: this.addressline3,
        city: this.city,
        postalcode: this.postalcode,
        country: this.country
      };
      masstransitHub.client.invoke("SendNewAccountRequest", request);
    }
  },
  mounted() {
    masstransitHub.client.on("PublishAccountCreated", async (account) => {
      this.$emit("registered-account", account);
    });
  }
}
</script>

<style lang="scss" scoped>
h2 {
  text-align: center;
  color: #333;
  position: absolute;
  top: 2%;
  right: 32%;
  font-size: 25px;
}

form {
  background-color: #FFFFFF;
  display: grid;  
  height: 100%;
  text-align: center;
  grid-template-columns: 1fr 1fr;
  padding-top: 10px;

  .fullcol {
    grid-column: 1 / 3;
    padding: 50px 15px 0;
    margin-bottom: 10px;

    position: relative;

    >span {
      position: absolute;
      height: 50%;
      vertical-align: middle;
      width: 35px;
      color: #333;
      top: 60px
    }

    >input {
      width: 100%;
    }
  }

  .halfcol {
    padding: 10px 15px;
    position: relative;

    >span {
      position: absolute;
      height: 50%;
      vertical-align: middle;
      width: 35px;
      color: #333;
      top: 20px
    }    
    >input {
      width: 100%;
    }
  }
}

input {
  background-color: #eee;
  border: none;
  padding: 8px 10px 9px 30px;
  margin: 4px 0;
}

select {
  position: relative;
  width: 100%;
  display: inline-block;
  padding: 0px 15px;
  height: 32px;
  cursor: pointer;
  border: 0;
  background: #eee;
  margin: 4px auto;
  appearance: none;
}

.select_arrow {
  position: absolute;
  top: calc(50% - 4px);
  right: 25px;
  width: 0;
  height: 0;
  pointer-events: none;
  border-width: 8px 5px 0 5px;
  border-style: solid;
  border-color: #7b7b7b transparent transparent transparent;
}

.button_container {
  grid-column: 1 / 3;
}

button {  
  border-radius: 20px;
  border: 1px solid #0151cc;
  background-color: #0151cc;
  color: #FFFFFF;
  font-size: 12px;
  font-weight: bold;
  padding: 12px 55px;
  letter-spacing: 1px;
  text-transform: uppercase;
  transition: transform 80ms ease-in;
  margin-bottom: 25px;
  margin-left: 10px;
}

button:active {
  transform: scale(0.95);
}

button:focus {
  outline: none;
}
</style>