<template>
  <div class="w-4/5 md:w-auto flex flex-col items-center p-4">
    <EmailInput input-label="Email" input-identifier="email" placeholder="Email" v-model="email" />
    <PasswordInput
      input-label="Mot de passe"
      input-identifier="password"
      placeholder="Mot de passe"
      v-model="password"
    />
    <ActionButton>Se connecter</ActionButton>
  </div>
</template>
<script setup lang="ts">
import { useForm } from 'vee-validate'
import PasswordInput from './inputs/PasswordInput/PasswordInput.vue'
import { ref } from 'vue'
import EmailInput from './inputs/EmailInput/EmailInput.vue'
import ActionButton from '../buttons/ActionButton.vue'

useForm<{ password: string }>({})

const password = ref<string>()
const email = ref<string>()
</script>
