import React, { useState } from 'react';
import './Login.css';
import loginpic from "../Images/loginpic.png";
import logo from "../Images/WorkPA-logo.png";
import { Stack } from '@mui/material';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';

function Login() {
  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');

  const [formData, setFormData] = useState({
    email: '',
    password: '',
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.get('https://localhost:7236/api/users/authenticate', {
        params: {
          email: formData.email,
          password: formData.password,
        }
      });
      console.log(response);
      if (response.status === 200) {
        console.log('Successfully logged in!');

        const token = response.data.token;
        const userData = response.data.userData;

        localStorage.setItem('token', token);
        localStorage.setItem('userId', userData.id);
        localStorage.setItem('firstName', userData.firstName);
        localStorage.setItem('lastName', userData.lastName);

        navigate('/home');
      } else {
        setErrorMessage('Invalid email or password');
        console.error('API request failed:', response.status);
      }
    } catch (error) {
      console.error('An error occurred:', error);
    }
  };


  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };


  return (
    <div className="login-container">
      <div className="login-content">
        <div className="login-image-container">
          <img src={loginpic} alt="Login Pic" />
          <div className="description">
            <p>Assess and track</p>
            <p>employee performance</p>
            <p>with <span className="darkblue-text">ease.</span></p>
          </div>
        </div>
        <Stack
          direction="column"
          justifyContent="center"
          alignItems="center"
          spacing={8}
          sx={{
            width: '50%',
            height: '100%',
          }}
        >
          <img src={logo} alt="WorkPA Logo" />
          <Stack
            direction="column"
            justifyContent="center"
            alignItems="flex-start"
            spacing={2}
            sx={{
              width: '75%',
            }}
          >
            <b>Email</b>
            <input
              type='email'
              className='form-control'
              style={{
                width: '100%'
              }}
              name="email"
              value={formData.email}
              onChange={handleChange}
              required
            />
            <b>Password</b>
            <div className="login-password-input">
              <input
                type={showPassword ? 'text' : 'password'}
                className='form-control'
                style={{
                  width: '100%'
                }}
                name="password"
                value={formData.password}
                onChange={handleChange}
                required
              />
              <button
                type="button"
                className="login-password-toggle"
                onClick={togglePasswordVisibility}
              >
                <FontAwesomeIcon icon={showPassword ? faEyeSlash : faEye} />
              </button>
            </div>

          </Stack>
          <button
            type='button'
            className="login-button"
            onClick={handleSubmit}
          >
            Login
          </button>
        </Stack>
      </div>
      {errorMessage && (
        <div className="error-message" style={{ padding: '10px' }}>
          <span style={{ color: 'red' }}>{errorMessage}</span>
        </div>
      )}
    </div >
  );
}


export default Login;
