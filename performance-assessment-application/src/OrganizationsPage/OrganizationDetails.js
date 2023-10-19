import React, { useState, useEffect } from "react";
import NavBar from "../Shared/NavBar";
import TopBarThree from "../Shared/TopBarThree";
import TopBarTwo from "../Shared/TopBarTwo";
import announcementPhoto from './Images/announcement.png';
import { Stack } from '@mui/material';

function OrganizationDetails() {
    const employee = JSON.parse(localStorage.getItem("employeeData"));
    const [team, setTeam] = useState({
        id: null,
        organization: "",
        businessType: "",
        businessAddress: "",
        teamCode: "",
        dateTimeCreated: "",
        dateTimeUpdated: ""
    });

    useEffect(() => {
        const fetchTeamDetails = async () => {
            try {
                if (employee.teamId) {
                    const response = await fetch(`https://localhost:7236/api/teams/${employee.teamId}`);
                    if (response.ok) {
                        const teamData = await response.json();
                        setTeam(teamData);
                    }
                }
            } catch (error) {
                console.error("Error fetching team details:", error);
            }
        };

        fetchTeamDetails();
    }, []);

    return (
        <NavBar>
            {employee.role === "Admin" ? <TopBarTwo /> : <TopBarThree />}
            <Stack
                direction="column"
                justifyContent="center"
                alignItems="center"
                spacing={2}
                sx={{
                    position: "fixed",
                    top: "50%",
                    left: "40%"
                }}
            >
                <h1 style={{color: "#055C9D", textAlign: "center", justifyContent: "center"}}>Welcome to {team.organization}!</h1>
                <img
                    src={announcementPhoto}
                    style={{
                        width: "150px",
                        height: "150px",
                    }}
                    alt="Announcement"
                />
                <p style={{textAlign: "center"}}>No announcement yet</p>
            </Stack>
            <Stack 
                direction="row"
                justifyContent="flex-start"
                alignItems="center"
                sx={{ position: "fixed", bottom: "0", margin: "25px 30px 25px 30px"}}
            >
                <button style={{
                    borderRadius: "10px",
                    border: "1px solid rgba(39, 198, 217, 0.90)",
                    background: "linear-gradient(254deg, #0273FF 40.22%, #00C0FE 76.98%, #00C0FE 88.25%)",
                    color: "#fff",
                    padding: "10px 20px",
                    fontSize: "16px",
                    cursor: "pointer",
                }}>
                    Add Announcement
                </button>
            </Stack>
        </NavBar>
    );
}

export default OrganizationDetails;
