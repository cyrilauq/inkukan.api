<script setup lang="ts">
import { useToasterStore } from '@/stores/toasterStore'

const toasterStore = useToasterStore()
</script>
<template>
  <Teleport to="body">
    <ul v-if="toasterStore.toasts.length" class="fixed z-100 top-0 right-0">
      <li v-for="toast in toasterStore.toasts" :class="toast.status" :key="toast.id">
        <!-- <Icon class="toaster__list-icon" /> -->
        <span class="toaster__list-text">
          {{ toast.text }}
        </span>
      </li>
    </ul>
  </Teleport>
</template>
<style lang="css" scoped>
.warning {
  background-color: yellow;
}

.error {
  background-color: red;
}

.success {
  background-color: green;
}
</style>
