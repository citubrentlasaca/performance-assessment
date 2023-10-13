import React, { useState } from 'react';
import './InvitationPage.css';
import MarkEmailReadOutlinedIcon from '@mui/icons-material/MarkEmailReadOutlined';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

function InvitationPage() {
  const [teamCode, setTeamCode] = useState('');
  const navigate = useNavigate();

  const handleJoinClick = async (e) => {
    e.preventDefault();
    console.log('Invitation Code Submitted:', teamCode);
    try {
      const response = await axios.get(`https://localhost:7236/api/teams/code/${teamCode}`);
      if (response.status === 200) {
        const data = response.data;
        if (data.id) {
          const joinResponse = await axios.post('https://localhost:7236/api/employees', {
            userId: 1, // Temporarily using user ID 1
            teamId: data.id,
          });
          if (joinResponse.status === 201) {
            console.log('Successfully joined the team!');
            navigate('/organizations');
          } else {
            console.error('Failed to join the team.');
            console.log(joinResponse);
          }
        } else {
          console.error('Invalid invitation code.');
        }
      } else {
        console.error('Failed to check the invitation code.');
      }
    } catch (error) {
      console.error('An error occurred:', error);
    }
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
            value={teamCode}
            placeholder="Enter invitation code"
            onChange={(e) => setTeamCode(e.target.value)}
          />
          <button type='button' className="invitationpage-button btn" onClick={handleJoinClick}>
            JOIN
          </button>
        </div>
    </div>
  );
}

export default InvitationPage;
