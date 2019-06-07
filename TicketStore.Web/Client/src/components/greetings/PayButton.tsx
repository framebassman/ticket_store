import React from 'react';
import Button from '@material-ui/core/Button';
import yellow from '@material-ui/core/colors/yellow';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';

export const PayButton = (props: any) => {
  const { roubles, target } = props;
  const theme = createMuiTheme({
    palette: {
      primary: yellow,
    }
  });
  if (roubles > 0) {
    return (
        <form style={{marginLeft: 'auto', marginRight: 'auto'}} method="POST" action="https://money.yandex.ru/quickpay/confirm.xml">
          <input type="hidden" name="receiver" value="410011021763706" />
          <input type="hidden" name="quickpay-form" value="small" />
          <input type="hidden" name="need-email" value="true" />
          <input type="hidden" name="targets" value={target} />
          <input type="hidden" name="sum" value={roubles} data-type="number"></input>
          <MuiThemeProvider theme={theme}>
            <Button variant="contained" color="primary" size="large" type="submit">Купить билет</Button>
          </MuiThemeProvider>
        </form>
    )
  } else {
    return null;
  }
}
