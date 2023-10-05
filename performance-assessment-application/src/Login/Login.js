import React from 'react';
import './Login.css';
import loginpic from "../Images/loginpic.png";
import logo from "../Images/WorkPA-logo.png";

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

      <div className="login-form-container">
        <form>
          <div className="login-form-group">
            <img className="logo" src={logo} alt="Logo" draggable="false" />
          </div>
          <div className="login-form-group">
            <label htmlFor="email">Email</label>
            <input type="email" id="email" name="email" required/>
          </div>
          <div className="login-form-group">
            <label  htmlFor="password">Password</label>
            <input type="password" id="password" name="password" required/>
          </div>
          <div className="login-button-container">
            <button className="login-button" type="login">Login</button>
          </div>
        </form>
      </div>
      </div>
    </div>
  );
}

export default Login;
