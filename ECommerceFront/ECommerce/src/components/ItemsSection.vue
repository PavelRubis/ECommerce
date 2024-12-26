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
          <CommandDialog @onOperationSubmited="deleteItem" ref="deleteDialog"></CommandDialog>
        </v-toolbar>
      </template>
      <template v-slot:[`item.cart`]="{ item }">
        <div class="cart-adder">
          <v-icon
            v-show="cartItems[item.id]?.itemsCount > 0"
            class="action-icon"
            @click="decrementItemQuantityInCart(item)"
          >
            fa-solid fa-minus
          </v-icon>
          <v-chip class="cart-adder-counter" v-show="cartItems[item.id]?.itemsCount > 0">
            {{ cartItems[item.id]?.itemsCount ?? '' }}
          </v-chip>
          <v-icon class="action-icon" @click="incrementItemQuantityInCart(item)">
            fa-solid fa-plus
          </v-icon>
          <v-icon @click="removeItemFromCart(item)"> fa-solid cross </v-icon>
        </div>
      </template>
      <template v-slot:[`item.actions`]="{ item }">
        <v-icon class="action-icon" @click="openEditionDialog(item)">
          fa-regular fa-pen-to-square
        </v-icon>
        <v-icon @click="openDeletionDialog(item)"> fa-regular fa-trash-can </v-icon>
      </template>
    </v-data-table>
  </div>
</template>

<script setup>
import { reactive, ref, inject, onMounted } from 'vue'
import RequestsService from '@/services/RequestsService'
import ItemEditionDialog from '@/components/ItemEditionDialog.vue'
import CommandDialog from '@/components/shared/CommandDialog.vue'
import { useUserStore } from '@/stores/UserStore'
const showAlert = inject('showAlert')
const showLoader = inject('showLoader')
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

const store = useUserStore()
let cartItems = reactive({})

const initCartItems = () => {
  if (typeof store.cart === 'object' && store.cart !== null) {
    cartItems = store.cart
  }
}
initCartItems()

const incrementItemQuantityInCart = (item) => {
  if (store.incrementItemQuantityInCart(item, item.id)) {
    cartItems[item.id] = store.cart[item.id]
  }
}
const decrementItemQuantityInCart = (item) => {
  if (store.decrementItemQuantityInCart(item.id)) {
    cartItems[item.id] = store.cart[item.id]
  }
}
const removeItemFromCart = (item) => {
  store.removeItemFromCart(item.id)
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
  { title: 'Add to cart', key: 'cart', sortable: false },
  { title: 'Edit or delete', key: 'actions', sortable: false },
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
}
.cart-adder {
  display: flex;
  flex-direction: row;
  align-items: center;
}
.action-icon,
.cart-adder-counter {
  margin-right: 10px;
}
</style>
