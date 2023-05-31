import React, { useState } from 'react';

import {
  Box,
  IconButton,
  Stack,
  TextField,
  FormControl,
  Select,
  MenuItem,
  Typography,
  Switch
} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import LockOpenIcon from '@mui/icons-material/LockOpen';
import AddBoxOutlinedIcon from '@mui/icons-material/AddBoxOutlined';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import ImageOutlinedIcon from '@mui/icons-material/ImageOutlined';

import Paragraph from './Paragraph';
import ShortAnswer from './ShortAnswer';
import Checkboxes from './Checkboxes';
import MultipleChoice from './MultipleChoice';

import axios from 'axios';

function NewQuestion({ index, title, description, handleDeleteComponent, handleAddComponent }) {
    const [question, setQuestion] = useState('');
    const [type, setType] = useState('Multiple choice');
    const [choices, setChoices] = useState([]);
    const [checkboxChoices, setCheckboxChoices] = useState([]);
    const [weight, setWeight] = useState(0);
    const [isRequired, setIsRequired] = useState(false);
    const [paragraphAnswer, setParagraphAnswer] = useState('');
    const [temporaryQuestion, setTemporaryQuestion] = useState('');
    const [isDisabled, setIsDisabled] = useState(false);
    const [uploadedImageData, setUploadedImageData] = useState({}); 
    const [tempChoices, setTempChoices] = useState([]);

    const handleImageUpload = (event) => {
      const file = event.target.files[0];
      const reader = new FileReader();
  
      reader.onloadend = () => {
        const imageData = reader.result;
  
        setUploadedImageData((prevData) => ({
          ...prevData,
          [index]: imageData,
        }));
      };
  
      if (file) {
        reader.readAsDataURL(file);
      }
    };
    
    const handleQuestionChange = (event) => {
      setQuestion(event.target.value);
    };
  
    const handleTypeChange = (event) => {
      setType(event.target.value);
    };
  
    const handleWeightChange = (event) => {
      setWeight(event.target.value);
    };
  
    const handleIsRequiredChange = (event) => {
      setIsRequired(event.target.checked);
    };
  
    const handleParagraphAnswerChange = (event) => {
      setParagraphAnswer(event.target.value);
    };
  
    const handleAddClick = () => {
      setIsDisabled(true);
      setTemporaryQuestion(question);
      handleAddComponent();
    };
  
    const handleUnlockClick = () => {
      setIsDisabled(false);
      setTemporaryQuestion(question);
    
      if (type === 'Multiple choice') {
        setTempChoices([...choices]);
      } else if (type === 'Checkboxes') {
        setTempChoices([...checkboxChoices]);
      }
    };
    
  
    const postItem = async () => {
      try {
        const assessmentData = {
          question: question,
          questionType: type,
          weight: weight,
          required: isRequired
        };
    
        const assessmentsResponse = await axios.get('https://localhost:7236/api/assessments');
        const assessments = assessmentsResponse.data;
        const matchingAssessment = assessments.find((assessment) => assessment.title === title);
        const assessmentId = matchingAssessment.id;
        assessmentData.assessmentId = assessmentId;
    
        const postResponse = await axios.post('https://localhost:7236/api/items', assessmentData);
        const itemId = postResponse.data.id;
        handleAddClick();
        console.log('Item added successfully!');
    
        if (type === 'Multiple choice') {
          for (const choice of choices) {
            await postMultipleChoiceChoices(choice, itemId);
          }
        } else if (type === 'Checkboxes') {
          for (const checkboxChoice of checkboxChoices) {
            await postCheckboxChoices(checkboxChoice, itemId);
          }
        }
      } catch (error) {
        console.error('Error adding item:', error);
      }
    };
    
    const postMultipleChoiceChoices = async (choice, itemId) => {
      try {
        const choiceData = {
          choiceValue: String(choice.label),
          itemId: itemId
        };
    
        await axios.post('https://localhost:7236/api/choices', choiceData);
    
        console.log('Multiple choice added successfully!');
      } catch (error) {
        console.error('Error adding multiple choice:', error);
      }
    };
    
    const postCheckboxChoices = async (checkboxChoice, itemId) => {
      try {
        const checkboxChoiceData = {
          choiceValue: String(checkboxChoice.label),
          itemId: itemId
        };
    
        await axios.post('https://localhost:7236/api/choices', checkboxChoiceData);
    
        console.log('Checkbox choice added successfully!');
      } catch (error) {
        console.error('Error adding checkbox choice:', error);
      }
    };    
      
    const putItem = async () => {
      try {
        const response = await axios.get("https://localhost:7236/api/items");
        const items = response.data;
        const item = items.find((item) => item.question === temporaryQuestion);
    
        if (item) {
          const itemId = item.id;
    
          const assessmentData = {
            question: question,
            questionType: type,
            weight: weight,
            required: isRequired,
          };
    
          await axios.put(`https://localhost:7236/api/items/${itemId}`, assessmentData);
          setIsDisabled(true);
          console.log("Item updated successfully!");
    
          if (type === "Multiple choice" || type === "Checkboxes") {
            const choicesResponse = await axios.get("https://localhost:7236/api/choices");
            const tempChoices = choicesResponse.data.filter(choice => choice.itemId === itemId);
    
            if (type === "Multiple choice") {
              for (let i = 0; i < tempChoices.length; i++) {
                const tempChoice = tempChoices[i];
                const choice = choices[i];
                await putMultipleChoiceChoice(itemId, tempChoice.id, choice.label);
              }
            } else if (type === "Checkboxes") {
              for (let i = 0; i < tempChoices.length; i++) {
                const tempCheckboxChoice = tempChoices[i];
                const checkboxChoice = checkboxChoices[i];
                await putCheckboxChoice(itemId, tempCheckboxChoice.id, checkboxChoice.label);
              }
            }
    
            if (type === "Multiple choice" && choices.length > tempChoices.length) {
              for (let i = tempChoices.length; i < choices.length; i++) {
                const choice = choices[i];
                await postMultipleChoiceChoices(choice, itemId);
              }
            } else if (type === "Checkboxes" && checkboxChoices.length > tempChoices.length) {
              for (let i = tempChoices.length; i < checkboxChoices.length; i++) {
                const checkboxChoice = checkboxChoices[i];
                await postCheckboxChoices(checkboxChoice, itemId);
              }
            }
          }
        } else {
          console.log("Item not found.");
        }
      } catch (error) {
        console.error("Error updating item:", error);
      }
    };
    
    const putMultipleChoiceChoice = async (itemId, choiceId, label) => {
      try {
        const choiceData = {
          choiceValue: String(label),
          itemId: itemId
        };
    
        await axios.put(`https://localhost:7236/api/choices/${choiceId}`, choiceData);
    
        console.log('Multiple choice choice updated successfully!');
      } catch (error) {
        console.error('Error updating multiple choice choice:', error);
      }
    };
    
    const putCheckboxChoice = async (itemId, choiceId, label) => {
      try {
        const checkboxChoiceData = {
          choiceValue: String(label),
          itemId: itemId
        };
    
        await axios.put(`https://localhost:7236/api/choices/${choiceId}`, checkboxChoiceData);
    
        console.log('Checkbox choice updated successfully!');
      } catch (error) {
        console.error('Error updating checkbox choice:', error);
      }
    };        

    const deleteItem = async () => {
      if (!question) {
        handleDeleteComponent(index);
        return;
      }
      try {
        const response = await axios.get("https://localhost:7236/api/items");
        const items = response.data;
        const item = items.find((item) => item.question === temporaryQuestion);
        if (item) {
          const itemId = item.id;
          await axios.delete(`https://localhost:7236/api/items/${itemId}`);
          handleDeleteComponent(index);
          console.log("Item deleted successfully!");
        } else {
          console.log("Item not found.");
        }
      } catch (error) {
        console.error("Error deleting item:", error);
      }
    };

  return (
    <Stack direction="row" justifyContent="center" alignItems="center" spacing={2}>
        <Stack direction="column" justifyContent="center" alignItems="flex-start" spacing={2}>
          <Box
            sx={{
              width: '750px',
              height: 'auto',
              backgroundColor: 'white',
              borderRadius: '10px',
              padding: '20px',
              opacity: isDisabled ? 0.75 : 1,
              pointerEvents: isDisabled ? 'none' : 'auto',
              marginLeft: "65px"
            }}
          >
            <Stack direction="row" justifyContent="flex-start" alignItems="flex-start" spacing={2}>
              <TextField
                multiline
                label="Question"
                variant="filled"
                value={question}
                onChange={handleQuestionChange}
                sx={{
                  width: '100%'
                }}
              />
              <input type="file" onChange={handleImageUpload} accept="image/*" style={{ display: 'none' }} id={`upload-input-${index}`} />
              <label htmlFor={`upload-input-${index}`}>
                <IconButton component="span">
                  <Box
                    sx={{
                      width: "50px",
                      height: "50px",
                      backgroundColor: "white",
                      display: "flex",
                      justifyContent: "center",
                      alignItems: "center",
                      borderRadius: "10px"
                    }}
                  >
                    <ImageOutlinedIcon />
                  </Box>
                </IconButton>
              </label>
              <FormControl
                sx={{
                  width: '270px'
                }}
              >
                <Select value={type} onChange={handleTypeChange}
                  sx={{
                    fontFamily: "Montserrat Regular"
                  }}
                >
                  <MenuItem value={'Short answer'}>Short answer</MenuItem>
                  <MenuItem value={'Paragraph'}>Paragraph</MenuItem>
                  <MenuItem value={'Multiple choice'}>Multiple choice</MenuItem>
                  <MenuItem value={'Checkboxes'}>Checkboxes</MenuItem>
                </Select>
              </FormControl>
            </Stack>
            <Stack justifyContent="center" alignItems="center">
              {uploadedImageData[index] && (
                <Box
                  sx={{
                    width: "500px",
                    height: "500px",
                    backgroundColor: "white",
                    display: "flex",
                    justifyContent: "center",
                    alignItems: "center",
                    borderRadius: "10px",
                    backgroundImage: `url(${uploadedImageData[index]})`,
                    backgroundSize: "cover",
                    backgroundPosition: "center",
                    marginTop: "10px"
                  }}
                />
              )}
            </Stack>
            {type === 'Multiple choice' && <MultipleChoice choices={choices} setChoices={setChoices} />}
            {type === 'Checkboxes' && (
              <Checkboxes checkboxChoices={checkboxChoices} setCheckboxChoices={setCheckboxChoices} />
            )}
            {type === 'Paragraph' && (
              <Paragraph
                value={paragraphAnswer}
                onChange={handleParagraphAnswerChange}
                label="Enter your long answer"
              />
            )}
            {type === 'Short answer' && ( <ShortAnswer label="Short answer"/> )}
            <hr
              style={{
                width: '100%',
                height: '1px',
                backgroundColor: 'black',
                margin: '20px 0 20px 0'
              }}
            />
            <Stack direction="row" justifyContent="flex-start" alignItems="center">
              <Typography variant="body1" fontFamily="Montserrat Regular" marginRight="10px">
                Weight value (0-100%):
              </Typography>
              <TextField
                variant="outlined"
                size="small"
                onChange={handleWeightChange}
                sx={{
                  width: '150px'
                }}
              />
              <IconButton onClick={deleteItem}
                sx={{
                  marginLeft: '200px',
                  marginRight: '20px'
                }}
              >
                <DeleteOutlineOutlinedIcon />
              </IconButton>
              <Typography variant="body1" fontFamily="Montserrat Regular">
                Required
              </Typography>
              <Switch checked={isRequired} onChange={handleIsRequiredChange} />
            </Stack>
          </Box>
        </Stack>
      <Box
        sx={{
          width: '50px',
          height: 'auto',
          backgroundColor: 'white',
          borderRadius: '10px',
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center'
        }}
      >
        <Stack direction="column" justifyContent="center" alignItems="center">
          <IconButton onClick={postItem}>
            <AddBoxOutlinedIcon />
          </IconButton>
          <IconButton onClick={handleUnlockClick}>
            <LockOpenIcon/>
          </IconButton>
          <IconButton onClick={putItem}>
            <EditIcon />
          </IconButton>
        </Stack>
      </Box>
    </Stack>
  )
}

export default NewQuestion