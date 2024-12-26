export const Roles = {
  MANAGER_ROLE: 'Manager',
  CUSTOMER_ROLE: 'Customer',
}

export const Actions = {
  SELF: 'SELF',
  ANY: 'ANY',
}

export const Permissions = {
  ORDERS: {
    [Roles.MANAGER_ROLE]: {
      READ: Actions.ANY,
      EDIT: Actions.ANY,
    },
    [Roles.CUSTOMER_ROLE]: {
      CREATE: Actions.SELF,
      READ: Actions.SELF,
      DELETE: Actions.SELF,
    },
  },
  ITEMS: {
    [Roles.MANAGER_ROLE]: {
      READ: Actions.ANY,
      CREATE: Actions.ANY,
      EDIT: Actions.ANY,
      DELETE: Actions.ANY,
    },
    [Roles.CUSTOMER_ROLE]: {
      READ: Actions.ANY,
    },
  },
  CUSTOMERS: {
    [Roles.MANAGER_ROLE]: {
      READ: Actions.ANY,
      CREATE: Actions.ANY,
      EDIT: Actions.ANY,
      DELETE: Actions.ANY,
    },
  },
}

export const ContentSections = {
  ITEMS: 'items',
  ORDERS: 'orders',
  CUSTOMERS: 'customers',
}

export const ItemsDefaults = {
  CATEGORY: {
    OTHER: 'Other',
    SHIRT: 'Shirt',
    T_SHIRT: 'TShirt',
    SWEATER: 'Sweater',
    JACKET: 'Jacket',
    JEANS: 'Jeans',
    SHORTS: 'Shorts',
    DRESS: 'Dress',
    SCARF: 'Scarf',
    HAT: 'Hat',
    SHOES: 'Shoes',
  },
  ID: '00000000-0000-0000-0000-000000000000',
}

export const AccountsDefaults = {
  ROLE: {
    CUSTOMER: 'Customer',
    MANAGER: 'Manager',
  },
  ID: '00000000-0000-0000-0000-000000000000',
}

export const OrderItemsDefaults = {
  ID: '00000000-0000-0000-0000-000000000000',
}

export const VuetifyDefaults = {
  UI_VARIANT: 'outlined',
}

export default {
  Roles,
  Permissions,
  ContentSections,
  ItemsDefaults,
  OrderItemsDefaults,
  VuetifyDefaults,
}
