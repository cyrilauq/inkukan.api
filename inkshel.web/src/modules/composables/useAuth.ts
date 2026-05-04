import { useAuthenticationStore } from "@/stores/authStore"
import { userApi } from "./useApi"
import type { User } from "../models/user"

export const useAuthentication = () => {
  const { post } = userApi()

  const { setUser } = useAuthenticationStore()

  const login = async (login: string, password: string) => {
    try {
      const user = await post<User>('auth/login', { login, password })
      setUser(user)
    } catch (err) {
      console.error(err);
    }
  }

  return { login }
}
