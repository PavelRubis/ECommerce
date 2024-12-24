<script setup>
import Tabs from '@/components/HeaderTabs.vue'
import AccountRightMenu from '@/components/AccountRightMenu.vue'
</script>
<template>
  <div class="header">
    <v-spacer></v-spacer>
    <Tabs></Tabs>
    <v-spacer></v-spacer>
    <v-divider class="divider" vertical></v-divider>
    <AccountRightMenu></AccountRightMenu>
  </div>
</template>
<style scoped>
.header {
  min-height: 70px;
  display: flex;
  align-items: center;
  background-color: var(--app-color);
  color: var(--text-color);
  border-color: rgba(var(--v-border-color), var(--v-border-opacity));
  border-style: solid;
  border-width: thin;
}
.divider {
  margin-right: 10px;
  border-color: var(--text-color);
}
</style>
