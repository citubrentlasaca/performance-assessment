import { Box, Stack } from '@mui/material'
import React from 'react'
import { Link } from 'react-router-dom'
import { useNavigate, useLocation } from 'react-router-dom';

function TopBarThree() {
    const navigate = useNavigate();
    const location = useLocation();
    const organizationId = location.pathname.split('/')[2];
    const employeeStorage = JSON.parse(localStorage.getItem('employeeData'));

    const navigateToOrganizations = () => {
        localStorage.removeItem('employeeData');
        navigate('/organizations');
    };

    const isActive = (path) => {
        return location.pathname.startsWith(path);
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
                    <li className={`nav-item h-100 col d-flex justify-content-center align-items-center ${isActive(`/organizations/${employeeStorage.teamId}/announcements`) ? 'active' : ''}`} >
                        <a className="nav-link h-100 w-100 p-0"
                            href={`/organizations/${organizationId}/announcements`}
                        >
                            <Box
                                sx={{
                                    height: "100%",
                                    width: "100%",
                                    display: 'flex',
                                    justifyContent: 'center',
                                    alignItems: 'center',
                                    backgroundColor: isActive(`/organizations/${employeeStorage.teamId}/announcements`) ? '#abe9f0' : '#055c9d',
                                    color: isActive(`/organizations/${employeeStorage.teamId}/announcements`) ? '#055c9d' : 'white',
                                }}
                            >

                                <Stack
                                    direction="row"
                                    justifyContent="center"
                                    alignItems="center"
                                    spacing={2}
                                >
                                    <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="currentColor" className="bi bi-people-fill" viewBox="0 0 16 16">
                                        <path d="M7 14s-1 0-1-1 1-4 5-4 5 3 5 4-1 1-1 1H7Zm4-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6Zm-5.784 6A2.238 2.238 0 0 1 5 13c0-1.355.68-2.75 1.936-3.72A6.325 6.325 0 0 0 5 9c-4 0-5 3-5 4s1 1 1 1h4.216ZM4.5 8a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5Z" />
                                    </svg>
                                    <b>General</b>
                                </Stack>
                            </Box>
                        </a>
                    </li>
                    <li className={`nav-item h-100 col d-flex justify-content-center align-items-center ${isActive(`/organizations/${employeeStorage.teamId}/performance`) ? 'active' : ''}`} >
                        <Link className="nav-link h-100 w-100 p-0 d-flex justify-content-center align-items-center"
                            to={`/organizations/${organizationId}/performance`}
                            style={{
                                backgroundColor: isActive(`/organizations/${employeeStorage.teamId}/performance`) ? '#abe9f0' : '#055c9d',
                                color: isActive(`/organizations/${employeeStorage.teamId}/performance`) ? '#055c9d' : 'white',
                            }}
                        >
                            <b>Performance</b>
                        </Link>
                    </li>
                    <li className={`nav-item h-100 col d-flex justify-content-center align-items-center ${isActive(`/organizations/${employeeStorage.teamId}/employee-assessments`) ? 'active' : ''}`} >
                        <Link className="nav-link h-100 w-100 p-0 d-flex justify-content-center align-items-center"
                            to={`/organizations/${organizationId}/employee-assessments`}
                            style={{
                                backgroundColor: isActive(`/organizations/${employeeStorage.teamId}/employee-assessments`) ? '#abe9f0' : '#055c9d',
                                color: isActive(`/organizations/${employeeStorage.teamId}/employee-assessments`) ? '#055c9d' : 'white',
                            }}
                        >
                            <b>Assessments</b>
                        </Link>
                    </li>
                    <li className={`nav-item h-100 col d-flex justify-content-center align-items-center ${isActive(`/organizations/${employeeStorage.teamId}/employee-analytics`) ? 'active' : ''}`} >
                        <a className="nav-link h-100 w-100 p-0 d-flex justify-content-center align-items-center"
                            href={`/organizations/${organizationId}/employee-analytics`}
                            style={{
                                backgroundColor: isActive(`/organizations/${employeeStorage.teamId}/employee-analytics`) ? '#abe9f0' : '#055c9d',
                                color: isActive(`/organizations/${employeeStorage.teamId}/employee-analytics`) ? '#055c9d' : 'white',
                            }}
                        >
                            <b>Analytics</b>
                        </a>
                    </li>
                </ul>
            </Stack>
        </Box>
    )
}

export default TopBarThree