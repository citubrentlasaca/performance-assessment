import React from 'react';

import { Stack, TextField } from '@mui/material';

function Paragraph() {

  return (
    <Stack direction="column" spacing={2}
      sx={{
        width: '100%',
      }}
    >
      <TextField
        variant="standard"
        label="Paragraph"
        disabled
        sx={{
          width: "100%"
        }}
      />
    </Stack>
  );
}

export default Paragraph;
