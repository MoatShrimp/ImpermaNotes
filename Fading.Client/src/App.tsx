import { useEffect, useState } from 'react';
import './App.css';
import MessageForm from './components/MessageForm';
import MessagesCanvas from './components/MessagesCanvas';
import { Msg } from './components/types.global';

export default function App() {

  const [messages, setMessages] = useState<Msg[]>([]);
  const addMessage = (newMsg:Msg) => setMessages(old => old.concat(newMsg));
  const removeMessage = (id:number) => setMessages(old => old.filter(msg => msg.id !== id));

  useEffect( () => {
    fetch('https://localhost:7213/api/Messages')
      .then(response => response.json())
      .then((data:Msg[]) => setMessages(data));
  }, []);

  return (
    <article className="wrap__app">
      <header className="app-header">
        <h1 className="app-header__logo app-header__box">Imperma notes</h1>
        <MessageForm addMessage={addMessage}/>
        <h2 className='app-header__box salt-logo'>&lt;salt/&gt;</h2>
      </header>
      <MessagesCanvas messages={messages} remove={removeMessage} />
    </article>
  );
}
