import { useLocalStorage } from "@/modules/composables/useLocalStorage"
import { type User } from "@/modules/models/user"

export const requireAuthentication = () => {
  const { get } = useLocalStorage()
  const connectedUser = get<User>("connectedUser");
  return connectedUser !== undefined
}
