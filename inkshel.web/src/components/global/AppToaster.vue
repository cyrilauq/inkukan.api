<script setup lang="ts">
import type { Toast } from '@/modules/models/toast'
import { useToasterStore } from '@/stores/toasterStore'

const toasterStore = useToasterStore()

const deleteToast = (toast: Toast) => toasterStore.remove(toast)
</script>
<template>
  <Teleport to="body">
    <ul v-if="toasterStore.toasts.length" class="fixed z-100 top-0 right-0">
      <li
        v-for="toast in toasterStore.toasts"
        :class="`${toast.status} m-4 p-2 rounded-xl`"
        :key="toast.id"
      >
        <div class="w-58 text-white cursor-pointer" @click="deleteToast(toast)">
          <div class="flex flex-row justify-between border-b-2 pb-2 mb-1">
            <img :src="`/images/icons/${toast.status}.svg`" />
            <span v-if="toast.title">{{ toast.title }}</span>
            <img src="/images/icons/close.svg" />
          </div>
          <span class="toaster__list-text">
            {{ toast.text }}
          </span>
        </div>
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
