<template>
  <div class="w-full">
    <div class="flex flex-col md:flex-row w-full">
      <label :for="inputIdentifier" class="w-full md:w-30">{{ inputLabel }}</label>
      <input
        class="grow"
        type="email"
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
import type { EmailInputProps } from './EmailInputProps'
import { useField } from 'vee-validate'
import * as yup from 'yup'
import ErrorsWrapper from '../ErrorsWrapper/ErrorsWrapper.vue'

const props = defineProps<EmailInputProps>()

const { value, errors } = useField(
  props.inputIdentifier,
  yup.string().required('Le mail est requis.').email('Le mail doit être un mail valide.'),
  {
    syncVModel: true,
    initialValue: props.modelValue,
  },
)
</script>
<style lang="css" scoped></style>
