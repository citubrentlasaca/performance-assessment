import React, { useState, useEffect } from "react";
import NavBar from "../Shared/NavBar"
import axios from "axios";
import { Skeleton, Stack } from '@mui/material';
import { useNavigate } from "react-router-dom";
import groupPhoto from './Images/group.png';

function Organizations() {
    const [userTeams, setUserTeams] = useState([]);
    const userId = localStorage.getItem('userId');
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const teamResponse = await axios.get(`https://workpa.azurewebsites.net/api/users/${userId}/teams`);
                const userTemp = [];
                for (const team of teamResponse.data) {
                    const employeeResponse = await axios.get(`https://workpa.azurewebsites.net/api/employees/teams/${team.id}`);
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
                `https://workpa.azurewebsites.net/api/employees/get-by-team-and-user?teamId=${teamId}&userId=${userId}`
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
            {loading ? (
                <Stack
                    direction="column"
                    justifyContent="flex-start"
                    alignItems="flex-start"
                    spacing={2}
                    sx={{
                        height: "100%",
                        width: "100%",
                        padding: "40px",
                        overflow: "auto"
                    }}
                >
                    <Skeleton>
                        <b style={{ color: "#055c9d" }}>Your teams</b>
                    </Skeleton>
                    <div className="text-center w-100">
                        <div className="row row-cols-md-4 row-cols-sm-2 row-cols-1 row-gap-1">
                            <div className="col"
                                style={{
                                    height: "250px",
                                    padding: "10px"
                                }}
                            >
                                <div
                                    style={{
                                        height: "100%",
                                        backgroundColor: "white",
                                        borderRadius: "10px",
                                        display: "flex",
                                        flexDirection: "column",
                                        justifyContent: "center",
                                        alignItems: "center",
                                        gap: "10px",
                                        cursor: "pointer",
                                        boxShadow: "0px 0px 10px rgba(0, 0, 0, 0.2)",
                                        transition: "box-shadow 0.3s ease-in-out",
                                        '&:hover': {
                                            boxShadow: "0px 0px 20px rgba(0, 0, 0, 0.4)"
                                        }
                                    }}
                                >
                                    <Skeleton>
                                        <img src={groupPhoto} style={{ maxWidth: "50%", maxHeight: "100%" }} draggable="false" />
                                    </Skeleton>
                                    <Skeleton variant='text' width={100} />
                                </div>
                            </div>
                        </div>
                    </div>
                </Stack>
            ) : (
                <Stack
                    direction="column"
                    justifyContent="flex-start"
                    alignItems="flex-start"
                    spacing={2}
                    sx={{
                        height: "100%",
                        width: "100%",
                        padding: "40px",
                        overflow: "auto"
                    }}
                >
                    <b style={{ color: "#055c9d" }}>Your teams</b>
                    {userTeams.length === 0 ? (
                        <Stack
                            justifyContent="center"
                            alignItems="center"
                            sx={{
                                height: "100%",
                                width: "100%",
                            }}
                        >
                            <p className="mb-0">No teams found</p>
                        </Stack>
                    ) : (
                        <div className="text-center w-100">
                            <div className="row row-cols-md-4 row-cols-sm-2 row-cols-1 row-gap-1">
                                {userTeams.map((team, index) => (
                                    <div className="col" key={index}
                                        style={{
                                            height: "250px",
                                            padding: "10px"
                                        }}
                                    >
                                        <div onClick={() => handleCardClick(team)}
                                            style={{
                                                height: "100%",
                                                backgroundColor: "white",
                                                borderRadius: "10px",
                                                display: "flex",
                                                flexDirection: "column",
                                                justifyContent: "center",
                                                alignItems: "center",
                                                gap: "10px",
                                                cursor: "pointer",
                                                boxShadow: "0px 0px 10px rgba(0, 0, 0, 0.2)",
                                                transition: "box-shadow 0.3s ease-in-out",
                                                '&:hover': {
                                                    boxShadow: "0px 0px 20px rgba(0, 0, 0, 0.4)"
                                                }
                                            }}
                                        >
                                            <img src={groupPhoto} alt={team.organization} style={{ maxWidth: "50%", maxHeight: "100%" }} draggable="false" />
                                            <b>{team.organization}</b>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        </div>
                    )}

                </Stack>
            )}
        </NavBar>
    )
}

export default Organizations