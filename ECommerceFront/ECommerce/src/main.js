import { createApp } from 'vue'
import App from './App.vue'

// Router
import router from '@/router'

// Vuetify
import 'vuetify/styles'
import '@fortawesome/fontawesome-free/css/all.css' // Ensure your project is capable of handling css files
import { createVuetify } from 'vuetify'
import { aliases, fa } from 'vuetify/iconsets/fa'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
const vuetify = createVuetify({
  components,
  directives,
  icons: {
    defaultSet: 'fa',
    aliases,
    sets: {
      fa,
    },
  },
})

// Pinia
import { createPinia } from 'pinia'
import piniaPluginPersistedState from 'pinia-plugin-persistedstate'
const pinia = createPinia()
pinia.use(piniaPluginPersistedState)

const app = createApp(App)
app.use(pinia)
app.use(router)
app.use(vuetify)

app.mount('#app')
