<script setup lang="ts">
import { onMounted } from 'vue'
import AppFooter from './components/global/AppFooter.vue'
import AppHeader from './components/global/AppHeader.vue'
import { useAuthentication } from './modules/composables/useAuth'
import { useRouter } from 'vue-router'
import AppToaster from './components/global/AppToaster.vue'

const { loadFromLocalStorage } = useAuthentication()
const router = useRouter()

onMounted(() => {
  if (loadFromLocalStorage()) {
    router.push({ name: 'dashboard' })
  }
})
</script>

<template>
  <div class="h-screen flex flex-col gap-6">
    <AppToaster />
    <AppHeader></AppHeader>
    <main class="grow px-2">
      <RouterView></RouterView>
    </main>
    <AppFooter></AppFooter>
  </div>
</template>

<style scoped></style>
