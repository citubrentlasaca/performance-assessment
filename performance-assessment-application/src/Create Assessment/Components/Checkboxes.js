import React from 'react';
import { Stack, Checkbox, TextField, Button } from '@mui/material';

function Checkboxes({ checkboxChoices, setCheckboxChoices }) {
  const handleCheckboxChange = (index) => (event) => {
    const newChoices = [...checkboxChoices];
    newChoices[index].label = event.target.value;
    setCheckboxChoices(newChoices);
  };

  const handleCheckboxCheckedChange = (index) => (event) => {
    const newChoices = [...checkboxChoices];
    newChoices[index].checked = event.target.checked;
    setCheckboxChoices(newChoices);
  };

  const handleAddCheckbox = () => {
    const newChoices = [...checkboxChoices, { label: '', checked: false }];
    setCheckboxChoices(newChoices);
  };

  return (
    <Stack direction="column" spacing={1}>
      {checkboxChoices.map((checkboxChoice, index) => (
        <Stack direction="row" alignItems="center" key={index}>
          <Checkbox
            checked={checkboxChoice.checked}
            onChange={handleCheckboxCheckedChange(index)}
            disabled
            sx={{ marginTop: '10px' }}
          />
          <TextField
            variant="standard"
            value={checkboxChoice.label}
            onChange={handleCheckboxChange(index)}
            sx={{ marginTop: '10px' }}
          />
        </Stack>
      ))}
      <Stack direction="row" justifyContent="left">
        <Button variant="outlined" size="small" onClick={handleAddCheckbox} sx={{ marginLeft: '10px', marginTop: '10px' }}>
            Add Option
        </Button>
      </Stack>
    </Stack>
  );
}

export default Checkboxes;
