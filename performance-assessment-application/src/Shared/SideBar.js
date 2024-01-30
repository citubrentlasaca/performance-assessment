import React, { useEffect, useState } from 'react';
import { Stack } from '@mui/material'
import { useLocation } from 'react-router-dom';
import logo from '../Images/WorkPA-logo.png'
import miniLogo from '../Images/Mini Logo.png'
import axios from 'axios';

function SideBar() {
    const [collapsed, setCollapsed] = useState(false);
    const userId = localStorage.getItem('userId');
    const location = useLocation();
    const [hasNewNotifications, setHasNewNotifications] = useState(false);

    const toggleCollapse = () => {
        setCollapsed(!collapsed);
    };

    const isActive = (path) => {
        return location.pathname.startsWith(path);
    };

    useEffect(() => {
        const fetchAssessmentsForEmployee = async (employeeId) => {
            try {
                const response = await axios.get(`https://workpa.azurewebsites.net/api/employee-assignscheduler-notifications/employees/${employeeId}`);
                let count = 0;
                for (const notification of response.data) {
                    if (notification.isRead === false) {
                        count++;
                    }
                }
                return count;
            } catch (error) {
                console.error('Error fetching assessments:', error);
                return [];
            }
        };

        const fetchAnnouncementsForEmployee = async (employeeId) => {
            try {
                const response = await axios.get(`https://workpa.azurewebsites.net/api/employee-announcement-notifications/employees/${employeeId}`);
                let count = 0;
                for (const notification of response.data) {
                    if (notification.isRead === false) {
                        count++;
                    }
                }
                return count;
            } catch (error) {
                console.error('Error fetching announcements:', error);
                return [];
            }
        };

        const fetchAdminNotificationsForEmployee = async (employeeId) => {
            try {
                const response = await axios.get(`https://workpa.azurewebsites.net/api/admin-notifications/employees/${employeeId}`);
                let count = 0;
                for (const notification of response.data) {
                    if (notification.isRead === false) {
                        count++;
                    }
                }
                return count;
            } catch (error) {
                console.error('Error fetching admin notifications:', error);
            }
        };

        const fetchNotificationsForEmployee = async () => {
            try {
                const employeeResponse = await axios.get(`https://workpa.azurewebsites.net/api/employees/users/${userId}`);
                const employeeData = employeeResponse.data;
                for (const employee of employeeData) {
                    if (employee.status === 'Active') {
                        const admin = await fetchAdminNotificationsForEmployee(employee.id);
                        const assessments = await fetchAssessmentsForEmployee(employee.id);
                        const announcements = await fetchAnnouncementsForEmployee(employee.id);
                        if (admin > 0 || assessments > 0 || announcements > 0) {
                            setHasNewNotifications(true);
                        }
                    }
                }
            } catch (error) {
                console.error('Error fetching notifications:', error);
            }
        };

        fetchNotificationsForEmployee();
    }, []);

    return (
        <div
            style={{
                width: collapsed ? '50px' : '300px',
                height: '100%',
                backgroundColor: '#abe9f0',
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'space-between',
                transition: 'width 0.3s',
            }}
        >
            <Stack
                direction="column"
                justifyContent="flex-start"
                alignItems="center"
                sx={{
                    width: '100%',
                    height: 'fit-content',
                }}
            >
                {collapsed ? (
                    <div
                        style={{
                            width: '100%',
                            height: '170px',
                            backgroundColor: 'white',
                            display: 'flex',
                            alignItems: 'center',
                            justifyContent: 'center',
                            padding: '20px'
                        }}
                    >
                        <img src={miniLogo} alt='Work PA Logo'
                            style={{
                                width: '25px',
                            }}
                        />
                    </div>
                ) : (
                    <div
                        style={{
                            width: '100%',
                            height: '170px',
                            backgroundColor: 'white',
                            display: 'flex',
                            alignItems: 'center',
                            justifyContent: 'center',
                            padding: '20px'
                        }}
                    >
                        <img src={logo} alt='Work PA Logo' style={{ width: '100%' }} />
                    </div>
                )}

                {collapsed ? (
                    <Stack
                        direction="column"
                        justifyContent="center"
                        alignItems="flex-start"
                        spacing={2}
                        sx={{
                            width: '100%',
                            height: 'fit-content',
                        }}
                    >
                        <ul className="nav flex-column"
                            style={{
                                width: '100%',
                            }}
                        >
                            <li className={`nav-item w-100 ${isActive('/home') ? 'active' : ''}`}
                                style={{
                                    height: '96px',
                                    display: 'flex',
                                    alignItems: 'center',
                                    justifyContent: 'center',
                                    backgroundColor: isActive('/home') ? '#055c9d' : '#abe9f0',
                                }}
                            >
                                <a className="nav-link" href="/home"
                                    style={{
                                        color: 'black'
                                    }}
                                >
                                    <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-house" viewBox="0 0 16 16">
                                        <path d="M8.707 1.5a1 1 0 0 0-1.414 0L.646 8.146a.5.5 0 0 0 .708.708L2 8.207V13.5A1.5 1.5 0 0 0 3.5 15h9a1.5 1.5 0 0 0 1.5-1.5V8.207l.646.647a.5.5 0 0 0 .708-.708L13 5.793V2.5a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1.293L8.707 1.5ZM13 7.207V13.5a.5.5 0 0 1-.5.5h-9a.5.5 0 0 1-.5-.5V7.207l5-5 5 5Z" />
                                    </svg>
                                </a>
                            </li>
                            <li className={`nav-item w-100 ${isActive('/organizations') ? 'active' : ''}`}
                                style={{
                                    height: '96px',
                                    display: 'flex',
                                    alignItems: 'center',
                                    justifyContent: 'center',
                                    backgroundColor: isActive('/organizations') ? '#055c9d' : '#abe9f0',
                                }}
                            >
                                <a className="nav-link" href="/organizations"
                                    style={{
                                        color: 'black'
                                    }}
                                >
                                    <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-diagram-3" viewBox="0 0 16 16">
                                        <path fillRule="evenodd" d="M6 3.5A1.5 1.5 0 0 1 7.5 2h1A1.5 1.5 0 0 1 10 3.5v1A1.5 1.5 0 0 1 8.5 6v1H14a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-1 0V8h-5v.5a.5.5 0 0 1-1 0V8h-5v.5a.5.5 0 0 1-1 0v-1A.5.5 0 0 1 2 7h5.5V6A1.5 1.5 0 0 1 6 4.5v-1zM8.5 5a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1zM0 11.5A1.5 1.5 0 0 1 1.5 10h1A1.5 1.5 0 0 1 4 11.5v1A1.5 1.5 0 0 1 2.5 14h-1A1.5 1.5 0 0 1 0 12.5v-1zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1zm4.5.5A1.5 1.5 0 0 1 7.5 10h1a1.5 1.5 0 0 1 1.5 1.5v1A1.5 1.5 0 0 1 8.5 14h-1A1.5 1.5 0 0 1 6 12.5v-1zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1zm4.5.5a1.5 1.5 0 0 1 1.5-1.5h1a1.5 1.5 0 0 1 1.5 1.5v1a1.5 1.5 0 0 1-1.5 1.5h-1a1.5 1.5 0 0 1-1.5-1.5v-1zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1z" />
                                    </svg>
                                </a>
                            </li>
                            <li className={`nav-item w-100 ${isActive('/notifications') ? 'active' : ''}`}
                                style={{
                                    height: '96px',
                                    display: 'flex',
                                    alignItems: 'center',
                                    justifyContent: 'center',
                                    backgroundColor: isActive('/notifications') ? '#055c9d' : '#abe9f0',
                                }}
                            >
                                <a className="nav-link" href={`/notifications/${userId}`}
                                    style={{
                                        color: 'black'
                                    }}
                                >
                                    <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill={hasNewNotifications ? '#dc3545' : 'currentColor'} className="bi bi-bell" viewBox="0 0 16 16">
                                        <path d="M8 16a2 2 0 0 0 2-2H6a2 2 0 0 0 2 2zM8 1.918l-.797.161A4.002 4.002 0 0 0 4 6c0 .628-.134 2.197-.459 3.742-.16.767-.376 1.566-.663 2.258h10.244c-.287-.692-.502-1.49-.663-2.258C12.134 8.197 12 6.628 12 6a4.002 4.002 0 0 0-3.203-3.92L8 1.917zM14.22 12c.223.447.481.801.78 1H1c.299-.199.557-.553.78-1C2.68 10.2 3 6.88 3 6c0-2.42 1.72-4.44 4.005-4.901a1 1 0 1 1 1.99 0A5.002 5.002 0 0 1 13 6c0 .88.32 4.2 1.22 6z" />
                                    </svg>
                                </a>
                            </li>
                        </ul>
                    </Stack>
                ) : (
                    <Stack
                        direction="column"
                        justifyContent="center"
                        alignItems="flex-start"
                        spacing={2}
                        sx={{
                            width: '100%',
                            height: 'fit-content',
                        }}
                    >
                        <ul className="nav flex-column"
                            style={{
                                width: '100%',
                            }}
                        >
                            <li className={`nav-item w-100 ${isActive('/home') ? 'active' : ''}`}
                                style={{
                                    height: '96px',
                                    backgroundColor: isActive('/home') ? '#055c9d' : '#abe9f0',
                                }}
                            >
                                <a className="nav-link" href="/home"
                                    style={{
                                        color: 'black'
                                    }}
                                >
                                    <Stack
                                        direction="row"
                                        justifyContent="flex-start"
                                        alignItems="center"
                                        spacing={2}
                                        sx={{
                                            width: '100%',
                                            padding: '20px'
                                        }}
                                    >
                                        <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="currentColor" className="bi bi-house" viewBox="0 0 16 16">
                                            <path d="M8.707 1.5a1 1 0 0 0-1.414 0L.646 8.146a.5.5 0 0 0 .708.708L2 8.207V13.5A1.5 1.5 0 0 0 3.5 15h9a1.5 1.5 0 0 0 1.5-1.5V8.207l.646.647a.5.5 0 0 0 .708-.708L13 5.793V2.5a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1.293L8.707 1.5ZM13 7.207V13.5a.5.5 0 0 1-.5.5h-9a.5.5 0 0 1-.5-.5V7.207l5-5 5 5Z" />
                                        </svg>
                                        <b>Home</b>
                                    </Stack>
                                </a>
                            </li>
                            <li className={`nav-item w-100 ${isActive('/organizations') ? 'active' : ''}`}
                                style={{
                                    height: '96px',
                                    backgroundColor: isActive('/organizations') ? '#055c9d' : '#abe9f0',
                                }}
                            >
                                <a className="nav-link" href="/organizations"
                                    style={{
                                        color: 'black'
                                    }}
                                >
                                    <Stack
                                        direction="row"
                                        justifyContent="flex-start"
                                        alignItems="center"
                                        spacing={2}
                                        sx={{
                                            width: '100%',
                                            padding: '20px'
                                        }}
                                    >
                                        <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="currentColor" className="bi bi-diagram-3" viewBox="0 0 16 16">
                                            <path fillRule="evenodd" d="M6 3.5A1.5 1.5 0 0 1 7.5 2h1A1.5 1.5 0 0 1 10 3.5v1A1.5 1.5 0 0 1 8.5 6v1H14a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-1 0V8h-5v.5a.5.5 0 0 1-1 0V8h-5v.5a.5.5 0 0 1-1 0v-1A.5.5 0 0 1 2 7h5.5V6A1.5 1.5 0 0 1 6 4.5v-1zM8.5 5a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1zM0 11.5A1.5 1.5 0 0 1 1.5 10h1A1.5 1.5 0 0 1 4 11.5v1A1.5 1.5 0 0 1 2.5 14h-1A1.5 1.5 0 0 1 0 12.5v-1zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1zm4.5.5A1.5 1.5 0 0 1 7.5 10h1a1.5 1.5 0 0 1 1.5 1.5v1A1.5 1.5 0 0 1 8.5 14h-1A1.5 1.5 0 0 1 6 12.5v-1zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1zm4.5.5a1.5 1.5 0 0 1 1.5-1.5h1a1.5 1.5 0 0 1 1.5 1.5v1a1.5 1.5 0 0 1-1.5 1.5h-1a1.5 1.5 0 0 1-1.5-1.5v-1zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1z" />
                                        </svg>
                                        <b>Organizations</b>
                                    </Stack>
                                </a>
                            </li>
                            <li className={`nav-item w-100 ${isActive('/notifications') ? 'active' : ''}`}
                                style={{
                                    height: '96px',
                                    backgroundColor: isActive('/notifications') ? '#055c9d' : '#abe9f0',
                                }}
                            >
                                <a className="nav-link" href={`/notifications/${userId}`}
                                    style={{
                                        color: 'black'
                                    }}
                                >
                                    <Stack
                                        direction="row"
                                        justifyContent="flex-start"
                                        alignItems="center"
                                        spacing={2}
                                        sx={{
                                            width: '100%',
                                            padding: '20px'
                                        }}
                                    >
                                        <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill={hasNewNotifications ? '#dc3545' : 'currentColor'} className="bi bi-bell" viewBox="0 0 16 16">
                                            <path d="M8 16a2 2 0 0 0 2-2H6a2 2 0 0 0 2 2zM8 1.918l-.797.161A4.002 4.002 0 0 0 4 6c0 .628-.134 2.197-.459 3.742-.16.767-.376 1.566-.663 2.258h10.244c-.287-.692-.502-1.49-.663-2.258C12.134 8.197 12 6.628 12 6a4.002 4.002 0 0 0-3.203-3.92L8 1.917zM14.22 12c.223.447.481.801.78 1H1c.299-.199.557-.553.78-1C2.68 10.2 3 6.88 3 6c0-2.42 1.72-4.44 4.005-4.901a1 1 0 1 1 1.99 0A5.002 5.002 0 0 1 13 6c0 .88.32 4.2 1.22 6z" />
                                        </svg>
                                        <b style={{
                                            color: hasNewNotifications ? '#dc3545' : 'black'
                                        }}>
                                            Notifications
                                        </b>
                                    </Stack>
                                </a>
                            </li>
                        </ul>
                    </Stack>
                )}

            </Stack>
            <button type='button' className='btn' onClick={toggleCollapse}>
                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-arrows-collapse-vertical" viewBox="0 0 16 16">
                    <path d="M8 15a.5.5 0 0 1-.5-.5v-13a.5.5 0 0 1 1 0v13a.5.5 0 0 1-.5.5ZM0 8a.5.5 0 0 1 .5-.5h3.793L3.146 6.354a.5.5 0 1 1 .708-.708l2 2a.5.5 0 0 1 0 .708l-2 2a.5.5 0 0 1-.708-.708L4.293 8.5H.5A.5.5 0 0 1 0 8Zm11.707.5 1.147 1.146a.5.5 0 0 1-.708.708l-2-2a.5.5 0 0 1 0-.708l2-2a.5.5 0 0 1 .708.708L11.707 7.5H15.5a.5.5 0 0 1 0 1h-3.793Z" />
                </svg>
            </button>
        </div>
    )
}

export default SideBar