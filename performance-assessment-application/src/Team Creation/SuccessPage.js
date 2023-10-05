import React from 'react';
import './SuccessPage.css';
import { useParams } from 'react-router-dom';

function SuccessPage() {
  const { data } = useParams();

  return (
    <div className="success-page">
      <div className="container">
        <div className="message">
          Your team has been successfully created.<br /><br />
          <h3>To invite others to join your team, simply click the 'Copy' button next to the invitation code below. 
            Share the copied code with your colleagues, and they can use it to join your team.</h3><br />
          <h2>Thank you for choosing <span className="darkblue-text">WorkPA!</span></h2>
        </div>
      </div>
      <div className="copy-container gap-3">
        <input
          class="successpage-input"
          id="textField"
          type="text"
          placeholder="https://workpa.com"
          value={data}
        />
        <button type='button' class='successpage-button btn'>COPY</button>
      </div>

    </div>
  );
}

export default SuccessPage;
