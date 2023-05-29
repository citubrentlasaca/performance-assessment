import React from 'react';

import { Stack, TextField } from '@mui/material';

function ShortAnswer({ label }) {

  return (
    <Stack direction="column" spacing={2}>
      <TextField
        disabled
        variant="standard"
        label={label}
        sx={{
          width: '565px',
          marginTop: '10px'
        }}
      />
    </Stack>
  );
}

export default ShortAnswer;
