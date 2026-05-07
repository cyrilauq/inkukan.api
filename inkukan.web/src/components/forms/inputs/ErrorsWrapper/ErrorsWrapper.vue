<template>
  <div
    v-if="errors && errors?.length > 0"
    class="error-info text-red-600 text-justify bg-red-100 p-2 rounded-lg flex flex-col"
  >
    <div class="flex gap-2 w-full" v-for="error in errors" :key="error">
      <CancelIcon class="mt-0.5" color="red" width="20" heigth="20" />
      <p>{{ error }}</p>
    </div>
  </div>
</template>
<script setup lang="ts">
import CancelIcon from '@/components/Icons/CancelIcon.vue'
import { type ErrorsWrapperProps } from './ErrorsWrapperProps'

defineProps<ErrorsWrapperProps>()
</script>
