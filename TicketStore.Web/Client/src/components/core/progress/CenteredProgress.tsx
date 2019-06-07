import React from 'react';
import CircularProgress from '@material-ui/core/CircularProgress';

export function CenteredProgress(props: any) {
  return (
    <div style={{display: 'flex', justifyContent: 'center'}}>
      <CircularProgress />
    </div>
  )
}
