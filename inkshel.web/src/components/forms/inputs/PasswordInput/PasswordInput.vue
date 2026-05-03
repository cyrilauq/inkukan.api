<template>
  <div class="w-full">
    <div class="flex flex-col md:flex-row">
      <label :for="inputIdentifier" class="w-full md:w-30">{{ inputLabel }}</label>
      <input
        type="password"
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
import type { PasswordInputProps } from './PasswordInputProps'
import { useField } from 'vee-validate'
import * as yup from 'yup'

const props = defineProps<PasswordInputProps>()

const { value, errors } = useField(props.inputIdentifier, yup.string().required().min(10), {
  syncVModel: true,
  initialValue: props.modelValue,
})
</script>
<style lang="css" scoped></style>
