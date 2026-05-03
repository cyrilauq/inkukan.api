<script setup lang="ts">
import LoginForm from './components/forms/LoginForm.vue'
import AppFooter from './components/global/AppFooter.vue'
import AppHeader from './components/global/AppHeader.vue'
</script>

<template>
  <div class="h-screen flex flex-col gap-6">
    <AppHeader></AppHeader>
    <main class="grow px-2">
      <LoginForm />
      <RouterView></RouterView>
    </main>
    <AppFooter></AppFooter>
  </div>
</template>

<style scoped></style>
