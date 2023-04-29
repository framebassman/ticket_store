import React from 'react';
import Button from '@material-ui/core/Button';

export interface PayButtonProps {
  className?: string,
  roubles: number,
  label: string,
  targets: string,
  yandexMoneyAccount: string
}

function isEmpty(origin: string) {
  return !origin || !origin.length;
}

export const PayButton = (props: PayButtonProps) => {
  const { roubles, label, targets, yandexMoneyAccount } = props;
  if (roubles <= 0 || isEmpty(yandexMoneyAccount)) {
    return null;
  } else {
    return (
      <form style={{marginLeft: 'auto', marginRight: 'auto'}} method="POST" action="https://yoomoney.ru/quickpay/confirm.xml">
        <input type="hidden" name="receiver" value={yandexMoneyAccount} />
        <input type="hidden" name="quickpay-form" value="small" />
        <input type="hidden" name="need-email" value="true" />
        <input type="hidden" name="label" value={label} />
        <input type="hidden" name="targets" value={targets} />
        <input type="hidden" name="sum" value={roubles} data-type="number"></input>
        <input type="hidden" name="paymentType" value="AC"/>
        <Button variant="contained" color="secondary" size="large" type="submit">Купить билет</Button>
      </form>
    )
  }
}
