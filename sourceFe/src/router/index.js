import { createRouter, createWebHistory } from 'vue-router';
import ProductSelector from '@/components/ProductSelector.vue';
import Report from '@/pages/Report.vue';
import OrderForm from '@/components/OrderForm.vue';
import Login from '@/components/Login.vue';
import OrderResult from '@/components/OrderResult.vue';

const routes = [
  {
    path: '/dashboard',
    name: 'ProductSelector',
    component: ProductSelector,
    meta: { 
      requireAuth: true, 
      title: "Dashboard - Lab 6" 
    }
  },
  {
    path: '/report',
    name: 'Report',
    component: Report,
    meta: { 
      requireAuth: true, 
      title: "Report - Lab 6" 
    }
  },
  {
    path: '/order',
    name: 'OrderForm',
    component: OrderForm,
    meta: { 
      requireAuth: true, 
      title: "Order - Lab 6" 
    }
  },
  {
    path: '/order-result/:id',
    name: 'OrderResult',
    component: OrderResult,
    meta: { 
      requireAuth: true, 
      title: "Order Result - Lab 6" 
    }
  },
  {
    path: '/',
    name: 'Login',
    component: Login,
    meta: { title: "Login - Lab 6" }
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

router.beforeEach((to, from, next) => {
  const token = localStorage.getItem("token")
  if (to.meta.requireAuth && !token) {
    next({name: "Login"})
  }
  else {
    next()
  }
})

router.afterEach((to) => {
  if (to.meta.title) {
    document.title = to.meta.title ?? "Loading...";
  }
});

export default router;
