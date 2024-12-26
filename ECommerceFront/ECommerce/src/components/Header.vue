<script setup>
import HeaderSectionsButtons from '@/components/HeaderSectionsButtons.vue'
import HeaderAccountDropdown from '@/components/HeaderAccountDropdown.vue'
import HeaderCartDialog from '@/components/HeaderCartDialog.vue'
import Logo from '@/components/shared/Logo.vue'

const emit = defineEmits(['changeSectionRequested'])
const changeSectionRequested = (sectionName) => {
  emit('changeSectionRequested', sectionName)
}
</script>

<template>
  <div class="header">
    <Logo :height="50"></Logo>
    <v-spacer></v-spacer>
    <HeaderSectionsButtons @changeSectionRequested="changeSectionRequested"></HeaderSectionsButtons>
    <v-spacer></v-spacer>
    <v-divider class="divider" vertical></v-divider>
    <HeaderCartDialog></HeaderCartDialog>
    <HeaderAccountDropdown></HeaderAccountDropdown>
  </div>
</template>

<style scoped>
.header {
  display: flex;
  align-items: center;
  min-height: 70px;
  background-color: var(--app-color);
  color: var(--text-color);
  border-color: rgba(var(--v-border-color), var(--v-border-opacity));
  border-bottom-width: thin;
  border-bottom-style: solid;
}
.divider {
  margin-right: 10px;
  border-color: var(--text-color);
}
</style>
