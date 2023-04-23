import React, { useState } from 'react';
import { Route, RouteComponentProps, withRouter } from 'react-router-dom';
import BottomNavigation from '@material-ui/core/BottomNavigation';
import BottomNavigationAction from '@material-ui/core/BottomNavigationAction';
import Box from '@material-ui/core/Box';
import CameraIcon from '@material-ui/icons/Camera';
import TouchAppIcon from '@material-ui/icons/TouchApp';

import Turnstile from './camera/TurnstileCamera';
import TurnstileManual from './manual/TurnstileManual';
import './TurnstileMenu.css';

const navigation = {
  camera: {
    path: '/turnstile/camera',
    index: 0
  },
  manual: {
    path: '/turnstile/manual',
    index: 1
  },
}

const navigationList = [navigation.camera, navigation.manual];

export const TurnstileMenu: React.FC<RouteComponentProps> = ({ location, history }) => {
  const index = navigationList.findIndex(navItem => location.pathname === navItem.path);
  const [value, setValue] = useState(index);

  return (
    <Box className="turnstile__parent">
      <Box className="turnstile__child">
        <Route path={navigation.camera.path} component={Turnstile} />
        <Route path={navigation.manual.path} component={TurnstileManual} />
      </Box>

      <Box className="turnstile__navbar">
        <BottomNavigation
          value={value}
          onChange={(_, newValue) => {
            const item = navigationList.find(navItem => newValue === navItem.index);
            if (item && item.path) {
              history.push(item.path);
              setValue(newValue);
            }
          }}
          showLabels
        >
          <BottomNavigationAction
            label="Скан Билета"
            icon={<CameraIcon />}
          />
          <BottomNavigationAction
            label="Ручной Ввод"
            icon={<TouchAppIcon />}
          />
        </BottomNavigation>
      </Box>
    </Box>
  )
}

export default withRouter(TurnstileMenu);
