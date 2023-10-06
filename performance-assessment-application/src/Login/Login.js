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
            <input type='text' className='form-control'
              style={{
                width: '100%'
              }}
            />
            <b>Password</b>
            <input type='text' className='form-control'
              style={{
                width: '100%'
              }}
            />
          </Stack>
          <button type='button'
            style={{
              width: '100px',
              height: '38px',
              backgroundImage: 'linear-gradient(to right, #0273ff , #00c6ff)',
              border: 'none',
              borderRadius: '6px',
              color: 'white'
            }}
          >Login</button>
        </Stack>

      </div>
    </div >
  );
}

export default Login;
