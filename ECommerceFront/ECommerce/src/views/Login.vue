<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import Footer from '@/components/shared/Footer.vue'
import AuthService from '@/services/AuthService'
import { useUserStore } from '@/stores/UserStore'

const router = useRouter()
let username = ref('')
let password = ref('')
let loading = ref(false)
let canSubmit = ref(true)
let errRes = ref(false)

const store = useUserStore()
const rules = ref({
  usernameRules: [
    (value) => {
      if (value?.length > 0) {
        return true
      }
      return 'Field is required.'
    },
  ],
  passwordRules: [
    (value) => {
      if (value?.length > 0) {
        return true
      }
      return 'Field is required.'
    },
  ],
})

onMounted(async () => {})

const onSubmit = async () => {
  loading.value = true
  const res = await AuthService.Login(username.value, password.value)
  errRes.value = res.err === true
  if (errRes.value) {
    store.setState({ username: username.value, ...res })
    await router.push('/')
    loading.value = false
  }
}
</script>
<template>
  <div class="login-form">
    <h3 v-if="errRes" class="invalid-creds-text">Invalid credentials</h3>
    <h3 class="promo-title">Sign in to ECommerce</h3>
    <v-sheet class="sheet" width="500" elevation="4" rounded>
      <v-form v-model="canSubmit" fast-fail @submit.prevent="onSubmit">
        <v-text-field
          v-model="username"
          :rules="rules.usernameRules"
          variant="outlined"
          label="username"
        ></v-text-field>
        <v-text-field
          v-model="password"
          :rules="rules.passwordRules"
          type="password"
          variant="outlined"
          label="password"
        ></v-text-field>
        <v-btn
          variant="outlined"
          class="mt-2"
          type="submit"
          block
          :disabled="!canSubmit"
          :loading="loading"
          >Sign in</v-btn
        >
      </v-form>
    </v-sheet>
  </div>
  <Footer></Footer>
</template>

<style scoped>
.login-form {
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.promo-title {
  margin-bottom: 15px;
}
.sheet {
  padding: 30px;
}
.invalid-creds-text {
  color: var(--error-color);
}
</style>
