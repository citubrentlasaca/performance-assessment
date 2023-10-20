import React, { useRef } from 'react';
import './SuccessPage.css';
import { useParams, useNavigate } from 'react-router-dom';
import { Stack } from "@mui/material";

function SuccessPage() {
  const { data } = useParams();
  const navigate = useNavigate();
  const inputRef = useRef(null);

  const handleCopyClick = () => {
    if (inputRef.current) {
      inputRef.current.select();
      document.execCommand('copy');
    }
  };

  const handleBackClick = () => {
    navigate(`/home`);
  };

  return (
    <div style={{height: "100vh"}}>
      <Stack
        direction="row"
        justifyContent="flex-end"
        alignItems="flex-start"
        sx={{marginBottom: "0", padding: "20px"}}
      >
        <button type="button" className="btn" onClick={handleBackClick}>
        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-arrow-return-left" viewBox="0 0 16 16">
          <path fill-rule="evenodd" d="M14.5 1.5a.5.5 0 0 1 .5.5v4.8a2.5 2.5 0 0 1-2.5 2.5H2.707l3.347 3.346a.5.5 0 0 1-.708.708l-4.2-4.2a.5.5 0 0 1 0-.708l4-4a.5.5 0 1 1 .708.708L2.707 8.3H12.5A1.5 1.5 0 0 0 14 6.8V2a.5.5 0 0 1 .5-.5z"/>
        </svg>
      </button>
      </Stack>
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
    </div>
    
  );
}

export default SuccessPage;
