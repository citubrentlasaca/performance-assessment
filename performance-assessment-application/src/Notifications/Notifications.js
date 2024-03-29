import React, { useState, useEffect } from 'react'
import NavBar from '../Shared/NavBar'
import { Box, Stack } from '@mui/material'
import { Link, useParams } from 'react-router-dom'
import axios from 'axios';

function Notifications() {
    const [admin, setAdmin] = useState([])
    const [announcements, setAnnouncements] = useState([])
    const [assessments, setAssessments] = useState([])
    const { id } = useParams();
    const [activeTab, setActiveTab] = useState('Admin');

    useEffect(() => {
        const fetchAssessmentsForEmployee = async (employeeId) => {
            try {
                const response = await axios.get(`https://workpa.azurewebsites.net/api/employee-assignscheduler-notifications/employees/${employeeId}`);
                for (const notification of response.data) {
                    if (notification.isRead === false) {
                        await axios.patch(`https://workpa.azurewebsites.net/api/employee-assignscheduler-notifications/${notification.id}/mark-as-read`, {
                            isRead: true,
                        });
                    }
                }
                const reversedData = response.data.reverse();

                const assessmentPromises = reversedData.map(async (item) => {
                    const assessmentResponse = await axios.get(`https://workpa.azurewebsites.net/api/assessments/${item.assessmentId}`);
                    const schedulerCheck = await axios.get(`https://workpa.azurewebsites.net/api/schedulers/assessments/${item.assessmentId}`);
                    const dateTimeCreated = new Date(item.dateTimeCreated);
                    const formattedDateTime = dateTimeCreated.toLocaleString('en-US', {
                        month: 'long',
                        day: 'numeric',
                        year: 'numeric',
                        hour: '2-digit',
                        minute: '2-digit',
                        hour12: true,
                    });

                    if (schedulerCheck.data.length === 0) {
                        return {
                            hasAssessment: false,
                            assessmentData: assessmentResponse.data,
                            dateTimeCreated: formattedDateTime,
                        };
                    } else {
                        return {
                            hasAssessment: true,
                            assessmentData: assessmentResponse.data,
                            dateTimeCreated: formattedDateTime,
                        };
                    }
                });

                const assessmentData = await Promise.all(assessmentPromises);
                return assessmentData;
            } catch (error) {
                console.error('Error fetching assessments:', error);
                return [];
            }
        };

        const fetchAnnouncementsForEmployee = async (employeeId) => {
            try {
                const response = await axios.get(`https://workpa.azurewebsites.net/api/employee-announcement-notifications/employees/${employeeId}`);
                for (const notification of response.data) {
                    if (notification.isRead === false) {
                        await axios.patch(`https://workpa.azurewebsites.net/api/employee-announcement-notifications/${notification.id}/mark-as-read`, {
                            isRead: true,
                        });
                    }
                }
                const reversedData = response.data.reverse();

                const announcementPromises = reversedData.map(async (item) => {
                    const announcementResponse = await axios.get(`https://workpa.azurewebsites.net/api/announcements/${item.announcementId}`);
                    const dateTimeCreated = new Date(item.dateTimeCreated);
                    const formattedDateTime = dateTimeCreated.toLocaleString('en-US', {
                        month: 'long',
                        day: 'numeric',
                        year: 'numeric',
                        hour: '2-digit',
                        minute: '2-digit',
                        hour12: true,
                    });

                    const teamResponse = await axios.get(`https://workpa.azurewebsites.net/api/teams/${announcementResponse.data.teamId}`);
                    const organization = teamResponse.data.organization;

                    return {
                        announcementData: announcementResponse.data,
                        dateTimeCreated: formattedDateTime,
                        organization,
                    };
                });

                const announcementData = await Promise.all(announcementPromises);
                return announcementData;
            } catch (error) {
                console.error('Error fetching announcements:', error);
                return [];
            }
        };

        const fetchAdminNotificationsForEmployee = async (employeeId) => {
            try {
                const response = await axios.get(`https://workpa.azurewebsites.net/api/admin-notifications/employees/${employeeId}`);
                for (const notification of response.data) {
                    if (notification.isRead === false) {
                        await axios.patch(`https://workpa.azurewebsites.net/api/admin-notifications/${notification.id}/mark-as-read`, {
                            isRead: true,
                        });
                    }
                }
                const reversedData = response.data.reverse();

                const formattedAdminNotifications = reversedData.map(item => {
                    const dateTimeCreated = new Date(item.dateTimeCreated);
                    const formattedDateTime = dateTimeCreated.toLocaleString('en-US', {
                        month: 'long',
                        day: 'numeric',
                        year: 'numeric',
                        hour: '2-digit',
                        minute: '2-digit',
                        hour12: true,
                    });

                    return {
                        ...item,
                        dateTimeCreated: formattedDateTime,
                    };
                });

                return formattedAdminNotifications;
            } catch (error) {
                console.error('Error fetching admin notifications:', error);
            }
        };

        const fetchNotificationsForEmployee = async () => {
            try {
                const employeeResponse = await axios.get(`https://workpa.azurewebsites.net/api/employees/users/${id}`);
                const employeeData = employeeResponse.data;
                const adminData = [];
                const assessmentsData = [];
                const announcementsData = [];
                for (const employee of employeeData) {
                    if (employee.status === 'Active') {
                        const admin = await fetchAdminNotificationsForEmployee(employee.id);
                        adminData.push(...admin);
                        const assessments = await fetchAssessmentsForEmployee(employee.id);
                        assessmentsData.push(...assessments);
                        const announcements = await fetchAnnouncementsForEmployee(employee.id);
                        announcementsData.push(...announcements);
                    }
                }
                setAdmin(adminData);
                setAssessments(assessmentsData);
                setAnnouncements(announcementsData);
            } catch (error) {
                console.error('Error fetching notifications:', error);
            }
        };

        fetchNotificationsForEmployee();
    }, [id]);

    const renderAdminTab = () => (
        <Stack
            direction="column"
            justifyContent="flex-start"
            alignItems="flex-start"
            spacing={2}
            sx={{
                width: '100%',
                height: '100%',
            }}
        >
            {admin.length === 0 ? (
                <Stack
                    direction="row"
                    justifyContent="center"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        width: '100%',
                        height: '100%',
                    }}
                >
                    <p>No notifications found</p>
                </Stack>
            ) : (
                admin.map((admin, index) => (
                    <Stack key={index}
                        direction="column"
                        justifyContent="flex-start"
                        alignItems="flex-start"
                        spacing={2}
                        sx={{
                            width: '100%',
                            padding: '10px',
                        }}
                    >
                        <p className='mb-0'>{admin.dateTimeCreated}</p>
                        <Box className='gap-2'
                            sx={{
                                width: '100%',
                                height: 'fit-content',
                                backgroundColor: 'white',
                                borderRadius: '10px',
                                display: 'flex',
                                flexDirection: 'column',
                                justifyContent: 'center',
                                alignItems: 'flex-start',
                                padding: '30px',
                            }}
                        >
                            <b>{admin.employeeName} failed to answer {admin.assessmentTitle} within the due date</b>
                            <p className='mb-0'>{admin.teamName}</p>
                        </Box>
                    </Stack>
                ))
            )}
            <div
                style={{
                    width: '100%',
                    height: '40px',
                    visibility: 'hidden',
                }}
            >
                Hidden
            </div>
        </Stack>
    );

    const renderAnnouncementsTab = () => (
        <Stack
            direction="column"
            justifyContent="flex-start"
            alignItems="flex-start"
            spacing={2}
            sx={{
                width: '100%',
                height: '100%',
            }}
        >
            {announcements.length === 0 ? (
                <Stack
                    direction="row"
                    justifyContent="center"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        width: '100%',
                        height: '100%',
                    }}
                >
                    <p>No notifications found</p>
                </Stack>
            ) : (
                announcements.map((announcement, index) => (
                    <Stack
                        key={index}
                        direction="column"
                        justifyContent="flex-start"
                        alignItems="flex-start"
                        spacing={2}
                        sx={{
                            width: '100%',
                            padding: '10px',
                        }}
                    >
                        <p className='mb-0'>{announcement.dateTimeCreated}</p>
                        <Box
                            className='gap-2'
                            sx={{
                                width: '100%',
                                height: 'fit-content',
                                backgroundColor: 'white',
                                borderRadius: '10px',
                                display: 'flex',
                                flexDirection: 'column',
                                justifyContent: 'center',
                                alignItems: 'flex-start',
                                padding: '30px',
                            }}
                        >
                            <p className='mb-0'>{announcement.organization} posted an announcement</p>
                            <b>{announcement.announcementData.content}</b>
                        </Box>
                    </Stack>
                ))
            )}
            <div
                style={{
                    width: '100%',
                    height: '40px',
                    visibility: 'hidden',
                }}
            >
                Hidden
            </div>
        </Stack>
    );

    const renderAssessmentsTab = () => (
        <Stack
            direction="column"
            justifyContent="flex-start"
            alignItems="flex-start"
            spacing={2}
            sx={{
                width: '100%',
                height: '100%',
            }}
        >
            {assessments.length === 0 ? (
                <Stack
                    direction="row"
                    justifyContent="center"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        width: '100%',
                        height: '100%',
                    }}
                >
                    <p>No notifications found</p>
                </Stack>
            ) : (
                assessments.map((assessment, index) => (
                    <Stack key={index}
                        direction="column"
                        justifyContent="flex-start"
                        alignItems="flex-start"
                        spacing={2}
                        sx={{
                            width: '100%',
                            padding: '10px',
                        }}
                    >
                        <p className='mb-0'>{assessment.dateTimeCreated}</p>
                        {assessment.hasAssessment ? (
                            <Link to={`/answerassessment/${assessment.assessmentData.id}`}
                                style={{
                                    textDecoration: 'none',
                                    color: 'black',
                                    width: '100%',
                                }}
                            >
                                <Box className='gap-2'
                                    sx={{
                                        width: '100%',
                                        height: 'fit-content',
                                        backgroundColor: 'white',
                                        borderRadius: '10px',
                                        display: 'flex',
                                        flexDirection: 'column',
                                        justifyContent: 'center',
                                        alignItems: 'flex-start',
                                        padding: '30px',
                                    }}
                                >
                                    <b>You have been assigned an assessment</b>
                                    <p className='mb-0'>{assessment.assessmentData.title}</p>
                                </Box>
                            </Link>
                        ) : (
                            <Box className='gap-2'
                                sx={{
                                    width: '100%',
                                    height: 'fit-content',
                                    backgroundColor: '#a0cce0',
                                    borderRadius: '10px',
                                    display: 'flex',
                                    flexDirection: 'column',
                                    justifyContent: 'center',
                                    alignItems: 'flex-start',
                                    padding: '30px',
                                }}
                            >
                                <b>You have been assigned an assessment</b>
                                <p className='mb-0'>{assessment.assessmentData.title}</p>
                            </Box>
                        )}

                    </Stack>
                ))
            )}
            <div
                style={{
                    width: '100%',
                    height: '40px',
                    visibility: 'hidden',
                }}
            >
                Hidden
            </div>
        </Stack >
    );

    const handleTabChange = (tab) => {
        setActiveTab(tab);
    };


    return (
        <NavBar>
            <Stack
                direction="column"
                justifyContent="flex-start"
                alignItems="center"
                spacing={2}
                sx={{
                    width: '100%',
                    height: '100%',
                    padding: '40px',
                    overflowY: 'auto',
                }}
            >
                <Stack
                    direction="row"
                    justifyContent="flex-start"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        width: '100%',
                    }}
                >
                    <ul className="nav">
                        <li className={`nav-item ${activeTab === 'Admin' ? 'active' : ''}`}>
                            <b
                                className="nav-link"
                                style={{
                                    color: activeTab === 'Admin' ? '#055c9d' : 'black',
                                    cursor: 'pointer',
                                }}
                                onClick={() => handleTabChange('Admin')}
                            >
                                <b>Admin</b>
                            </b>
                        </li>
                        <li className={`nav-item ${activeTab === 'Assessments' ? 'active' : ''}`}>
                            <b
                                className="nav-link"
                                style={{
                                    color: activeTab === 'Assessments' ? '#055c9d' : 'black',
                                    cursor: 'pointer',
                                }}
                                onClick={() => handleTabChange('Assessments')}
                            >
                                <b>Assessments</b>
                            </b>
                        </li>
                        <li className={`nav-item ${activeTab === 'Announcements' ? 'active' : ''}`}>
                            <b
                                className="nav-link"
                                style={{
                                    color: activeTab === 'Announcements' ? '#055c9d' : 'black',
                                    cursor: 'pointer',
                                }}
                                onClick={() => handleTabChange('Announcements')}
                            >
                                <b>Announcements</b>
                            </b>
                        </li>
                    </ul>
                </Stack>
                {activeTab === 'Admin' ? renderAdminTab() : null}
                {activeTab === 'Announcements' ? renderAnnouncementsTab() : null}
                {activeTab === 'Assessments' ? renderAssessmentsTab() : null}
            </Stack>
        </NavBar>
    )
}

export default Notifications