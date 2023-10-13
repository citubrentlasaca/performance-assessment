import React, { useState } from 'react';
import './InvitationPage.css';
import MarkEmailReadOutlinedIcon from '@mui/icons-material/MarkEmailReadOutlined';
import { useNavigate } from 'react-router-dom';
import { Stack } from '@mui/material';

function InvitationPage() {
  const [teamCode, setTeamCode] = useState('');
  const navigate = useNavigate();
  const [invalidCodeMessage, setInvalidCodeMessage] = useState('');
  const [alreadyJoinedMessage, setAlreadyJoinedMessage] = useState('');
  const [showModal, setShowModal] = useState(false);

  const handleJoinClick = async (e) => {
    e.preventDefault();
    console.log('Invitation Code Submitted:', teamCode);
    try {
      const response = await fetch(`https://localhost:7236/api/teams/code/${teamCode}`);
      if (response.status === 200) {
        const data = await response.json();
        if (data.id) {
          const joinResponse = await fetch('https://localhost:7236/api/employees', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({
              userId: 6, // Temporarily using user ID 1
              teamId: data.id,
            }),
          });

          if (joinResponse.status === 201) {
            console.log('Successfully joined the team!');
            setTimeout(() => {
              navigate('/organizations');
            }, 3000);
          } else if (joinResponse.status === 409) {
            setAlreadyJoinedMessage('You have already joined the team.');
            setShowModal(true);
          } else {
            console.error('Failed to join the team.');
          }
        } else {
          console.error('Invalid invitation code.');
        }
      } else {
        setInvalidCodeMessage('Invalid code. Please check the inputted code.');
      }
    } catch (error) {
      console.error('An error occurred:', error);
    }
  };

  return (
    <Stack
      direction="column"
      justifyContent="center"
      alignItems="center"
      spacing={2}
      sx={{
        width: '100%',
        height: '100%'
      }}
    >
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
            value={teamCode}
            placeholder="Enter invitation code"
            onChange={(e) => setTeamCode(e.target.value)}
          />
          <button type='button' className="invitationpage-button btn" onClick={handleJoinClick}>
            JOIN
          </button>
        </div>
        {invalidCodeMessage && (
          <div className="error-message" style={{ padding: '10px' }}>
            <span style={{ color: 'red' }}>{invalidCodeMessage}</span>
          </div>
        )}
        {showModal && (
          <div className="modal">
            <div className="modal-content">
              <b>{alreadyJoinedMessage}</b>
              <button className="ok-button" onClick={() => navigate('/organizations')}>
                OK
              </button>
            </div>
          </div>
        )}
      </div>
    </Stack>

  );
}

export default InvitationPage;