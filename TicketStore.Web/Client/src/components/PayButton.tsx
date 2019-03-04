import React from 'react';

function iframe(roubles: number): string {
  return `<iframe src="https://money.yandex.ru/quickpay/button-widget?targets=%D0%9A%D1%83%D0%BF%D0%B8%D1%82%D1%8C%20%D0%B1%D0%B8%D0%BB%D0%B5%D1%82%20%D0%BD%D0%B0%20%D0%BA%D0%BE%D0%BD%D1%86%D0%B5%D1%80%D1%82%20The%20Cellophane%20Heads%2020%20%D0%B0%D0%BF%D1%80%D0%B5%D0%BB%D1%8F%202019&default-sum=${roubles}&button-text=11&any-card-payment-type=on&button-size=m&button-color=orange&mail=on&successURL=https%3A%2F%2Fromashov.tech%2Ftickets%2Ffarewell&quickpay=small&account=410011021763706&" width="197" height="36" frameborder="0" allowtransparency="true" scrolling="no"></iframe>`;
}

export const PayButton = (props: any) => {
  const { className, roubles } = props;  
  return (
    <div className={className} dangerouslySetInnerHTML={{__html: iframe(roubles)}}/>
  )
}
