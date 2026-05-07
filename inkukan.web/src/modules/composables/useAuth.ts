import { useAuthenticationStore } from "@/stores/authStore"
import { userApi } from "./useApi"
import type { User } from "../models/user"
import { useLocalStorage } from "./useLocalStorage"
import { useToaster } from '@/modules/composables/useToaster'

export const useAuthentication = () => {
  const { post } = userApi()

  const { setUser } = useAuthenticationStore()
  const { store, get } = useLocalStorage()
  const toaster = useToaster()

  const login = async (login: string, password: string) => {
    try {
      const user = await post<User>('auth/login', { login, password })
      setUser(user)
      store("connectedUser", user)
      toaster.showSuccess({ text: "Successfully connected" })
    } catch (err) {
      console.error(err);
      toaster.showError({ text: "An unexpected error occured" })
    }
  }

  const register = async (user: User, password: string): Promise<boolean> => {
    try {
      const createdUser = await post<User>('auth/register', { ...user, username: user.pseudo, password })
      setUser(createdUser)
      store("connectedUser", createdUser)
      toaster.showSuccess({ text: "Tu t'es inscrit avec succès" })
      return true;
    } catch (err) {
      toaster.showError({ text: "An unexpected error occured" })
      return false;
    }
  }

  const loadFromLocalStorage = () => {
    try {
      const user = get<User>("connectedUser")
      if (user) {
        setUser(user)
        toaster.showSuccess({ text: "Session restored" })
        return true
      }
    } catch (err) {
      console.error(err)
      toaster.showError({ text: "An unexpected error occured" })
    } finally {
      return false
    }
  }

  return { login, register, loadFromLocalStorage }
}
