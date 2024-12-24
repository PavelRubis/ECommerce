<script setup>
import Footer from '@/components/shared/Footer.vue'
import Header from '@/components/Header.vue'
</script>
<template>
  <div class="layout">
    <Header></Header>
    <div class="layout"></div>
    <Footer></Footer>
  </div>
</template>
<style scoped>
.layout {
  height: 100%;
}
.divider {
  margin-right: 10px;
  border-color: var(--text-color);
}
</style>
