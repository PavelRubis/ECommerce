<script setup>
import { ContentSections } from '@/meta'
import { ref } from 'vue'
import Footer from '@/components/shared/Footer.vue'
import Header from '@/components/Header.vue'
import HomeContent from '@/components/HomeContent.vue'

const currentSection = ref(ContentSections.ITEMS)
const changeSectionRequested = (sectionName) => {
  currentSection.value = sectionName
  console.log(currentSection.value)
}
</script>

<template>
  <div class="home-layout">
    <Header @changeSectionRequested="changeSectionRequested"></Header>
    <HomeContent :section="currentSection"></HomeContent>
    <Footer></Footer>
  </div>
</template>

<style scoped>
.home-layout {
  height: 100%;
  padding-right: 10px;
  padding-left: 10px;
}
</style>
