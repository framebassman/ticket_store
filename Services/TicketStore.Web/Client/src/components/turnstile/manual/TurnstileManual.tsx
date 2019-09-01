import React, { useState } from 'react';
import { connect } from 'react-redux';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Container from '@material-ui/core/Container';
import Box from '@material-ui/core/Box';

import { actionCreators } from '../../../store/Turnstile/actions';
import { Status } from '../camera/Status';
import './TurnstileManual.css';

const TurnstileManual = ({ verify, pass, wait }) => {
  const [ticketNumber, setTicketNumber] = useState('');

  return (
    <Container className="turnstile__manual" fixed>
      <Status pass={pass} wait={wait}/>
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
          onClick={() => verify(ticketNumber)}
        >
          Проверить Билет
        </Button>
      </Box>
    </Container>
  )
};

export default connect(
  (state: any) => state.turnstile,
  actionCreators
)(TurnstileManual);
