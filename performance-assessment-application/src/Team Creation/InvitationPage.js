import React from 'react';
import './InvitationPage.css';
import MarkEmailReadOutlinedIcon from '@mui/icons-material/MarkEmailReadOutlined';

function InvitationPage() {
  const handleJoinClick = (e) => {
    e.preventDefault();
    const invitationCode = document.getElementById('invitationCode').value;
    // Process the invitation code here
    console.log('Invitation Code Submitted:', invitationCode);
  };

  return (
    <div className="invitation-page">
      <div className="invitation-container">
        <div className="invitation-message">
        <div className="email-icon">
          <MarkEmailReadOutlinedIcon fontSize="large" />
        </div>
          <h2>CHECK YOUR EMAIL</h2>
          <h3>Please enter the shared invitation code</h3>
          <h3>sent to you by your company/organization.</h3>
        </div>
      </div>
      <div className="join-container gap-3">
          <input
            className="invitationpage-input"
            type="text"
            id="invitationCode"
            placeholder="Enter invitation code"
          />
          <button type='button' className="invitationpage-button btn" onClick={handleJoinClick}>
            JOIN
          </button>
        </div>
    </div>
  );
}

export default InvitationPage;
