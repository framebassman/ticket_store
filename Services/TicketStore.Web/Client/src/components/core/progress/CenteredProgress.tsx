import React, { ReactNode } from 'react';
import CircularProgress from '@material-ui/core/CircularProgress';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';
import './CenteredProgress.css';

export function CenteredProgress({ children }: { children: ReactNode }) {
  return (
    <div className="centred-progress">
      <CircularProgress color="secondary" />
      <Typography align="center" component="div">
        <Box marginTop={2}>
          {children}
        </Box>
      </Typography>
    </div>
  )
}
