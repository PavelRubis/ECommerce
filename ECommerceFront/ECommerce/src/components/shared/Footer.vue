<script setup></script>
<template>
  <v-footer class="footer" border>
    <v-row justify="center" no-gutters>
      <v-col class="text-center mt-4" cols="12">
        {{ new Date().getFullYear() }} — ECommerce. The best for the best.
      </v-col>
    </v-row>
  </v-footer>
</template>
<style scoped>
.footer {
  background-color: var(--app-color);
  color: var(--text-color);
  font-weight: bold;
}
</style>
