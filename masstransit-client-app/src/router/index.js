import { createWebHistory, createRouter } from "vue-router"
import Register from "@/views/Account/Register"
import Home from "@/views/Home"

const routes = [
    {
        path: "/",
        name: "Home",
        component: Home        
    },
    {
        path: "/register",
        name: "Register",
        component: Register
    }   
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router