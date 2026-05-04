import type { User } from "@/modules/models/user";
import { defineStore } from "pinia";
import { ref } from "vue";

export const useAuthenticationStore = defineStore("authentication", () => {
  const connectedUser = ref<User>()

  const setUser = (user: User) => connectedUser.value = user

  return { connectedUser, setUser }
})
