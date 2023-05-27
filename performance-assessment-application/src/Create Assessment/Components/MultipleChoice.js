import { FormControl, RadioGroup, Stack, FormControlLabel, Radio, TextField } from '@mui/material';
import React from 'react';

function MultipleChoice({ choice, setChoices }) {
  const handleChoiceChange = (index) => (event) => {
    const newChoices = [...choice];
    newChoices[index] = event.target.value;
    setChoices(newChoices);
  };

  return (
    <Stack direction="row" justifyContent="flex-start" alignItems="center" margin="10px 0 0 20px">
      <FormControl>
        <RadioGroup>
          <Stack direction="row" alignItems="center">
            <FormControlLabel value="option1" control={<Radio disabled />} />
            <TextField variant="standard" sx={{ width: "500px" }} value={choice[0]} onChange={handleChoiceChange(0)} />
          </Stack>
        </RadioGroup>
        <RadioGroup>
          <Stack direction="row" alignItems="center">
            <FormControlLabel value="option1" control={<Radio disabled />} />
            <TextField variant="standard" sx={{ width: "500px" }} value={choice[1]} onChange={handleChoiceChange(1)} />
          </Stack>
        </RadioGroup>
        <RadioGroup>
          <Stack direction="row" alignItems="center">
            <FormControlLabel value="option1" control={<Radio disabled />} />
            <TextField variant="standard" sx={{ width: "500px" }} value={choice[2]} onChange={handleChoiceChange(2)} />
          </Stack>
        </RadioGroup>
        <RadioGroup>
          <Stack direction="row" alignItems="center">
            <FormControlLabel value="option1" control={<Radio disabled />} />
            <TextField variant="standard" sx={{ width: "500px" }} value={choice[3]} onChange={handleChoiceChange(3)} />
          </Stack>
        </RadioGroup>
      </FormControl>
    </Stack>
  );
}

export default MultipleChoice;
