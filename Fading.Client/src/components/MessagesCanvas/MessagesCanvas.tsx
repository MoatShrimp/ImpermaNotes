import './style.css';
import { MessageCanvasProps } from './types';
import MessageCard from '../MessageCard';

export default function MessageCanvas({ messages, remove }:MessageCanvasProps) {

  return (
    <main className='message-container'>
      {messages.map(message => <MessageCard message={message} remove={remove} key={message.id.toString()}/>)}
    </main>
  );
}