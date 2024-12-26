<template>
  <v-dialog max-width="600px">
    <v-card>
      <v-card-title style="text-align: center">{{ props.title }}</v-card-title>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn :variant="VuetifyDefaults.UI_VARIANT" @click="onOperationCanceled">Cancel</v-btn>
        <v-btn :variant="VuetifyDefaults.UI_VARIANT" @click="onOperationSubmited">Ok</v-btn>
        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script setup>
import { reactive, ref } from 'vue'
import { VuetifyDefaults } from '@/meta'
const props = defineProps(['title'])

const dialog = ref(false)
const itemToOperate = reactive({})

const openItemOperationDialog = (item) => {
  dialog.value = true
  itemToOperate.value = item
}
defineExpose({
  openItemOperationDialog,
})

const emit = defineEmits(['onOperationSubmited'])
const onOperationSubmited = () => {
  dialog.value = false
  emit('onOperationSubmited', itemToOperate.value)
}
const onOperationCanceled = () => {
  dialog.value = false
}
</script>
<style scoped></style>
