import { createRouter, createWebHistory } from 'vue-router'
import RegisterAccount from "@/components/Account/RegisterAccount";
import AccountRegistered from "@/components/Account/AccountRegistered";

const routes = [
    {
        path: "/",
        name: "Registration",
        component: RegisterAccount
    },
    {
        path: "/AccountRegistered",
        name: "AccountRegistered",
        component: AccountRegistered
    }
]

const router = createRouter( {
    history: createWebHistory(),
    routes 
});

export default router
