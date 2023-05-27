import React, { useState } from 'react';
import {
  Box,
  IconButton,
  Stack,
  TextField,
  FormControl,
  MenuItem,
  Select,
  Radio,
  RadioGroup,
  FormControlLabel,
  Typography,
  Divider,
  Switch,
} from '@mui/material';
import ImageOutlinedIcon from '@mui/icons-material/ImageOutlined';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import AddBoxOutlinedIcon from '@mui/icons-material/AddBoxOutlined';

function AssessmentQuestion() {
  const [type, setType] = useState('Multiple choice');
  const [selectedValue, setSelectedValue] = useState('a');
  const [choices, setChoices] = useState([{ id: 1, value: '' }]);

  const handleChoiceChange = (event) => {
    setSelectedValue(event.target.value);
  };

  const handleChange = (event) => {
    setType(event.target.value);
  };

  const handleAddChoice = () => {
    const newChoiceId = choices.length + 1;
    setChoices([...choices, { id: newChoiceId, value: '' }]);
  };

  const handleChoiceValueChange = (id, value) => {
    const updatedChoices = choices.map((choice) => {
      if (choice.id === id) {
        return { ...choice, value };
      }
      return choice;
    });
    setChoices(updatedChoices);
  };

  const handleDeleteChoice = (id) => {
    if (choices.length > 1) {
      const updatedChoices = choices.filter((choice) => choice.id !== id);
      setChoices(updatedChoices);
    }
  };

  return (
    <Stack direction="row" justifyContent="center" alignItems="flex-start">
      <Box
        sx={{
          width: '750px',
          height: 'auto',
          backgroundColor: 'white',
          borderRadius: '10px',
          padding: '20px',
        }}
      >
        <Stack direction="column" justifyContent="center" alignItems="flex-start" spacing={2}>
          <Stack direction="row" justifyContent="center" alignItems="flex-start" spacing={2}>
            <TextField
              multiline
              label="Question"
              variant="filled"
              sx={{
                width: '400px',
              }}
            />
            <IconButton>
              <ImageOutlinedIcon
                sx={{
                  marginTop: '5px',
                }}
              />
            </IconButton>
            <FormControl
              sx={{
                width: '280px',
              }}
            >
              <Select value={type} onChange={handleChange}>
                <MenuItem value={'Short answer'}>Short answer</MenuItem>
                <MenuItem value={'Paragraph'}>Paragraph</MenuItem>
                <MenuItem value={'Multiple choice'}>Multiple choice</MenuItem>
                <MenuItem value={'Checkboxes'}>Checkboxes</MenuItem>
              </Select>
            </FormControl>
          </Stack>
          {type === 'Multiple choice' && (
            <Stack direction="column" spacing={2} alignItems="flex-start">
              {choices.map((choice) => (
                <Stack direction="row" alignItems="center">
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
                            width: "370px"
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
          )}
            <hr
                style={{
                    width: "100%",
                    height: "1px",
                    backgroundColor: "black"
                }}
            />
            <Stack direction="row" justifyContent="center" alignItems="center">
                
                <Typography variant="body1" fontFamily="Montserrat Regular" marginRight="10px">
                    Weight value (0-100%):
                </Typography>
                <TextField variant="outlined" size="small" 
                    sx={{
                        width: "100px",
                        marginRight: "280px"
                    }}
                />
                <IconButton>
                    <DeleteOutlineOutlinedIcon/>
                </IconButton>
                <Typography variant="body1" fontFamily="Montserrat Regular">
                    Required
                </Typography>
                <Switch/>
            </Stack>
        </Stack>
      </Box>
    </Stack>
  );
}

export default AssessmentQuestion;