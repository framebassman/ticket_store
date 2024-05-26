import React, { useState } from 'react';
import { connect } from 'react-redux';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Container from '@mui/material/Container';
import Box from '@mui/material/Box';

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
            label="Номер Билета"
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
            Проверить Билет
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
