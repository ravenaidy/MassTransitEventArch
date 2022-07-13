import { createWebHistory, createRouter } from "vue-router"
import RegisterAccount from "@/components/Account/RegisterAccount";
import AccountRegistered from "@/components/Account/AccountRegistered";

const routes = [
    {
        path: "/registeraccount",
        name: "RegisterAccount",
        component: RegisterAccount
    },
    {
        path: "/accountregistered",
        name: "AccountRegistered",
        component: AccountRegistered        
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router