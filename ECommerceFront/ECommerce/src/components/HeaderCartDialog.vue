<script setup>
import { ref, inject } from 'vue'
import { useUserStore } from '@/stores/UserStore'
import CartDialog from '@/components/CartDialog.vue'
import RequestsService from '@/services/RequestsService'
/* const showAlert = inject('showAlert')
const showLoader = inject('showLoader') */

const store = useUserStore()
const cartDialog = ref(null)

const onCartBtnClick = () => {
  cartDialog.value.openOrderItemsDialog()
}

const createOrder = async () => {
  try {
    //showLoader(true)
    const newOrder = {
      customerId: store.customerId,
      status: 'New',
      orderItems: Object.values(store.cart),
    }
    await RequestsService.Post('api/orders/create', newOrder)
    store.clearCart()
    //showAlert(RequestsService.getDefaultSuccessConfig())
  } catch (err) {
    //showAlert(RequestsService.getDefaultErrorConfig(err))
  } finally {
    //showLoader(false)
  }
}
</script>

<template>
  <v-btn class="btn" variant="outlined" icon="fa-solid fa-cart-arrow-down" @click="onCartBtnClick">
  </v-btn>
  <CartDialog ref="cartDialog" @on-save="createOrder"></CartDialog>
</template>

<style scoped>
.btn {
  height: 100%;
  font-weight: bold;
  border-color: var(--text-color);
  margin-right: 10px;
}
</style>
