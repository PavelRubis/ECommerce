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
    },
    {
      path: '/login',
      name: 'login',
      component: Login,
    },
  ],
})

router.beforeEach((to, from, next) => {
  const userRole = useUserStore?.stateGetter?.role
  const isUnAuth = typeof userRole !== 'string' || userRole === null
  if (isUnAuth && to.name !== 'login') {
    next('/login')
  } else {
    next()
  }
})

export default router
