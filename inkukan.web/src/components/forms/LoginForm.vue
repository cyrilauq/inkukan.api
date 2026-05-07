<template>
  <div class="w-full flex flex-col items-center p-4 bg-white text-black font-normal">
    <h3>Connection</h3>
    <hr class="w-full mt-2 mb-4" />
    <EmailInput input-label="Email" input-identifier="login" placeholder="Email" />
    <PasswordInput
      input-label="Mot de passe"
      input-identifier="password"
      placeholder="Mot de passe"
    />
    <ActionButton class="mt-6" :disabled="!canClickConnect" @click="onSubmit"
      >Se connecter</ActionButton
    >
  </div>
</template>
<script setup lang="ts">
import { useForm } from 'vee-validate'
import PasswordInput from './inputs/PasswordInput/PasswordInput.vue'
import { computed } from 'vue'
import EmailInput from './inputs/EmailInput/EmailInput.vue'
import ActionButton from '../buttons/ActionButton.vue'
import { useAuthentication } from '@/modules/composables/useAuth'
import { useRouter } from 'vue-router'

const emits = defineEmits<{
  (e: 'cancel'): void
}>()

const { errors, values } = useForm<{ password: string; login: string }>({})

const canClickConnect = computed(
  () => errors.value.login === undefined && errors.value.password === undefined,
)

const { login } = useAuthentication()
const router = useRouter()

const onSubmit = async () => {
  await login(values.login, values.password)
  await router.push({ name: 'dashboard' })
  emits('cancel')
}
</script>
