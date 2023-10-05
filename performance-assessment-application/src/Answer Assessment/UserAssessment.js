import React, { useState, useEffect } from 'react'
import NavBar from '../Shared/NavBar'
import { Box, Stack } from '@mui/material'
import { NavLink } from 'react-router-dom';
import TopBarTwo from '../Shared/TopBarTwo';

function UserAssessment() {
    const [assessments, setAssessments] = useState([]);

    useEffect(() => {
        fetch('https://localhost:7236/api/assessments')
            .then((response) => response.json())
            .then((data) => {
                setAssessments(data);
            })
            .catch((error) => {
                console.error('Error fetching assessments:', error);
            });
    }, []);

    return (
        <NavBar>
            <Stack
                direction="column"
                justifyContent="center"
                alignItems="center"
                sx={{
                    width: "100%",
                    height: "100%",
                }}
            >
                <TopBarTwo />
                <Stack
                    direction="column"
                    justifyContent="center"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        width: "100%",
                        height: "100%",
                        padding: "40px"
                    }}
                >
                    <Stack
                        direction="row"
                        justifyContent="flex-start"
                        alignItems="center"
                        spacing={2}
                        sx={{
                            width: "100%",
                        }}
                    >
                        <ul class="nav">
                            <li class="nav-item">
                                <a class="nav-link" href="#"
                                    style={{
                                        color: "black",
                                    }}
                                >
                                    <b>Upcoming</b>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#"
                                    style={{
                                        color: "black",
                                    }}
                                >
                                    <b>Completed</b>
                                </a>
                            </li>
                        </ul>
                    </Stack>
                    {assessments.map((assessment) => (
                        <NavLink to={`/answerassessment/${assessment.id}`}
                            style={{
                                width: "100%",
                                color: "black",
                                textDecoration: "none",
                            }}
                        >
                            <Box
                                sx={{
                                    width: "100%",
                                    height: "100px",
                                    backgroundColor: "white",
                                    borderRadius: "10px",
                                    display: "flex",
                                    justifyContent: "start",
                                    alignItems: "center",
                                    padding: "20px",
                                }}
                            >
                                <b key={assessment.id}>{assessment.title}</b>
                            </Box>
                        </NavLink>
                    ))}
                </Stack>
            </Stack>
        </NavBar >
    )
}

export default UserAssessment