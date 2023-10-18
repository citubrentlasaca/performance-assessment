import React from "react";
import { Box, Stack } from "@mui/material";
import groupPhoto from './Images/group.png';
import { useNavigate } from "react-router-dom";

const TeamCard = ({ organization, onClick }) => {
  const navigate = useNavigate();

  const handleCardClick = () => {
    if (onClick) {
      onClick();
    } else {
      navigate("/");
    }
  };

  return (
    <div onClick={handleCardClick} style={{ textDecoration: "none", cursor: "pointer" }}>
      <Box
        sx={{
          width: "230px",
          height: "230px",
          backgroundColor: "white",
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          borderRadius: "20px",
          boxShadow: "0px 0px 10px rgba(0, 0, 0, 0.2)",
          transition: "box-shadow 0.3s ease-in-out",
          "&:hover": {
            boxShadow: "0px 0px 20px rgba(0, 0, 0, 0.4)",
          },
        }}
      >
        <Stack direction="column" justifyContent="center" alignItems="center" spacing={1}>
          <img
            src={groupPhoto}
            alt={organization}
            style={{
              width: "150px",
              height: "150px",
            }}
          />
          <b style={{ color: "black" }}>{organization}</b>
        </Stack>
      </Box>
    </div>
  );
};

export default TeamCard;