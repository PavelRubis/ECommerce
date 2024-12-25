<template>
  <div>
    <v-data-table :headers="headers" :items="accounts.values">
      <template v-slot:top>
        <v-toolbar class="table-toolbar" flat>
          <AccountEditionDialog
            @onCreationSubmited="createAccount"
            @onEditionSubmited="editAccount"
            @onCreationRequested="openCreationDialog"
            ref="editDialog"
          ></AccountEditionDialog>
          <ItemDeletionDialog
            @onDeletionSubmited="deleteAccount"
            ref="deleteDialog"
          ></ItemDeletionDialog>
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
import AccountEditionDialog from '@/components/AccountEditionDialog.vue'
import ItemDeletionDialog from '@/components/ItemDeletionDialog.vue'
const editDialog = ref(null)
const deleteDialog = ref(null)

const openCreationDialog = (account) => {
  editDialog.value.openAccountCreationDialog(account)
}
const openEditionDialog = (account) => {
  editDialog.value.openAccountEditionDialog(account)
}
const openDeletionDialog = (account) => {
  deleteDialog.value.openItemDeletionDialog(account)
}

const createAccount = async (account) => {
  try {
    showLoader(true)
    await RequestsService.Post('api/accounts/create', account)
    accounts.values.push(account)
    showAlert(RequestsService.getDefaultSuccessConfig())
  } catch (err) {
    showAlert(RequestsService.getDefaultErrorConfig(err))
  } finally {
    showLoader(false)
  }
}
const editAccount = async (account) => {
  try {
    showLoader(true)
    await RequestsService.Put('api/accounts/edit', account)
    showAlert(RequestsService.getDefaultSuccessConfig())
  } catch (err) {
    showAlert(RequestsService.getDefaultErrorConfig(err))
  } finally {
    showLoader(false)
  }
}
const deleteAccount = async (account) => {
  try {
    showLoader(true)
    await RequestsService.Delete('api/accounts/delete/' + account.id)
    accounts.values = accounts.values.filter((el) => el.id !== account.id)
    showAlert(RequestsService.getDefaultSuccessConfig())
  } catch (err) {
    showAlert(RequestsService.getDefaultErrorConfig(err))
  } finally {
    showLoader(false)
  }
}

const headers = [
  {
    title: 'Username',
    align: 'start',
    sortable: false,
    key: 'username',
  },
  {
    title: 'Role',
    align: 'start',
    sortable: false,
    key: 'role',
  },
  {
    title: 'Code',
    align: 'start',
    sortable: false,
    key: 'customer.code',
  },
  {
    title: 'Discount',
    align: 'start',
    sortable: true,
    key: 'customer.discount',
  },
  {
    title: 'Address',
    align: 'start',
    sortable: false,
    key: 'customer.address',
  },
  { title: 'Actions', key: 'actions', sortable: false },
]

const accounts = reactive({})

onMounted(async () => {
  try {
    showLoader(true)
    const data = await RequestsService.Get('api/accounts/all')
    accounts.values = data.data
  } catch (err) {
    err.msg = 'Failed to load accounts.'
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
