import { useEffect, useState } from 'react';
import './style.css';
import { MessageCardProps } from './types';

export default function MessageCanvas({ message, remove }:MessageCardProps) {

  const getTimeLeftString = (time:number) => `${Math.floor(time/1000)}s`;

  const deathTime = new Date(Date.parse(message.deathTime)).getTime();
  const [death, setDeath] = useState(getTimeLeftString(deathTime - Date.now()));
  const [fading, setFading] = useState(false);

  useEffect(() => {
    const ticker = setInterval(() => {

      const timeLeft = deathTime - Date.now();
      if (timeLeft < 0) remove(message.id);
      if (!fading && timeLeft < 10000) setFading(true);

      setDeath(getTimeLeftString(timeLeft));
    }, 500);

    return () => clearInterval(ticker);
   } , [deathTime, message.id, remove, fading]);

  return (
    <section className={"message-card" + (fading ? " message-card--fade" : "")}>
      <h3 className="message-card__title">{message.title}</h3>
      <p className="message-card__message">{message.message}</p>
      <p className="message-card__time-left">{death}</p>
    </section>
  );
}