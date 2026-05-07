import type { InputBaseProps } from "../Base/InputBaseProps";
import * as yup from 'yup'

export type TextInputProps = InputBaseProps<string> & {
  validator: yup.StringSchema<string | undefined, yup.AnyObject, undefined, "">
}
