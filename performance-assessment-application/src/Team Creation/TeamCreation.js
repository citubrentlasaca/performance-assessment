import React, { useState } from 'react';
import './TeamCreation.css'; 
import image from "../Images/teamRegistration.png";
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function TeamCreation() {
  const [formData, setFormData] = useState({});
  const [submittedData, setSubmittedData] = useState(null);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const formData = new FormData(e.target);
    const data = {};
    formData.forEach((value, key) => {
      data[key] = value;
    });

    console.log('Form Data Submitted:', data);

    try {
      const response = await axios.post('https://localhost:7236/api/teams', data);

      setSubmittedData(response.data);

      navigate(`/success/${response.data}`);
    } catch (error) {
      console.error('Error:', error);
    }
  };

  return (
    <div className="tc-container">
      <div className="tc-content">
        <div className="tc-form-container">
          <h2>Team Creation</h2>
          <h2>Registration Form</h2>
          <form onSubmit={handleSubmit}>            
          <div className="tc-form-group">
              <label htmlFor="organization">Organization / Company Name</label>
              <input type="text" id="organization" name="organization" required />
            </div>
            <div className="tc-form-group">
              <label htmlFor="firstName">First Name</label>
              <input type="text" id="firstName" name="firstName" required />
            </div>
            <div className="tc-form-group">
              <label htmlFor="lastName">Last Name</label>
              <input type="text" id="lastName" name="lastName" required />
            </div>
            <div className="tc-form-group">
              <label htmlFor="businessType">Type of Business / Company</label>
              <input type="text" id="type" name="businessType" required />
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
        <div className="tc-image-container">
          <img src={image} alt="UserRegistration" className="registration-image" />
        </div>
      </div>

    </div>
  );
}

export default TeamCreation;
