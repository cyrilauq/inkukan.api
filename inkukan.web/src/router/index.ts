import DashboardView from '@/views/DashboardView.vue'
import HomeView from '@/views/HomeView.vue'
import { createRouter, createWebHistory } from 'vue-router'
import { requireAuthentication } from './guards/requiredAuthenticationGuard'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "",
      name: "home",
      component: HomeView
    },
    {
      path: "/dashboard",
      name: "dashboard",
      component: DashboardView,
      beforeEnter: requireAuthentication,
    }
  ],
})

export default router
