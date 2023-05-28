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
import AddBoxOutlinedIcon from '@mui/icons-material/AddBoxOutlined';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import MultipleChoice from './MultipleChoice';
import Checkboxes from './Checkboxes';
import { initializeApp } from 'firebase/app';
import { getFirestore, collection, addDoc } from 'firebase/firestore';
import Paragraph from './Paragraph';
import ShortAnswer from './ShortAnswer';

function AssessmentQuestion() {
  const [question, setQuestion] = useState('');
  const [type, setType] = useState('Multiple choice');
  const [choice, setChoice] = useState(['', '', '', '']);
  const [weight, setWeight] = useState(0);
  const [isRequired, setIsRequired] = useState(false);
  const [checkboxes, setCheckboxes] = useState([{ label: '', checked: false }]);
  const [paragraphAnswer, setParagraphAnswer] = useState('');
  const [shortAnswer, setShortAnswer] = useState('');

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

  const handleCheckboxesChange = (newCheckboxes) => {
    setCheckboxes(newCheckboxes);
  };

  const handleParagraphAnswerChange = (event) => {
    setParagraphAnswer(event.target.value);
  };

  const handleShortAnswerChange = (event) => {
    setShortAnswer(event.target.value);
  };

  const addQuestion = () => {
    const firebaseConfig = {
        apiKey: "AIzaSyAOVXrrv4E8-iOL-VpvNknCAR9VpJarxzs",
        authDomain: "performance-assessment-c9485.firebaseapp.com",
        projectId: "performance-assessment-c9485",
        storageBucket: "performance-assessment-c9485.appspot.com",
        messagingSenderId: "304338500801",
        appId: "1:304338500801:web:a42c3d48a3d68a850fe840",
        measurementId: "G-TZ08H4CPFJ"
    };

    const app = initializeApp(firebaseConfig);
    const db = getFirestore(app);
    const questionsCollectionRef = collection(db, 'question');

    addDoc(questionsCollectionRef, {
      question: question,
      type: type,
      choice: choice,
      weight: weight,
      isRequired: isRequired,
      paragraphAnswer: paragraphAnswer,
      shortAnswer: shortAnswer
    })
      .then((docRef) => {
        console.log('Document written with ID: ', docRef.id);
      })
      .catch((error) => {
        console.error('Error adding document: ', error);
      });
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
            padding: '20px'
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
            <FormControl
              sx={{
                width: '220px'
              }}
            >
              <Select value={type} onChange={handleTypeChange}>
                <MenuItem value={'Short answer'}>Short answer</MenuItem>
                <MenuItem value={'Paragraph'}>Paragraph</MenuItem>
                <MenuItem value={'Multiple choice'}>Multiple choice</MenuItem>
                <MenuItem value={'Checkboxes'}>Checkboxes</MenuItem>
              </Select>
            </FormControl>
          </Stack>
          {type === 'Multiple choice' && <MultipleChoice choice={choice} setChoices={setChoice} />}
          {type === 'Checkboxes' && (
            <Checkboxes choices={checkboxes} setChoices={handleCheckboxesChange} />
          )}
          {type === 'Paragraph' && (
            <Paragraph
              value={paragraphAnswer}
              onChange={handleParagraphAnswerChange}
              label="Enter your long answer"
            />
          )}
          {type === 'Short answer' && (
            <ShortAnswer
              value={shortAnswer}
              onChange={handleShortAnswerChange}
              label="Enter your short answer"
            />
          )}
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
            <IconButton
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
          height: '50px',
          backgroundColor: 'white',
          borderRadius: '10px',
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center'
        }}
      >
        <IconButton onClick={addQuestion}>
          <AddBoxOutlinedIcon />
        </IconButton>
      </Box>
    </Stack>
  );
}

export default AssessmentQuestion;
