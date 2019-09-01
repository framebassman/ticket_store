import { DetectedBarcode } from './camera/DetectedBarcode';

export interface ScannedTicket {
  concertLabel: string,
  used: boolean,
}

export interface TurnstileState {
  scannedTicket?: ScannedTicket,
  ticketFound?: boolean,
  wait?: boolean,
}
