import { Box, Stack } from '@mui/material'
import React from 'react'
import { Link } from 'react-router-dom'
import { useNavigate, useLocation } from 'react-router-dom';

function TopBarThree() {
    const navigate = useNavigate();
    const location = useLocation();
    const organizationId = location.pathname.split('/')[2];

    const navigateToOrganizations = () => {
        localStorage.removeItem('employeeData');
        navigate('/organizations');
    };

    return (
        <Box
            sx={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                width: "100%",
                height: '100px',
                backgroundColor: '#055c9d'
            }}
        >

            <Stack
                direction="row"
                justifyContent="center"
                alignItems="center"
                sx={{
                    width: '100%',
                    height: '100%',
                }}
            >
                <ul className="nav h-100 w-100 d-flex justify-content-center align-items-center">
                    <li className="nav-item h-100 col d-flex justify-content-center align-items-center">
                        <a className="nav-link h-100 w-100 p-0" 
                            href={`/organizations/${organizationId}`}
                            style={{
                                color: '#055c9d',
                            }}
                        >
                            <Box
                                sx={{
                                    height: "100%",
                                    width: "100%",
                                    backgroundColor: "#abe9f0",
                                    display: 'flex',
                                    justifyContent: 'center',
                                    alignItems: 'center',
                                }}
                            >

                                <Stack
                                    direction="row"
                                    justifyContent="center"
                                    alignItems="center"
                                    spacing={2}
                                >
                                    <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" className="bi bi-people-fill" viewBox="0 0 16 16">
                                        <path d="M7 14s-1 0-1-1 1-4 5-4 5 3 5 4-1 1-1 1H7Zm4-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6Zm-5.784 6A2.238 2.238 0 0 1 5 13c0-1.355.68-2.75 1.936-3.72A6.325 6.325 0 0 0 5 9c-4 0-5 3-5 4s1 1 1 1h4.216ZM4.5 8a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5Z" />
                                    </svg>
                                    <b>General</b>
                                </Stack>
                            </Box>
                        </a>
                    </li>
                    <li className="nav-item h-100 col d-flex justify-content-center align-items-center">
                        <Link className="nav-link h-100 w-100 p-0 d-flex justify-content-center align-items-center"
                            to={`/organizations/${organizationId}/performance`}
                            style={{
                                color: 'white',
                            }}
                        >
                            <b>Performance</b>
                        </Link>
                    </li>
                    <li className="nav-item h-100 col d-flex justify-content-center align-items-center">
                        <Link className="nav-link h-100 w-100 p-0 d-flex justify-content-center align-items-center" 
                            to={`/organizations/${organizationId}/employee-assessments`}
                            style={{
                                color: 'white',
                            }}
                        >
                            <b>Assessments</b>
                        </Link>
                    </li>
                    <li className="nav-item h-100 col d-flex justify-content-center align-items-center">
                        <a className="nav-link h-100 w-100 p-0 d-flex justify-content-center align-items-center" 
                            href={`/organizations/${organizationId}/employee-analytics`}
                            style={{
                                color: 'white',
                            }}
                        >
                            <b>Analytics</b>
                        </a>
                    </li>
                    <li className="nav-item h-100 col d-flex justify-content-center align-items-center">
                        <button type="button" className="btn" onClick={navigateToOrganizations}>
                            <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="white" className="bi bi-three-dots" viewBox="0 0 16 16">
                                <path d="M3 9.5a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3zm5 0a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3zm5 0a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3z" />
                            </svg>
                        </button>
                    </li>
                </ul>
            </Stack>
        </Box>
    )
}

export default TopBarThree