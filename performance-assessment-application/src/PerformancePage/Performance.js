import React, { useState, useEffect } from 'react'
import NavBar from "../Shared/NavBar"
import { Box, Stack } from '@mui/material'
import TopBarThree from "../Shared/TopBarThree"
import { useNavigate } from 'react-router-dom';

function Performance() {
    const [assessments, setAssessments] = useState([]);
    const [activeTab, setActiveTab] = useState('Performance Report');
    const navigate = useNavigate();
    const employeeId = 30; // Temporary variable for employeeId

    useEffect(() => {
        const fetchData = async () => {
            try {
                const assessmentResponse = await fetch(`https://localhost:7236/api/assessments`);
                const assessmentData = await assessmentResponse.json();
                setAssessments(assessmentData);
            } catch (error) {
                console.error(`Error fetching data:`, error);
            }
        };

        fetchData();
    }, []);

    const handleTabClick = (tabName) => {
        setActiveTab(tabName);
    };

    const filteredAssessments = assessments.filter(
        assessment => assessment.title === "Daily Performance Report"
    );

    return (
        <NavBar>
            <TopBarThree />
            <Stack
                direction="column"
                justifyContent="center"
                alignItems="flex-start"
                spacing={2}
                sx={{
                    width: "100%",
                    height: '100%',
                    padding: '40px',
                }}
            >
                <Stack direction="row" justifyContent="flex-start" alignItems="center" spacing={2}
                    sx={{
                        width: "100%",
                    }}
                >
                    <ul className="nav">
                        <li className={`nav-item ${activeTab === 'Upcoming' ? 'active' : ''}`}>
                            <b
                                className="nav-link"
                                href="#"
                                style={{
                                    color: activeTab === 'Performance Report' ? '#055c9d' : 'black',
                                    cursor: 'pointer',
                                }}
                                onClick={() => handleTabClick('Performance Report')}
                            >
                                <b>Performance Report</b>
                            </b>
                        </li>
                        <li className={`nav-item ${activeTab === 'Completed' ? 'active' : ''}`}>
                            <b
                                className="nav-link"
                                style={{
                                    color: activeTab === 'Completed' ? '#055c9d' : 'black',
                                    cursor: 'pointer',
                                }}
                                onClick={() => handleTabClick('Completed')}
                            >
                                <b>Completed</b>
                            </b>
                        </li>
                    </ul>
                </Stack>
                {activeTab === 'Performance Report' && (
                    <Stack direction="column" justifyContent="center" alignItems="stretch" spacing={2}
                        style={{
                            width: "100%",
                            padding: '10px'
                        }}
                    >
                        {filteredAssessments.map((assessment, index) => (
                            <Box
                                key={index}
                                sx={{
                                    height: "100px",
                                    width: "100%",
                                    backgroundColor: "white",
                                    borderRadius: "10px",
                                    display: 'flex',
                                    justifyContent: 'center',
                                    alignItems: 'center',
                                    padding: '30px',
                                }}
                            >
                                <Stack
                                    direction="row"
                                    justifyContent="space-between"
                                    alignItems="center"
                                    spacing={2}
                                    sx={{
                                        width: "100%"
                                    }}
                                >
                                    <b>{assessment.title}</b>
                                    <Stack
                                        direction="row"
                                        justifyContent="center"
                                        alignItems="center"
                                    >
                                        <button type="button" className="btn">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-eye" viewBox="0 0 16 16" onClick={() => navigate(`/answerassessment/${assessment.id}`)}>
                                                <path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8zM1.173 8a13.133 13.133 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.133 13.133 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5c-2.12 0-3.879-1.168-5.168-2.457A13.134 13.134 0 0 1 1.172 8z" />
                                                <path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5zM4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0z" />
                                            </svg>
                                        </button>
                                    </Stack>
                                </Stack>
                            </Box>
                        ))}
                    </Stack>
                )}
            </Stack>
        </NavBar>
    )
}

export default Performance