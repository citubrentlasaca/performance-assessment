import React, { useState } from 'react'
import { Stack } from '@mui/material';
import logo from './Images/Work PA Logo.png';
import landingPhoto from './Images/Landing Page Photo.png';
import { Link } from 'react-router-dom';

function LandingPage() {
    const [signInHover, setSignInHover] = useState(false);
    const [registerHover, setRegisterHover] = useState(false);

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

    const handleSignInHover = () => {
        setSignInHover(true);
    };

    const handleSignInLeave = () => {
        setSignInHover(false);
    };

    const handleRegisterHover = () => {
        setRegisterHover(true);
    };

    const handleRegisterLeave = () => {
        setRegisterHover(false);
    };

    return (
        <Stack
            direction="column"
            justifyContent="flex-start"
            alignItems="center"
            spacing={0}
            sx={{
                width: "100%",
                height: "100%",
            }}
        >
            <div
                style={{
                    display: 'flex',
                    flexDirection: 'row',
                    alignItems: 'center',
                    justifyContent: 'space-between',
                    width: "100%",
                    height: "100px",
                    backgroundColor: 'white',
                    padding: "20px"
                }}
            >
                <img src={logo} alt="Work PA Logo" />
                <Link to="/login">
                    <button style={signInHover ? { ...normalStyle, ...hoverStyle } : normalStyle}
                        onMouseEnter={handleSignInHover}
                        onMouseLeave={handleSignInLeave}
                    >
                        Sign In
                    </button>
                </Link>
            </div>
            <Stack
                direction="row"
                justifyContent="space-evenly"
                alignItems="center"
                spacing={2}
                sx={{
                    width: '100%',
                    height: 'calc(100% - 100px)'
                }}
            >
                <Stack
                    direction="column"
                    justifyContent="center"
                    alignItems="flex-start"
                    spacing={2}
                    sx={{
                        width: '50%',
                        height: '100%',
                        padding: '60px'
                    }}
                >
                    <h1
                        style={{
                            fontWeight: 'bold',
                            fontSize: '50px',
                        }}
                    >
                        Revolutionize Your Workforce Performance
                    </h1>
                    <p>Unlock Greater Productivity, Boost Engagement, and Foster Growth with Our Innovative Performance Assessment App</p>
                    <b
                        style={{
                            color: '#065d9d'
                        }}
                    >
                        Assess and track employee performance with ease.
                    </b>
                    <Link to="/register">
                        <button style={registerHover ? { ...normalStyle, ...hoverStyle } : normalStyle}
                            onMouseEnter={handleRegisterHover}
                            onMouseLeave={handleRegisterLeave}
                        >
                            Register Now
                        </button>
                    </Link>
                </Stack>
                <div
                    style={{
                        width: '50%',
                        height: '100%',
                        display: 'flex',
                        justifyContent: 'center',
                        alignItems: 'center',
                        padding: '60px'
                    }}
                >
                    <img src={landingPhoto} alt="Landing Page"
                        style={{
                            height: '100%'
                        }}
                    />
                </div>
            </Stack>
        </Stack>
    )
}

export default LandingPage