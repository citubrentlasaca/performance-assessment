import React, { useState, useEffect } from 'react';
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
import { getFirestore, collection, setDoc, doc, updateDoc, deleteDoc } from 'firebase/firestore';
import Paragraph from './Paragraph';
import ShortAnswer from './ShortAnswer';
import AssessmentTitle from './AssessmentTitle';
import EditIcon from '@mui/icons-material/Edit';
import { app } from '../../firebase';
import QuestionMarkIcon from '@mui/icons-material/QuestionMark';

function AssessmentQuestion() {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [question, setQuestion] = useState('');
  const [type, setType] = useState('Multiple choice');
  const [choices, setChoices] = useState([]);
  const [checkboxChoices, setCheckboxChoices] = useState([]);
  const [weight, setWeight] = useState(0);
  const [isRequired, setIsRequired] = useState(false);
  const [paragraphAnswer, setParagraphAnswer] = useState('');
  const [shortAnswerInput, setShortAnswerInput] = useState('');
  const [temporaryQuestion, setTemporaryQuestion] = useState('');
  const [editIconEnabled, setEditIconEnabled] = useState(true);
  const [questionMarkIconEnabled, setQuestionMarkIconEnabled] = useState(false);

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

  const handleCheckboxChoicesChange = (newChoices) => {
    setCheckboxChoices(newChoices);
  };

  const handleParagraphAnswerChange = (event) => {
    setParagraphAnswer(event.target.value);
  };

  const handleShortAnswerChange = (event) => {
    setShortAnswerInput(event.target.value);
  };

  const handleQuestionClick = () => {
    setEditIconEnabled(false);
    setQuestionMarkIconEnabled(true);
    setTemporaryQuestion(question);
  };

  const handleEditClick = () => {
    setQuestionMarkIconEnabled(false);
    setEditIconEnabled(true);
  };

  const addAssessment = () => {
    const db = getFirestore(app);
    const assessmentCollectionRef = collection(db, title);

    if(type == "Multiple choice"){
      setDoc(doc(assessmentCollectionRef, question), {
        assessmentDescription: description,
        question: question,
        type: type,
        choice: choices,
        weight: weight,
        isRequired: isRequired
      })
        .then(() => {
          console.log('Document written with ID: ', question);
        })
        .catch((error) => {
          console.error('Error adding document: ', error);
        });
    }
    else if(type == "Short answer" || type == "Paragraph"){
      setDoc(doc(assessmentCollectionRef, question), {
        assessmentDescription: description,
        question: question,
        type: type,
        weight: weight,
        isRequired: isRequired
      })
        .then(() => {
          console.log('Document written with ID: ', question);
        })
        .catch((error) => {
          console.error('Error adding document: ', error);
        });
    }
    else if(type == "Checkboxes"){
      setDoc(doc(assessmentCollectionRef, question), {
        assessmentDescription: description,
        question: question,
        type: type,
        checkboxChoices: checkboxChoices,
        weight: weight,
        isRequired: isRequired
      })
        .then(() => {
          console.log('Document written with ID: ', question);
        })
        .catch((error) => {
          console.error('Error adding document: ', error);
        });
    }
  };

  const updateAssessment = () => {
    handleEditClick();
    const db = getFirestore(app);
    const assessmentCollectionRef = collection(db, title);
    const documentId = temporaryQuestion;
    let updatedFields = {};
  
    if (type === "Multiple choice") {
      updatedFields = {
        question: question,
        type: type,
        choice: choices,
        weight: weight,
        isRequired: isRequired
      };
    } else if (type === "Short answer" || type === "Paragraph") {
      updatedFields = {
        question: question,
        type: type,
        weight: weight,
        isRequired: isRequired
      };
    } else if (type === "Checkboxes") {
      updatedFields = {
        question: question,
        type: type,
        checkboxChoices: checkboxChoices,
        weight: weight,
        isRequired: isRequired
      };
    }

    const documentRef = doc(assessmentCollectionRef, documentId);
  
    updateDoc(documentRef, updatedFields)
      .then(() => {
        console.log('Document successfully updated!');
      })
      .catch((error) => {
        console.error('Error updating document:', error);
      });
  };

  const deleteDocument = () => {
    const db = getFirestore(app);
    const assessmentCollectionRef = collection(db, title);
    const documentRef = doc(assessmentCollectionRef, question);

    deleteDoc(documentRef)
      .then(() => {
        console.log('Document successfully deleted!');
      })
      .catch((error) => {
        console.error('Error deleting document:', error);
      });
  };

  return (
    <Stack direction="column" justifyContent="center" alignItems="center" spacing={2}>
      <AssessmentTitle title={title} description={description} setTitle={setTitle} setDescription={setDescription}/>
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
              <IconButton onClick={deleteDocument}
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
          <IconButton onClick={addAssessment}>
            <AddBoxOutlinedIcon />
          </IconButton>
          <IconButton disabled={!editIconEnabled} onClick={handleQuestionClick}>
            <QuestionMarkIcon/>
          </IconButton>
          <IconButton disabled={!questionMarkIconEnabled} onClick={updateAssessment}>
            <EditIcon />
          </IconButton>
        </Stack>
      </Box>
    </Stack>
    </Stack>
  );
}

export default AssessmentQuestion;
