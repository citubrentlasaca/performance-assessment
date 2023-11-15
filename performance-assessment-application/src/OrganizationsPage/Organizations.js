import React, { useState, useEffect } from "react";
import NavBar from "../Shared/NavBar"
import TeamCard from "./TeamCard";
import axios from "axios";
import { Stack, Grid, Typography } from '@mui/material';
import { useNavigate } from "react-router-dom";

function Organizations() {
    const [userTeams, setUserTeams] = useState([]);
    const userId = localStorage.getItem('userId');
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const teamResponse = await axios.get(`https://localhost:7236/api/users/${userId}/teams`);
                const userTemp = [];
                for (const team of teamResponse.data) {
                    const employeeResponse = await axios.get(`https://localhost:7236/api/employees/teams/${team.id}`);
                    for (const employee of employeeResponse.data) {
                        if (employee.status === 'Active' && employee.userId === Number(userId)) {
                            userTemp.push(team);
                        }
                    }
                }
                setUserTeams(userTemp);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, [userId]);

    async function fetchEmployeeDetails(teamId, userId) {
        try {
            const response = await axios.get(
                `https://localhost:7236/api/employees/get-by-team-and-user?teamId=${teamId}&userId=${userId}`
            );
            return response.data;
        } catch (error) {
            console.error("Error fetching employee details:", error);
            return "";
        }
    }

    const handleCardClick = async (team) => {
        const employeeData = await fetchEmployeeDetails(team.id, userId);
        localStorage.setItem("employeeData", JSON.stringify(employeeData));
        navigate(`/organizations/${team.id}/announcements`);
    };

    return (
        <NavBar>
            <Stack
                direction="column"
                justifyContent="center"
                alignItems="center"
                spacing={2}
                sx={{
                    height: "100%",
                    width: "100%",
                    padding: "40px",
                }}
            >
                <Stack
                    direction="row"
                    justifyContent="flex-start"
                    alignItems="center"
                    spacing={2}
                    sx={{
                        width: "100%"
                    }}
                >

                    <b
                        style={{
                            color: '#065d9d',
                        }}
                    >
                        Your teams
                    </b>
                </Stack>
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
                        <div className="spinner-border" role="status">
                            <span className="visually-hidden">Loading...</span>
                        </div>
                    </Stack>
                ) : userTeams.length === 0 ? (
                    <Stack
                        direction="column"
                        justifyContent="center"
                        alignItems="center"
                        style={{
                            width: "100%",
                            height: "100%",
                        }}
                    >
                        <p className="mb-0">No teams created or joined yet.</p>
                    </Stack>
                ) : (
                    <Grid container
                        spacing={3}
                        style={{
                            paddingLeft: "70px",
                        }}
                    >
                        {userTeams.map((team) => (
                            <Grid item key={team.id} xs={12} sm={6} md={4} lg={3}>
                                <TeamCard
                                    organization={team.organization}
                                    onClick={() => handleCardClick(team)}
                                />
                            </Grid>
                        ))}
                    </Grid>
                )}
            </Stack>
        </NavBar>
    )
}

export default Organizations