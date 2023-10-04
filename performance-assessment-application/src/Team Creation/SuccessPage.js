import React from 'react';
import './SuccessPage.css';

function SuccessPage() {
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
      <div className="copy-container">
        <input
          class="successpage-input"
          id="textField"
          type="text"
          defaultValue="https://workpa.com"
          readOnly
        />
        <button class='successpage-button'>COPY</button>
      </div>

    </div>
  );
}

export default SuccessPage;
