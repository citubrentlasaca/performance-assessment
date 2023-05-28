import React from 'react';
import { Stack, TextField } from '@mui/material';

function Paragraph() {

  return (
    <Stack direction="column" spacing={2}>
      <TextField
        variant="standard"
        label= "Paragraph"
        disabled
        sx={{ 
          marginTop: '10px',
          width: "565px"
        }}
      />
    </Stack>
  );
}

export default Paragraph;
