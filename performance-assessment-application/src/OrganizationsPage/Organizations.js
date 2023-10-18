import React, { useState, useEffect } from "react";
import NavBar from "../Shared/NavBar"
import TeamCard from "./TeamCard";
import axios from "axios";
import { Stack, Grid } from '@mui/material'
import { useNavigate } from "react-router-dom";

function Organizations() {
    const [userTeams, setUserTeams] = useState([]);
    const userId = localStorage.getItem('userId');
    const navigate = useNavigate();

    useEffect(() => {
        axios
          .get(`https://localhost:7236/api/users/${userId}/teams`)
          .then((response) => {
            setUserTeams(response.data);
          })
          .catch((error) => {
            console.error("Error fetching user teams:", error);
          });
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
        console.log(team.id);
        const employeeData = await fetchEmployeeDetails(team.id, userId);
        localStorage.setItem("employeeData", JSON.stringify(employeeData));
        navigate(`/organizations/${team.id}`);
    };

    return (
        <NavBar>
            <Stack direction="row" justifyContent="flex-start" alignItems="center">
                <b
                    style={{
                        color: '#065d9d',
                        padding: '30px'
                    }}
                >
                    Your teams
                </b>
            </Stack>
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
        </NavBar>
    )
}

export default Organizations