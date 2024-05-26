import React, { ReactNode } from 'react';
import './CenteredProgress.css';
import { Box, Typography, CircularProgress } from '@mui/material';

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
