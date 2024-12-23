import { createApp } from 'vue'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
const vuetify = createVuetify({
  components,
  directives,
})

// Components
import App from './App.vue'

// Router
import router from '@/router'

const app = createApp(App)

app.use(router)
app.use(vuetify)

app.mount('#app')
