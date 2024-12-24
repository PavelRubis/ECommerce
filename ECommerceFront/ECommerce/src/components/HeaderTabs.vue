<script setup></script>

<template>
  <div class="header-tabs">
    <v-btn class="btn" variant="outlined">Items</v-btn>
    <v-btn class="btn" variant="outlined">Orders</v-btn>
    <v-btn class="btn" variant="outlined">Customers</v-btn>
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
