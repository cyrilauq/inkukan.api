import type { ToastStatus } from "./toastStatus"

export type Toast = {
  text: string
  title?: string
  status: ToastStatus
  id: number
}
