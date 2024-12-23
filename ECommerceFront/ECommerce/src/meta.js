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

export default { Roles, Permissions }
