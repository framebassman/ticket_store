import { DetectedBarcode } from './DetectedBarcode';

export interface TurnstileState {
  scanning: boolean,
  result: DetectedBarcode,
  pass?: boolean,
  wait?: boolean,
  isRequested: boolean
}
