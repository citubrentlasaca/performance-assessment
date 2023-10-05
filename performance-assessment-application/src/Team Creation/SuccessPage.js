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
      <div className="copy-container gap-3">
        <input
          class="successpage-input"
          id="textField"
          type="text"
          placeholder="https://workpa.com"
        />
        <button type='button' class='successpage-button btn'>COPY</button>
      </div>

    </div>
  );
}

export default SuccessPage;
