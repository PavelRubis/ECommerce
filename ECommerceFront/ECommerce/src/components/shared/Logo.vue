<script setup>
const props = defineProps(['height'])
const height = props.height ?? 80
</script>
<template>
  <a class="logo" href="/">
    <img src="/assets/logo.png" :height="height" />
  </a>
</template>
<style scoped>
.logo {
  display: flex;
  justify-content: center;
  cursor: pointer;
}
.logo > img {
  overflow: unset;
  width: 100%;
}
</style>
