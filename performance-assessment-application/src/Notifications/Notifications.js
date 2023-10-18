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
        // Define a function to fetch assessments for a given employee
        const fetchAssessmentsForEmployee = async (employeeId) => {
            try {
                const response = await axios.get(`https://localhost:7236/api/employee-assignscheduler-notifications/employees/${employeeId}`);
                const reversedData = response.data.reverse();

                const assessmentPromises = reversedData.map(async (item) => {
                    const assessmentResponse = await axios.get(`https://localhost:7236/api/assessments/${item.assessmentId}`);
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
                        assessmentData: assessmentResponse.data,
                        dateTimeCreated: formattedDateTime,
                    };
                });

                const assessmentData = await Promise.all(assessmentPromises);
                return assessmentData;
            } catch (error) {
                console.error('Error fetching assessments:', error);
                return [];
            }
        };

        // Define a function to fetch announcements for a given employee
        const fetchAnnouncementsForEmployee = async (employeeId) => {
            try {
                const response = await axios.get(`https://localhost:7236/api/employee-announcement-notifications/employees/${employeeId}`);
                const reversedData = response.data.reverse();

                const announcementPromises = reversedData.map(async (item) => {
                    const announcementResponse = await axios.get(`https://localhost:7236/api/announcements/${item.announcementId}`);
                    const dateTimeCreated = new Date(item.dateTimeCreated);
                    const formattedDateTime = dateTimeCreated.toLocaleString('en-US', {
                        month: 'long',
                        day: 'numeric',
                        year: 'numeric',
                        hour: '2-digit',
                        minute: '2-digit',
                        hour12: true,
                    });

                    // Fetch team information based on announcementResponse.data.teamId
                    const teamResponse = await axios.get(`https://localhost:7236/api/teams/${announcementResponse.data.teamId}`);
                    const organization = teamResponse.data.organization;

                    return {
                        announcementData: announcementResponse.data,
                        dateTimeCreated: formattedDateTime,
                        organization, // Add the organization to the returned object
                    };
                });

                const announcementData = await Promise.all(announcementPromises);
                return announcementData;
            } catch (error) {
                console.error('Error fetching announcements:', error);
                return [];
            }
        };

        // Fetch data for the selected employee based on the 'id' from useParams
        axios.get(`https://localhost:7236/api/employees/users/${id}`)
            .then((response) => {
                const employeeData = response.data;

                // Create arrays to store assessments and announcements
                const assessmentsData = [];
                const announcementsData = [];

                // Loop through each employee object and fetch assessments and announcements
                Promise.all(
                    employeeData.map(async (employee) => {
                        const assessments = await fetchAssessmentsForEmployee(employee.id);
                        assessmentsData.push(...assessments);
                        const announcements = await fetchAnnouncementsForEmployee(employee.id);
                        announcementsData.push(...announcements);
                    })
                ).then(() => {
                    // Set the state variables after all data has been fetched
                    setAssessments(assessmentsData);
                    setAnnouncements(announcementsData);
                });
            })
            .catch((error) => {
                console.error('Error fetching employee data:', error);
            });
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
                padding: '10px',
            }}
        >
            <p>Admin</p>
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
            {announcements.map((announcement, index) => (
                <Stack key={index}
                    direction="column"
                    justifyContent="flex-start"
                    alignItems="flex-start"
                    spacing={2}
                    sx={{
                        width: '100%',
                        height: '100%',
                        padding: '10px',
                    }}
                >
                    <p className='mb-0'>{announcement.dateTimeCreated}</p>
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
                        <p className='mb-0'>{announcement.organization} posted an announcement</p>
                        <b>{announcement.announcementData.content}</b>
                    </Box>
                </Stack>
            ))}
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

            {assessments.map((assessment, index) => (
                <Stack key={index}
                    direction="column"
                    justifyContent="flex-start"
                    alignItems="flex-start"
                    spacing={2}
                    sx={{
                        width: '100%',
                        height: '100%',
                        padding: '10px',
                    }}
                >
                    <p className='mb-0'>{assessment.dateTimeCreated}</p>
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
                </Stack>
            ))
            }
        </Stack >
    );

    const handleTabChange = (tab) => {
        setActiveTab(tab);
    };


    return (
        <NavBar>
            <Stack
                direction="column"
                justifyContent="center"
                alignItems="center"
                sx={{
                    width: '100%',
                    height: '100%',
                }}
            >
                <Stack
                    direction="column"
                    justifyContent="center"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        width: '100%',
                        height: '100%',
                        padding: '40px',
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
            </Stack>
        </NavBar>
    )
}

export default Notifications