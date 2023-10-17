import React, { useRef } from 'react';
import './SuccessPage.css';
import { useParams } from 'react-router-dom';

function SuccessPage() {
  const { data } = useParams();

  const inputRef = useRef(null);

  const handleCopyClick = () => {
    if (inputRef.current) {
      inputRef.current.select();
      document.execCommand('copy');
    }
  };

  return (
    <div className="success-page">
      <div className="container">
        <div className="message">
          Your team has been successfully created.<br /><br />
          <h3 className='successpage-h3'>To invite others to join your team, simply click the 'Copy' button next to the invitation code below.
            Share the copied code with your colleagues, and they can use it to join your team.</h3><br />
          <h2 className='successpage-h2'>Thank you for choosing <span className="darkblue-text">WorkPA!</span></h2>
        </div>
      </div>
      <div className="copy-container gap-3">
        <input
          className="successpage-input"
          id="textField"
          type="text"
          placeholder="https://workpa.com"
          defaultValue={data}
          readOnly
          ref={inputRef}
        />
        <button type='button' className='successpage-button btn' onClick={handleCopyClick}>COPY</button>
      </div>
    </div>
  );
}

export default SuccessPage;
