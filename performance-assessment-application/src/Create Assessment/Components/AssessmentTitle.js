import React from 'react';
import { Box, Stack, TextField } from '@mui/material';

function AssessmentTitle({ title, description, setTitle, setDescription}) {
  const handleTitleChange = (event) => {
    setTitle(event.target.value);
  }
  
  const handleDescriptionChange = (event) => {
    setDescription(event.target.value);
  }

  return (
    <Stack direction="row" justifyContent="center" alignItems="center" spacing={2} marginTop="10px">
      <Box
        sx={{
          width: "750px",
          height: "150px",
          backgroundColor: "white",
          borderTop: "10px solid #27c6d9",
          borderRadius: "10px",
          padding: "0 20px",
        }}
      >
        <TextField label="Assessment Title" value={title} onChange={handleTitleChange} variant="standard" fullWidth sx={{ marginTop: "10px" }} />
        <TextField label="Assessment Description" value={description} onChange={handleDescriptionChange} variant="standard" fullWidth />
      </Box>
    </Stack>
  );
}

export default AssessmentTitle;
