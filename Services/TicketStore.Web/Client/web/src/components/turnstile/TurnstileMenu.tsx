import React from 'react';
import { useLocation, Link } from 'react-router-dom';

import './TurnstileMenu.css';
import { Box, BottomNavigation, BottomNavigationAction } from '@mui/material';
import CameraIcon from '@mui/icons-material/Camera';
import TouchAppIcon from '@mui/icons-material/TouchApp';

export default function TurnstileMenu() {
  return (
    <Box className="turnstile__parent">
      <Box className="turnstile__navbar">
        <BottomNavigation
          value={useLocation().pathname}
          showLabels
        >
          <BottomNavigationAction
            label="Скан Билета"
            icon={<CameraIcon />}
            component={Link}
            to="/turnstile/camera"
            value="/turnstile/camera"
          />
          <BottomNavigationAction
            label="Ручной Ввод"
            icon={<TouchAppIcon />}
            component={Link}
            to="/turnstile/manual"
            value="/turnstile/manual"
          />
        </BottomNavigation>
      </Box>
    </Box>
  )
}
