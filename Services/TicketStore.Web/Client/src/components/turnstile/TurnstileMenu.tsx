import React, { Fragment } from 'react';
import { Route } from 'react-router-dom';

import Turnstile from './camera/TurnstileCamera';
import TurnstileManual from './manual/TurnstileManual';

export const TurnstileMenu = () => {
    return (
        <Fragment>
            <Route path="/turnstile/camera" component={Turnstile}/>
            <Route path="/turnstile/manual" component={TurnstileManual}/>
        </Fragment>
    )
}
