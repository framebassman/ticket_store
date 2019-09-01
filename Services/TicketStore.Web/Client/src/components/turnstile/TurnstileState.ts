import { DetectedBarcode } from './camera/DetectedBarcode';

export interface ScannedTicket {
  concertLabel: string,
  used: boolean,
}

export interface TurnstileState {
  scanning: boolean,
  result: DetectedBarcode,
  scannedTicket?: ScannedTicket,
  pass?: boolean,
  wait?: boolean,
  isRequested: boolean
}
