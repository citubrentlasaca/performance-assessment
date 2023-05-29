import React from 'react';

import { Stack, TextField, IconButton, Radio, RadioGroup, FormControlLabel } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';

function MultipleChoice({ choices, setChoices }) {
  const handleChoiceChange = (index) => (event) => {
    const newChoices = [...choices];
    newChoices[index] = { label: event.target.value };
    setChoices(newChoices);
  };
  
  const handleAddRadio = () => {
    const newChoices = [...choices, { label: '' }];
    setChoices(newChoices);
  };

  return (
    <Stack direction="column" spacing={1}>
      {choices.map((choice, index) => (
        <Stack direction="row" alignItems="center" key={index}>
          <RadioGroup
            value={choice.label}
            onChange={handleChoiceChange(index)}
            sx={{ marginTop: '10px', marginLeft: "10px" }}
          >
            <FormControlLabel
              value={choice.label}
              control={<Radio disabled/>}
            />
          </RadioGroup>
          <TextField
            variant="standard"
            value={choice.label}
            onChange={handleChoiceChange(index)}
            sx={{ marginTop: '10px', width: "440px" }}
          />
        </Stack>
      ))}
      <Stack direction="row" justifyContent="left">
        <IconButton onClick={handleAddRadio}>
          <AddIcon/>
        </IconButton>
      </Stack>
    </Stack>
  );
}

export default MultipleChoice;
