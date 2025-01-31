<script setup>
import { ContentSections } from '@/meta'
import { hasPermission } from '@/utils/hasPermission'

const emit = defineEmits(['changeSectionRequested'])
const onClick = (sectionName) => {
  emit('changeSectionRequested', sectionName)
}
</script>

<template>
  <div class="header-tabs">
    <v-btn
      class="btn"
      variant="outlined"
      v-if="hasPermission('ITEMS', 'READ')"
      @click="() => onClick(ContentSections.ITEMS)"
      >Items</v-btn
    >
    <v-btn
      class="btn"
      v-if="hasPermission('ORDERS', 'READ') || hasPermission('ORDERS', 'READ', true)"
      variant="outlined"
      @click="() => onClick(ContentSections.ORDERS)"
      >Orders</v-btn
    >
    <v-btn
      class="btn"
      v-if="hasPermission('CUSTOMERS', 'READ')"
      variant="outlined"
      @click="() => onClick(ContentSections.CUSTOMERS)"
      >Customers</v-btn
    >
  </div>
</template>

<style scoped>
.header-tabs {
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}
.btn {
  height: 100%;
  margin-right: 10px;
  font-weight: bold;
  border-color: var(--text-color);
}
</style>
