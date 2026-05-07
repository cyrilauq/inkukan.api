export const useLocalStorage = () => {
  const store = (key: string, value: object) => localStorage.setItem(key, JSON.stringify(value))
  const get = <T>(key: string) => {
    const storedValue = localStorage.getItem(key)

    return storedValue ? JSON.parse(storedValue) as T : undefined
  }

  return { store, get }
}
