import React from 'react';
import { Box, Stack, TextField } from '@mui/material';

function AssessmentTitle() {

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
        <TextField label="Assessment Title" variant="standard" fullWidth sx={{ marginTop: "10px" }} />
        <TextField label="Assessment Description" variant="standard" fullWidth />
      </Box>
    </Stack>
  );
}

export default AssessmentTitle;
