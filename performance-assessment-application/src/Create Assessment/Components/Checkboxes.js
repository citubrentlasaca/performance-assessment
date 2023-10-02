import React from 'react';

import { Stack, Checkbox, TextField, IconButton } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';

function Checkboxes({ checkboxChoices, setCheckboxChoices, checkboxChoiceWeight, setCheckboxChoiceWeight }) {
  const handleCheckboxChoicesChange = (index) => (event) => {
    const newChoices = [...checkboxChoices];
    newChoices[index] = { label: event.target.value };
    setCheckboxChoices(newChoices);
  };

  const handleChoiceWeightChange = (index) => (event) => {
    const newChoiceWeight = [...checkboxChoiceWeight];
    newChoiceWeight[index] = event.target.value;
    setCheckboxChoiceWeight(newChoiceWeight);
  };

  const handleAddCheckbox = () => {
    const newChoices = [...checkboxChoices, { label: '' }];
    setCheckboxChoices(newChoices);
  };

  return (
    <Stack direction="column" spacing={1}
      sx={{
        width: "100%",
      }}
    >
      {checkboxChoices.map((checkboxChoice, index) => (
        <Stack direction="row" alignItems="center" justifyContent="center" key={index} spacing={2}
          sx={{
            width: "100%",
          }}
        >
          <Checkbox
            checked={checkboxChoice.checked}
            disabled
          />
          <TextField
            variant="standard"
            value={checkboxChoice.label}
            onChange={handleCheckboxChoicesChange(index)}
            sx={{ width: "100%" }}
          />
          <input type='number' value={checkboxChoiceWeight[index]} onChange={handleChoiceWeightChange(index)}
            style={{
              width: '173.5px',
              border: '1px solid #c4c4c4',
            }}
          />
        </Stack>
      ))}
      <Stack direction="row" justifyContent="left">
        <IconButton onClick={handleAddCheckbox}>
          <AddIcon />
        </IconButton>
      </Stack>
    </Stack>
  );
}

export default Checkboxes;
