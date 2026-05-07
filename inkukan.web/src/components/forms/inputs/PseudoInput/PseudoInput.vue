<template>
  <div class="w-full">
    <div class="flex flex-col md:flex-row">
      <label :for="inputIdentifier" class="w-full md:w-30">{{ inputLabel }}</label>
      <input
        class="grow"
        type="text"
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
import type { PseudoInputProps } from './PseudoInputProps'
import { useField } from 'vee-validate'
import * as yup from 'yup'
import ErrorsWrapper from '../ErrorsWrapper/ErrorsWrapper.vue'

const props = defineProps<PseudoInputProps>()

const validator = yup
  .string()
  .required('Le pseudo est requis')
  .matches(/^[a-zA-Z0-9]+$/, 'Le pseudo ne peut contenir que des caractères alphanumérique')

const { value, errors } = useField(props.inputIdentifier, validator, {
  syncVModel: true,
  initialValue: props.modelValue,
})
</script>
<style lang="css" scoped></style>
