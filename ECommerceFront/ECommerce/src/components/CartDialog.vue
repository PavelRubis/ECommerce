<template>
  <v-dialog v-model="dialog" max-width="1000px">
    <v-card>
      <v-card-title>
        <span class="text-h5">Cart items</span>
      </v-card-title>
      <v-data-table :headers="headers" :items="cartItemsTableArray">
        <template v-slot:[`item.actions`]="{ item }">
          <div class="cart-adder">
            <v-icon
              v-show="cartItems[item.itemId]?.itemsCount > 0"
              class="action-icon"
              @click="decrementItemQuantityInCart(item)"
            >
              fa-solid fa-minus
            </v-icon>
            <v-chip class="cart-adder-counter" v-show="cartItems[item.itemId]?.itemsCount > 0">
              {{ cartItems[item.itemId]?.itemsCount ?? '' }}
            </v-chip>
            <v-icon class="action-icon" @click="incrementItemQuantityInCart(item)">
              fa-solid fa-plus
            </v-icon>
          </div>
        </template>
      </v-data-table>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn :variant="VuetifyDefaults.UI_VARIANT" @click="onOrderCreationCanceled">
          Close
        </v-btn>
        <v-btn :variant="VuetifyDefaults.UI_VARIANT" @click="onOrderCreationSubmited"> Save </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script setup>
import { reactive, ref } from 'vue'
import { Roles, VuetifyDefaults } from '@/meta'
import { useUserStore } from '@/stores/UserStore'

const dialog = ref(false)
let order = reactive({})

const store = useUserStore()
let cartItems = reactive({})
let cartItemsTableArray = reactive([])

const initCartItems = () => {
  if (typeof store.cart === 'object' && store.cart !== null) {
    cartItems = store.cart
  }
}
initCartItems()

const incrementItemQuantityInCart = (item) => {
  if (store.incrementItemQuantityInCart(item, item.itemId)) {
    cartItems[item.itemId] = store.cart[item.itemId]
  }
}
const decrementItemQuantityInCart = (item) => {
  if (item.itemsCount === 1) {
    removeItemFromCart(item)
    return
  }
  if (store.decrementItemQuantityInCart(item.itemId)) {
    cartItems[item.itemId] = store.cart[item.itemId]
  }
}
const removeItemFromCart = (item) => {
  store.removeItemFromCart(item.itemId)
  cartItemsTableArray = cartItemsTableArray.filter((el) => el.itemId !== item.itemId)
}

const openOrderItemsDialog = () => {
  cartItemsTableArray = Object.values(store.cart)
  dialog.value = true
}

defineExpose({
  openOrderItemsDialog,
})

const emit = defineEmits(['onSave'])
const onOrderCreationSubmited = () => {
  dialog.value = false
  emit('onSave')
  cartItemsTableArray = []
}
const onOrderCreationCanceled = () => {
  dialog.value = false
}

const headers = [
  {
    title: 'Code',
    align: 'start',
    sortable: false,
    key: 'code',
  },
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
    key: 'itemsPrice',
  },
  {
    title: 'Quantity',
    align: 'start',
    sortable: false,
    key: 'itemsCount',
  },
  { title: 'Actions', key: 'actions', sortable: false },
]
</script>
<style scoped>
.action-icon,
.cart-adder-counter {
  margin-right: 10px;
}
</style>
