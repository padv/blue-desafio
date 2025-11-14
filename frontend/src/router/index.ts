/// <reference types="vite/client" />

import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/LoginView.vue'),
    meta: { requiresAuth: false }
  },
  {
    path: '/contacts',
    name: 'Contacts',
    component: () => import('@/views/ContactsView.vue'),
    meta: { requiresAuth: true }
  },
  {
    path: '/',
    redirect: '/contacts'
  }
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

// Guard para proteger rotas
router.beforeEach((to, _from, next) => {
  const isAuthenticated = localStorage.getItem('token')
  const requiresAuth = to.meta?.requiresAuth as boolean | undefined

  if (requiresAuth && !isAuthenticated) {
    next('/login')
  } else {
    next()
  }
})

export default router
