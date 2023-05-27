import React, { useState, useEffect, useRef } from 'react';
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
import MultipleChoiceComponent from './MultipleChoice';

function AssessmentQuestion() {
  const [type, setType] = useState('Multiple choice');
  const [selectedValue, setSelectedValue] = useState('a');
  const [choices, setChoices] = useState([{ id: 1, value: '' }]);
  const [isBoxActive, setIsBoxActive] = useState(false);
  const boxRef = useRef(null);
  const [duplicatedComponents, setDuplicatedComponents] = useState([]); // Initialize the state

  const handleBoxClick = () => {
    setIsBoxActive(!isBoxActive);
  };

  useEffect(() => {
    const handleOutsideClick = (event) => {
      if (boxRef.current && !boxRef.current.contains(event.target)) {
        setIsBoxActive(false);
      }
    };

    document.addEventListener('click', handleOutsideClick);

    return () => {
      document.removeEventListener('click', handleOutsideClick);
    };
  }, []);

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

  const handleDuplicateComponent = () => {
    const duplicatedChoices = choices.map((choice) => ({
      ...choice,
      id: choice.id + choices.length,
    }));

    const duplicatedComponent = {
      key: Date.now(),
      choices: duplicatedChoices,
      selectedValue,
    };

    setDuplicatedComponents([...duplicatedComponents, duplicatedComponent]);
  };

  return (
    <Stack direction="column" justifyContent="center" alignItems="center" spacing={2}>
        <Stack direction="row" justifyContent="center" alignItems="center" spacing={2}>
      <Box
        ref={boxRef}
        sx={{
          width: '750px',
          height: 'auto',
          backgroundColor: 'white',
          borderRadius: '10px',
          padding: '20px',
        }}
        onClick={handleBoxClick}
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
            <MultipleChoiceComponent
                choices={choices}
                selectedValue={selectedValue}
                handleChoiceChange={handleChoiceChange}
                handleAddChoice={handleAddChoice}
                handleChoiceValueChange={handleChoiceValueChange}
                handleDeleteChoice={handleDeleteChoice}
            />
          )}
          <hr
            style={{
              width: '100%',
              height: '1px',
              backgroundColor: 'black',
            }}
          />
          <Stack direction="row" justifyContent="center" alignItems="center">
            <Typography variant="body1" fontFamily="Montserrat Regular" marginRight="10px">
              Weight value (0-100%):
            </Typography>
            <TextField
              variant="outlined"
              size="small"
              sx={{
                width: '100px',
                marginRight: '280px',
              }}
            />
            <IconButton>
              <DeleteOutlineOutlinedIcon />
            </IconButton>
            <Typography variant="body1" fontFamily="Montserrat Regular">
              Required
            </Typography>
            <Switch />
          </Stack>
        </Stack>
      </Box>
      {isBoxActive && (
        <Box
          sx={{
            width: '50px',
            height: '50px',
            backgroundColor: 'white',
            borderRadius: '10px',
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'center',
            alignItems: 'center',
          }}
        >
          <IconButton onClick={handleDuplicateComponent}>
            <AddBoxOutlinedIcon
              sx={{
                color: 'black',
              }}
            />
          </IconButton>
        </Box>
      )}
    </Stack>
    {duplicatedComponents.map((component) => (
        <AssessmentQuestion
          key={component.key}
          choices={component.choices}
          selectedValue={component.selectedValue}
        />
      ))}
    </Stack>
  );
}

export default AssessmentQuestion;
