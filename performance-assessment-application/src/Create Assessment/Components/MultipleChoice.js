import React from 'react';
import {
  Stack,
  Radio,
  RadioGroup,
  FormControlLabel,
  TextField,
  IconButton,
} from '@mui/material';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import AddBoxOutlinedIcon from '@mui/icons-material/AddBoxOutlined';

function MultipleChoiceComponent({
  choices,
  selectedValue,
  handleChoiceChange,
  handleAddChoice,
  handleChoiceValueChange,
  handleDeleteChoice,
}) {
  return (
    <Stack direction="column" spacing={2} alignItems="flex-start">
      {choices.map((choice) => (
        <Stack direction="row" alignItems="center" key={choice.id}>
          <RadioGroup value={selectedValue} onChange={handleChoiceChange}>
            <FormControlLabel
              value={`option-${choice.id}`}
              control={<Radio disabled />}
              label={
                <TextField
                  value={choice.value}
                  onChange={(e) => handleChoiceValueChange(choice.id, e.target.value)}
                  variant="outlined"
                  size="small"
                  sx={{
                    width: '370px',
                  }}
                />
              }
            />
          </RadioGroup>
          <IconButton onClick={handleAddChoice}>
            <AddBoxOutlinedIcon />
          </IconButton>
          <IconButton
            onClick={() => handleDeleteChoice(choice.id)}
            disabled={choices.length <= 1}
          >
            <DeleteOutlineOutlinedIcon />
          </IconButton>
        </Stack>
      ))}
    </Stack>
  );
}

export default MultipleChoiceComponent;