import React, { useState } from 'react';
import './UserRegistration.css';
import image from './userRegistration.png';
import checkmark from './checkmark.png'
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import axios from 'axios';
import { Link } from 'react-router-dom';

function UserRegistration() {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    emailAddress: '',
    password: '',
    confirmPassword: '',
  });

  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);
  const [registrationSuccess, setRegistrationSuccess] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };

  const toggleConfirmPasswordVisibility = () => {
    setShowConfirmPassword(!showConfirmPassword);
  };

  const handleLoginClick = () => {
    window.location.href = '/login';
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post('https://localhost:7236/api/users', formData);
      if (response.status === 201) {
        setRegistrationSuccess(true);
        console.log('User created successfully');
      } else {
        console.error('API request failed:', response.status);
      }
    } catch (error) {
      console.error('An error occurred:', error);
    }
  };

  return (
    <div className="registration-container">
      {registrationSuccess && <div className="register-backdrop"></div>}
      <div className="registration-content">
        <div className="registration-form-container">
          <h1>Registration Form</h1>
          <form onSubmit={handleSubmit}>
            <div className="registration-form-group">
              <label htmlFor="firstName">First Name</label>
              <input
                type="text"
                id="firstName"
                name="firstName"
                value={formData.firstName}
                onChange={handleChange}
                required
              />
            </div>
            <div className="registration-form-group">
              <label htmlFor="lastName">Last Name</label>
              <input
                type="text"
                id="lastName"
                name="lastName"
                value={formData.lastName}
                onChange={handleChange}
                required
              />
            </div>
            <div className="registration-form-group">
              <label htmlFor="emailAddress">Email Address</label>
              <input
                type="emailAddress"
                id="emailAddress"
                name="emailAddress"
                value={formData.emailAddress}
                onChange={handleChange}
                required
              />
            </div>
            <div className="registration-form-group">
              <label htmlFor="password">Password</label>
              <div className="register-password-input">
                <input
                  type={showPassword ? 'text' : 'password'}
                  id="password"
                  name="password"
                  value={formData.password}
                  onChange={handleChange}
                  required
                />
                <span className="register-toggle-password" onClick={togglePasswordVisibility}>
                  {showPassword ? <FontAwesomeIcon icon={faEyeSlash} /> : <FontAwesomeIcon icon={faEye} />}
                </span>
              </div>
            </div>
            <div className="registration-form-group">
              <label htmlFor="confirmPassword">Confirm Password</label>
              <div className="register-password-input">
                <input
                  type={showConfirmPassword ? 'text' : 'password'}
                  id="confirmPassword"
                  name="confirmPassword"
                  value={formData.confirmPassword}
                  onChange={handleChange}
                  required
                />
                <span className="register-toggle-password" onClick={toggleConfirmPasswordVisibility}>
                  {showConfirmPassword ? <FontAwesomeIcon icon={faEyeSlash} /> : <FontAwesomeIcon icon={faEye} />}
                </span>
              </div>
            </div>
            <div className="registration-button-container">
              <button className="registration-submit-button" type="submit">
                Submit
              </button>
            </div>
          </form>
          {/* Registration Success Modal */}
          {registrationSuccess && (
            <div className="registration-success-modal active">
              <img src={checkmark} alt="checkmark" className="checkmark" />
              <p className='success-header'>AWESOME!</p>
              <p className='success-text'>Your account has been <strong>successfully</strong> created</p>
              <button className="login-button" onClick={handleLoginClick}>Login</button>
            </div>
          )}
        </div>
        <div className="registration-image-container">
          <img src={image} alt="UserRegistration" className="registration-image" />
        </div>
      </div>
    </div>
  );
}

export default UserRegistration;