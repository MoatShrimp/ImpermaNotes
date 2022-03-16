import { FormEvent, useRef } from 'react';
import { Msg } from '../types.global';
import './style.css';
import { MessageFormProps } from './types';

export default function MessageForm({ addMessage }:MessageFormProps) {

  const titleInput = useRef<HTMLInputElement>(null);
  const detailsInput = useRef<HTMLTextAreaElement>(null);

  const getInputValue = (input:any) => input.current!.value.trim();
  const resetInputValues = (...inputs:any[])=> inputs.forEach(input => input.current!.value = '');

  const newMessage = (event:FormEvent) => {
    event.preventDefault();

    if (getInputValue(titleInput) === '') return;

    const msgBody = JSON.stringify({
      title: getInputValue(titleInput),
      message: getInputValue(detailsInput)
    });

    resetInputValues(titleInput, detailsInput);
    
    fetch('https://localhost:7213/api/Messages', {
      method: 'POST',
      body: msgBody,
      headers: { 'Content-Type': 'application/json' }
    }).then(response => response.json())
      .then((data:Msg) => addMessage(data));
  };

  return (
    <form onSubmit={newMessage} className="message-form">
      <input 
        ref={titleInput} 
        type="text" 
        placeholder="All of this"
        maxLength={30}
        className="message-form__input message-form__-title" />
      <textarea 
        ref={detailsInput}
        placeholder="Will be gone in just a few minutes"
        maxLength={100}
        className="message-form__input message-form__details" />
      <input
        type="submit"
        value="Add Entry"
        className="message-form__input message-form__button"/>
  </form>
  );
}