<script setup></script>
<template>
  <v-footer class="footer" border>
    <v-row justify="center" no-gutters>
      <v-col class="text-center mt-4" cols="12">
        ECommerce - common place for uncommon shopping. © {{ new Date().getFullYear() }}.
      </v-col>
    </v-row>
  </v-footer>
</template>
<style scoped>
.footer {
  background-color: var(--app-color);
  color: var(--text-color);
  font-weight: bold;
  border-style: solid;
  border-bottom-width: medium;
  border-right: none;
  border-left: none;
}
</style>
