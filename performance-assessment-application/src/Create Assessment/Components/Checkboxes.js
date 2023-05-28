import React from 'react';
import { Stack, Checkbox, TextField, Button } from '@mui/material';

function Checkboxes({ choices, setChoices }) {
  const handleCheckboxChange = (index) => (event) => {
    const newChoices = [...choices];
    newChoices[index].label = event.target.value;
    setChoices(newChoices);
  };

  const handleCheckboxCheckedChange = (index) => (event) => {
    const newChoices = [...choices];
    newChoices[index].checked = event.target.checked;
    setChoices(newChoices);
  };

  const handleAddCheckbox = () => {
    const newChoices = [...choices, { label: '', checked: false }];
    setChoices(newChoices);
  };

  return (
    <Stack direction="column" spacing={1}>
      {choices.map((choice, index) => (
        <Stack direction="row" alignItems="center" key={index}>
          <Checkbox
            checked={choice.checked}
            onChange={handleCheckboxCheckedChange(index)}
            disabled
            sx={{ marginTop: '10px' }}
          />
          <TextField
            variant="standard"
            value={choice.label}
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
