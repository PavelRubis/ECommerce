<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template v-slot:activator="{ props }">
      <v-btn
        @click="emit('onCreationRequested')"
        v-bind="props"
        :variant="VuetifyDefaults.UI_VARIANT"
      >
        Create new
      </v-btn>
    </template>
    <v-card>
      <v-card-title>
        <span class="text-h5">{{ title }}</span>
      </v-card-title>

      <v-card-text>
        <v-container>
          <v-row>
            <v-col cols="12">
              <v-text-field
                v-model="editedAccount.username"
                label="Username"
                :variant="VuetifyDefaults.UI_VARIANT"
              ></v-text-field>
            </v-col>
            <v-col cols="12"
              ><v-select
                v-model="editedAccount.role"
                :items="[Roles.CUSTOMER_ROLE, Roles.MANAGER_ROLE]"
                label="Role"
                variant="outlined"
              ></v-select>
            </v-col>
            <v-col cols="12">
              <v-text-field
                v-model="editedAccount.customer.code"
                label="Code"
                :variant="VuetifyDefaults.UI_VARIANT"
              ></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-text-field
                v-model="editedAccount.customer.discount"
                label="Discount"
                :variant="VuetifyDefaults.UI_VARIANT"
              ></v-text-field>
            </v-col>
            <v-col cols="12">
              <v-text-field
                v-model="editedAccount.password"
                type="password"
                variant="outlined"
                label="Password"
              ></v-text-field>
            </v-col>

            <v-col cols="12">
              <v-text-field
                v-model="editedAccount.customer.name"
                label="Name"
                :variant="VuetifyDefaults.UI_VARIANT"
              ></v-text-field>
            </v-col>
            <v-col cols="12"
              ><v-text-field
                v-model="editedAccount.customer.address"
                label="Address"
                :variant="VuetifyDefaults.UI_VARIANT"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-container>
      </v-card-text>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn :variant="VuetifyDefaults.UI_VARIANT" @click="onEditionCanceled"> Cancel </v-btn>
        <v-btn :variant="VuetifyDefaults.UI_VARIANT" @click="onEditionSubmited"> Save </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script setup>
import { reactive, ref } from 'vue'
import { Roles, AccountsDefaults, VuetifyDefaults } from '@/meta'

const dialog = ref(false)
const title = ref('New Customer')
let editedAccount = reactive({
  id: AccountsDefaults.ID,
  username: '',
  role: Roles.CUSTOMER_ROLE,
  customer: {
    name: '',
    code: '',
    address: '',
    discount: 0,
  },
})

function openAccountCreationDialog() {
  editedAccount.id = AccountsDefaults.ID
  editedAccount.customer.name = ''
  editedAccount.customer.code = ''
  editedAccount.customer.discount = 0
  title.value = 'New Customer'
  dialog.value = true
}
const openAccountEditionDialog = (acc) => {
  title.value = 'Edit Customer'
  editedAccount = acc
  dialog.value = true
}

defineExpose({
  openAccountEditionDialog,
  openAccountCreationDialog,
})

const emit = defineEmits(['onCreationRequested', 'onCreationSubmited', 'onEditionSubmited'])
const onEditionSubmited = () => {
  dialog.value = false
  if (editedAccount.id === AccountsDefaults.ID) {
    emit('onCreationSubmited', editedAccount)
    return
  }
  emit('onEditionSubmited', editedAccount)
}
const onEditionCanceled = () => {
  dialog.value = false
}
</script>
<style scoped></style>
