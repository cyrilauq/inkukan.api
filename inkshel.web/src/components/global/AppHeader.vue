<template>
  <header class="py-4 px-2 bg-blue-400 flex flex-col gap-4 text-white font-bold">
    <div class="flex justify-between">
      <h1>Inkshelf</h1>
      <div class="flex gap-2" v-if="!authenticationStore.connectedUser">
        <ActionButton @click="onConnectClicked">Se connecter</ActionButton>
        <ActionButton @click="onRegisterClicked">S'inscrire</ActionButton>
      </div>
      <div v-else>
        <span>{{ authenticationStore.connectedUser.email }}</span>
      </div>
    </div>
    <div>
      <nav>
        <ul class="flex flex-row gap-8">
          <li><RouterLink :to="{ name: 'home' }">Accueil</RouterLink></li>
          <template v-if="authenticationStore.connectedUser">
            <li><RouterLink :to="{ name: 'dashboard' }">Dashboard</RouterLink></li>
          </template>
        </ul>
      </nav>
    </div>
    <div
      class="top-0 left-0 w-screen h-screen fixed flex items-center justify-center bg-blue-100/50 bg-opacity-20"
      v-if="formShowed !== undefined"
      @click="onOutsideFormClicked"
    >
      <div ref="formWrapperRef" class="flex w-4/5 md:w-auto">
        <LoginForm v-if="formShowed === 'login'" @cancel="closeForm" />
        <RegisterForm v-if="formShowed === 'register'" @cancel="closeForm" />
      </div>
    </div>
  </header>
</template>
<script setup lang="ts">
import { ref, useTemplateRef } from 'vue'
import ActionButton from '../buttons/ActionButton.vue'
import LoginForm from '../forms/LoginForm.vue'
import { useAuthenticationStore } from '@/stores/authStore'
import RegisterForm from '../forms/RegisterForm.vue'

const authenticationStore = useAuthenticationStore()

const formShowed = ref<'login' | 'register' | undefined>()

const onConnectClicked = () => (formShowed.value = 'login')
const onRegisterClicked = () => (formShowed.value = 'register')

const formWrapperRef = useTemplateRef('formWrapperRef')

const onOutsideFormClicked = (e: any) => {
  e.preventDefault()
  if (!formWrapperRef.value?.contains(e.target)) {
    closeForm()
  }
}

const closeForm = () => (formShowed.value = undefined)
</script>
