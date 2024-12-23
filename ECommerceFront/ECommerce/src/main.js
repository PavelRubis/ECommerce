import { createApp } from 'vue'
// Components
import App from './App.vue'

// Router
import router from '@/router'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
const vuetify = createVuetify({
  components,
  directives,
})

// Pinia
import { createPinia } from 'pinia'

const app = createApp(App)

app.use(router)
app.use(vuetify)
app.use(createPinia())

app.mount('#app')
