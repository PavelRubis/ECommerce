<template>
  <v-container v-if="loaderValue" class="loader">
    <v-progress-circular indeterminate :size="55"></v-progress-circular>
  </v-container>
  <div class="content">
    <ItemsSection v-if="props.section === ContentSections.ITEMS"></ItemsSection>
    <AccountsSection v-if="props.section === ContentSections.CUSTOMERS"></AccountsSection>
    <OrdersSection v-if="props.section === ContentSections.ORDERS"></OrdersSection>
  </div>
  <v-alert
    v-model:="alertConfig.visible"
    :title="alertConfig.title"
    :text="alertConfig.text"
    :type="alertConfig.type"
    :max-width="400"
    closable
  ></v-alert>
</template>

<script setup>
import { ContentSections } from '@/meta'
import { reactive, provide, ref } from 'vue'
import ItemsSection from '@/components/ItemsSection.vue'
import AccountsSection from '@/components/AccountsSection.vue'
import OrdersSection from '@/components/OrdersSection.vue'
const props = defineProps(['section'])

const loaderValue = ref(false)
const alertConfig = reactive({
  type: 'info',
  visible: false,
  text: '',
})
const showAlert = (config) => {
  alertConfig.title = config.title
  alertConfig.text = config.text
  alertConfig.type = config.type
  alertConfig.visible = true
}
const showLoader = (val) => {
  loaderValue.value = val === true
}
provide('showAlert', showAlert)
provide('showLoader', showLoader)
</script>

<style scoped>
.content {
  height: 100%;
}
.loader {
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>
