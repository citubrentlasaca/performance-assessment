import React from 'react'
import { Stack } from '@mui/material';
import logo from './Images/Work PA Logo.png';
import landingPhoto from './Images/Landing Page Photo.png';
import { NavLink } from 'react-router-dom';

function LandingPage() {
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
                    padding: "50px"
                }}
            >
                <Stack
                    direction="row"
                    justifyContent="center"
                    alignItems="center"
                    spacing={8}
                >
                    <img src={logo} alt="Work PA Logo" />
                    <ul class="nav gap-4">
                        <li class="nav-item">
                            <a class="nav-link" href="#"
                                style={{
                                    color: 'black'
                                }}
                            >Pricing</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#"
                                style={{
                                    color: 'black'
                                }}
                            >About Us</a>
                        </li>
                    </ul>
                </Stack>
                <NavLink to="/login">
                    <button
                        style={{
                            background: 'linear-gradient(to right, #0076fe, #00c5ff)',
                            color: 'white',
                            border: 'none',
                            borderRadius: '10px',
                            width: '100px',
                            height: '40px'
                        }}
                    >
                        Sign In
                    </button>
                </NavLink>
            </div>
            <Stack
                direction="row"
                justifyContent="space-evenly"
                alignItems="center"
                spacing={2}
                sx={{
                    width: '100%',
                    height: '100%'
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
                        padding: '50px'
                    }}
                >
                    <h1
                        style={{
                            fontWeight: 'bold'
                        }}
                    >
                        Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur
                    </h1>
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
                    <b
                        style={{
                            color: '#065d9d'
                        }}
                    >
                        Assess and track employee performance with ease.
                    </b>
                    <Stack
                        direction="row"
                        justifyContent="center"
                        alignItems="center"
                        spacing={2}
                        sx={{
                            width: '100%',
                        }}
                    >
                        <button
                            style={{
                                background: 'linear-gradient(to right, #0076fe, #00c5ff)',
                                color: 'white',
                                border: 'none',
                                borderRadius: '10px',
                                width: '200px',
                                height: '40px'
                            }}
                        >
                            Register Now
                        </button>
                    </Stack>
                </Stack>
                <Stack
                    direction="column"
                    justifyContent="center"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        width: '50%',
                        height: '100%',
                        padding: '50px'
                    }}
                >
                    <img src={landingPhoto} alt="Work PA Logo"
                        style={{
                            width: '700px',
                            height: '700px'
                        }}
                    />
                </Stack>
            </Stack>
        </Stack>
    )
}

export default LandingPage