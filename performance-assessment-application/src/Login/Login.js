import React, { useState } from 'react';
import './Login.css';
import loginpic from "../Images/loginpic.png";
import logo from "../Images/WorkPA-logo.png";
import { Stack } from '@mui/material';
import axios from 'axios'; 
import { useNavigate } from 'react-router-dom';

function Login() {
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
        console.log('Login successful');
      } else {
        console.error('API request failed:', response.status);
      }
    } catch (error) {
      console.error('An error occurred:', error);
    }
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
            <input
              type='password'
              className='form-control'
              style={{
                width: '100%'
              }}
              name="password"
              value={formData.password} 
              onChange={handleChange} 
              required
            />
          </Stack>
          <button
            type='button'
            style={{
              width: '100px',
              height: '38px',
              backgroundImage: 'linear-gradient(to right, #0273ff , #00c6ff)',
              border: 'none',
              borderRadius: '6px',
              color: 'white'
            }}
            onClick={handleSubmit}
          >
            Login
          </button>
        </Stack>

      </div>
    </div >
  );
}


export default Login;
