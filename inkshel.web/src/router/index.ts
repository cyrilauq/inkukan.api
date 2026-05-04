import { useAuthenticationStore } from '@/stores/authStore'
import DashboardView from '@/views/DashboardView.vue'
import HomeView from '@/views/HomeView.vue'
import { createRouter, createWebHistory } from 'vue-router'

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
      meta: {
        authenticationRequired: true
      }
    }
  ],
})

router.beforeEach((to, _, next) => {
  const authenticationStore = useAuthenticationStore()

  if (!to.meta?.authenticationRequired) {
    next()
    return
  }

  if (to.meta?.authenticationRequired && authenticationStore.connectedUser) {
    next()
    return
  }

  next({ name: "home" })
})

export default router
