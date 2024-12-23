<script setup>
import { ref } from 'vue'

let username = ref('')
const usernameRules = [
  (value) => {
    if (value?.length >= 0) {
      return true
    }
    return 'Field is required.'
  },
]
let password = ref('')
const passwordRules = [
  (value) => {
    if (value?.length >= 0) {
      return true
    }
    return 'Field is required.'
  },
]

const onClick = () => {
  alert('hi!')
}
</script>
<template>
  <div class="login-form">
    <h3 class="promo-title">ECommerce - лучший для всех</h3>
    <v-sheet class="sheet" width="500" elevation="4" rounded>
      <v-form fast-fail @submit.prevent="onClick">
        <v-text-field
          variant="outlined"
          v-model="username"
          :rules="usernameRules"
          label="username"
          required
        ></v-text-field>
        <v-text-field
          variant="outlined"
          v-model="password"
          :rules="passwordRules"
          label="password"
          type="password"
          required
        ></v-text-field>
        <v-btn variant="outlined" class="mt-2" type="submit" block>Sign in</v-btn>
      </v-form>
    </v-sheet>
  </div>
  <v-footer border>
    <v-row justify="center" no-gutters>
      <v-col class="text-center mt-4" cols="12">
        {{ new Date().getFullYear() }} — <strong>ECommerce</strong>
      </v-col>
    </v-row>
  </v-footer>
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
</style>
