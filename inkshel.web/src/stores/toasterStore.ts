import { type Toast } from "@/modules/models/toast";
import { defineStore } from "pinia";
import { ref } from "vue";

export const useToasterStore = defineStore("toaster", () => {
  const toasts = ref<Array<Toast>>(Array.of<Toast>())

  const push = (toast: Toast) => toasts.value = [...toasts.value, toast]
  const remove = (toast: Toast) => toasts.value = toasts.value.filter(t => t.id !== toast.id)

  return { toasts, push, remove }
});
