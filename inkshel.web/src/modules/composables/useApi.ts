import redaxios from 'redaxios'
import { useAuthenticationStore } from '@/stores/authStore'

export const userApi = () => {
  const authenticationStore = useAuthenticationStore()

  const axios = redaxios.create({
    baseURL: `${import.meta.env.VITE_API_URL}`,
    headers: {
      Authorization: `Bearer ${authenticationStore.connectedUser?.accessToken}`,
    }
  })

  const get = async <T>(endpoint: string) => (await axios.get(endpoint)).data as T
  const post = async <T>(endpoint: string, body?: object) => (await axios.post(endpoint, { ...body })).data as T

  return { get, post }
}
