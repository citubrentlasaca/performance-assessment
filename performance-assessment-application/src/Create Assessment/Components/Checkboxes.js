import React from 'react';

import { Stack, Checkbox, TextField, IconButton } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';

function Checkboxes({ checkboxChoices, setCheckboxChoices }) {
  const handleCheckboxChoicesChange = (index) => (event) => {
    const newChoices = [...checkboxChoices];
    newChoices[index] = { label: event.target.value }; 
    setCheckboxChoices(newChoices);
  };
  
  const handleAddCheckbox = () => {
    const newChoices = [...checkboxChoices, { label: '' }];
    setCheckboxChoices(newChoices);
  };

  return (
    <Stack direction="column" spacing={1}>
      {checkboxChoices.map((checkboxChoice, index) => (
        <Stack direction="row" alignItems="center" key={index}>
          <Checkbox
            checked={checkboxChoice.checked}
            disabled
            sx={{ marginTop: '10px' }}
          />
          <TextField
            variant="standard"
            value={checkboxChoice.label}
            onChange={handleCheckboxChoicesChange(index)}
            sx={{ marginTop: '10px', width: "520px" }}
          />
        </Stack>
      ))}
      <Stack direction="row" justifyContent="left">
        <IconButton onClick={handleAddCheckbox}>
          <AddIcon/>
        </IconButton>
      </Stack>
    </Stack>
  );
}

export default Checkboxes;
