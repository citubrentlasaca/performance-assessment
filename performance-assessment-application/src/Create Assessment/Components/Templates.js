import { Box, Stack } from '@mui/material'
import React, { useState, useEffect } from 'react'
import NavBar from '../../Shared/NavBar'
import TopBarTwo from '../../Shared/TopBarTwo'
import { NavLink } from 'react-router-dom';
import AssignAssessmentModal from './AssignAssessmentModal';

function Templates() {
    const [assessments, setAssessments] = useState([]);
    const [loading, setLoading] = useState(true);
    const [open, setOpen] = useState(false);

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const assessmentResponse = await fetch(`https://localhost:7236/api/assessments`);
                const assessmentData = await assessmentResponse.json();
                setAssessments(assessmentData);
                setLoading(false);
            } catch (error) {
                console.error(`Error fetching data:`, error);
                setLoading(false);
            }
        };

        fetchData();
    }, []);

    return (
        <NavBar>
            {loading ? (
                <Stack
                    justifyContent="center"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        height: "100%",
                        width: "100%"
                    }}
                >
                    <div class="spinner-border" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                </Stack>
            ) : (
                <Stack
                    direction="column"
                    justifyContent="center"
                    alignItems="center"
                    sx={{
                        height: "100%",
                        width: "100%"
                    }}
                >
                    <TopBarTwo />
                    <Stack
                        direction="column"
                        justifyContent="center"
                        alignItems="center"
                        spacing={2}
                        sx={{
                            height: "100%",
                            width: "100%",
                            padding: '40px'
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
                            <b>Create Assessment</b>
                            <button type="button" class="btn">
                                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-arrow-return-left" viewBox="0 0 16 16">
                                    <path fill-rule="evenodd" d="M14.5 1.5a.5.5 0 0 1 .5.5v4.8a2.5 2.5 0 0 1-2.5 2.5H2.707l3.347 3.346a.5.5 0 0 1-.708.708l-4.2-4.2a.5.5 0 0 1 0-.708l4-4a.5.5 0 1 1 .708.708L2.707 8.3H12.5A1.5 1.5 0 0 0 14 6.8V2a.5.5 0 0 1 .5-.5z" />
                                </svg>
                            </button>
                        </Stack>
                        <Box
                            sx={{
                                height: "100px",
                                width: "100%",
                                backgroundColor: "white",
                                borderRadius: "10px",
                                display: 'flex',
                                justifyContent: 'center',
                                alignItems: 'center',
                                padding: '30px'
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
                                <b>Blank Assessment</b>
                                <NavLink to="/createassessment">
                                    <button type="button" class="btn">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                            <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
                                        </svg>
                                    </button>
                                </NavLink>
                            </Stack>
                        </Box>
                        <hr
                            style={{
                                height: "1px",
                                width: "100%",
                            }}
                        />
                        <Stack
                            direction="row"
                            justifyContent="space-between"
                            alignItems="center"
                            spacing={2}
                            sx={{
                                width: "100%"
                            }}
                        >
                            <b>Assessments</b>
                        </Stack>
                        {assessments.map((assessment, index) => (
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
                                    marginBottom: '10px',
                                }}
                            >
                                <AssignAssessmentModal open={open} handleClose={handleClose} assessmentId={assessment.id} />
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
                                        <button type="button" class="btn" onClick={handleOpen}>
                                            <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-person-add" viewBox="0 0 16 16">
                                                <path d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7Zm.5-5v1h1a.5.5 0 0 1 0 1h-1v1a.5.5 0 0 1-1 0v-1h-1a.5.5 0 0 1 0-1h1v-1a.5.5 0 0 1 1 0Zm-2-6a3 3 0 1 1-6 0 3 3 0 0 1 6 0ZM8 7a2 2 0 1 0 0-4 2 2 0 0 0 0 4Z" />
                                                <path d="M8.256 14a4.474 4.474 0 0 1-.229-1.004H3c.001-.246.154-.986.832-1.664C4.484 10.68 5.711 10 8 10c.26 0 .507.009.74.025.226-.341.496-.65.804-.918C9.077 9.038 8.564 9 8 9c-5 0-6 3-6 4s1 1 1 1h5.256Z" />
                                            </svg>
                                        </button>
                                        <button type="button" class="btn">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-trash" viewBox="0 0 16 16">
                                                <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5Zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5Zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6Z" />
                                                <path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1ZM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118ZM2.5 3h11V2h-11v1Z" />
                                            </svg>
                                        </button>
                                    </Stack>
                                </Stack>
                            </Box>
                        ))}
                    </Stack >
                </Stack>
            )}
        </NavBar >
    )
}

export default Templates