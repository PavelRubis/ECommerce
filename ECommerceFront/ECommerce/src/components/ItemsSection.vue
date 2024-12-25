<template>
  <div class="items-data-table">
    <v-data-table :headers="headers" :items="items.values">
      <template v-slot:top>
        <v-toolbar class="table-toolbar" flat>
          <ItemEditionDialog
            @onCreationSubmited="createItem"
            @onEditionSubmited="editItem"
            @onCreationRequested="openCreationDialog"
            ref="editDialog"
          ></ItemEditionDialog>
          <CommandDialog
            @onOperationSubmited="deleteItem"
            ref="deleteDialog"
          ></CommandDialog>
        </v-toolbar>
      </template>
      <template v-slot:[`item.actions`]="{ item }">
        <v-icon class="edit-icon" @click="openEditionDialog(item)">
          fa-regular fa-pen-to-square
        </v-icon>
        <v-icon @click="openDeletionDialog(item)"> fa-regular fa-trash-can </v-icon>
      </template>
    </v-data-table>
  </div>
</template>

<script setup>
import { reactive, ref, inject, onMounted } from 'vue'
const showAlert = inject('showAlert')
const showLoader = inject('showLoader')
import RequestsService from '@/services/RequestsService'
import ItemEditionDialog from '@/components/ItemEditionDialog.vue'
import CommandDialog from '@/components/shared/CommandDialog.vue'
const editDialog = ref(null)
const deleteDialog = ref(null)

const openCreationDialog = (item) => {
  editDialog.value.openItemCreationDialog(item)
}
const openEditionDialog = (item) => {
  editDialog.value.openItemEditionDialog(item)
}
const openDeletionDialog = (item) => {
  deleteDialog.value.openItemOperationDialog(item)
}

const createItem = async (item) => {
  try {
    showLoader(true)
    await RequestsService.Post('api/items/create', item)
    items.values.push(item)
    showAlert(RequestsService.getDefaultSuccessConfig())
  } catch (err) {
    showAlert(RequestsService.getDefaultErrorConfig(err))
  } finally {
    showLoader(false)
  }
}
const editItem = async (item) => {
  try {
    showLoader(true)
    await RequestsService.Put('api/items/edit', item)
    showAlert(RequestsService.getDefaultSuccessConfig())
  } catch (err) {
    showAlert(RequestsService.getDefaultErrorConfig(err))
  } finally {
    showLoader(false)
  }
}
const deleteItem = async (item) => {
  try {
    showLoader(true)
    await RequestsService.Delete('api/items/delete/' + item.id)
    items.values = items.values.filter((el) => el.id !== item.id)
    showAlert(RequestsService.getDefaultSuccessConfig())
  } catch (err) {
    showAlert(RequestsService.getDefaultErrorConfig(err))
  } finally {
    showLoader(false)
  }
}

const headers = [
  {
    title: 'Name',
    align: 'start',
    sortable: false,
    key: 'name',
  },
  {
    title: 'Price',
    align: 'start',
    sortable: true,
    key: 'price',
  },
  {
    title: 'Category',
    align: 'start',
    sortable: false,
    key: 'category',
  },
  {
    title: 'Code',
    align: 'start',
    sortable: false,
    key: 'code',
  },
  { title: 'Actions', key: 'actions', sortable: false },
]

const items = reactive({})

onMounted(async () => {
  try {
    showLoader(true)
    const data = await RequestsService.Get('api/items/all')
    items.values = data.data
  } catch (err) {
    err.msg = 'Failed to load items.'
    showAlert(RequestsService.getDefaultErrorConfig(err))
  } finally {
    showLoader(false)
  }
})
</script>

<style scoped>
.table-toolbar {
  background-color: var(--app-color);
  color: var(--text-color);
  border-color: var(--text-color);
  border-style: solid;
  border-bottom-width: medium;
}
.edit-icon {
  margin-right: 10px;
}
</style>
