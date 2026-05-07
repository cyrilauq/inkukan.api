<template>
  <div class="w-full flex flex-col items-center p-4 bg-white text-black font-normal justify-center">
    <h3>Inscription</h3>
    <hr class="w-full mt-2 mb-4" />
    <ErrorsWrapper :errors="formErrors" />
    <TextInput
      input-identifier="lastname"
      input-label="Nom"
      placeholder="Nom"
      :validator="namesValidators"
    />
    <TextInput
      input-identifier="firstname"
      input-label="Prénom"
      placeholder="Prénom"
      :validator="namesValidators"
    />
    <PseudoInput input-label="Pseudo" input-identifier="pseudo" placeholder="Pseudo" />
    <EmailInput input-label="Email" input-identifier="email" placeholder="Email" />
    <PasswordInput input-label="Mot de passe" input-identifier="password" />
    <PasswordInput
      input-label="Confirmation de mot de passe"
      input-identifier="confirmPassword"
      must-match="password"
    />
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
import TextInput from './inputs/TextInput/TextInput.vue'
import * as yup from 'yup'
import ErrorsWrapper from './inputs/ErrorsWrapper/ErrorsWrapper.vue'

const namesValidators = yup.string().required().min(3).max(100)

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
const { errors, values, validate } = useForm<RegisterFormSchema>()

const formErrors = computed(() => {
  const formFields = Object.entries(errors.value).map((e) => e[0])
  if (formFields.filter((f) => errors.value[f] !== undefined).length > 0)
    return ['Le formulaire contient des erreurs']
  else return []
})

const canClickConnect = computed(
  () =>
    errors.value.email === undefined &&
    errors.value.confirmPassword === undefined &&
    errors.value.password === undefined,
)

const { register } = useAuthentication()
const router = useRouter()

const onSubmit = async () => {
  if (!(await validate()).valid) return
  if (!(await register({ ...values }, values.password))) return
  await router.push({ name: 'dashboard' })
  emits('cancel')
}
</script>
