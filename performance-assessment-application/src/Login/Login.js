import React from 'react';
import './Login.css'; 
import loginpic from "../Images/loginpic.png";

function Login() {
  return (
    <div className="login-container">
      <div className="left-section">
        <img src={loginpic} alt="Login Pic" />
          <div className="description">
            <p>Assess and track</p>
            <p>employee performance</p>
            <p>with ease.</p>
          </div>
        </div>

    </div>
  );
}

export default Login;
