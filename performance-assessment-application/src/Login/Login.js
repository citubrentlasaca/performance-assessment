import React from 'react';
import './Login.css';
import loginpic from "../Images/loginpic.png";
import logo from "../Images/WorkPA-logo.png";
import { Stack } from '@mui/material';

function Login() {
  return (
    <div className="login-container">
      <div className="login-content">
        <div className="login-image-container">
          <img src={loginpic} alt="Login Pic" draggable="false" />
          <div className="description">
            <p>Assess and track</p>
            <p>employee performance</p>
            <p>with <span className="darkblue-text">ease.</span></p>
          </div>
        </div>

        <div className="login-form-container gap-5">
          <img src={logo} alt="Work PA Logo" />
          <Stack
            direction="column"
            justifyContent="center"
            alignItems="flex-start"
            spacing={2}
            sx={{
              width: "75%",
            }}
          >
            <b>Email</b>
            <input type='text' className='form-control'
              style={{
                border: '2px solid #aee5ed',
                borderRadius: '10px'
              }}
            />
            <b>Password</b>
            <input type='text' className='form-control'
              style={{
                border: '2px solid #aee5ed',
                borderRadius: '10px',
              }}
            />
          </Stack>
          <button type='button'
            style={{
              background: 'linear-gradient(to right, #0076fe, #00c5ff)',
              border: 'none',
              borderRadius: '10px',
              width: '100px',
              height: '40px',
              color: 'white'
            }}
          >Login</button>
        </div>
      </div>
    </div>
  );
}

export default Login;
