import React, { useState, useEffect } from "react";
import NavBar from "../Shared/NavBar"
import TopBarThree from "../Shared/TopBarThree"
import TeamCard from "./TeamCard";
import axios from "axios";
import { Stack } from '@mui/material'

function Organizations() {
    const [userTeams, setUserTeams] = useState([]);
    const userId = 1;

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
            <Stack direction="column" justifyContent="flex-start" alignItems="flex-start" spacing={4}
                style={{
                    paddingLeft: "70px",
                }}
            >
                {chunkArray(userTeams, 4).map((teamRow, rowIndex) => (
                    <div
                        key={rowIndex}
                        style={{
                            display: "flex",
                            gap: "30px",
                        }}
                    >
                        {teamRow.map((team) => (
                        <TeamCard
                            key={team.id}
                            organization={team.organization}
                            onClick={`/organizations/${team.id}`}
                        />
                        ))}
                    </div>
                ))}
            </Stack>
        </NavBar>
    )
}

export default Organizations

function chunkArray(array, chunkSize) {
    const chunkedArray = [];
    for (let i = 0; i < array.length; i += chunkSize) {
      chunkedArray.push(array.slice(i, i + chunkSize));
    }
    return chunkedArray;
}