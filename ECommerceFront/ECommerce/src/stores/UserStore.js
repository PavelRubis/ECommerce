import { defineStore } from 'pinia'
import { Permissions } from '@/meta'

export const useUserStore = defineStore('user', {
  state: () => ({
    id: null,
    username: null,
    role: null,
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
      this.role = accountData.role
      this.id = accountData.id
    },
  },
  persist: true,
})
