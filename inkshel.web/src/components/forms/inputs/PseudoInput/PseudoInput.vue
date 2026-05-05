<template>
  <div class="w-full">
    <div class="flex flex-col md:flex-row">
      <label :for="inputIdentifier" class="w-full md:w-30">{{ inputLabel }}</label>
      <input
        type="text"
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
import type { PseudoInputProps } from './PseudoInputProps'
import { useField } from 'vee-validate'
import * as yup from 'yup'

const props = defineProps<PseudoInputProps>()

const { value, errors } = useField(props.inputIdentifier, yup.string().required(), {
  syncVModel: true,
  initialValue: props.modelValue,
})
</script>
<style lang="css" scoped></style>
