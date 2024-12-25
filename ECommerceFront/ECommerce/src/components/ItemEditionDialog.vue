<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template v-slot:activator="{ props }">
      <v-btn
        @click="emit('onCreationRequested')"
        v-bind="props"
        :variant="VuetifyDefaults.UI_VARIANT"
      >
        Create new
      </v-btn>
    </template>
    <v-card>
      <v-card-title>
        <span class="text-h5">{{ title }}</span>
      </v-card-title>

      <v-card-text>
        <v-container>
          <v-row>
            <v-col cols="12"
              ><v-select
                v-model="editedItem.category"
                :items="Object.values(ItemsDefaults.CATEGORY)"
                label="Category"
                variant="outlined"
              ></v-select>
            </v-col>
            <v-col cols="12">
              <v-text-field
                v-model="editedItem.name"
                label="Name"
                :variant="VuetifyDefaults.UI_VARIANT"
              ></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-text-field
                v-model="editedItem.price"
                label="Price"
                :variant="VuetifyDefaults.UI_VARIANT"
              ></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-text-field
                v-model="editedItem.code"
                label="Code"
                :variant="VuetifyDefaults.UI_VARIANT"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-container>
      </v-card-text>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn :variant="VuetifyDefaults.UI_VARIANT" @click="onEditionCanceled"> Cancel </v-btn>
        <v-btn :variant="VuetifyDefaults.UI_VARIANT" @click="onEditionSubmited"> Save </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script setup>
import { reactive, ref } from 'vue'
import { ItemsDefaults, VuetifyDefaults } from '@/meta'

const dialog = ref(false)
const title = ref('New Item')
const editedItem = reactive({
  id: ItemsDefaults.ID,
  category: ItemsDefaults.CATEGORY.OTHER,
  code: '',
  name: '',
  price: 0,
})

function openItemCreationDialog() {
  editedItem.id = ItemsDefaults.ID
  editedItem.category = ItemsDefaults.CATEGORY.OTHER
  editedItem.code = ''
  editedItem.name = ''
  editedItem.price = 0
  title.value = 'New Item'
  dialog.value = true
}
const openItemEditionDialog = (item) => {
  editedItem.id = item.id
  editedItem.category = item.category
  editedItem.code = item.code
  editedItem.name = item.name
  editedItem.price = item.price
  title.value = 'Edit Item'
  dialog.value = true
}

defineExpose({
  openItemEditionDialog,
  openItemCreationDialog,
})

const emit = defineEmits(['onCreationRequested', 'onCreationSubmited', 'onEditionSubmited'])
const onEditionSubmited = () => {
  dialog.value = false
  if (editedItem.id === ItemsDefaults.ID) {
    emit('onCreationSubmited', editedItem)
    return
  }
  emit('onEditionSubmited', editedItem)
}
const onEditionCanceled = () => {
  dialog.value = false
}
</script>
<style scoped></style>
