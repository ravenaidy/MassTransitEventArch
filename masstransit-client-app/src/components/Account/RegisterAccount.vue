<template>
  <div class="registercontainer" v-if="showRegistration">
    <form @submit.prevent="register">
      <div class="form">
        <h2>Registration</h2>
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
          <input class="button" type="submit" value="Register" />
        </div>
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
    masstransitHub.start();
    masstransitHub.client.on("PublishAccountCreated", async (request) => {
      let isUserRegistered = await JSON.parse(request);
      this.$emit("registered-account", isUserRegistered.isRegistered);
    });
  }
}
</script>

<style scoped lang="scss">
@import "../../assets/scss/config";

.registercontainer {
  margin: 0 auto;
  min-height: 400px;
  background-color: $background-color;
  overflow: visible;

  .form {
    width: 600px;
    position: absolute;
    background-color: #fff;
    color: #333;
    margin: 0 auto;
    border: 1px solid;
    padding: 10px;
    top: 15%;
    left: 34.5%;
    display: grid;
    border-radius: 10px;
    grid-template-columns: 1fr 1fr;
    box-shadow: 0 0 3px rgba(0, 0, 0, 0.1);

    h2 {
      grid-column: 1 / 3;
      text-align: center;
      margin: 10px 0px 0px 0px;
      color: #333;
    }

    .fullcol {
      grid-column: 1 / 3;
      padding: 10px 20px;
      position: relative;

      >span {
        position: absolute;
        height: 57%;
        border-right: 1px solid grey;
        text-align: center;
        vertical-align: middle;
        width: 35px;

        >i {
          position: absolute;
          margin: 2.5px -6px;
        }
      }
    }

    .halfcol {
      padding: 10px 20px;
      position: relative;

      >span {
        position: absolute;
        height: 56%;
        border-right: 1px solid grey;
        text-align: center;
        vertical-align: middle;
        width: 35px;

        >i {
          position: absolute;
          margin: 2.5px -6px;
        }
      }
    }
  }
}

input {
  &[type="text"],
  &[type="email"],
  &[type="password"] {
    width: 100%;
    height: 30px;
    padding: 8px 10px 9px 40px;
    outline: none;
  }

  &[type="text"]:hover,
  &[type="email"]:hover,
  &[type="password"]:hover {
    background: #fafafa;
    border: 1px solid #42b983;
  }

  &[type="text"]:focus,
  &[type="email"]:focus,
  &[type="password"]:focus {  
    border: 1px solid #42b983;
    background: #fafafa;    
  }

  &[type="submit"] {
    width: 95%;
    height: 35px;
    margin: 10px 15px;
    background: darken($color: #a1c3ff, $amount: 20%);
    border: none;
    color: #fff;
    font-size: medium;

    &:hover {
      background: lighten($color: $background-color, $amount: 5%);
    }

    &:focus {
      background: lighten($color: $background-color, $amount: 5%);
    }
  }
}

select {
  width: 100%;
  height: 30px;
  padding: 0px 15px;
  background: #fff;
  cursor: pointer;
  border: 1px solid grey;
  border-radius: 0;
  appearance: none;

  &::-ms-expand {
    display: none;
  }

  &:hover,
  &:focus {
    color: black;
    background: #fafafa;
    border-color: black;
    outline: none;
  }
}

.select_arrow {
  position: absolute;
  top: calc(50% - 3px);
  right: 30px;
  width: 0;
  height: 0;
  pointer-events: none;
  border-width: 8px 5px 0 5px;
  border-style: solid;
  border-color: #7b7b7b transparent transparent transparent;

  &:hover,
  &:focus {
    border-top-color: black;
  }
}

@media (max-width: 600px) {
  .registercontainer {
    .fullcol {
      width: 100%;      
    }
  }
  .bottom_row {
    .col_half {
      width: 50%;
      float: left;
    }
  }
  .form_container {
    .row {
      .col_half.last {
        border-left: none;
      }
    }
  }
  .remember_me {
    padding-bottom: 20px;
  }
}

</style>