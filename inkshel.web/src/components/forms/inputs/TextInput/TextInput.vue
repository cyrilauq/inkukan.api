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
import ErrorsWrapper from '../ErrorsWrapper/ErrorsWrapper.vue'
import type { TextInputProps } from './TextInputProps'
import { useField } from 'vee-validate'

const props = defineProps<TextInputProps>()

const { value, errors } = useField(props.inputIdentifier, props.validator, {
  syncVModel: true,
  initialValue: props.modelValue,
})
</script>
<style lang="css" scoped></style>
