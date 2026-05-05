import { type Toast } from "@/modules/models/toast";
import type { ToastPayload } from "@/modules/models/toastPayload";
import type { ToastStatus } from "@/modules/models/toastStatus";
import { useToasterStore } from "@/stores/toasterStore";

const defaultTimeout: number = 20000;

export const useToaster = () => {
  const toasterStore = useToasterStore()

  const createToast = (status: ToastStatus, text: string, title?: string): Toast => {
    return { status, text, title, id: Math.random() * 1000 }
  }

  const showToast = (toastPayload: ToastPayload, status: ToastStatus) => {
    const toast = { status, text: toastPayload.text, title: toastPayload.title, id: Math.random() * 1000 } as Toast

    toasterStore.push(toast)

    setTimeout(() => toasterStore.remove(toast), defaultTimeout);
  }

  const showError = (toastPayload: ToastPayload) => showToast({ ...toastPayload, title: 'Erreur', timeout: 10000 }, "error")
  const showWarning = (toastPayload: ToastPayload) => showToast({ ...toastPayload, title: 'Avertissement', timeout: 5000 }, "warning")
  const showSuccess = (toastPayload: ToastPayload) => showToast({ ...toastPayload, title: 'Succès', timeout: 2000 }, "success")

  return { createToast, showToast, showError, showWarning, showSuccess }
}
