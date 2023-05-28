import React from 'react';
import { Stack, TextField } from '@mui/material';

function ShortAnswer({ label, value, onChange }) {
  const handleTextChange = (event) => {
    onChange(event.target.value);
  };

  return (
    <Stack direction="column" spacing={2}>
      <TextField
        variant="standard"
        label={label}
        value={value}
        onChange={handleTextChange}
        disabled
        sx={{
          width: '400px',
          marginTop: '10px'
        }}
      />
    </Stack>
  );
}

export default ShortAnswer;
