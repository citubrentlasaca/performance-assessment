import React, { useState, useEffect } from 'react'
import NavBar from '../Shared/NavBar'
import { Box, Stack } from '@mui/material'
import { Link, useParams } from 'react-router-dom'
import axios from 'axios';

function Notifications() {
    const [announcements, setAnnouncements] = useState([])
    const [assessments, setAssessments] = useState([])
    const { id } = useParams();
    const [activeTab, setActiveTab] = useState('Assessments');

    useEffect(() => {
        const fetchData = async () => {
            try {
                // Step 1: Fetch employee-announcement-notifications and employee-assignscheduler-notifications
                const [announcementsResponse, assessmentsResponse] = await Promise.all([
                    axios.get(`https://localhost:7236/api/employee-announcement-notifications/employees/${id}`),
                    axios.get(`https://localhost:7236/api/employee-assignscheduler-notifications/employees/${id}`),
                ]);

                // Step 2: Process announcementsResponse
                const announcementsData = announcementsResponse.data.reverse(); // Reverse the data
                const announcementsWithDetails = await Promise.all(announcementsData.map(async (announcement) => {
                    const announcementDetailsResponse = await axios.get(`https://localhost:7236/api/announcements/${announcement.announcementId}`);

                    const dateTimeCreated = new Date(announcement.dateTimeCreated);
                    const formattedDateTime = dateTimeCreated.toLocaleString('en-US', {
                        month: 'long',
                        day: 'numeric',
                        year: 'numeric',
                        hour: '2-digit',
                        minute: '2-digit',
                        hour12: true,
                    });

                    return {
                        details: announcementDetailsResponse.data,
                        dateTimeCreated: formattedDateTime,
                    };
                }));

                setAnnouncements(announcementsWithDetails);

                // Step 3: Process assessmentsResponse
                const assessmentsData = assessmentsResponse.data.reverse(); // Reverse the data
                const assessmentsWithDetails = await Promise.all(assessmentsData.map(async (assessment) => {
                    const assessmentDetailsResponse = await axios.get(`https://localhost:7236/api/assessments/${assessment.assessmentId}`);

                    const dateTimeCreated = new Date(assessment.dateTimeCreated);
                    const formattedDateTime = dateTimeCreated.toLocaleString('en-US', {
                        month: 'long',
                        day: 'numeric',
                        year: 'numeric',
                        hour: '2-digit',
                        minute: '2-digit',
                        hour12: true,
                    });

                    return {
                        details: assessmentDetailsResponse.data,
                        dateTimeCreated: formattedDateTime,
                    };
                }));

                setAssessments(assessmentsWithDetails);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, [id]);

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
                    <Box
                        sx={{
                            width: '100%',
                            height: 'fit-content',
                            backgroundColor: 'white',
                            borderRadius: '10px',
                            display: 'flex',
                            justifyContent: 'space-between',
                            alignItems: 'center',
                            padding: '30px',
                        }}
                    >
                        <b>{announcement.details.content}</b>
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
                    <Link to={`/answerassessment/${assessment.details.id}`}
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
                            <p className='mb-0'>{assessment.details.title}</p>
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
                    {activeTab === 'Announcements' ? renderAnnouncementsTab() : null}
                    {activeTab === 'Assessments' ? renderAssessmentsTab() : null}
                </Stack>
            </Stack>
        </NavBar>
    )
}

export default Notifications