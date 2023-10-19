import React, { useState } from 'react';
import './UserRegistration.css';
import image from './userRegistration.png';
import checkmark from './checkmark.png'
import axios from 'axios';
import { Link } from 'react-router-dom';
import { Stack } from '@mui/material';
import { useNavigate } from "react-router-dom";

function UserRegistration() {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [emailAddress, setEmailAddress] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);
  const [firstNameError, setFirstNameError] = useState('');
  const [lastNameError, setLastNameError] = useState('');
  const [emailAddressError, setEmailAddressError] = useState('');
  const [passwordError, setPasswordError] = useState('');
  const [confirmPasswordError, setConfirmPasswordError] = useState('');
  const [registrationSuccess, setRegistrationSuccess] = useState(false);
  const [registerHover, setRegisterHover] = useState(false);
  const [loginHover, setLoginHover] = useState(false);
  const navigate = useNavigate();

  const handleFirstNameChange = (e) => {
    setFirstName(e.target.value);
    setFirstNameError('');
  };

  const handleLastNameChange = (e) => {
    setLastName(e.target.value);
    setLastNameError('');
  };

  const handleEmailAddressChange = (e) => {
    setEmailAddress(e.target.value);
    setEmailAddressError('');
  };

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
    setPasswordError('');
    setConfirmPasswordError('');
  };

  const handleConfirmPasswordChange = (e) => {
    setConfirmPassword(e.target.value);
    setPasswordError('');
    setConfirmPasswordError('');
  };

  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };

  const toggleConfirmPasswordVisibility = () => {
    setShowConfirmPassword(!showConfirmPassword);
  };

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

  const handleRegisterHover = () => {
    setRegisterHover(true);
  };

  const handleRegisterLeave = () => {
    setRegisterHover(false);
  };

  const handleLoginHover = () => {
    setLoginHover(true);
  };

  const handleLoginLeave = () => {
    setLoginHover(false);
  };

  const handleLoginClick = () => {
    navigate('/login');
  };

  const handleSubmitClick = () => {
    setFirstNameError('');
    setLastNameError('');
    setEmailAddressError('');
    setPasswordError('');
    setConfirmPasswordError('');

    if (firstName === '') {
      setFirstNameError('First Name is required');
    }
    if (lastName === '') {
      setLastNameError('Last Name is required');
    }
    if (emailAddress === '') {
      setEmailAddressError('Email Address is required');
    }
    if (password === '') {
      setPasswordError('Password is required');
    }
    if (confirmPassword === '') {
      setConfirmPasswordError('Confirm Password is required');
    }

    if (password !== confirmPassword) {
      setPasswordError('Passwords do not match');
      setConfirmPasswordError('Passwords do not match');
    }

    if (firstName !== '' && lastName !== '' && emailAddress !== '' && password !== '' && confirmPassword !== '' && password === confirmPassword) {
      const formData = {
        firstName: firstName,
        lastName: lastName,
        emailAddress: emailAddress,
        password: password,
        confirmPassword: confirmPassword,
      };

      axios
        .post('https://localhost:7236/api/users', formData)
        .then((response) => {
          setRegistrationSuccess(true);
          console.log('User registered successfully', response.data);
        })
        .catch((error) => {
          console.error('User registration failed', error);
        });
      return;
    }
  };

  return (
    <Stack
      direction="row"
      justifyContent="center"
      alignItems="center"
      spacing={0}
      sx={{
        weight: '100%',
        height: '100%',
      }}
    >
      <div className='gap-3'
        style={{
          width: '50%',
          height: '100%',
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',
          alignItems: 'center',
          backgroundColor: '#d6f4f8',
        }}
      >
        <h1
          style={{
            fontWeight: 'bold',
            color: '#055c9d'
          }}
        >
          Registration Form
        </h1>
        <Stack
          direction="row"
          justifyContent="center"
          alignItems="flex-start"
          spacing={2}
          sx={{
            width: '75%'
          }}
        >
          <Stack
            direction="column"
            justifyContent="center"
            alignItems="flex-start"
            spacing={1}
            sx={{
              width: '50%'
            }}
          >
            <b>First Name</b>
            <input type='text' className='form-control' value={firstName} onChange={handleFirstNameChange}
              style={{
                border: '2px solid #85dde7'
              }}
            />
            {firstNameError && <div style={{ color: 'red', fontSize: '12px' }}>{firstNameError}</div>}
          </Stack>
          <Stack
            direction="column"
            justifyContent="center"
            alignItems="flex-start"
            spacing={1}
            sx={{
              width: '50%'
            }}
          >
            <b>Last Name</b>
            <input type='text' className='form-control' value={lastName} onChange={handleLastNameChange}
              style={{
                border: '2px solid #85dde7'
              }}
            />
            {lastNameError && <div style={{ color: 'red', fontSize: '12px' }}>{lastNameError}</div>}
          </Stack>
        </Stack>
        <Stack
          direction="column"
          justifyContent="center"
          alignItems="flex-start"
          spacing={1}
          sx={{
            width: '75%'
          }}
        >
          <b>Email Address</b>
          <input type='text' className='form-control' value={emailAddress} onChange={handleEmailAddressChange}
            style={{
              border: '2px solid #85dde7'
            }}
          />
          {emailAddressError && <div style={{ color: 'red', fontSize: '12px' }}>{emailAddressError}</div>}
        </Stack>
        <Stack
          direction="column"
          justifyContent="center"
          alignItems="flex-start"
          spacing={1}
          sx={{
            width: '75%'
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
        <Stack
          direction="column"
          justifyContent="center"
          alignItems="flex-start"
          spacing={1}
          sx={{
            width: '75%'
          }}
        >
          <b>Confirm Password</b>
          <div className="input-group" >
            <input type={showConfirmPassword ? 'text' : 'password'} className="form-control" aria-describedby="basic-addon2" value={confirmPassword} onChange={handleConfirmPasswordChange}
              style={{
                border: '2px solid #85dde7',
                borderRight: 'none'
              }}
            />
            {showConfirmPassword ? (
              <button className="btn btn-outline-secondary" type="button" id="basic-addon2" onClick={toggleConfirmPasswordVisibility}
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
              <button className="btn btn-outline-secondary" type="button" id="basic-addon2" onClick={toggleConfirmPasswordVisibility}
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
          {confirmPasswordError && <div style={{ color: 'red', fontSize: '12px' }}>{confirmPasswordError}</div>}
        </Stack>
        <p className='mb-0'>Already have an account? You can log in&nbsp;
          <Link to='/login'>here</Link>.
        </p>
        <button style={registerHover ? { ...normalStyle, ...hoverStyle } : normalStyle} onClick={handleSubmitClick}
          onMouseEnter={handleRegisterHover}
          onMouseLeave={handleRegisterLeave}
        >
          Submit
        </button>
      </div>
      <div
        style={{
          width: '50%',
          height: '100%',
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',
          alignItems: 'center',
          backgroundColor: 'white',
        }}
      >
        <img src={image} alt="User Registration" style={{ height: '100%', maxWidth: '100%' }} />
      </div>
      {registrationSuccess && (
        <div>
          <div className={`overlay ${registrationSuccess ? 'active' : ''}`}></div>
          <div className={`registration-success-modal ${registrationSuccess ? 'active' : ''}`}>
            <img src={checkmark} alt="checkmark" className="checkmark" />
            <p className='success-header'>AWESOME!</p>
            <p className='success-text'>Your account has been <strong>successfully</strong> created</p>
            <button style={loginHover ? { ...normalStyle, ...hoverStyle } : normalStyle} onClick={handleLoginClick}
              onMouseEnter={handleLoginHover}
              onMouseLeave={handleLoginLeave}
            >
              Login
            </button>
          </div>
        </div>
      )}
    </Stack>
  );
}

export default UserRegistration;