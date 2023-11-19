import React, { useState } from 'react';
import './TeamCreation.css';
import image from "../Images/teamRegistration.png";
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function TeamCreation() {
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const formData = new FormData(e.target);
    const data = {};
    formData.forEach((value, key) => {
      data[key] = value;
    });

    try {
      const response = await axios.post('https://workpa.azurewebsites.net/api/teams', data);
      console.log("Team is created successfully");

      const teamDetails = await axios.get(`https://workpa.azurewebsites.net/api/teams/code/${response.data}`);

      await axios.post('https://workpa.azurewebsites.net/api/employees', {
        userId: localStorage.getItem('userId'),
        teamId: teamDetails.data.id
      });

      navigate(`/success/${response.data}`);
    } catch (error) {
      console.error('Error:', error);
    }
  };

  return (
    <div className="tc-container">
      <div className="tc-content">
        <div className="tc-form-container gap-2">
          <h2>Team Creation <br /> Registration Form</h2>
          <form onSubmit={handleSubmit} className='teamcreation-form-container'>
            <div className="tc-form-group">
              <label htmlFor="organization">Organization / Company Name</label>
              <input type="text" id="organization" name="organization" required />
            </div>
            <div className="tc-form-group">
              <label htmlFor="businessType">Type of Business / Company</label>
              <select id="businessType" name="businessType" required>
                <option value="Sales">Sales</option>
                <option value="Manufacturing">Manufacturing</option>
                <option value="Construction">Construction</option>
                <option value="Delivery and Logistics">Delivery and Logistics</option>
              </select>
            </div>
            <div className="tc-form-group">
              <label htmlFor="businessAddress">Business Address</label>
              <input type="text" id="businessAddress" name="businessAddress" required />
            </div>
            <div className="tc-button-container">
              <button className="tc-submit-button" type="submit">
                Save
              </button>
            </div>
          </form>
        </div>
        <div className="tc-image-container"
          style={{
            width: "50%",
            height: '100%',
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
          }}
        >
          <img src={image} alt='Work PA'
            style={{
              height: '100%'
            }}
          />
        </div>
      </div>

    </div >
  );
}

export default TeamCreation;
