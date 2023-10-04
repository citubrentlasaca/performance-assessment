import React from 'react';
import './Login.css';
import loginpic from "../Images/loginpic.png";
import logo from "../Images/WorkPA-logo.png";

function Login() {
  return (
    <div className="login-container">
      <div className="left-section">
        <img src={loginpic} alt="Login Pic" draggable="false" />
        <div className="description">
          <p>Assess and track</p>
          <p>employee performance</p>
          <p>with <span className="darkblue-text">ease.</span></p>
        </div>
      </div>

      <div className="right-section">
        <img className="logo" src={logo} alt="Logo" draggable="false" />
        <label class='login-label'>Email</label>
        <input class='login-input' type="email" id="email" />
        <label class='login-label'>Password</label>
        <input class='login-input' type="password" id="password" />
        <button class='login-button'>Login</button>
      </div>
    </div>
  );
}

export default Login;
