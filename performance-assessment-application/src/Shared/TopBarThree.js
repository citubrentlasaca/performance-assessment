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
                    <li className="nav-item h-100 col d-flex justify-content-center align-items-center">
                        <button type="button" className="btn" onClick={navigateToOrganizations}>
                            <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="white" className="bi bi-diagram-3" viewBox="0 0 16 16">
                                <path fill-rule="evenodd" d="M6 3.5A1.5 1.5 0 0 1 7.5 2h1A1.5 1.5 0 0 1 10 3.5v1A1.5 1.5 0 0 1 8.5 6v1H14a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-1 0V8h-5v.5a.5.5 0 0 1-1 0V8h-5v.5a.5.5 0 0 1-1 0v-1A.5.5 0 0 1 2 7h5.5V6A1.5 1.5 0 0 1 6 4.5v-1zM8.5 5a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1zM0 11.5A1.5 1.5 0 0 1 1.5 10h1A1.5 1.5 0 0 1 4 11.5v1A1.5 1.5 0 0 1 2.5 14h-1A1.5 1.5 0 0 1 0 12.5v-1zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1zm4.5.5A1.5 1.5 0 0 1 7.5 10h1a1.5 1.5 0 0 1 1.5 1.5v1A1.5 1.5 0 0 1 8.5 14h-1A1.5 1.5 0 0 1 6 12.5v-1zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1zm4.5.5a1.5 1.5 0 0 1 1.5-1.5h1a1.5 1.5 0 0 1 1.5 1.5v1a1.5 1.5 0 0 1-1.5 1.5h-1a1.5 1.5 0 0 1-1.5-1.5v-1zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1z" />
                            </svg>
                        </button>
                    </li>
                </ul>
            </Stack>
        </Box>
    )
}

export default TopBarThree