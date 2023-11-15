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

    try {
      const joinResponse = await fetch('https://localhost:7236/api/employees/withteamcode', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          userId: localStorage.getItem('userId'),
          teamCode: teamCode,
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
      } else if (joinResponse.status === 404) {
        setInvalidCodeMessage("Team does not exist");
      } else {
        setInvalidCodeMessage("Invalid invitation code");
        console.error('Failed to join the team.');
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
          <svg xmlns="http://www.w3.org/2000/svg" width="100" height="100" fill="#055c9d" className="bi bi-upc" viewBox="0 0 16 16">
            <path d="M3 4.5a.5.5 0 0 1 1 0v7a.5.5 0 0 1-1 0v-7zm2 0a.5.5 0 0 1 1 0v7a.5.5 0 0 1-1 0v-7zm2 0a.5.5 0 0 1 1 0v7a.5.5 0 0 1-1 0v-7zm2 0a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5v-7zm3 0a.5.5 0 0 1 1 0v7a.5.5 0 0 1-1 0v-7z" />
          </svg>
          <p className='mb-0'
            style={{
              color: "#055c9d",
              textAlign: "center",
            }}
          >
            Please enter the invitation code sent to you by your company/organization.
          </p>
        </div>
        <div className="join-container">
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