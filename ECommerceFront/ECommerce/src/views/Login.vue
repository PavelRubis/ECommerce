<script setup>
import { ref, onMounted } from 'vue'
import Footer from '@/components/shared/Footer.vue'
import AuthService from '@/services/AuthService'
import { useUserStore } from '@/stores/UserStore'

let username = ref('')
let password = ref('')
let canSubmit = ref(true)
let authRes = ref(true)

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
  const res = await AuthService.Login(username.value, password.value)
  authRes.value = res.err === true
  if (!authRes.value) {
    useUserStore.setState({ username: username.value, ...res })
  }
}
</script>
<template>
  <div class="login-form">
    <h3 v-if="!authRes" class="invalid-creds-text">Invalid credentials</h3>
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
        <v-btn variant="outlined" class="mt-2" type="submit" block :disabled="!canSubmit"
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
