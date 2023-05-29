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
  Switch,
  Button
} from '@mui/material';
import AddBoxOutlinedIcon from '@mui/icons-material/AddBoxOutlined';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import MultipleChoice from './MultipleChoice';
import Checkboxes from './Checkboxes';
import { getFirestore, collection, setDoc, getDoc, doc, updateDoc, deleteDoc, deleteField } from 'firebase/firestore';
import Paragraph from './Paragraph';
import ShortAnswer from './ShortAnswer';
import AssessmentTitle from './AssessmentTitle';
import EditIcon from '@mui/icons-material/Edit';
import { app } from '../../firebase';
import LockOpenIcon from '@mui/icons-material/LockOpen';
import NewQuestion from './NewQuestion';

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
  const [isDisabled, setIsDisabled] = useState(false);
  const [componentCount, setComponentCount] = useState(1);

  const handleAddComponent = () => {
    setComponentCount(componentCount + 1);
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

  const handleCheckboxChoicesChange = (newChoices) => {
    setCheckboxChoices(newChoices);
  };

  const handleParagraphAnswerChange = (event) => {
    setParagraphAnswer(event.target.value);
  };

  const handleShortAnswerChange = (event) => {
    setShortAnswerInput(event.target.value);
  };

  const handleAddClick = () => {
    setIsDisabled(true);
    setTemporaryQuestion(question);
  };

  const handleUnlockClick = () => {
    setIsDisabled(false);
    setTemporaryQuestion(question);
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
          handleAddClick();
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
  
    // Get the existing document data
    getDoc(documentRef)
      .then((docSnap) => {
        if (docSnap.exists()) {
          // Extract the existing field values from the document
          const existingData = docSnap.data();
  
          // Remove existing fields from the updatedFields object
          for (const field in existingData) {
            if (!(field in updatedFields)) {
              updatedFields[field] = deleteField();
            }
          }
  
          // Update the document with the updated fields
          updateDoc(documentRef, updatedFields)
            .then(() => {
              setIsDisabled(true);
              console.log('Document successfully updated!');
            })
            .catch((error) => {
              console.error('Error updating document:', error);
            });
        } else {
          console.error('Document does not exist.');
        }
      })
      .catch((error) => {
        console.error('Error getting document:', error);
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
      {Array.from({ length: componentCount }).map((_, index) => (
        <NewQuestion key={index} index={index} title={title} description={description} setTitle={setTitle} setDescription={setDescription}/>
      ))}
      <Button variant="contained" onClick={handleAddComponent}
        sx={{
          backgroundColor: "white",
          color: "black",
          fontFamily: "Montserrat Regular",
          "&:hover": {
            backgroundColor: "white",
            color: "black",
            fontFamily: "Montserrat Regular",
          },
        }}
      >
        Add new question
      </Button>
    </Stack>
  );
}

export default AssessmentQuestion;
