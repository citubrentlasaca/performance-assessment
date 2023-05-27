import { Box, IconButton, Stack, TextField, FormControl, Select, MenuItem, Typography, Switch, RadioGroup, FormControlLabel, Radio } from '@mui/material';
import React, { useState } from 'react';
import AddBoxOutlinedIcon from '@mui/icons-material/AddBoxOutlined';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import MultipleChoice from './MultipleChoice';

function AssessmentQuestion() {
  const [type, setType] = useState('Multiple choice');
  const [choice, setChoice] = useState('');

  const handleTypeChange = (event) => {
    setType(event.target.value);
  };

  const handleChoiceChange = (event) => {
    setChoice(event.target.value);
  };

  return (
    <Stack direction="row" justifyContent="center" alignItems="center" spacing={2}>
      <Stack direction="column" justifyContent="center" alignItems="flex-start" spacing={2}>
        <Box
          sx={{
            width: "750px",
            height: "auto",
            backgroundColor: "white",
            borderRadius: "10px",
            padding: "20px"
          }}
        >
          <Stack direction="row" justifyContent="flex-start" alignItems="flex-start" spacing={2}>
            <TextField multiline label="Question" variant="filled"
              sx={{
                width: "100%"
              }}
            />
            <FormControl
              sx={{
                width: "220px"
              }}
            >
              <Select
                value={type}
                onChange={handleTypeChange}
              >
                <MenuItem value={"Short answer"}>Short answer</MenuItem>
                <MenuItem value={"Paragraph"}>Paragraph</MenuItem>
                <MenuItem value={"Multiple choice"}>Multiple choice</MenuItem>
                <MenuItem value={"Checkboxes"}>Checkboxes</MenuItem>
              </Select>
            </FormControl>
          </Stack>
          {type === 'Multiple choice' && <MultipleChoice choice={choice} handleChoiceChange={handleChoiceChange} />}
          <hr
            style={{
              width: "100%",
              height: "1px",
              backgroundColor: "black",
              margin: "20px 0 20px 0"
            }}
          />
          <Stack direction="row" justifyContent="flex-start" alignItems="center">
            <Typography variant="body1" fontFamily="Montserrat Regular" marginRight="10px">
              Weight choice (0-100%):
            </Typography>
            <TextField variant="outlined" size="small"
              sx={{
                width: "150px"
              }}
            />
            <IconButton
              sx={{
                marginLeft: "200px",
                marginRight: "20px"
              }}
            >
              <DeleteOutlineOutlinedIcon />
            </IconButton>
            <Typography variant="body1" fontFamily="Montserrat Regular">
              Required
            </Typography>
            <Switch />
          </Stack>
        </Box>
      </Stack>
      <Box
        sx={{
          width: "50px",
          height: "50px",
          backgroundColor: "white",
          borderRadius: "10px",
          display: "flex",
          justifyContent: "center",
          alignItems: "center"
        }}
      >
        <IconButton>
          <AddBoxOutlinedIcon />
        </IconButton>
      </Box>
    </Stack>
  );
}

export default AssessmentQuestion;
