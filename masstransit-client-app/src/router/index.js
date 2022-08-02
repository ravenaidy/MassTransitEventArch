import { createWebHistory, createRouter } from "vue-router"
import Register from "@/views/Account/Register"
import Home from "@/views/Home"
import Dashboard from "@/views/DashBoard"

const routes = [
    {
        path: "/",
        name: "Home",
        component: Home        
    },
    {
        path: "/login",
        name: "Login",
        component: Register
    },
    {
        path: "/dashboard",
        name: "Dashboard",
        component: Dashboard
    }   
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router