import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/Home.vue'
import Login from '@/views/Login.vue'
import { useUserStore } from '@/stores/UserStore'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      meta: { requiresAuth: true },
    },
    {
      path: '/login',
      name: 'login',
      component: Login,
    },
  ],
})

router.beforeEach((to) => {
  const store = useUserStore()
  const userRole = store.role
  if (to.meta.requiresAuth && !userRole) {
    return '/login'
  }
})

export default router
