<template>
  <div class="w-full">
    <div class="flex flex-col md:flex-row w-full">
      <label :for="inputIdentifier" class="w-full md:w-30">{{ inputLabel }}</label>
      <input
        class="grow"
        type="password"
        :id="inputIdentifier"
        :name="inputIdentifier"
        :placeholder="placeholder"
        v-model="value"
        :disabled="disabled"
      />
    </div>
    <ErrorsWrapper :errors="errors" />
  </div>
</template>
<script lang="ts" setup>
import type { PasswordInputProps } from './PasswordInputProps'
import { useField, useFormValues } from 'vee-validate'
import { computed, watch } from 'vue'
import * as yup from 'yup'
import ErrorsWrapper from '../ErrorsWrapper/ErrorsWrapper.vue'

const props = defineProps<PasswordInputProps>()

const passwordSchema = computed(() => {
  let schema = yup.string().required(`${props.inputLabel} est requis`)

  if (props.mustMatch) {
    schema = schema.test(
      'passwords-match',
      'Les mots de passe doivent être identiques',
      (value) => {
        // On compare la valeur actuelle avec celle du champ cible dans formValues
        return value === formValues.value[props.mustMatch!]
      },
    )
  } else {
    schema = schema
      .matches(/[0-9]/, 'Le mot de passe doit contenir au moins un chiffre')
      .matches(/[a-z]/, 'Le mot de passe doit contenir au moins une lettre minuscule')
      .matches(/[A-Z]/, 'Le mot de passe doit contenir au moins une lettre majuscule')
      .matches(/[^a-zA-Z0-9]/, 'Le mot de passe doit contenir au moins un caractère spécial')
      .min(10, 'Le mot de passe doit faire minimum 10 caractères de long')
  }

  return schema
})

const { value, errors, validate } = useField(() => props.inputIdentifier, passwordSchema, {
  syncVModel: true,
  initialValue: props.modelValue,
  validateOnValueUpdate: true,
})

const formValues = useFormValues()
if (props.mustMatch) {
  watch(
    () => formValues.value[props.mustMatch!],
    () => {
      if (value.value) validate()
    },
  )
}
</script>
<style lang="css" scoped></style>
