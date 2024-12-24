<script setup>
import { ref } from 'vue'
import { useUserStore } from '@/stores/UserStore'
import { useRouter } from 'vue-router'
import AuthService from '@/services/AuthService'

const router = useRouter()
const store = useUserStore()

let data = ref([{ title: 'My account info' }, { title: 'Log out' }])
let isDialogOpedned = ref(false)
let dialogText = ref('')

const onMenuBtnClick = (index) => {
  switch (index) {
    case 0:
      isDialogOpedned.value = true
      dialogText.value = 'Username: ' + store.username + '. Role: ' + store.role + '.'
      break
    case 1:
      AuthService.Logout().finally(() => {
        store.setState({})
        router.push('/login')
      })
      break
  }
}
</script>

<template>
  <v-btn id="profile-btn" class="btn" variant="outlined" icon="fa-regular fa-user"> </v-btn>
  <v-menu activator="#profile-btn">
    <v-list>
      <v-list-item
        @click="onMenuBtnClick(index)"
        v-for="(item, index) in data"
        :key="index"
        :value="index"
      >
        <v-list-item-title>{{ item.title }}</v-list-item-title>
      </v-list-item>
    </v-list>
  </v-menu>

  <v-dialog v-model="isDialogOpedned" width="auto">
    <v-card
      max-width="500"
      prepend-icon="fa-solid fa-user-large"
      :text="dialogText"
      title="Account information"
    >
      <template v-slot:actions>
        <v-btn class="ms-auto" text="Close" @click="isDialogOpedned = false"></v-btn>
      </template>
    </v-card>
  </v-dialog>
</template>

<style scoped>
.btn {
  height: 100%;
  margin-right: 10px;
  font-weight: bold;
  border-color: var(--text-color);
}
</style>
