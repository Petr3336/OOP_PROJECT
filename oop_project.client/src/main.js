import './assets/main.css'

import { createApp } from 'vue'

import App from './App.vue'

// Vue router
import router from './router/router.js'

//Pinia
import { createPinia } from 'pinia'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import '@mdi/font/css/materialdesignicons.css' // Ensure you are using css-loader


const vuetify = createVuetify({
  components,
  directives,
  theme: {
    defaultTheme: window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light'
  }
})

const pinia = createPinia()

import '@imengyu/vue3-context-menu/lib/vue3-context-menu.css'
import ContextMenu from '@imengyu/vue3-context-menu'

export const app = createApp(App).use(vuetify).use(router).use(pinia).use(ContextMenu).mount('#app')
