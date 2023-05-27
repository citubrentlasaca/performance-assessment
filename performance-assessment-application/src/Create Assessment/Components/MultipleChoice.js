import { FormControl, RadioGroup, Stack, FormControlLabel, Radio, TextField } from '@mui/material';
import React from 'react';

function MultipleChoice({ choice, handleChoiceChange }) {
  return (
    <Stack direction="row" justifyContent="flex-start" alignItems="center" margin="10px 0 0 20px">
    <FormControl>
      <RadioGroup value={choice} onChange={handleChoiceChange}>
        <Stack direction="row" alignItems="center">
          <FormControlLabel value="option1" control={<Radio disabled />} />
          <TextField variant="standard" sx={{ width: "500px" }} />
        </Stack>
      </RadioGroup>
      <RadioGroup value={choice} onChange={handleChoiceChange}>
        <Stack direction="row" alignItems="center">
          <FormControlLabel value="option1" control={<Radio disabled />} />
          <TextField variant="standard" sx={{ width: "500px" }} />
        </Stack>
      </RadioGroup>
      <RadioGroup value={choice} onChange={handleChoiceChange}>
        <Stack direction="row" alignItems="center">
          <FormControlLabel value="option1" control={<Radio disabled />} />
          <TextField variant="standard" sx={{ width: "500px" }} />
        </Stack>
      </RadioGroup>
      <RadioGroup value={choice} onChange={handleChoiceChange}>
        <Stack direction="row" alignItems="center">
          <FormControlLabel value="option1" control={<Radio disabled />} />
          <TextField variant="standard" sx={{ width: "500px" }} />
        </Stack>
      </RadioGroup>
    </FormControl>
  </Stack>
  )
}

export default MultipleChoice