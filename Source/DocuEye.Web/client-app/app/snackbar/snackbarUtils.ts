import { enqueueSnackbar, type VariantType } from "notistack"

export const snackbarUtils = {
    success(msg: string) {
        this.toast(msg, 'success')
    },
    warning(msg: string) {
        this.toast(msg, 'warning')
    },
    info(msg: string) {
        this.toast(msg, 'info')
    },
    error(msg: string) {
        this.toast(msg, 'error')
    },
    toast(msg: string, variant: VariantType = 'default') {
        enqueueSnackbar(msg, { variant })
    }
}