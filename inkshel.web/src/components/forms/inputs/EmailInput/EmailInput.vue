<template>
  <div class="w-full">
    <div class="flex flex-col md:flex-row">
      <label :for="inputIdentifier">{{ inputLabel }}</label>
      <input
        type="email"
        :id="inputIdentifier"
        :name="inputIdentifier"
        :placeholder="placeholder"
        v-model="value"
        :disabled="disabled"
      />
    </div>
    <div v-if="errors?.length > 0" class="error-info">
      <p v-for="error in errors" :key="error">{{ error }}</p>
    </div>
  </div>
</template>
<script lang="ts" setup>
import type { EmailInputProps } from './EmailInputProps'
import { useField } from 'vee-validate'
import * as yup from 'yup'

const props = defineProps<EmailInputProps>()

const { value, errors } = useField(props.inputIdentifier, yup.string().required().min(10), {
  syncVModel: true,
  initialValue: props.modelValue,
})
</script>
<style lang="css" scoped></style>
