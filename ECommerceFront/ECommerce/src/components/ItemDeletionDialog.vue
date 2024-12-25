<template>
  <v-dialog v-model="dialog" max-width="500px">
    <v-card>
      <v-card-title class="text-h5">Are you sure you want to delete this item?</v-card-title>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn :variant="VuetifyDefaults.UI_VARIANT" @click="onDeletionCanceled">Cancel</v-btn>
        <v-btn :variant="VuetifyDefaults.UI_VARIANT" @click="onDeletionSubmited">Delete</v-btn>
        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script setup>
import { reactive, ref } from 'vue'
import { VuetifyDefaults } from '@/meta'

const dialog = ref(false)
const itemToDelete = reactive({})

const openItemDeletionDialog = (item) => {
  dialog.value = true
  itemToDelete.value = item
}
defineExpose({
  openItemDeletionDialog,
})

const emit = defineEmits(['onDeletionSubmited'])
const onDeletionSubmited = () => {
  dialog.value = false
  emit('onDeletionSubmited', itemToDelete.value)
}
const onDeletionCanceled = () => {
  dialog.value = false
}
</script>
<style scoped></style>
