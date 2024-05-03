import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/ProjectCreationView.vue'
import ProductAPIView from '@/views/ProductAPIView.vue'


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/', name: 'home', component: HomeView },
    { path: '/Products', name: 'Products', component: ProductAPIView },
    //{ path: '/about', name: 'about', component: AboutView }
    // Добавьте другие маршруты здесь
  ]
})

export default router