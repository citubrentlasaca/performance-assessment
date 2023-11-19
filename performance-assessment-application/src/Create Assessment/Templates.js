import { Box, Skeleton, Stack } from '@mui/material'
import React, { useState, useEffect } from 'react'
import NavBar from '../Shared/NavBar'
import TopBarTwo from '../Shared/TopBarTwo'
import { Link } from 'react-router-dom';
import AssignAssessmentModal from './AssignAssessmentModal';
import axios from 'axios';

function Templates() {
    const [assessments, setAssessments] = useState([]);
    const [loading, setLoading] = useState(true);
    const [open, setOpen] = useState(false);
    const [selectedAssessmentId, setSelectedAssessmentId] = useState(null);
    const [selectedAssessmentTitle, setSelectedAssessmentTitle] = useState('');
    const [disabledIds, setDisabledIds] = useState([]);
    const employeeStorage = JSON.parse(localStorage.getItem("employeeData"));

    const handleOpen = (assessmentId, assessmentTitle) => {
        setSelectedAssessmentId(assessmentId);
        setSelectedAssessmentTitle(assessmentTitle);
        setOpen(true);
    };
    const handleClose = () => setOpen(false);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const assessmentResponse = await axios.get(`https://workpa.azurewebsites.net/api/assessments/employee/${employeeStorage.id}`);
                const assessmentData = await assessmentResponse.data;
                let disabledTemp = [];
                for (const assessment of assessmentData) {
                    const response = await axios.get(`https://workpa.azurewebsites.net/api/results/assessments/${assessment.id}`);
                    for (const result of response.data) {
                        disabledTemp.push(result.assessmentId);
                    }
                }
                setAssessments(assessmentData);
                setDisabledIds(disabledTemp);
                setLoading(false);
            } catch (error) {
                console.error(`Error fetching data:`, error);
                setLoading(false);
            }
        };

        fetchData();
    }, [employeeStorage.id]);


    const handleDeleteAssessment = (assessmentId) => {
        axios
            .delete(`https://workpa.azurewebsites.net/api/assessments/${assessmentId}`)
            .then((response) => {
                console.log('Assessment deleted successfully');
                setAssessments((prevAssessments) =>
                    prevAssessments.filter((assessment) => assessment.id !== assessmentId)
                );
            })
            .catch((error) => {
                console.error('Error deleting assessment:', error);
            });
    };

    return (
        <NavBar>
            {loading ? (
                <>
                    <TopBarTwo />
                    <Stack
                        direction="column"
                        justifyContent="flex-start"
                        alignItems="center"
                        spacing={2}
                        sx={{
                            height: "calc(100% - 100px)",
                            width: "100%",
                            padding: '40px',
                            overflow: 'auto',
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
                            <Skeleton>
                                <b>Create Assessment</b>
                            </Skeleton>
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
                                <Skeleton>
                                    <b>Blank Assessment</b>
                                </Skeleton>
                                <Skeleton>
                                    <button type="button" className="btn">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                            <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
                                        </svg>
                                    </button>
                                </Skeleton>
                            </Stack>
                        </Box>
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
                                <Skeleton>
                                    <b>Daily Performance Report</b>
                                </Skeleton>
                                <Skeleton>
                                    <button type="button" className="btn">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                            <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
                                        </svg>
                                    </button>
                                </Skeleton>
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
                            <Skeleton>
                                <b>Assessments</b>
                            </Skeleton>
                        </Stack>
                    </Stack>
                </>
            ) : (
                <>
                    <AssignAssessmentModal open={open} handleClose={handleClose} assessmentId={selectedAssessmentId} assessmentTitle={selectedAssessmentTitle} />
                    <TopBarTwo />
                    <Stack
                        direction="column"
                        justifyContent="flex-start"
                        alignItems="center"
                        spacing={2}
                        sx={{
                            height: "calc(100% - 100px)",
                            width: "100%",
                            padding: '40px',
                            overflow: 'auto',
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
                                <Link to="/createassessment" style={{ color: 'black' }}>
                                    <button type="button" className="btn">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                            <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
                                        </svg>
                                    </button>
                                </Link>
                            </Stack>
                        </Box>
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
                                <b>Daily Performance Report</b>
                                <Link to="/createreport" style={{ color: 'black' }}>
                                    <button type="button" className="btn">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                            <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
                                        </svg>
                                    </button>
                                </Link>
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
                        {assessments ? (
                            assessments.map((assessment, index) => (
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
                                            <button type="button" className="btn" onClick={() => handleOpen(assessment.id, assessment.title)}>
                                                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-person-add" viewBox="0 0 16 16">
                                                    <path d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7Zm.5-5v1h1a.5.5 0 0 1 0 1h-1v1a.5.5 0 0 1-1 0v-1h-1a.5.5 0 0 1 0-1h1v-1a.5.5 0 0 1 1 0Zm-2-6a3 3 0 1 1-6 0 3 3 0 0 1 6 0ZM8 7a2 2 0 1 0 0-4 2 2 0 0 0 0 4Z" />
                                                    <path d="M8.256 14a4.474 4.474 0 0 1-.229-1.004H3c.001-.246.154-.986.832-1.664C4.484 10.68 5.711 10 8 10c.26 0 .507.009.74.025.226-.341.496-.65.804-.918C9.077 9.038 8.564 9 8 9c-5 0-6 3-6 4s1 1 1 1h5.256Z" />
                                                </svg>
                                            </button>
                                            <Link to={`/organizations/${employeeStorage.teamId}/admin-assessments/${assessment.id}`} style={{ color: 'black' }}>
                                                <button type="button" className="btn">
                                                    <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-eye" viewBox="0 0 16 16">
                                                        <path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8zM1.173 8a13.133 13.133 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.133 13.133 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5c-2.12 0-3.879-1.168-5.168-2.457A13.134 13.134 0 0 1 1.172 8z" />
                                                        <path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5zM4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0z" />
                                                    </svg>
                                                </button>
                                            </Link>
                                            {disabledIds.includes(assessment.id) ? (
                                                <button type="button" className="btn" disabled
                                                    style={{
                                                        border: 'none',
                                                    }}
                                                >
                                                    <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                                                        <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                                        <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
                                                    </svg>
                                                </button>
                                            ) : (
                                                <Link to={`/adminassessments/${assessment.id}`} style={{ color: 'black' }}>
                                                    <button type="button" className="btn">
                                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                                                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                                            <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
                                                        </svg>
                                                    </button>
                                                </Link>
                                            )}

                                            <button type="button" className="btn" onClick={() => handleDeleteAssessment(assessment.id)}>
                                                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-trash" viewBox="0 0 16 16">
                                                    <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5Zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5Zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6Z" />
                                                    <path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1ZM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118ZM2.5 3h11V2h-11v1Z" />
                                                </svg>
                                            </button>
                                        </Stack>
                                    </Stack>
                                </Box>
                            ))
                        ) : (null)}
                    </Stack>
                </>
            )}
        </NavBar >
    )
}

export default Templates