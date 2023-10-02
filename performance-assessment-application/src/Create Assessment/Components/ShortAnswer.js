import React from 'react';

import { Stack, TextField } from '@mui/material';

function ShortAnswer({ label }) {

  return (
    <Stack direction="column" spacing={2}
      sx={{
        width: '100%',
      }}
    >
      <TextField
        disabled
        variant="standard"
        label={label}
        sx={{
          width: '100%',
        }}
      />
    </Stack>
  );
}

export default ShortAnswer;
