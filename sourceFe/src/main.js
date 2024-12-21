import { createApp } from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import axios from 'axios'
import VueAxios from 'vue-axios'
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import { createPinia } from 'pinia'
import "./plugins/removeToken"
import "./plugins/addHeadersAuthorization"
import "./style.css"
import '@mdi/font/css/materialdesignicons.css'
import { aliases, mdi } from 'vuetify/iconsets/mdi-svg'

const pinia = createPinia()
const options = {};
const vuetify = createVuetify({
    components,
    directives,
    icons: {
    defaultSet: 'mdi',
    aliases,
    sets: {
      mdi,
    },
  },
  })

createApp(App).use(router).use(VueAxios, axios).use(Toast, options).use(vuetify).use(pinia).mount('#app')
