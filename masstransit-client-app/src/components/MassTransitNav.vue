<template>
    <nav class="navbar">
        <div class="container">
            <div class="logo">MassTransit</div>
            <ul>
                <li>
                    <router-link to="/">Home</router-link>
                </li>
                <li>
                    <router-link to="/register">Login</router-link>
                </li>
            </ul>
        </div>
    </nav>
</template>

<script>
export default {
    name: "MassTransitNav"
}
</script>

<style lang="scss" scoped>
@import "../assets/scss/config";

.navbar {
    background: #3474e6;
    color: #fff;
    height: 60px;

    .container {
        display: flex;
        justify-content: space-between;
        align-items: center;
        height: 100%;

        .logo {
            font-size: x-large;
            font-weight: bold;
        }

        ul {
            display: flex;
            list-style: none;
        }

        li {
            margin-left: 20px;
        }

        a {
            color: #fff;
            text-decoration: none;
            font-size: 18px;
            font-weight: bold;
        }

        a:hover {
            color: lightblue;
        }
    }
}
</style>