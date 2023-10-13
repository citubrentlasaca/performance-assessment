import React, { useState, useEffect } from "react";
import NavBar from "../Shared/NavBar"
import TopBarThree from "../Shared/TopBarThree"
import TeamCard from "./TeamCard";
import axios from "axios";

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
            <TopBarThree />
            <div style={{ display: "flex", flexDirection: "column", alignItems: "center" }}>
                {chunkArray(userTeams, 4).map((teamRow, rowIndex) => (
                    <div
                        key={rowIndex}
                        style={{
                        display: "flex",
                        justifyContent: "center",
                        gap: "20px",
                        }}
                    >
                        {teamRow.map((team) => (
                        <TeamCard
                            key={team.id}
                            organization={team.organization}
                            onClick={`/organizations/teams/${team.id}`}
                        />
                        ))}
                    </div>
                ))}
            </div>
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