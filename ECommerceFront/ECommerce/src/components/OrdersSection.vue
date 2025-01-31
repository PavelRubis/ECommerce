<template>
  <div>
    <v-data-table :headers="headers" :items="orders.values">
      <template v-slot:top>
        <v-toolbar class="table-toolbar" flat>
          <CommandDialog
            class="command-dialog"
            @onOperationSubmited="deleteOrder"
            ref="deleteDialog"
            :title="'Do you want to delete this order?'"
          ></CommandDialog>
          <CommandDialog
            class="command-dialog"
            @onOperationSubmited="submitShippingOrder"
            ref="submitDialog"
            :title="'Do you want to submit shipping for this order?'"
          ></CommandDialog>
          <CommandDialog
            class="command-dialog"
            @onOperationSubmited="completeOrder"
            ref="completeDialog"
            :title="'Do you want to mark that order as completed?'"
          ></CommandDialog>
        </v-toolbar>
      </template>
      <template v-slot:[`item.info`]="{ item }">
        <v-icon class="action-icon" @click="openDeletionDialog(item)"> fa-solid fa-list </v-icon>
      </template>
      <template v-slot:[`item.actions`]="{ item }">
        <v-icon
          v-if="hasPermission('ORDERS', 'EDIT')"
          class="action-icon"
          @click="openSubmitionDialog(item)"
        >
          fa-regular fa-thumbs-up
        </v-icon>
        <v-icon
          v-if="hasPermission('ORDERS', 'EDIT')"
          class="action-icon"
          @click="openCompletionDialog(item)"
        >
          fa-solid fa-circle-check
        </v-icon>
        <v-icon
          v-if="hasPermission('ORDERS', 'DELETE', true) && item.status === 'New'"
          class="action-icon"
          @click="openDeletionDialog(item)"
        >
          fa-regular fa-trash-can
        </v-icon>
      </template>
    </v-data-table>
  </div>
</template>

<script setup>
import { Roles, VuetifyDefaults } from '@/meta'
import { hasPermission } from '../utils/hasPermission'
import { reactive, ref, inject, onMounted } from 'vue'
import { useUserStore } from '@/stores/UserStore'
const showAlert = inject('showAlert')
const showLoader = inject('showLoader')
import RequestsService from '@/services/RequestsService'
import CommandDialog from '@/components/shared/CommandDialog.vue'
const deleteDialog = ref(null)
const submitDialog = ref(null)
const completeDialog = ref(null)
const store = useUserStore()

const openDeletionDialog = (order) => {
  deleteDialog.value.openItemOperationDialog(order)
}
const openSubmitionDialog = (order) => {
  submitDialog.value.openItemOperationDialog(order)
}
const openCompletionDialog = (order) => {
  completeDialog.value.openItemOperationDialog(order)
}

const submitShippingOrder = async (order) => {
  try {
    showLoader(true)
    await RequestsService.Put('api/orders/submitshipping/' + order.id)
    showAlert(RequestsService.getDefaultSuccessConfig())
  } catch (err) {
    showAlert(RequestsService.getDefaultErrorConfig(err))
  } finally {
    showLoader(false)
  }
}
const completeOrder = async (order) => {
  try {
    showLoader(true)
    await RequestsService.Put('api/orders/complete/' + order.id)
    showAlert(RequestsService.getDefaultSuccessConfig())
  } catch (err) {
    showAlert(RequestsService.getDefaultErrorConfig(err))
  } finally {
    showLoader(false)
  }
}
const deleteOrder = async (order) => {
  try {
    showLoader(true)
    await RequestsService.Delete('api/orders/delete/' + order.id)
    orders.values = orders.values.filter((el) => el.id !== order.id)
    showAlert(RequestsService.getDefaultSuccessConfig())
  } catch (err) {
    showAlert(RequestsService.getDefaultErrorConfig(err))
  } finally {
    showLoader(false)
  }
}

const headers = [
  {
    title: 'Number',
    align: 'start',
    sortable: true,
    key: 'orderNumber',
  },
  {
    title: 'Creation date',
    align: 'start',
    sortable: true,
    key: 'orderDate',
  },
  {
    title: 'Status',
    align: 'start',
    sortable: false,
    key: 'status',
  },
  {
    title: 'Shipment date',
    align: 'start',
    sortable: true,
    key: 'shipmentDate',
  },
]
if (hasPermission('ORDERS', 'DELETE', true) || hasPermission('ORDERS', 'EDIT')) {
  headers.push({ title: 'Actions', key: 'actions', sortable: false })
}

const orders = reactive({})

onMounted(async () => {
  try {
    showLoader(true)
    const data = await RequestsService.Get('api/orders/all')
    orders.values = data.data
  } catch (err) {
    err.msg = 'Failed to load orders.'
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
.action-icon {
  margin-right: 20px;
}
.command-dialog {
  font-size: xx-small;
}
</style>
