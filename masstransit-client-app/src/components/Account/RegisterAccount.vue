<template>
  <div class="register-container sign-up-container">        
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
        <input type="text" v-model="addressline3" placeholder="Address line 3" required />
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
      <div class="fullcol">
        <button>Sign Up</button>
      </div>
    </form>
  </div>
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