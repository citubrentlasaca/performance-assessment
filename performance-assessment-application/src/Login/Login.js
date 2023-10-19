import { Stack } from '@mui/material'
import React, { useState } from 'react'
import loginpic from "../Images/loginpic.png";
import logo from "../Images/WorkPA-logo.png";
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [loginHover, setLoginHover] = useState(false);
  const [emailError, setEmailError] = useState('');
  const [passwordError, setPasswordError] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const navigate = useNavigate();

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
    setEmailError('');
    setErrorMessage('');
  }

  const handlePasswordChange = (e) => {
    setPassword(e.target.value)
    setPasswordError('');
    setErrorMessage('');
  }

  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword);
  }

  const normalStyle = {
    background: 'linear-gradient(to right, #0076fe, #00c5ff)',
    color: 'white',
    border: 'none',
    borderRadius: '10px',
    width: 'fit-content',
    padding: '10px',
  };

  const hoverStyle = {
    background: 'linear-gradient(to right, #0076fe, #00c5ff)',
    color: 'white',
    border: 'none',
    borderRadius: '10px',
    width: 'fit-content',
    padding: '10px',
    boxShadow: '0px 0px 15px rgba(0, 0, 0, 0.3)'
  };

  const handleLoginHover = () => {
    setLoginHover(true);
  };

  const handleLoginLeave = () => {
    setLoginHover(false);
  };

  const handleLoginClick = async () => {
    setEmailError('');
    setPasswordError('');

    if (email === '') {
      setEmailError('Email Address is required');
    }
    if (password === '') {
      setPasswordError('Password is required');
    }

    if (email !== '' && password !== '') {
      try {
        const response = await axios.get('https://localhost:7236/api/users/authenticate', {
          params: {
            email: email,
            password: password,
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

          console.error('API request failed:', response.status);
        }
      } catch (error) {
        setErrorMessage('Invalid email or password');
        console.error('An error occurred:', error);
      }
    }
  };

  return (
    <Stack
      direction="row"
      justifyContent="center"
      alignItems="center"
      spacing={0}
      sx={{
        width: '100%',
        height: '100%',
      }}
    >
      <div
        style={{
          width: '50%',
          height: '100%',
          backgroundColor: 'white',
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',
          alignItems: 'center',
        }}
      >
        <img src={loginpic} alt="Login Pic" style={{ height: 'auto', maxWidth: '100%' }} />
        <h1
          style={{
            textAlign: 'center',
            fontWeight: 'bold',
            color: '#63d6e4'
          }}
        >
          Assess and track<br />employee performance<br />with <span style={{ color: '#055c9d' }}>ease.</span>
        </h1>
      </div>
      <div className='gap-3'
        style={{
          width: '50%',
          height: '100%',
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',
          alignItems: 'center',
        }}
      >
        <Stack
          direction="column"
          justifyContent="center"
          alignItems="center"
          spacing={12}
          sx={{
            width: '75%',
            height: '100%'
          }}
        >
          <img src={logo} alt="WorkPA Logo" style={{ width: '50%' }} />
          <Stack
            direction="column"
            justifyContent="center"
            alignItems="center"
            spacing={2}
            sx={{
              width: '100%',
            }}
          >
            <Stack
              direction="column"
              justifyContent="center"
              alignItems="flex-start"
              spacing={1}
              sx={{
                width: '100%'
              }}
            >
              <b>Email Address</b>
              <input type='text' className='form-control' value={email} onChange={handleEmailChange}
                style={{
                  border: '2px solid #85dde7'
                }}
              />
              {emailError && <div style={{ color: 'red', fontSize: '12px' }}>{emailError}</div>}
            </Stack>
            <Stack
              direction="column"
              justifyContent="center"
              alignItems="flex-start"
              spacing={1}
              sx={{
                width: '100%'
              }}
            >
              <b>Password</b>
              <div className="input-group" >
                <input type={showPassword ? 'text' : 'password'} className="form-control" aria-describedby="basic-addon1" value={password} onChange={handlePasswordChange}
                  style={{
                    border: '2px solid #85dde7',
                    borderRight: 'none'
                  }}
                />
                {showPassword ? (
                  <button className="btn btn-outline-secondary" type="button" id="basic-addon1" onClick={togglePasswordVisibility}
                    style={{
                      border: '2px solid #85dde7',
                      borderLeft: 'none',
                      backgroundColor: 'white'
                    }}
                  >
                    <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="black" className="bi bi-eye-slash" viewBox="0 0 16 16">
                      <path d="M13.359 11.238C15.06 9.72 16 8 16 8s-3-5.5-8-5.5a7.028 7.028 0 0 0-2.79.588l.77.771A5.944 5.944 0 0 1 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.134 13.134 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755-.165.165-.337.328-.517.486l.708.709z" />
                      <path d="M11.297 9.176a3.5 3.5 0 0 0-4.474-4.474l.823.823a2.5 2.5 0 0 1 2.829 2.829l.822.822zm-2.943 1.299.822.822a3.5 3.5 0 0 1-4.474-4.474l.823.823a2.5 2.5 0 0 0 2.829 2.829z" />
                      <path d="M3.35 5.47c-.18.16-.353.322-.518.487A13.134 13.134 0 0 0 1.172 8l.195.288c.335.48.83 1.12 1.465 1.755C4.121 11.332 5.881 12.5 8 12.5c.716 0 1.39-.133 2.02-.36l.77.772A7.029 7.029 0 0 1 8 13.5C3 13.5 0 8 0 8s.939-1.721 2.641-3.238l.708.709zm10.296 8.884-12-12 .708-.708 12 12-.708.708z" />
                    </svg>
                  </button>
                ) : (
                  <button className="btn btn-outline-secondary" type="button" id="basic-addon1" onClick={togglePasswordVisibility}
                    style={{
                      border: '2px solid #85dde7',
                      borderLeft: 'none',
                      backgroundColor: 'white'
                    }}
                  >
                    <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="black" className="bi bi-eye" viewBox="0 0 16 16">
                      <path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8zM1.173 8a13.133 13.133 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.133 13.133 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5c-2.12 0-3.879-1.168-5.168-2.457A13.134 13.134 0 0 1 1.172 8z" />
                      <path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5zM4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0z" />
                    </svg>
                  </button>
                )}
              </div>
              {passwordError && <div style={{ color: 'red', fontSize: '12px' }}>{passwordError}</div>}
            </Stack>
            {errorMessage && <div style={{ color: 'red' }}>{errorMessage}</div>}
            <p className='mb-0'>Don't have an account yet? You can register&nbsp;
              <Link to='/register'>here</Link>.
            </p>
            <button style={loginHover ? { ...normalStyle, ...hoverStyle } : normalStyle} onClick={handleLoginClick}
              onMouseEnter={handleLoginHover}
              onMouseLeave={handleLoginLeave}
            >
              Login
            </button>
          </Stack>
        </Stack>
      </div>
    </Stack>
  )
}

export default Login