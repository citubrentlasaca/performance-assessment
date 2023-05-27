import React, { useState, useEffect, useRef } from 'react';
import { Box, IconButton, Stack, TextField } from '@mui/material';
import AddBoxOutlinedIcon from '@mui/icons-material/AddBoxOutlined';

function AssessmentTitle() {
  const [isBoxActive, setIsBoxActive] = useState(false);
  const firstBoxRef = useRef(null);

  const handleBoxClick = () => {
    setIsBoxActive(!isBoxActive);
  };

  useEffect(() => {
    const handleOutsideClick = (event) => {
      if (firstBoxRef.current && !firstBoxRef.current.contains(event.target)) {
        setIsBoxActive(false);
      }
    };

    document.addEventListener('click', handleOutsideClick);

    return () => {
      document.removeEventListener('click', handleOutsideClick);
    };
  }, []);

  return (
    <Stack direction="row" justifyContent="center" alignItems="center" spacing={2} marginTop="10px">
      <Box
        ref={firstBoxRef}
        sx={{
          width: "750px",
          height: "150px",
          backgroundColor: "white",
          borderTop: "10px solid #27c6d9",
          borderRadius: "10px",
          padding: "0 20px 0 20px",
        }}
        onClick={handleBoxClick}
      >
        <TextField label="Assessment Title" variant="standard"
          sx={{
            width: "100%",
            marginTop: "10px"
          }}
        />
        <TextField label="Assessment Description" variant="standard"
          sx={{
            width: "100%",
          }}
        />
      </Box>
      {isBoxActive && (
        <Box
          sx={{
            width: "50px",
            height: "50px",
            backgroundColor: "white",
            borderRadius: "10px",
            display: "flex",
            flexDirection: "column",
            justifyContent: "center",
            alignItems: "center",
          }}
        >
          <IconButton>
            <AddBoxOutlinedIcon
              sx={{
                color: "black"
              }}
            />
          </IconButton>
        </Box>
      )}
    </Stack>
  );
}

export default AssessmentTitle;
