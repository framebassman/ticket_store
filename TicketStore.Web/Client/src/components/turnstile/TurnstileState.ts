import { DetectedBarcode } from './camera/DetectedBarcode';

export interface TurnstileState {
  scanning: boolean,
  result: DetectedBarcode,
  pass: boolean,
  wait: boolean,
  isRequested: boolean
}
