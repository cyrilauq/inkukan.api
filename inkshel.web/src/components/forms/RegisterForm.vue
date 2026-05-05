<template>
  <div class="w-full flex flex-col items-center p-4 bg-white text-black font-normal">
    <h3>Inscription</h3>
    <hr class="w-full mt-2 mb-4" />
    <PseudoInput input-label="Pseudo" input-identifier="pseudo" placeholder="Pseudo" />
    <EmailInput input-label="Email" input-identifier="email" placeholder="Email" />
    <PasswordInput input-label="Mot de passe" input-identifier="password" />
    <PasswordInput input-label="Confirmation de mot de passe" input-identifier="confirmPassword" />
    <ActionButton class="mt-6" :disabled="!canClickConnect" @click="onSubmit"
      >S'inscrire</ActionButton
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
import PseudoInput from './inputs/PseudoInput/PseudoInput.vue'

const emits = defineEmits<{
  (e: 'cancel'): void
}>()

type RegisterFormSchema = {
  firstname: string
  lastname: string
  email: string
  pseudo: string
  password: string
  confirmPassword: string
}
const { errors, values } = useForm<RegisterFormSchema>({})

const canClickConnect = computed(
  () =>
    errors.value.email === undefined &&
    errors.value.confirmPassword === undefined &&
    errors.value.password === undefined,
)

const { register } = useAuthentication()
const router = useRouter()

const onSubmit = async () => {
  register({ ...values }, values.password)
  await router.push({ name: 'dashboard' })
  emits('cancel')
}
</script>
