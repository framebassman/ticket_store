import React, { useState } from 'react';
import { connect } from 'react-redux';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Container from '@material-ui/core/Container';
import Box from '@material-ui/core/Box';

import { actionCreators, TurnstileActions } from '../../../store/Turnstile/actions';
import { VerificationMethod } from '../../../store/Turnstile/verificationMethods';
import Status from '../status/Status';
import './TurnstileManual.css';

const TurnstileManual = ({ verify }: TurnstileActions) => {
  const [ticketNumber, setTicketNumber] = useState('');

  return (
    <Container className="turnstile__manual" fixed>
      <Status />
      <form 
        onSubmit={e => {
          e.preventDefault();
          verify(ticketNumber, VerificationMethod.Manual);
        }}
      >
        <Box>
          <TextField
            id="ticket_number"
            label="Ticket number"
            onChange={(e) => setTicketNumber(e.target.value)}
            margin="normal"
            variant="outlined"
            inputProps={{
              type: 'number'
            }}
          />
        </Box>
        <Box>
          <Button
            id="verify"
            variant="contained"
            color="primary"
            size="large"
            type="submit"
          >
            Verity the ticket
          </Button>
        </Box>
      </form>
    </Container>
  )
};

export default connect(
  () => ({}),
  actionCreators
)(TurnstileManual);
