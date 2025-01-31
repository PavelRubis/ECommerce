import { Permissions, Actions } from '@/meta'
import { useUserStore } from '@/stores/UserStore'

export const hasPermission = (section, action, isSelf = false) => {
  const store = useUserStore()
  if (!store.role) return false

  const rolePermissions = Permissions[store.role][section] || {}
  const permission = rolePermissions[action]

  if (!permission) return false
  if (permission === Actions.ANY) return true
  if (permission === Actions.SELF) return isSelf

  return false
}
