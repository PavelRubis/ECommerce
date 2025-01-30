import { defineStore } from 'pinia'
import { Permissions } from '@/meta'

export const useUserStore = defineStore('user', {
  state: () => ({
    id: null,
    customerId: null,
    username: null,
    role: null,
    cart: {},
  }),
  getters: {
    ordersPermissions: (state) => {
      const uname = state.username
      if (typeof uname === 'string' && uname.length) {
        return Permissions.ORDERS[uname]
      }
      return null
    },
    itemsPermissions: (state) => {
      const uname = state.username
      if (typeof uname === 'string' && uname.length) {
        return Permissions.ITEMS[uname]
      }
      return null
    },
    customersPermissions: (state) => {
      const uname = state.username
      if (typeof uname === 'string' && uname.length) {
        return Permissions.CUSTOMERS[uname]
      }
      return null
    },
  },
  actions: {
    setState(accountData) {
      this.username = accountData.username
      this.role = accountData.role,
      this.id = accountData.id,
      this.customerId = accountData.customerId
    },
    incrementItemQuantityInCart(item, itemId) {
      if (typeof itemId !== 'string' || itemId === null) {
        return false
      }
      let cartItem = this.cart[itemId]
      if (typeof cartItem !== 'object' || cartItem === null) {
        cartItem = {
          itemId,
          itemsCount: 1,
          itemsPrice: item.price,
          name: item.name,
          code: item.code,
        }
        this.cart[itemId] = cartItem
        return true
      }
      cartItem.itemsCount++
      return true
    },
    decrementItemQuantityInCart(itemId) {
      if (typeof itemId !== 'string' || itemId === null) {
        return false
      }
      const cartItem = this.cart[itemId]
      if (typeof cartItem !== 'object' || cartItem === null) {
        return false
      }

      if (cartItem.itemsCount > 1) {
        cartItem.itemsCount--
        return true
      }
      delete this.cart[itemId]
      return true
    },
    removeItemFromCart(itemId) {
      const cartItem = this.cart[itemId]
      if (cartItem === undefined) {
        return false
      }
      delete this.cart[itemId]
      return true
    },
    getItemsCountInCart: (itemId) => {
      let cartItem = this?.cart[itemId]
      if (typeof cartItem !== 'object' || cartItem === null) {
        return 0
      }
      return cartItem.itemsCount
    },
    clearCart: () => {
      this.cart = {}
    },
  },
  persist: true,
})
