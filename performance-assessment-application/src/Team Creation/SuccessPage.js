import React from 'react';
import './SuccessPage.css';

function SuccessPage() {
  return (
    <div className="success-page">
      <div className="container">
        <div className="message">
          Your team has been successfully created.<br /><br /><br />
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
        <button class='successpage-button'>Copy</button>
      </div>

    </div>
  );
}

export default SuccessPage;
