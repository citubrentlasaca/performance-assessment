import React from 'react';

import { Stack, TextField, IconButton, Radio, RadioGroup, FormControlLabel } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';

function MultipleChoice({ choices, setChoices, choiceWeight, setChoiceWeight }) {
  const handleChoiceChange = (index) => (event) => {
    const newChoices = [...choices];
    newChoices[index] = { label: event.target.value };
    setChoices(newChoices);
  };

  const handleChoiceWeightChange = (index) => (event) => {
    const newChoiceWeight = [...choiceWeight];
    newChoiceWeight[index] = event.target.value;
    setChoiceWeight(newChoiceWeight);
  };


  const handleAddRadio = () => {
    const newChoices = [...choices, { label: '' }];
    const newChoiceWeight = [...choiceWeight, 0];
    setChoices(newChoices);
    setChoiceWeight(newChoiceWeight);
  };

  return (
    <Stack direction="column" spacing={1}
      sx={{
        width: "100%",
      }}
    >
      {choices.map((choice, index) => (
        <Stack direction="row" alignItems="center" justifyContent="center" key={index} spacing={2}
          sx={{
            width: "100%",
          }}
        >
          <RadioGroup
            value={choice.label}
            onChange={handleChoiceChange(index)}
          >
            <FormControlLabel
              value={choice.label}
              control={<Radio disabled />}
              sx={{
                margin: "0px",
              }}
            />
          </RadioGroup>
          <TextField
            variant="standard"
            value={choice.label}
            onChange={handleChoiceChange(index)}
            sx={{ width: "100%" }}
          />
          <input type='number' value={choiceWeight[index]} onChange={handleChoiceWeightChange(index)}
            style={{
              width: '173.5px',
              border: '1px solid #c4c4c4',
            }}
          />
        </Stack>
      ))
      }
      <Stack direction="row" justifyContent="left">
        <IconButton onClick={handleAddRadio}>
          <AddIcon />
        </IconButton>
      </Stack>
    </Stack >
  );
}

export default MultipleChoice;
